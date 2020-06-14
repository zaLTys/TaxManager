using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaxManager.Api.Contexts;
using TaxManager.Core.Models;

namespace TaxManager.Api.DataAccess
{
    public class TestRepository : ITaxRepository
    {
        private readonly List<MunicipalityDto> _municipalities;
        private readonly List<TaxEntryDto> _taxEntries;


        public TestRepository(List<MunicipalityDto> municipalities, List<TaxEntryDto> taxEntries)
        {
            _municipalities = municipalities;

            _taxEntries = taxEntries;
        }


        public async Task<List<MunicipalityDto>> GetAllMunicipalitiesAsync()
        {
            return await new Task<List<MunicipalityDto>>(GetAllMunicipalities);
        }

        public List<MunicipalityDto> GetAllMunicipalities()
        {
            return _municipalities;
        }

        public async Task<MunicipalityDto> GetMunicipalityAsync(string municipalityName)
        {
            return _municipalities.SingleOrDefault(x => x.Name == municipalityName);
        }

        public async Task<IEnumerable<TaxEntryDto>> GetTaxEntriesAsync(int municipalityId, DateTime date)
        {
            var municipalityFound = _municipalities.SingleOrDefault(x => x.Id == municipalityId);
            if (municipalityFound == null)
                return null;
            var taxEntriesForDate = _taxEntries.Where(x => x.DateFrom <= date && x.DateTo >= date).ToList();
            return taxEntriesForDate;
        }



    }}

    
