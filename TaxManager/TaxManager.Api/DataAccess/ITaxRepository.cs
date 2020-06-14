using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaxManager.Api.Models;
using TaxManager.Core.Models;

namespace TaxManager.Api.DataAccess
{
    public interface ITaxRepository
    {
        Task<List<MunicipalityDto>> GetAllMunicipalitiesAsync();

        List<MunicipalityDto> GetAllMunicipalities();
        Task<MunicipalityDto> GetMunicipalityAsync(int i);
        Task<IEnumerable<TaxEntryDto>> GetMunicipalityTaxesForDate(string municipality, DateTime date);
    }
}