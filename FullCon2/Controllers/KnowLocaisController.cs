using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DBConnection;
using Domain;

namespace FullCon2.Controllers
{
    public class KnowLocaisController : BaseController
    {
        private readonly CoreDbContext _context;

        public KnowLocaisController(CoreDbContext context)
        {
            _context = context;
        }

        // GET: KnowLocais
        public async Task<IActionResult> Index()
        {
            return View(await _context.knowLocais.ToListAsync());
        }

        // GET: KnowLocais/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var knowLocais = await _context.knowLocais
                .FirstOrDefaultAsync(m => m.Id == id);
            if (knowLocais == null)
            {
                return NotFound();
            }

            return View(knowLocais);
        }

        // GET: KnowLocais/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: KnowLocais/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("nome,Id,DataInclusion,UserName")] KnowLocais knowLocais)
        {
            if (ModelState.IsValid)
            {
                _context.Add(knowLocais);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(knowLocais);
        }

        // GET: KnowLocais/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var knowLocais = await _context.knowLocais.FindAsync(id);
            if (knowLocais == null)
            {
                return NotFound();
            }
            return View(knowLocais);
        }

        // POST: KnowLocais/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("nome,Id,DataInclusion,UserName")] KnowLocais knowLocais)
        {
            if (id != knowLocais.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(knowLocais);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KnowLocaisExists(knowLocais.Id))
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
            return View(knowLocais);
        }

        // GET: KnowLocais/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var knowLocais = await _context.knowLocais
                .FirstOrDefaultAsync(m => m.Id == id);
            if (knowLocais == null)
            {
                return NotFound();
            }

            return View(knowLocais);
        }

        // POST: KnowLocais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var knowLocais = await _context.knowLocais.FindAsync(id);
            _context.knowLocais.Remove(knowLocais);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KnowLocaisExists(int id)
        {
            return _context.knowLocais.Any(e => e.Id == id);
        }
    }
}
