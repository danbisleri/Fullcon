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
    public class ClienteAnexosController : BaseController
    {
        private readonly CoreDbContext _context;

        public ClienteAnexosController(CoreDbContext context)
        {
            _context = context;
        }

        // GET: ClienteAnexos
        public async Task<IActionResult> Index()
        {
            var coreDbContext = _context.clienteAnexos.Include(c => c.cliente);
            return View(await coreDbContext.ToListAsync());
        }

        // GET: ClienteAnexos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clienteAnexos = await _context.clienteAnexos
                .Include(c => c.cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clienteAnexos == null)
            {
                return NotFound();
            }

            return View(clienteAnexos);
        }

        // GET: ClienteAnexos/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.clientes, "Id", "Id");
            return View();
        }

        // POST: ClienteAnexos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCliente,arquivo,tipo,nome,tamanho,Id,DataInclusion,UserName")] ClienteAnexos clienteAnexos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clienteAnexos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.clientes, "Id", "Id", clienteAnexos.IdCliente);
            return View(clienteAnexos);
        }

        // GET: ClienteAnexos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clienteAnexos = await _context.clienteAnexos.FindAsync(id);
            if (clienteAnexos == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.clientes, "Id", "Id", clienteAnexos.IdCliente);
            return View(clienteAnexos);
        }

        // POST: ClienteAnexos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCliente,arquivo,tipo,nome,tamanho,Id,DataInclusion,UserName")] ClienteAnexos clienteAnexos)
        {
            if (id != clienteAnexos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clienteAnexos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteAnexosExists(clienteAnexos.Id))
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
            ViewData["IdCliente"] = new SelectList(_context.clientes, "Id", "Id", clienteAnexos.IdCliente);
            return View(clienteAnexos);
        }

        // GET: ClienteAnexos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clienteAnexos = await _context.clienteAnexos
                .Include(c => c.cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clienteAnexos == null)
            {
                return NotFound();
            }

            return View(clienteAnexos);
        }

        // POST: ClienteAnexos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clienteAnexos = await _context.clienteAnexos.FindAsync(id);
            _context.clienteAnexos.Remove(clienteAnexos);
            await _context.SaveChangesAsync();


            //return RedirectToAction(nameof(Index));

            return RedirectToAction("Create", "Clientes", new { id = clienteAnexos.IdCliente });
        }

        public async Task<IActionResult> DownloadAsync(int id)
        {
            var cliAnexos = await _context.clienteAnexos.FindAsync(id);
            var content = new System.IO.MemoryStream(cliAnexos.arquivo);
            var contentType = cliAnexos.tipo;
            var fileName = cliAnexos.nome;
            return File(content, contentType, fileName);
        }

        private bool ClienteAnexosExists(int id)
        {
            return _context.clienteAnexos.Any(e => e.Id == id);
        }
    }
}
