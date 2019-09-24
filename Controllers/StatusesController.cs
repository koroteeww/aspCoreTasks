using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using aspCoreEmpty.Models;

namespace aspCoreEmpty.Controllers
{
    public class StatusesController : Controller
    {
        private readonly tasksContext _context;

        public StatusesController(tasksContext context)
        {
            _context = context;
        }

        // GET: Statuses
        public async Task<IActionResult> Index()
        {
            return View(await _context.Statuses.ToListAsync());
        }

        // GET: Statuses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statuses = await _context.Statuses
                .FirstOrDefaultAsync(m => m.Idstatuses == id);
            if (statuses == null)
            {
                return NotFound();
            }

            return View(statuses);
        }

        // GET: Statuses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Statuses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idstatuses,Name")] Statuses statuses)
        {
            if (ModelState.IsValid)
            {
                if (statuses.Idstatuses == 0)
                {
                    //generate id
                    int maxid = 0;
                    
                    foreach (var cat in _context.Statuses)
                    {
                        if (cat.Idstatuses > maxid)
                        {
                            maxid = cat.Idstatuses;
                        }
                    }
                    statuses.Idstatuses = maxid + 1;
                }
                _context.Add(statuses);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(statuses);
        }

        // GET: Statuses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statuses = await _context.Statuses.FindAsync(id);
            if (statuses == null)
            {
                return NotFound();
            }
            return View(statuses);
        }

        // POST: Statuses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idstatuses,Name")] Statuses statuses)
        {
            if (id != statuses.Idstatuses)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(statuses);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatusesExists(statuses.Idstatuses))
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
            return View(statuses);
        }

        // GET: Statuses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var statuses = await _context.Statuses
                .FirstOrDefaultAsync(m => m.Idstatuses == id);
            if (statuses == null)
            {
                return NotFound();
            }

            return View(statuses);
        }

        // POST: Statuses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var statuses = await _context.Statuses.FindAsync(id);
            _context.Statuses.Remove(statuses);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StatusesExists(int id)
        {
            return _context.Statuses.Any(e => e.Idstatuses == id);
        }
    }
}
