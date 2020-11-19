using DBConnection;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullCon2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientConnectionController : ControllerBase
    {
        private readonly CoreDbContext _context;

        public ClientConnectionController(CoreDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> CreateClientConnection(Clientes cliCon)
        {
            if (cliCon != null)
            {
                Clientes cliente;
                List<ClienteLinks> cliLink = new List<ClienteLinks>();
                List<ClienteConexoes> cliConexao = new List<ClienteConexoes>();
                bool addSomething = false;

                try
                {
                    var clientes = _context.clientes.Include(c => c.clienteconexao);

                    if (!clientes.Where(s => s.nome == cliCon.nome).Any())
                    {
                        addSomething = true;
                        cliente = new Clientes() { nome = cliCon.nome };
                    }
                    else
                    {
                        cliente = clientes.Where(s => s.nome == cliCon.nome).FirstOrDefault();
                    }

                    foreach (var con in cliCon.clienteconexao)
                    {
                        if (!clientes.Where(s => s.clienteconexao.Where(c => c.descricao == con.descricao).Any()).Any())
                        {
                            addSomething = true;
                            cliConexao.Add(new ClienteConexoes()
                            {
                                descricao = con.descricao,
                                ip = con.ip,
                                senha = con.senha,
                                usuario = con.usuario,
                                obsConexao = con.obsConexao
                            });
                        }
                    }

                    foreach (var link in cliCon.clientelinks)
                    {
                        if (!clientes.Where(s => s.clientelinks.Where(c => c.link == link.link && c.cliente == cliente).Any()).Any())
                        {
                            addSomething = true;
                            cliLink.Add(new ClienteLinks()
                            {
                                nome = link.nome,
                                senha = link.senha,
                                usuario = link.usuario,
                                link = link.link
                            });
                        }
                    }

                    cliente.clientelinks = cliLink;

                    cliente.clienteconexao = cliConexao;

                    if (!addSomething)
                    {
                        return BadRequest("Cliente e Conexão já existem.");
                    }
                    else
                    { 
                        _context.Add(cliente);

                        await _context.SaveChangesAsync();

                        return Ok();
                    }
                }
                catch(Exception e)
                {
                    return BadRequest(e);
                }
            }
            return BadRequest();

        }
    }
}
