using Common.LatestRate;
using Newtonsoft.Json;
using System;
using System.IO;

namespace Task1a
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var dummyResponse = File.ReadAllText("response.json");

            var res = JsonConvert.DeserializeObject<LatestRate>(dummyResponse);


            Console.ReadKey();
        }
    }
}
