using Common;
using Common.API.Models;
using Common.Database;
using Common.Fixer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Task2.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ConvertController : ControllerBase
    {
        private readonly RatesContext context;
        private readonly IApiClient api;

        public ConvertController(RatesContext context, IApiClient api)
        {
            this.context = context;
            this.api = api;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ConvertResponse), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ErrorResponse))]
        public async Task<IActionResult> Convert(string from, string to, decimal amount, DateTime? date = null)
        {
            if (from is null || to is null)
            {
                return BadRequest(new ErrorResponse("One or more symbols missing"));
            }

            FixerResponse<Common.Fixer.Models.RatesResponse> response;
            if (date is null)
                response = await api.GetLatest(from, to);
            else
                response = await api.GetHistorical((DateTime)date, from, to);
            
            if (!response.Success)
                return BadRequest(new ErrorResponse(response.Error.Info));

            if (response.Content.Rates.Count != 2)
                return BadRequest(new ErrorResponse("One or more symbols not found"));


            var converter = new CurrencyConverter(response.Content.Rates);
            var convertResponse = new ConvertResponse
            {
                Date = response.Content.Date,
                From = new ConvertResponse.Amount
                { 
                    Currency = from.ToUpper(),
                    Value = amount 
                },
                To = new ConvertResponse.Amount
                { 
                    Currency = to.ToUpper(),
                    Value = converter.Convert(from, to, amount) 
                }
            };

            return Ok(convertResponse);
        }



    }
}

