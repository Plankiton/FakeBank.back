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

            if (Hasher.Verify(pass, ChallengeClient.Password))
                return ChallengeClient;
            return Unauthorized();
        }

        // PUT: api/Challenge/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChallengeClient(long id, Client ChallengeClient)
        {
            if (id != ChallengeClient.Id)
            {
                return BadRequest();
            }

            _context.Entry(ChallengeClient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChallengeClientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Challenge
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Client>> PostChallengeClient(Client ChallengeClient)
        {

            ChallengeClient.Password = Hasher.Hash(ChallengeClient.Password);
            _context.Clients.Add(ChallengeClient);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChallengeClient", new { id = ChallengeClient.Id }, ChallengeClient);
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
