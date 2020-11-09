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
    public class OperationController : Controller
    {
        private readonly ChallengeContext _context;

        public OperationController(ChallengeContext context)
        {
            _context = context;
        }

        // GET: api/history
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Operation>>> GetOperation()
        {
            return await _context.Operations.ToListAsync();
        }

        // GET: api/history
        [HttpGet("{pass}/{id}")]
        public async Task<ActionResult<IEnumerable<Operation>>> GetClientOperations(string pass, long id)
        {
            var ChallengeClient = await _context.Clients.FindAsync(id);
            if (Hasher.Verify(pass, ChallengeClient.Password))
                return await _context.Operations.Where((e) => e.Client == id.ToString()).ToListAsync();
            return Unauthorized();
        }

    }
}
