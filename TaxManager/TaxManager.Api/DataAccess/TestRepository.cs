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


        public TestRepository()
        {
            _municipalities = new List<MunicipalityDto>
            {
                new MunicipalityDto (1,"Vilnius"),
                new MunicipalityDto (2,"Kaunas")

            };

            _taxEntries = new List<TaxEntryDto>
            {
                new TaxEntryDto(1, DateTime.Today, DateTime.Today.AddDays(1), 1, TaxTypes.Daily),
                new TaxEntryDto(2, DateTime.Today, DateTime.Today.AddDays(1), 1, TaxTypes.Daily),
                new TaxEntryDto(3, DateTime.Today, DateTime.Today.AddDays(1), 1, TaxTypes.Daily),
                new TaxEntryDto(4, DateTime.Today, DateTime.Today.AddDays(1), 1, TaxTypes.Daily)
            };
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
            return await new Task<MunicipalityDto>(() => GetMunicipality(municipalityName));
        }

        public async Task<IEnumerable<TaxEntryDto>> GetMunicipalityTaxesForDate(string municipalityName, DateTime date)
        {
            var municipalityFound = _municipalities.SingleOrDefault(x => x.Name == municipalityName);
            if (municipalityFound == null)
                return null;
            var taxEntriesForDate = _taxEntries.Where(x => x.DateFrom <= date && x.DateTo >= date).ToList();
            return taxEntriesForDate;
        }

        public MunicipalityDto GetMunicipality(string municipalityName)
        {
            return new MunicipalityDto(1, "Vilnius");
        }

    }}

    
