using AuctionScopic.Data;
using AuctionScopic.Domain;
using AuctionScopic.Notifications;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AuctionScopic.Handlers
{
    public class BidOcurredEventHandler : INotificationHandler<BidOcurredEvent>
    {
        private readonly ApplicationDbContext _context;
        private readonly IMediator _mediatr;
        public BidOcurredEventHandler(ApplicationDbContext context, IMediator mediatr)
        {
            _context = context;
            _mediatr = mediatr;
        }

        public async Task Handle(BidOcurredEvent notification, CancellationToken cancellationToken)
        {
            //get the latestBid to perform check based on this one
            var latestBid = await _context.Bids.Where(x => x.ItemId == notification.ItemId).OrderByDescending(x => x.BidAmount).FirstOrDefaultAsync();

            //check if any user has AutoBid enabled for this item exluding User who performed the latest bid and ordering by created date prop of autobid

            var autoBids = await _context.AutoBids
                .Where(x => x.ItemId == notification.ItemId && x.IsActivated && x.UserId != notification.UserId)
                .OrderBy(x => x.Created).ToListAsync();

            //need this because latest bid will be overriden when autobid happens
            var newBidOcurrence = latestBid;

            //variable to check if new bid was added so we perform this again
            var isNeededToRepeatThisAction = false;
            foreach (var autoBid in autoBids)
            {

                var user = await _context.Users.Where(x => x.Id == autoBid.UserId.ToString()).FirstOrDefaultAsync();
                // case when user doesn't have money to continue on bidding
                if ((newBidOcurrence.BidAmount + autoBid.IncreaseAmount) > user.WalletAmount)
                {
                    autoBid.DisableAutoBidding();
                    _context.AutoBids.Update(autoBid);
                    await _context.SaveChangesAsync();
                }
                //case when user will bid
                else
                {
                    // this is because this event will be called again when this operation finishes
                    var result = new Bid().CreateWithoutHandlingEvents(notification.ItemId, latestBid.BidAmount + autoBid.IncreaseAmount, Guid.Parse(user.Id));
                    if (result.Failure)
                        continue;

                    await _context.Bids.AddAsync(result.Value);
                    await _context.SaveChangesAsync();
                    newBidOcurrence = result.Value;
                    isNeededToRepeatThisAction = true;
                }
            }

            //register new event which takes the latest bid userId
            if (isNeededToRepeatThisAction)
               await _mediatr.Publish(new BidOcurredEvent(newBidOcurrence.ItemId, newBidOcurrence.UserId));


        }
    }
}
