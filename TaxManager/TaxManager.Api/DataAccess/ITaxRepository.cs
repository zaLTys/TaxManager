using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaxManager.Api.Entities;
using TaxManager.Api.Models;

namespace TaxManager.Api.DataAccess
{
    public interface ITaxRepository
    {
        Task<List<Municipality>> GetAllMunicipalitiesAsync();
        List<Municipality> GetAllMunicipalities();
        Task<Municipality> GetMunicipalityAsync(string municipalityName);
        Task<IEnumerable<TaxEntry>> GetTaxEntriesAsync(int municipalityId, DateTime date);
        Task<TaxEntry> InsertTaxEntryAsync(TaxEntry taxEntryToInsert);
    }
}