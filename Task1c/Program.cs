using Common.Database;
using Common.Database.Models;
using Common.Fixer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Task1c
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var updateCurencies = args.SingleOrDefault(a => a == "-u");

            var context = new RatesContextFactory().CreateDbContext(args);
            var api = new ApiClient(new HttpClient());
            
            if (updateCurencies is not null)
            {
                var symbolsResponse = await api.GetSymbols();
                if (!symbolsResponse.Success)
                {
                    Console.WriteLine(symbolsResponse.Error.Info);
                    return;
                }

                await UpdateCurrencies(context, symbolsResponse.Content.Symbols);
                Console.WriteLine("Updated symbols");
            }

            var response = await api.GetLatest();
            if (!response.Success)
            {
                Console.WriteLine(response.Error.Info);
                return;
            }
            Console.WriteLine("Got latest rates");

            var currencies = await context.Currencies.ToListAsync();
            var rates = response.Content.Rates.Select(r => new Rate
            {
                Currency = currencies.SingleOrDefault(c => c.Symbol == r.Key) ?? new Currency { Symbol = r.Key },
                Date = response.Content.Date,
                Timestamp = response.Content.Timestamp,
                Value = r.Value
            });

            context.AddRange(rates);
            await context.SaveChangesAsync();
            Console.WriteLine(rates.Count() + " rates saved");
        }

        static async Task UpdateCurrencies(RatesContext context, IDictionary<string, string> symbols)
        {
            var currencies = await context.Currencies.ToListAsync();
            var toUpdate = new List<Currency>();
            var toAdd = new List<Currency>();
            foreach ((var symbol, var name) in symbols)
            {
                var currency = currencies.SingleOrDefault(c => c.Symbol == symbol);
                if (currency is null)
                {
                    currency = new Currency { Symbol = symbol, FullName = name };
                    toAdd.Add(currency);
                }
                else
                {
                    currency.FullName = name;
                    toUpdate.Add(currency);
                }
            }

            context.AddRange(toAdd);
            context.UpdateRange(toUpdate);
            await context.SaveChangesAsync();
        }
    }
}
