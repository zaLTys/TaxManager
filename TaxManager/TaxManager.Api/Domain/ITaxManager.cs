using System.Collections.Generic;
using System.Threading.Tasks;
using TaxManager.Api.Models;

namespace TaxManager.Api.Domain
{
    public interface ITaxManager
    {
        public Task<IEnumerable<MunicipalityDto>> GetMunicipalitiesAsync();
        public Task<ResultDto> GetMunicipalityTaxForDateAsync(string municipalityName, string date);
        public Task<TaxEntryDto> InsertTaxEntryAsync(string municipalityName, TaxEntryCreateDto taxEntry);
        
    }
}