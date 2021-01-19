using Common;
using Common.API.Models;
using Common.Database;
using Common.Fixer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task2.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RatesController : ControllerBase
    {
        private readonly RatesContext context;
        private readonly IFixerClient api;

        public RatesController(RatesContext context, IFixerClient api)
        {
            this.context = context;
            this.api = api;
        }

        [HttpGet("{symbol}")]
        [ProducesResponseType(typeof(IDictionary<ulong, decimal>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(string symbol)
        {
            if (string.IsNullOrEmpty(symbol) || !context.Currencies.Any(c => c.Symbol == symbol))
            {
                return NotFound();
            }

            var currency = await context.Currencies
                .Include(c => c.Rates)
                .SingleOrDefaultAsync(c => c.Symbol == symbol);

            var rates = currency.Rates
                .ToLookup(r => r.Timestamp, r => r.Value)
                .ToDictionary(l => l.Key, l => l.First());

            return Ok(rates);
        }
    }
}

