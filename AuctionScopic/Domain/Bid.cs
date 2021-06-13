using AuctionScopic.Extensions;
using AuctionScopic.Models;
using AuctionScopic.Notifications;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionScopic.Domain
{
    public class Bid : BaseEntity
    {
        public Bid()
        {

        }

        public Bid(int itemId, decimal bidAmount, Guid userId)
        {
            ItemId = itemId;
            BidAmount = bidAmount;
            UserId = userId;
        }

        public decimal BidAmount { get; private set; }

        public int ItemId { get; private set; }
        [ForeignKey("ItemId")]
        public virtual Item Item { get; private set; }
        public Guid UserId { get; private set; }

        public Result<Bid> Create(int itemId, decimal bidAmount, Guid userId)
        {
            AddChangedEvent();
            return Result.Ok(new Bid(itemId, bidAmount, userId));
        }

        public Result<Bid> CreateWithoutHandlingEvents(int itemId, decimal bidAmount, Guid userId)
        {
            return Result.Ok(new Bid(itemId, bidAmount, userId));
        }


        private void AddChangedEvent()
        {
            var changeEvent = new BidOcurredEvent(ItemId, UserId);
            AddDomainEvent(changeEvent);
        }
    }
}
