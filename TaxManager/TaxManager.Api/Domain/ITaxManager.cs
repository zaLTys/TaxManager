using System.Collections.Generic;
using System.Threading.Tasks;
using TaxManager.Core.Models;

namespace TaxManager.Api.Domain
{
    public interface ITaxManager
    {
        public Task<IEnumerable<MunicipalityDto>> GetMunicipalitiesAsync();
        public List<TaxEntryDto> GetMunicipalityTaxesForDate(string municipality, string date);
        
    }
}