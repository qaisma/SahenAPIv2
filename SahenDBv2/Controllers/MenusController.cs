using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SahenDBv2.DB;

namespace SahenDBv2.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MenusController : ControllerBase
    {
        private readonly SahenDBContext _context;

        public MenusController(SahenDBContext context)
        {
            _context = context;
        }


        [HttpGet("{restaurantId}")]
        public async Task<ActionResult<IEnumerable<Menu>>> GetByRestaurantId(Guid restaurantId)
        {
            return await _context.Menus.Where(m=>m.RestaurantId==restaurantId)
                .Include(m=>m.MenuItems).ToListAsync();
        }

        // GET: api/Menus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Menu>> GetMenu(Guid id)
        {
            var menu = await _context.Menus.FindAsync(id);

            if (menu == null)
            {
                return NotFound();
            }

            return menu;
        }

        // GET: api/Menus/Restaurant/5
        [HttpGet("{restaurantId}")]
        public async Task<ActionResult<IEnumerable<Menu>>> GetRestaurantMenus(Guid restaurantId)
        {
            return await _context.Menus.Where(m=>m.RestaurantId==restaurantId).ToListAsync();
        }

        // PUT: api/Menus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMenu(Guid id, Menu menu)
        {
            if (id != menu.Id)
            {
                return BadRequest();
            }

            _context.Entry(menu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Menus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Menu>> PostMenu(Menu menu)
        {
            _context.Menus.Add(menu);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMenu", new { id = menu.Id }, menu);
        }

        // DELETE: api/Menus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenu(Guid id)
        {
            var menu = await _context.Menus.FindAsync(id);
            if (menu == null)
            {
                return NotFound();
            }

            _context.Menus.Remove(menu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MenuExists(Guid id)
        {
            return _context.Menus.Any(e => e.Id == id);
        }
    }
}
