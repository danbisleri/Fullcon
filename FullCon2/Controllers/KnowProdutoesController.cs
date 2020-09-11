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
    public class KnowProdutoesController : BaseController
    {
        private readonly CoreDbContext _context;

        public KnowProdutoesController(CoreDbContext context)
        {
            _context = context;
        }

        // GET: KnowProdutoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.knowProduto.ToListAsync());
        }

        // GET: KnowProdutoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var knowProduto = await _context.knowProduto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (knowProduto == null)
            {
                return NotFound();
            }

            return View(knowProduto);
        }

        // GET: KnowProdutoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: KnowProdutoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("nome,versao,Id,DataInclusion,UserName")] KnowProduto knowProduto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(knowProduto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(knowProduto);
        }

        // GET: KnowProdutoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var knowProduto = await _context.knowProduto.FindAsync(id);
            if (knowProduto == null)
            {
                return NotFound();
            }
            return View(knowProduto);
        }

        // POST: KnowProdutoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("nome,versao,Id,DataInclusion,UserName")] KnowProduto knowProduto)
        {
            if (id != knowProduto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(knowProduto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KnowProdutoExists(knowProduto.Id))
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
            return View(knowProduto);
        }

        // GET: KnowProdutoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var knowProduto = await _context.knowProduto
                .FirstOrDefaultAsync(m => m.Id == id);
            if (knowProduto == null)
            {
                return NotFound();
            }

            return View(knowProduto);
        }

        // POST: KnowProdutoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var knowProduto = await _context.knowProduto.FindAsync(id);
            _context.knowProduto.Remove(knowProduto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KnowProdutoExists(int id)
        {
            return _context.knowProduto.Any(e => e.Id == id);
        }
    }
}
