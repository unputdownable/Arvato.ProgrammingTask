using Common;
using Common.Fixer;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;

namespace Task1a
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length != 3)
            {
                Console.WriteLine("Argument(s) missing (currency from, currency to, amount)");
                return;
            }
            var currencyFrom = args[0];
            var currencyTo = args[1];
            decimal amount;
            try
            {
                amount = Convert.ToDecimal(args[2]);
            }
            catch (Exception e)
            {
                Console.WriteLine("Could not parse amount: " + e.Message);
                return;
            }
            
            using var api = new ApiClient(new HttpClient());

            var response = await api.GetLatest(currencyFrom, currencyTo);
            if (!response.Success)
            {
                Console.WriteLine(response.Error.Info);
                return;
            }

            var latest = response.Content;
            if (latest.Rates.Count != 2)
            {
                Console.WriteLine("One or more symbols not valid");
                return;
            }
            Console.WriteLine("Got latest rates");

            var converter = new CurrencyConverter(latest.Rates);
            var convertedAmount = converter.Convert(currencyFrom, currencyTo, amount);
            Console.WriteLine($"{amount:0.00} {currencyFrom} is {convertedAmount:0.00} {currencyTo}");
            
            

        }
    }
}
