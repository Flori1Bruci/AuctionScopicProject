using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionScopic.Domain
{
    public class AutoBid : BaseEntity
    {
        public AutoBid()
        {

        }
        public int ItemId { get; set; }
        public virtual Item Item { get; set; }
        public Guid UserId { get; set; }
        public bool IsActivated { get; set; }
        public decimal IncreaseAmount { get; set; }


        public AutoBid Create(int itemId, Guid userId, bool isActivated, decimal increaseAmount)
        {
            return new AutoBid
            {
                ItemId = itemId,
                UserId = userId,
                IsActivated = isActivated,
                IncreaseAmount = increaseAmount
            };
        }


        public void EnableAutoBidding()
        {
            IsActivated = true;
        }

        public void DisableAutoBidding()
        {
            IsActivated = false;
        }
    }
}
