using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DBConnection;
using Domain;
using System.Text;
using System.IO;
using Microsoft.AspNetCore.Hosting.Server;

namespace FullCon2.Controllers
{
    public class KnowBaseAnexosController : BaseController
    {
        private readonly CoreDbContext _context;

        public KnowBaseAnexosController(CoreDbContext context)
        {
            _context = context;
        }

        // GET: KnowBaseAnexos
        public async Task<IActionResult> Index()
        {
            return View(await _context.knowBase.ToListAsync());
        }

        // GET: KnowBaseAnexos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var knowBaseAnexos = await _context.knowBase
                .FirstOrDefaultAsync(m => m.Id == id);
            if (knowBaseAnexos == null)
            {
                return NotFound();
            }

            return View(knowBaseAnexos);
        }

        // GET: KnowBaseAnexos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: KnowBaseAnexos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdKnowbase,arquivo,tipo,nome,Id,DataInclusion,UserName")] KnowBaseAnexos knowBaseAnexos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(knowBaseAnexos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(knowBaseAnexos);
        }

        // GET: KnowBaseAnexos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var knowBaseAnexos = await _context.knowBase.FindAsync(id);
            if (knowBaseAnexos == null)
            {
                return NotFound();
            }
            return View(knowBaseAnexos);
        }

        // POST: KnowBaseAnexos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdKnowbase,arquivo,tipo,nome,Id,DataInclusion,UserName")] KnowBaseAnexos knowBaseAnexos)
        {
            if (id != knowBaseAnexos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(knowBaseAnexos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KnowBaseAnexosExists(knowBaseAnexos.Id))
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
            return View(knowBaseAnexos);
        }

        // GET: KnowBaseAnexos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var knowBaseAnexos = await _context.knowBaseAnexo.FirstOrDefaultAsync(m => m.Id == id);

            if (knowBaseAnexos == null)
            {
                return NotFound();
            }

            return RedirectToAction("Create", "Clientes", new { id = knowBaseAnexos.IdKnowbase});

            //return View(knowBaseAnexos);
        }

        // POST: KnowBaseAnexos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var knowBase = await _context.knowBaseAnexo.FindAsync(id);
            _context.knowBaseAnexo.Remove(knowBase);
            await _context.SaveChangesAsync();
            return RedirectToAction("Create","KnowBases", new { id = knowBase.IdKnowbase } );
        }

        private bool KnowBaseAnexosExists(int id)
        {
            return _context.knowBase.Any(e => e.Id == id);
        }

        public async Task<IActionResult> DownloadAsync(int id)
        {
            var knowBaseAnexos = await _context.knowBaseAnexo.FindAsync(id);
            var content = new System.IO.MemoryStream(knowBaseAnexos.arquivo);
            var contentType = knowBaseAnexos.tipo;
            var fileName = knowBaseAnexos.nome;
            return File(content, contentType, fileName);
        }

    }
}
