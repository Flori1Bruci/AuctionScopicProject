using AuctionScopic.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuctionScopic.Domain
{
    public class Item : BaseEntity
    {
        public Item()
        {

        }
        public Item(string name, string description, decimal initialPrice, bool availableForAuction, DateTime? finishAuctionTime, decimal requiredIncreaseAmount)
        {
            Name = name;
            Description = description;
            InitialPrice = initialPrice;
            AvailableForAuction = availableForAuction;
            FinishAuctionTime = finishAuctionTime;
            RequiredIncreaseAmount = requiredIncreaseAmount;
        }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal InitialPrice { get; private set; }
        public bool IsSold { get; private set; }
        public bool AvailableForAuction { get; private set; }
        public DateTime? FinishAuctionTime { get; private set; }
        public decimal RequiredIncreaseAmount { get; private set; }
        public Guid? SoldTo { get; private set; }

        public bool IsValidAuctionTime()
        {
            return FinishAuctionTime <= DateTime.UtcNow;
        }


        public static Result<Item> Create(string name, string description, decimal initialPrice, bool availableForAuction, DateTime? finishAuctionTime, decimal requiredIncreaseAmount)
        {

            return Result.Ok(new Item(name, description, initialPrice, availableForAuction, finishAuctionTime, requiredIncreaseAmount));
        }
        public Result SellItem()
        {
            IsSold = true;
            SoldTo = ClaimsPrincipal.Current.GetUserId();
            return Result.Ok();

        }




    }
}
