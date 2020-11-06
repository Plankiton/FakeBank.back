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
    public class HistoryController : Controller
    {
        private readonly ChallengeContext _context;

        public HistoryController(ChallengeContext context)
        {
            _context = context;
        }

        // GET: api/client/5/history
        [HttpGet]
        public async Task<ActionResult<IEnumerable<History>>> GetClientHistory(string pass, long id)
        {
            var query = _context.Operations.Where(e => e.Client.Id == id);
            var history = new List<History>(query.Count());

            foreach (History h in query)
            {
                history.Add(h);
            }

            return CreatedAtAction("GetClientHistory", new { id = 0 }, history);
        }

    }
}
