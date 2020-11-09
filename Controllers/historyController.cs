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

        // GET: api/history
        [HttpGet]
        public async Task<ActionResult<IEnumerable<History>>> GetHistory()
        {
            return await _context.Operations.ToListAsync();
        }

    }
}
