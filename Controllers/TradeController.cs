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
    public class TradeController : Controller
    {
        private readonly ChallengeContext _context;

        public TradeController(ChallengeContext context)
        {
            _context = context;
        }


        // POST: api/trade
        [HttpPost]
        public async Task<ActionResult<Operation>> Get(TradeRequest request)
        {
            long id = request.ReceiverId;
            var ReceiverClient = await _context.Clients.FindAsync(id);

            id = request.SenderId;
            var ChallengeClient = await _context.Clients.FindAsync(id);

            if (ChallengeClient == null || ReceiverClient == null)
            {
                return NotFound();
            }

            ChallengeClient.Balance -= request.Value;
            ReceiverClient.Balance += request.Value;

            var operation = new Operation{
                    Sender = ChallengeClient.Id.ToString(),
                    Receiver = ReceiverClient.Id.ToString(),
                    Type = "Trade",
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
