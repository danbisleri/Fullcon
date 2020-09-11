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
    public class ClienteConexoesController : BaseController
    {
        private readonly CoreDbContext _context;

        public ClienteConexoesController(CoreDbContext context)
        {
            _context = context;
        }

        // GET: ClienteConexoes
        public async Task<IActionResult> Index()
        {
            var coreDbContext = _context.clienteConexoes.Include(c => c.cliente);
            return View(await coreDbContext.ToListAsync());
        }

        // GET: ClienteConexoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clienteConexoes = await _context.clienteConexoes
                .Include(c => c.cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clienteConexoes == null)
            {
                return NotFound();
            }

            return View(clienteConexoes);
        }

        // GET: ClienteConexoes/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.clientes, "Id", "Id");
            return View();
        }

        // POST: ClienteConexoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCliente,descricao,ip,usuario,senha,obs,Id,DataInclusion,UserName")] ClienteConexoes clienteConexoes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clienteConexoes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.clientes, "Id", "Id", clienteConexoes.IdCliente);
            return View(clienteConexoes);
        }

        // GET: ClienteConexoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clienteConexoes = await _context.clienteConexoes.FindAsync(id);
            if (clienteConexoes == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.clientes, "Id", "Id", clienteConexoes.IdCliente);
            return View(clienteConexoes);
        }

        // POST: ClienteConexoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCliente,descricao,ip,usuario,senha,obs,Id,DataInclusion,UserName")] ClienteConexoes clienteConexoes)
        {
            if (id != clienteConexoes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clienteConexoes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteConexoesExists(clienteConexoes.Id))
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
            ViewData["IdCliente"] = new SelectList(_context.clientes, "Id", "Id", clienteConexoes.IdCliente);
            return View(clienteConexoes);
        }

        // GET: ClienteConexoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clienteConexoes = await _context.clienteConexoes
                .Include(c => c.cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clienteConexoes == null)
            {
                return NotFound();
            }

            return View(clienteConexoes);
        }

        // POST: ClienteConexoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clienteConexoes = await _context.clienteConexoes.FindAsync(id);
            _context.clienteConexoes.Remove(clienteConexoes);
            await _context.SaveChangesAsync();
            // return RedirectToAction(nameof(Index));

            return RedirectToAction("Create", "Clientes", new { id = clienteConexoes.IdCliente});
        }

        private bool ClienteConexoesExists(int id)
        {
            return _context.clienteConexoes.Any(e => e.Id == id);
        }
    }
}
