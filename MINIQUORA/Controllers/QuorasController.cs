using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MINIQUORA.Data;
using MINIQUORA.Models;

namespace MINIQUORA.Controllers
{
    public class QuorasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuorasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Quoras
        public async Task<IActionResult> Index()
        {
              return _context.Quora != null ? 
                          View(await _context.Quora.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Quora'  is null.");
        }
        // GET: Quoras/showsearchform

        public async Task<IActionResult> ShowSearchForm()
        {
            return View();

        }
        // Post Search: Quoras/showsearchresults

        public async Task<IActionResult> ShowSearchResults(string SearchPhrase)
        {
            if (SearchPhrase is null)
            {
                throw new ArgumentNullException(nameof(SearchPhrase));
            }

            return View("Index", await _context.Quora.Where(j => j.QuoraQuestion.Contains("SearchPhrase")).ToListAsync());

        }
 

        // GET: Quoras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Quora == null)
            {
                return NotFound();
            }

            var quora = await _context.Quora
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quora == null)
            {
                return NotFound();
            }

            return View(quora);
        }

        // GET: Quoras/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Quoras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,QuoraQuestion,QuoraAnswer")] Quora quora)
        {
            if (ModelState.IsValid)
            {
                _context.Add(quora);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(quora);
        }

        // GET: Quoras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Quora == null)
            {
                return NotFound();
            }

            var quora = await _context.Quora.FindAsync(id);
            if (quora == null)
            {
                return NotFound();
            }
            return View(quora);
        }

        // POST: Quoras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,QuoraQuestion,QuoraAnswer")] Quora quora)
        {
            if (id != quora.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quora);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuoraExists(quora.Id))
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
            return View(quora);
        }

        // GET: Quoras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Quora == null)
            {
                return NotFound();
            }

            var quora = await _context.Quora
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quora == null)
            {
                return NotFound();
            }

            return View(quora);
        }

        // POST: Quoras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Quora == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Quora'  is null.");
            }
            var quora = await _context.Quora.FindAsync(id);
            if (quora != null)
            {
                _context.Quora.Remove(quora);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuoraExists(int id)
        {
          return (_context.Quora?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
