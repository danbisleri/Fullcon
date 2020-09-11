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
    public class ClienteLinksController : BaseController
    {
        private readonly CoreDbContext _context;

        public ClienteLinksController(CoreDbContext context)
        {
            _context = context;
        }

        // GET: ClienteLinks
        public async Task<IActionResult> Index()
        {
            var coreDbContext = _context.clienteLinks.Include(c => c.cliente);
            return View(await coreDbContext.ToListAsync());
        }

        // GET: ClienteLinks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clienteLinks = await _context.clienteLinks
                .Include(c => c.cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clienteLinks == null)
            {
                return NotFound();
            }

            return View(clienteLinks);
        }

        // GET: ClienteLinks/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.clientes, "Id", "Id");
            return View();
        }

        // POST: ClienteLinks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCliente,link,usuario,senha,Id")] ClienteLinks clienteLinks)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clienteLinks);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.clientes, "Id", "Id", clienteLinks.IdCliente);
            return View(clienteLinks);
        }

        // GET: ClienteLinks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clienteLinks = await _context.clienteLinks.FindAsync(id);
            if (clienteLinks == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.clientes, "Id", "Id", clienteLinks.IdCliente);
            return View(clienteLinks);
        }

        // POST: ClienteLinks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCliente,link,usuario,senha,Id,DataInclusion,UserName")] ClienteLinks clienteLinks)
        {
            if (id != clienteLinks.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clienteLinks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteLinksExists(clienteLinks.Id))
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
            ViewData["IdCliente"] = new SelectList(_context.clientes, "Id", "Id", clienteLinks.IdCliente);
            return View(clienteLinks);
        }

        // GET: ClienteLinks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clienteLinks = await _context.clienteLinks
                .Include(c => c.cliente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clienteLinks == null)
            {
                return NotFound();
            }

            //var clienteLinks = await _context.clienteLinks.FindAsync(id);
            _context.clienteLinks.Remove(clienteLinks);
            await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Create", "Clientes", new { id = clienteLinks.cliente.Id });

            //return View(clienteLinks);
        }

        // POST: ClienteLinks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clienteLinks = await _context.clienteLinks.FindAsync(id);
            _context.clienteLinks.Remove(clienteLinks);
            await _context.SaveChangesAsync();
            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Create", "Clientes", new { id = clienteLinks.IdCliente });
        }

        private bool ClienteLinksExists(int id)
        {
            return _context.clienteLinks.Any(e => e.Id == id);
        }
    }
}
