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
    public class TakeOutController : Controller
    {
        private readonly ChallengeContext _context;

        public TakeOutController(ChallengeContext context)
        {
            _context = context;
        }


        // POST: api/takeout
        [HttpPost]
        public async Task<ActionResult<Client>> Get(GenericRequest request)
        {
            var ChallengeClient = await _context.Clients.FindAsync(request.ClientId);

            if (ChallengeClient == null)
            {
                return NotFound();
            }

            ChallengeClient.Balance -= request.Value;

            _context.Operations.Add(new History{
                    Client = ChallengeClient.Id,
                    Type = "TakeOut",
                    Value = request.Value.ToString(),
                    Date = DateTime.Now });
            await _context.SaveChangesAsync();

            if (Hasher.Verify(request.Password, ChallengeClient.Password))
                return ChallengeClient;
            return Unauthorized();
        }

    }
}
