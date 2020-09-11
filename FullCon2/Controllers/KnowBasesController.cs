using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DBConnection;
using Domain;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;

namespace FullCon2.Controllers
{
    public class KnowBasesController : BaseController
    {
        private readonly CoreDbContext _context;

        public KnowBasesController(CoreDbContext context)
        {
            _context = context;
        }

        // GET: KnowBases
        public async Task<IActionResult> Index()
        {
            var knowBase = await _context.knowBase.Include(a => a.knowbaseanexo).ToListAsync();

            return View(knowBase.OrderBy(b => b.descricao));
        }

        // GET: KnowBases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var knowBase = await _context.knowBase
                .FirstOrDefaultAsync(m => m.Id == id);
            if (knowBase == null)
            {
                return NotFound();
            }

            return View(knowBase);
        }

        // GET: KnowBases/Create
        public async Task<IActionResult> Create(int? id)
        {
            if (id == null)
            {
                return View();
            }

            var knowBase = await _context.knowBase.Include(a => a.knowbaseanexo).FirstOrDefaultAsync(k => k.Id == id);

            if (knowBase == null)
            {
                return NotFound();
            }

            return View(knowBase);
        }

        // POST: KnowBases/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? id, [Bind("erro,descricao,solucao,obs,Id")] KnowBase knowBase, IFormFile[] file, string[] idAnexo)
        {
            if (ModelState.IsValid)
            {
                List<KnowBaseAnexos> listAnexo = new List<KnowBaseAnexos>();

                if (knowBase.Id == id)
                {
                    for (int i = 0; i < file.Length; i++)
                    {
                        using (var ms = new MemoryStream())
                        {
                            KnowBaseAnexos anexo = new KnowBaseAnexos();
                            file[i].CopyTo(ms);
                            anexo.arquivo = ms.ToArray();
                            anexo.nome = file[i].FileName;
                            anexo.tipo = file[i].ContentType;

                            listAnexo.Add(anexo);
                        }

                    }

                    knowBase.knowbaseanexo = listAnexo;

                    _context.Update(knowBase);
                    await _context.SaveChangesAsync();

                }
                else
                {
                    for (int i = 0; i < file.Length; i++)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file[i].CopyTo(ms);
                            listAnexo.Add(new KnowBaseAnexos()
                            {
                                arquivo = ms.ToArray(),
                                nome = file[i].Name,
                                tipo = file[i].ContentType
                            });
                        }
                    }

                    knowBase.knowbaseanexo = listAnexo;

                    _context.Add(knowBase);
                    await _context.SaveChangesAsync();

                }
            }
            //ViewData["IdCliente"] = new SelectList(_context.clientes, "Id", "Id", knowBase.IdCliente);
            //ViewData["IdKnowLocais"] = new SelectList(_context.knowLocais, "Id", "Id", knowBase.IdKnowLocais);
            //ViewData["IdKnowProduto"] = new SelectList(_context.knowProduto, "Id", "Id", knowBase.IdKnowProduto);
            //return View(knowBase);
            return RedirectToAction(nameof(Index));
        }


        // GET: KnowBases/Edit/5
        public IActionResult Edit(int? id)
        {
            return RedirectToAction("Create", "KnowBases", new { id });
        }

        // POST: KnowBases/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdCliente,IdKnowProduto,IdKnowLocais,erro,descricao,solucao,obs,versao,Id,DataInclusion,UserName")] KnowBase knowBase)
        {
            if (id != knowBase.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(knowBase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KnowBaseExists(knowBase.Id))
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
            //ViewData["IdCliente"] = new SelectList(_context.clientes, "Id", "Id", knowBase.IdCliente);
            //ViewData["IdKnowLocais"] = new SelectList(_context.knowLocais, "Id", "Id", knowBase.IdKnowLocais);
            //ViewData["IdKnowProduto"] = new SelectList(_context.knowProduto, "Id", "Id", knowBase.IdKnowProduto);
            return View(knowBase);
        }



        public ActionResult ModalDelKnow(int id)
        {

            var model = _context.knowBase.FirstOrDefault(c => c.Id == id);

            return PartialView("modal_deleteknow", model);

        }

        public ActionResult ModalDelAnexo(int id)
        {

            var model = _context.knowBaseAnexo.FirstOrDefault(c => c.Id == id);

            return PartialView("modal_deleteknowanexo", model);

        }

        // POST: KnowBases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var knowBase = await _context.knowBase.FindAsync(id);
            _context.knowBase.Remove(knowBase);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: KnowBases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var knowBase = await _context.knowBase
                .FirstOrDefaultAsync(m => m.Id == id);
            if (knowBase == null)
            {
                return NotFound();
            }

            return View(knowBase);
        }

        private bool KnowBaseExists(int id)
        {
            return _context.knowBase.Any(e => e.Id == id);
        }



    }
}
