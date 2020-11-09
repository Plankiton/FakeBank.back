using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Challenge.Models;

namespace Challenge.Controllers
{
    [Route("api/[controller]")] 
    [ApiController]
    public class ClientController : Controller
    {
        private readonly ChallengeContext _context;

        public ClientController(ChallengeContext context)
        {
            _context = context;
        }

        // GET: api/client
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetChallengeClients()
        {

            if (!_context.Clients.Any()){
                foreach (var c in new List<double> {10, 29, 1.6, 7, 8.3}){
                    var client = new Client {
                            Name = "Client"+c,
                            Balance = c*c,
                            Password = Hasher.Hash("joao")
                            };
                    _context.Clients.Add(client);
                    _context.Operations.Add(new Operation{ Client = client.Id.ToString(), Type = "GetClient", Date = DateTime.Now });
                }
            }

            await _context.SaveChangesAsync();
            return await _context.Clients.ToListAsync();
        }

        // GET: api/client/pass/5
        [HttpGet("byname/{pass}/{name}")]
        public async Task<ActionResult<ClientResponse>> GetChallengeClientByName(string pass, string name)
        {
            var ChallengeClient = _context.Clients.First((c) => c.Name == name);

            if (ChallengeClient == null)
            {
                return NotFound();
            }

            _context.Operations.Add(new Operation{
                    Client = ChallengeClient.Id.ToString(),
                    Type = "GetClient",
                    Date = DateTime.Now });

            if (Hasher.Verify(pass, ChallengeClient.Password)){
                await _context.SaveChangesAsync();
                return new ClientResponse {
                    Id = ChallengeClient.Id,
                    Name = ChallengeClient.Name,
                    Balance = ChallengeClient.Balance
                };
            }

            return Unauthorized();
        }

        // GET: api/client/pass/5
        [HttpGet("{pass}/{id}")]
        public async Task<ActionResult<ClientResponse>> GetChallengeClient(string pass, long id)
        {
            var ChallengeClient = await _context.Clients.FindAsync(id);

            if (ChallengeClient == null)
            {
                return NotFound();
            }

            _context.Operations.Add(new Operation{
                    Client = ChallengeClient.Id.ToString(),
                    Type = "GetClient",
                    Date = DateTime.Now });

            if (Hasher.Verify(pass, ChallengeClient.Password)){
                await _context.SaveChangesAsync();
                return new ClientResponse {
                    Id = ChallengeClient.Id,
                    Name = ChallengeClient.Name,
                    Balance = ChallengeClient.Balance
                };
            }

            return Unauthorized();
        }

        // POST: api/Challenge
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Operation>> PostChallengeClient(ClientRequest client)
        {
            var ChallengeClient = new Client {
                Name = client.Name,
                Password = client.Password,
                Balance = 0
            };

            ChallengeClient.Password = Hasher.Hash(ChallengeClient.Password);
            _context.Clients.Add(ChallengeClient);

            var operation = new Operation{
                    Client = ChallengeClient.Id.ToString(),
                    Type = "CreateClient",
                    Date = DateTime.Now
                    };

            _context.Operations.Add(operation);
            await _context.SaveChangesAsync();

            return operation;
        }

        // DELETE: api/Challenge/5
        [HttpDelete("{pass}/{id}")]
        public async Task<ActionResult<Operation>> DeleteChallengeClient(string pass, long id)
        {
            var ChallengeClient = await _context.Clients.FindAsync(id);
            if (ChallengeClient == null)
            {
                return NotFound();
            }

            var operation = new Operation{ Client = ChallengeClient.Id.ToString(), Type = "RemoveClient", Date = DateTime.Now };
            _context.Operations.Add(operation);

            if (Hasher.Verify(pass, ChallengeClient.Password)){
                _context.Clients.Remove(ChallengeClient);
                await _context.SaveChangesAsync();

                return operation;
            }

            return Unauthorized();
        }

        private bool ChallengeClientExists(long id)
        {
            return _context.Clients.Any(e => e.Id == id);
        }
    }
}
