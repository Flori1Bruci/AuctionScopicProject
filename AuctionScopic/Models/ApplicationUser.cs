using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionScopic.Models
{
    public class ApplicationUser : IdentityUser
    {
        public decimal WalletAmount { get; set; }
    }
}
