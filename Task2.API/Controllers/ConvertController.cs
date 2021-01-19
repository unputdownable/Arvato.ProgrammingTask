using Common.Database;
using Common.Database.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Task2.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ConvertController : ControllerBase
    {
        private readonly RatesContext context;

        public ConvertController(RatesContext context)
        {
            this.context = context;
        }


        [HttpGet]
        public async Task<IActionResult> Convert(string from, string to, decimal amount, DateTime? date = null)
        {
            if (from is null || to is null)
            {
                return BadRequest("One or more symbols missing");
            }

            var currencyFrom = await context.Currencies.Include(c => c.Rates).SingleOrDefaultAsync(c => c.Symbol == from);
            var currencyTo = await context.Currencies.Include(c => c.Rates).SingleOrDefaultAsync(c => c.Symbol == to);
            
            if (currencyFrom is null || currencyTo is null)
            {
                return BadRequest("One or more symbols unsupported");
            }

            Rate rateFrom;
            Rate rateTo;
            if (date is null)
            {
                rateFrom = currencyFrom.Rates.OrderBy(r => r.Date).LastOrDefault();
                rateTo = currencyTo.Rates.OrderBy(r => r.Date).LastOrDefault();
            }
            else
            {
                rateFrom = currencyFrom.Rates.SingleOrDefault(r => r.Date == date);
                rateTo = currencyFrom.Rates.SingleOrDefault(r => r.Date == date);
            }

            var convertedAmount = (amount / rateFrom?.Value) * rateTo?.Value;

            return Ok(convertedAmount);
        }

    }
}
