using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projeKayıpBul.Data;
using projeKayıpBul.Models;

namespace projeKayıpBul.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LostItemsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LostItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/LostItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LostItem>>> GetLostItem()
        {
            return await _context.LostItem.ToListAsync();
        }

        // GET: api/LostItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LostItem>> GetLostItem(int id)
        {
            var lostItem = await _context.LostItem.FindAsync(id);

            if (lostItem == null)
            {
                return NotFound();
            }

            return lostItem;
        }

        // PUT: api/LostItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutLostItem(int id, LostItem lostItem)
        {
            if (id != lostItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(lostItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LostItemExists(id))
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

        // POST: api/LostItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<LostItem>> PostLostItem(LostItem lostItem)
        {
            _context.LostItem.Add(lostItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLostItem", new { id = lostItem.Id }, lostItem);
        }

        // DELETE: api/LostItems/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLostItem(int id)
        {
            var lostItem = await _context.LostItem.FindAsync(id);
            if (lostItem == null)
            {
                return NotFound();
            }

            _context.LostItem.Remove(lostItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LostItemExists(int id)
        {
            return _context.LostItem.Any(e => e.Id == id);
        }
    }
}
