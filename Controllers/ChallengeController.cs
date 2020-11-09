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
        public async Task<ActionResult<IEnumerable<Client>>> GetChallengeClient()
        {

            if (!_context.Clients.Any()){
                foreach (var c in new List<double> {10, 29, 1.6, 7, 8.3}){
                    var client = new Client {
                            Name = "Client"+c,
                            Balance = c*c,
                            Password = Hasher.Hash("joao")
                            };
                    _context.Clients.Add(client);
                    _context.Operations.Add(new History{ Client = client.Id, Type = "GetClient", Date = DateTime.Now });
                }
            }

            await _context.SaveChangesAsync();
            return await _context.Clients.ToListAsync();
        }

        // GET: api/client/5
        [HttpGet("{pass}/{id}")]
        public async Task<ActionResult<Client>> GetChallengeClient(string pass, long id)
        {
            var ChallengeClient = await _context.Clients.FindAsync(id);

            if (ChallengeClient == null)
            {
                return NotFound();
            }

            _context.Operations.Add(new History{ Client = ChallengeClient.Id, Type = "GetClient", Date = DateTime.Now });
            await _context.SaveChangesAsync();

            if (Hasher.Verify(pass, ChallengeClient.Password))
                return ChallengeClient;
            return Unauthorized();
        }

        // POST: api/Challenge
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Client>> PostChallengeClient(Client ChallengeClient)
        {

            ChallengeClient.Password = Hasher.Hash(ChallengeClient.Password);
            _context.Clients.Add(ChallengeClient);

            _context.Operations.Add(new History{
                    Client = ChallengeClient.Id,
                    Type = "CreateClient",
                    Value = ChallengeClient.Id.ToString(),
                    Date = DateTime.Now
                    });
            await _context.SaveChangesAsync();

            return CreatedAtAction("CreateChallengeClient", new { id = ChallengeClient.Id }, ChallengeClient);
        }

        // DELETE: api/Challenge/5
        [HttpDelete("{pass}/{id}")]
        public async Task<ActionResult<Client>> DeleteChallengeClient(string pass, long id)
        {
            var ChallengeClient = await _context.Clients.FindAsync(id);
            if (ChallengeClient == null)
            {
                return NotFound();
            }

            _context.Operations.Add(new History{ Client = ChallengeClient.Id, Type = "RemoveClient", Date = DateTime.Now });
            await _context.SaveChangesAsync();

            if (Hasher.Verify(pass, ChallengeClient.Password)){
                _context.Clients.Remove(ChallengeClient);
                await _context.SaveChangesAsync();

                return ChallengeClient;
            }

            return Unauthorized();
        }

        private bool ChallengeClientExists(long id)
        {
            return _context.Clients.Any(e => e.Id == id);
        }
    }
}
