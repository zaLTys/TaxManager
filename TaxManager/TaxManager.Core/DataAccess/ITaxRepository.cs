using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaxManager.Core.Models;

namespace TaxManager.Core.DataAccess
{
    public interface ITaxRepository
    {
        public Task<IEnumerable<Municipality>> GetAllMunicipalities();
        public IEnumerable<TaxEntry> GetAllTaxEntriesForMunicipality(int municipalityId);
        public IEnumerable<TaxEntry> GetTaxEntriesForMunicipalityByDate(int municipalityId, DateTime date);

    }
}