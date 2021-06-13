using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuctionScopic.Extensions
{
    public static class PrincipalHelpers
    {
        public static Guid? GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                return null;

            return Guid.Parse(principal.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).Select(x => x.Value).FirstOrDefault());
        }
    }
}
