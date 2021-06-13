using AuctionScopic.Data;
using AuctionScopic.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionScopic.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]/[action]")]
    public class ItemController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ItemController(ApplicationDbContext context)
        {
            _context = context;
        }



        /// <summary>
        /// Method to retrieve full list of items
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _context.Items.ToListAsync();
            return Ok(response);
        }

        /// <summary>
        /// Method to retrieve one item
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _context.Items.FirstOrDefaultAsync(x => x.Id == id);
            return Ok(response);
        }


        /// <summary>
        /// Method to create Item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Item item)
        {
            if (ModelState.IsValid)
                return BadRequest("This model is not valid");

            var result = Item.Create(item.Name, item.Description, item.InitialPrice, item.AvailableForAuction, item.FinishAuctionTime, item.RequiredIncreaseAmount);
            if (result.Failure)
                return BadRequest(result.Error);

            await _context.Items.AddAsync(result.Value);
            await _context.SaveChangesAsync();
            return Ok(result.Value);
        }

        /// <summary>
        /// method to sellItem 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Sell(int id)
        {
            if (ModelState.IsValid)
                return BadRequest("This model is not valid");

            var item = await _context.Items.Where(x => x.Id == id).FirstOrDefaultAsync();
            var result = item.SellItem();
            if (result.Failure)
                return BadRequest(result.Error);
            return Ok(item);
        }
    }
}
