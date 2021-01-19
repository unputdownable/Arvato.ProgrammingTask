using Common;
using Common.Fixer;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using System.Globalization;
using Common.Fixer.Models;
using System.Threading;

namespace Task1b
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            if (args.Length < 3)
            {
                Console.WriteLine("Argument(s) missing (currency from, currency to, amount)");
                return;
            }
            var currencyFrom = args[0]?.ToUpper();
            var currencyTo = args[1]?.ToUpper();
            decimal amount;
            DateTime? date = null;
            try
            {
                amount = Convert.ToDecimal(args[2]);
                if (args.Length == 4)
                {
                    date = Convert.ToDateTime(args[3]);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            
            using var api = new FixerClient(new HttpClient());

            FixerResponse<RatesResponse> response;
            if (date is not null)
                response = await api.GetHistorical((DateTime)date, currencyFrom, currencyTo);
            else
                response = await api.GetLatest(currencyFrom, currencyTo);


            if (!response.Success)
            {
                Console.WriteLine(response.Error.Info);
                return;
            }

            if (response.Content.Rates.Count != 2)
            {
                Console.WriteLine("One or more currency symbols not valid");
                return;
            }
            
            if (date is not null) 
                Console.WriteLine($"Got rates from {(DateTime)date:yyyy-MM-dd}");
            else
                Console.WriteLine("Got latest rates");

            var converter = new CurrencyConverter(response.Content.Rates);
            var convertedAmount = converter.Convert(currencyFrom, currencyTo, amount);

            Console.WriteLine($"{amount:0.00} {currencyFrom} {(date.HasValue ? "was" : "is")} {convertedAmount:0.00} {currencyTo}");
        }
    }
}
