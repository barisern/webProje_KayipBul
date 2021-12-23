using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projeKayıpBul.Data;
using projeKayıpBul.Models;

namespace projeKayıpBul.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LostItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LostItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/LostItems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.LostItem.Include(l => l.ApplicationUser).Include(l => l.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/LostItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lostItem = await _context.LostItem
                .Include(l => l.ApplicationUser)
                .Include(l => l.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lostItem == null)
            {
                return NotFound();
            }

            return View(lostItem);
        }

        // GET: Admin/LostItems/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.ApplicationUser, "Id", "Id");
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");
            return View();
        }

        // POST: Admin/LostItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Address,LosingDate,MoreInfo,Status,Photo,CategoryId,UserId")] LostItem lostItem)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                if (files.Count > 0)
                {
                    byte[] p1 = null;
                    using (var fs1 = files[0].OpenReadStream())
                    {
                        using (var ms1 = new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            p1 = ms1.ToArray();
                        }
                    }
                    lostItem.Photo = p1;
                }
                _context.Add(lostItem);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", lostItem.UserId);
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", lostItem.CategoryId);
            return View(lostItem);
        }

        // GET: Admin/LostItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lostItem = await _context.LostItem.FindAsync(id);
            if (lostItem == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", lostItem.UserId);
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", lostItem.CategoryId);
            return View(lostItem);
        }

        // POST: Admin/LostItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Address,LosingDate,MoreInfo,Status,Photo,CategoryId,UserId")] LostItem lostItem)
        {
            if (id != lostItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lostItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LostItemExists(lostItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUser, "Id", "Id", lostItem.UserId);
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", lostItem.CategoryId);
            return View(lostItem);
        }

        // GET: Admin/LostItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lostItem = await _context.LostItem
                .Include(l => l.ApplicationUser)
                .Include(l => l.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lostItem == null)
            {
                return NotFound();
            }

            return View(lostItem);
        }

        // POST: Admin/LostItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lostItem = await _context.LostItem.FindAsync(id);
            _context.LostItem.Remove(lostItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LostItemExists(int id)
        {
            return _context.LostItem.Any(e => e.Id == id);
        }
    }
}
