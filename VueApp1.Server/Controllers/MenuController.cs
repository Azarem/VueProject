using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VueApp1.Server.Models;

namespace VueApp1.Server.Controllers
{
    [Route("Menu")]
    public class MenuController : Controller
    {
        public RepoContext _context;

        public MenuController(RepoContext context)
        {
            _context = context;
        }

        [HttpGet, Route("")]
        public async Task<IActionResult> Index([FromQuery] int personId)
            => Json(await _context.Menus.Where(x => x.PersonId == personId).ToListAsync());

        [HttpGet, Route("{id:int}")]
        public async Task<IActionResult> GetSingle([FromRoute] int id)
            => Json(await _context.Menus.FindAsync(id));

        [HttpPut, Route("")]
        public async Task<IActionResult> Put([FromBody] Menu menu)
        {
            if (menu.Id == 0)
            {
                _context.Menus.Add(menu);
                await _context.SaveChangesAsync();
                return Created();
            }

            var record = await _context.Menus.FindAsync(menu.Id)
                ?? throw new("Menu not found");

            record.Title = menu.Title;
            record.TotalPrice = menu.TotalPrice;
            record.TotalPaid = menu.TotalPaid;

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut, Route("{id:int}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] Menu menu)
        {
            menu.Id = id;
            return await Put(menu);
        }

        [HttpPost, Route("")]
        public async Task<IActionResult> Post([FromBody] Menu menu)
        {
            menu.Id = 0;
            return await Put(menu);
        }

        [HttpDelete, Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var menu = await _context.Menus.FindAsync(id)
                ?? throw new("Menu not found");
            _context.Menus.Remove(menu);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
