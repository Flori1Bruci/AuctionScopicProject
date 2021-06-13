using AuctionScopic.Data;
using AuctionScopic.Domain;
using AuctionScopic.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AuctionScopic.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class BidController : Controller
    {
        private readonly ApplicationDbContext _context;
        public BidController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method to retrieve one item
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetLatestBid([FromQuery] int itemId)
        {
            //get the bid with largest bidAmount
            var response = await _context.Bids.Where(x => x.ItemId == itemId).OrderByDescending(x => x.BidAmount).FirstOrDefaultAsync();
            return Ok(response);
        }


        /// <summary>
        /// Method to create Item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Bid model)
        {
            if (ModelState.IsValid)
                return BadRequest("This model is not valid");

            var bid = new Bid();
            var userId = User.GetUserId();
            var result = bid.Create(model.ItemId, model.BidAmount,(Guid)userId);
            if (result.Failure)
                return BadRequest(result.Error);
            await _context.Bids.AddAsync(bid);
            await _context.SaveChangesAsync();
            return Ok(bid);
        }
    }
}
