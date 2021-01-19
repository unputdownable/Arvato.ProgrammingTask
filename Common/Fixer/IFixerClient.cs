using Common.Fixer.Models;
using System;
using System.Threading.Tasks;

namespace Common.Fixer
{
    public interface IFixerClient
    {
        public Task<FixerResponse<SymbolsResponse>> GetSymbols();
        public Task<FixerResponse<RatesResponse>> GetHistorical(DateTime date, params string[] symbols);
        public Task<FixerResponse<RatesResponse>> GetLatest(params string[] symbols);
    }
}
