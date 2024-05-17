using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VueApp1.Server.Models;

namespace VueApp1.Server.Controllers
{
    [Route("Item")]
    public class ItemController : Controller
    {
        public RepoContext _context;

        public ItemController(RepoContext context)
        {
            _context = context;
        }

        [HttpGet, Route("")]
        public async Task<IActionResult> Index([FromQuery] int menuId)
            => Json(await _context.MenuItems.Where(x => x.MenuId == menuId).ToListAsync());

        [HttpGet, Route("{id:int}")]
        public async Task<IActionResult> GetSingle([FromRoute] int id)
            => Json(await _context.MenuItems.FindAsync(id));

        [HttpPut, Route("")]
        public async Task<IActionResult> Put([FromBody] MenuItem item)
        {
            if (item.Id == 0)
            {
                _context.MenuItems.Add(item);
                await _context.SaveChangesAsync();
                return Created();
            }

            var record = await _context.MenuItems.FindAsync(item.Id)
                ?? throw new("Menu not found");

            record.Name = item.Name;
            record.Description = item.Description;
            record.UnitPrice = item.UnitPrice;
            record.Quantity = item.Quantity;
            record.TotalPrice = item.TotalPrice;

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut, Route("{id:int}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] MenuItem item)
        {
            item.Id = id;
            return await Put(item);
        }

        [HttpPost, Route("")]
        public async Task<IActionResult> Post([FromBody] MenuItem item)
        {
            item.Id = 0;
            return await Put(item);
        }

        [HttpDelete, Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var menu = await _context.MenuItems.FindAsync(id)
                ?? throw new("Menu not found");
            _context.MenuItems.Remove(menu);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
