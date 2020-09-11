using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DBConnection;
using Domain;
using System.Net;
using System.Threading;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace FullCon2.Controllers
{
    public class ClientesController : BaseController
    {
        private readonly CoreDbContext _context;

        public ClientesController(CoreDbContext context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            var cli = await _context.clientes.Include(c => c.clientelinks).Include(c => c.clienteconexao).Include(c => c.clienteanexos).ToListAsync();

            return View(cli.OrderBy(c => c.nome));
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientes = await _context.clientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clientes == null)
            {
                return NotFound();
            }

            return View(clientes);
        }

        // GET: Clientes/Create
        public async Task<IActionResult> Create(int? id)
        {
            if (id == null)
            {
                return View();
            }

            Clientes clientes = await _context.clientes.Include(c => c.clientelinks).Include(c => c.clienteconexao).Include(c => c.clienteanexos).FirstOrDefaultAsync(c => c.Id == id);

            if (clientes == null)
            {
                return NotFound();
            }

            return View(clientes);

        }


        // POST: Clientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int? id, [Bind("nome,obs,Id")] Clientes clientes
                                               , string[] link , string[] user, string[] pass, string[] nomelink, int?[] linkid
                                               , string[] conexao_pass, string[] conexao_user, string[] conexao_ip, string[] conexao_desc, string[] obsConexao,  int?[] conexao_id 
                                               , IFormFile[] file, string[] idAnexo)
        {
            if (ModelState.IsValid)
            {
                List<ClienteLinks> cliLink = new List<ClienteLinks>();
                List<ClienteAnexos> cliAnexo = new List<ClienteAnexos>();
                List<ClienteConexoes> cliConexao = new List<ClienteConexoes>();

                if (clientes.Id == id)
                {
                    try
                    {
                        for (int i = 0; i < linkid.Length; i++)
                        {
                            try
                            {
                                if (linkid[i].ToString() != null && !string.IsNullOrEmpty(linkid[i].ToString()))
                                {
                                    try
                                    {
                                        var idlink = string.IsNullOrEmpty(linkid[i].ToString());

                                        if (!string.IsNullOrEmpty(linkid[i].ToString()))
                                        {
                                            ClienteLinks clienteLinks = _context.clienteLinks.Find(linkid[i]);
                                            clienteLinks.link = link[i];
                                            clienteLinks.senha = pass[i];
                                            clienteLinks.usuario = user[i];
                                            clienteLinks.nome = nomelink[i];

                                            cliLink.Add(clienteLinks);
                                        }
                                    }
                                    catch
                                    {
                                        cliLink.Add(new ClienteLinks()
                                        {
                                            link = link[i],
                                            senha = pass[i],
                                            usuario = user[i],
                                            nome = nomelink[i]
                                        });
                                    }
                                }

                                clientes.clientelinks = cliLink;
                            }
                            catch
                            { }
                        }


                        for (int i = 0; i < conexao_id.Length; i++)
                        {
                            try
                            {
                                if ( conexao_id[i].ToString() != null && !string.IsNullOrEmpty(conexao_id[i].ToString()))
                                {
                                    try
                                    {
                                        var id_conexao = string.IsNullOrEmpty(conexao_id[i].ToString());

                                        if (!string.IsNullOrEmpty(conexao_id[i].ToString()))
                                        {
                                            ClienteConexoes clicon = _context.clienteConexoes.Find(conexao_id[i]);

                                            clicon.descricao  = conexao_desc[i];
                                            clicon.senha = conexao_pass[i];
                                            clicon.usuario = conexao_user[i];
                                            clicon.ip = conexao_ip[i];
                                            clicon.obsConexao = obsConexao[i];

                                            cliConexao.Add(clicon);
                                        }
                                    }
                                    catch
                                    {
                                        cliConexao.Add(new ClienteConexoes()
                                        {
                                            descricao = conexao_desc[i],
                                            ip = conexao_ip[i],
                                            senha = conexao_pass[i],
                                            usuario = conexao_user[i],
                                            obsConexao = obsConexao[i]
                                        });
                                    }
                                }

                                clientes.clienteconexao = cliConexao;
                            }
                            catch
                            { }
                        }

                        try
                        {
                            for (int a = 0; a < file.Length; a++)
                            {
                                try
                                {
                                    using (var ms = new MemoryStream())
                                    {
                                        ClienteAnexos anexo = new ClienteAnexos();
                                        file[a].CopyTo(ms);
                                        anexo.arquivo = ms.ToArray();
                                        anexo.nome = file[a].FileName;
                                        anexo.tipo = file[a].ContentType;

                                        cliAnexo.Add(anexo);
                                    }

                                }
                                catch
                                { }

                            }
                            clientes.clienteanexos = cliAnexo;
                        }
                        catch
                        { }

                        _context.Update(clientes);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ClientesExists(clientes.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }



                    //return RedirectToAction(nameof(Edit));
                    //return RedirectToAction(nameof(Index));
                }
                else
                {
                    //_context.Add(clientes);
                    //List<ClienteLinks> cl = new List<ClienteLinks>();

                    for (int i = 0; i < link.Length; i++)
                    {
                        cliLink.Add(new ClienteLinks()
                        {
                            link = link[i],
                            senha = pass[i],
                            usuario = user[i],
                            nome = nomelink[i]
                        });
                    }

                    for (int i = 0; i < conexao_desc.Length; i++)
                    {
                        cliConexao.Add(new ClienteConexoes()
                        {
                            descricao = conexao_desc[i],
                            ip = conexao_ip[i],
                            senha = conexao_pass[i],
                            usuario = conexao_user[i],
                            obsConexao = obsConexao[i]
                        });
                    }

                    for (int i = 0; i < file.Length; i++)
                    {
                        using (var ms = new MemoryStream())
                        {
                            file[i].CopyTo(ms);
                            cliAnexo.Add(new ClienteAnexos()
                            {
                                arquivo = ms.ToArray(),
                                nome = file[i].Name,
                                tipo = file[i].ContentType
                            });
                        }
                    }

                    clientes.clientelinks = cliLink;

                    clientes.clienteconexao = cliConexao;

                    clientes.clienteanexos = cliAnexo;

                    

                    _context.Add(clientes);

                    await _context.SaveChangesAsync();
                    //return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                //Danger("Model Invalid!");
            }
            //Success("Cliente salvo com sucesso.");
            return RedirectToAction(nameof(Index));
            //return View(clientes);
        }

        // GET: Clientes/Create/5
        public IActionResult Edit(int? id)
        {
            return RedirectToAction("Create", "Clientes", new { id });
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("nome,obs,Id,DataInclusion,UserName")] Clientes clientes)
        {
            if (id != clientes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clientes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientesExists(clientes.Id))
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
            return View(clientes);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var clientes = await _context.clientes.FindAsync(id);
            _context.clientes.Remove(clientes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clientes = await _context.clientes.FindAsync(id);
            _context.clientes.Remove(clientes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientesExists(int id)
        {
            return _context.clientes.Any(e => e.Id == id);
        }

        public ActionResult ModalDel(int id)
        {

            var model = _context.clienteLinks.FirstOrDefault(c => c.Id == id);

            return PartialView("modal_delete", model);

        }


        public ActionResult ModalAnexoCliente(int id)
        {

            var model = _context.clientes.Include(c => c.clienteanexos).FirstOrDefault(c => c.Id == id);

            return PartialView("modal_anexo", model);

        }

        public ActionResult ModalDelCliente(int id)
        {

            var model = _context.clientes.FirstOrDefault(c => c.Id == id);

            return PartialView("modal_deletecliente", model);

        }

        public ActionResult ModalObs(int id)
        {
            var model = _context.clientes.FirstOrDefault(c => c.Id == id);
            return PartialView("modal_obs", model);
        }


        public ActionResult ModalDelAnexo(int id)
        {

            var model = _context.clienteAnexos.FirstOrDefault(c => c.Id == id);

            return PartialView("modal_deleteclianexo", model);

        }


        public ActionResult TesteUrl(string endereco)
        {
            bool ret = false;
            if (!string.IsNullOrEmpty(endereco))
                ret = UrlIsUp(endereco);

            return Json(ret);
        }

        public bool UrlIsUp(string link)
        {
            //try
            //{
            //    //Creating the HttpWebRequest
            //    HttpWebRequest request = WebRequest.Create(link) as HttpWebRequest;
            //    //Setting the Request method HEAD, you can also use GET too.
            //    request.Method = "HEAD";
            //    request.Timeout = 3000;
            //    //Getting the Web Response.
            //    HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            //    //Returns TRUE if the Status code == 200
            //    response.Close();
            //    return (response.StatusCode == HttpStatusCode.OK);
            //}
            //catch
            //{
            //    //teste web service
            //    try
            //    {
            //        WebClient client = new WebClient();
            //        var request = (HttpWebRequest)WebRequest.Create(link);
            //        request.Timeout = 3000;
            //        var response = (HttpWebResponse)request.GetResponse();
            //        if (response.StatusCode == HttpStatusCode.OK)
            //        {
            //            return true;
            //        }
            //        else
            //        {
            //            return false;
            //        }
            //    }
            //    catch
            //    {
            //        return false;
            //    }

            //}

            try
            {
                WebClient client = new WebClient();
                var request = (HttpWebRequest)WebRequest.Create(link);
                request.Timeout = 3000;
                var response = (HttpWebResponse)request.GetResponse();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                try
                {
                    //Creating the HttpWebRequest
                    HttpWebRequest request = WebRequest.Create(link) as HttpWebRequest;
                    //Setting the Request method HEAD, you can also use GET too.
                    request.Method = "HEAD";
                    request.Timeout = 3000;
                    //Getting the Web Response.
                    HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                    //Returns TRUE if the Status code == 200
                    response.Close();
                    return (response.StatusCode == HttpStatusCode.OK);
                }
                catch
                {
                    return false;
                }
            }
        }


    }
}
