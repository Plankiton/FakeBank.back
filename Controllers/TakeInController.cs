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
    public class TakeInController : Controller
    {
        private readonly ChallengeContext _context;

        public TakeInController(ChallengeContext context)
        {
            _context = context;
        }


        // POST: api/takein
        [HttpPost]
        public async Task<ActionResult<Operation>> Get(GenericRequest request)
        {
            var ChallengeClient = await _context.Clients.FindAsync(request.ClientId);

            if (ChallengeClient == null)
            {
                return NotFound();
            }

            ChallengeClient.Balance += request.Value;

            var operation = new Operation{
                    Client = ChallengeClient.Id.ToString(),
                    Type = "TakeIn",
                    Value = request.Value.ToString(),
                    Date = DateTime.Now };
            _context.Operations.Add(operation);

            if (Hasher.Verify(request.Password, ChallengeClient.Password)){
                await _context.SaveChangesAsync();
                return operation;
            }

            return Unauthorized();
        }

    }
}
