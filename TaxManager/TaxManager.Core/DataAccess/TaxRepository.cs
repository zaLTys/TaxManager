using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaxManager.Core.Models;

namespace TaxManager.Core.DataAccess
{
    public class TaxRepository : ITaxRepository
    {
        public Task<List<Municipality>> GetAllMunicipalitiesAsync()
        {
            var result = await _context.PathResult.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            return result.ToApiModel(true);
            return new List<Municipality>();
        }

        public List<Municipality> GetAllMunicipalities()
        {
            
        }

        public List<TaxEntry> GetAllTaxEntriesForMunicipality(int municipalityId)
        {
            return new List<TaxEntry>();
        }

        public List<TaxEntry> GetTaxEntriesForMunicipalityByDate(int municipalityId, DateTime date)
        {
            return new List<TaxEntry>();
        }


    }}

    
