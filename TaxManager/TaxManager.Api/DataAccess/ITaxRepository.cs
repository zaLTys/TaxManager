using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaxManager.Core.Models;

namespace TaxManager.Api.DataAccess
{
    public interface ITaxRepository
    {
        public Task<IEnumerable<MunicipalityDto>> GetAllMunicipalitiesAsync();
        public IEnumerable<TaxEntryDto> GetAllTaxEntriesForMunicipality(int municipalityId);
        public IEnumerable<TaxEntryDto> GetTaxEntriesForMunicipalityByDate(int municipalityId, DateTime date);

        public IEnumerable<MunicipalityDto> GetAllMunicipalities();
    }
}