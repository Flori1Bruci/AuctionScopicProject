using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionScopic.Notifications
{
    public class BidOcurredEvent : INotification
    {
        public BidOcurredEvent(int itemId, Guid userId)
        {
            ItemId = itemId;
            UserId = userId;
        }
        public int ItemId { get; set; }
        public Guid UserId { get; set; }
    }
}
