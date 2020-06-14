using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaxManager.Api.Contexts;
using TaxManager.Api.Entities;
using TaxManager.Api.Models;

namespace TaxManager.Api.DataAccess
{
    public class TestRepository : ITaxRepository
    {
        private readonly List<Municipality> _municipalities;
        private readonly List<TaxEntry> _taxEntries;


        public TestRepository(List<Municipality> municipalities, List<TaxEntry> taxEntries, IMapper mapper)
        {

            _municipalities = municipalities;
            _taxEntries = taxEntries;
        }


        public async Task<List<Municipality>> GetAllMunicipalitiesAsync()
        {
            return await new Task<List<Municipality>>(GetAllMunicipalities);
        }

        public List<Municipality> GetAllMunicipalities()
        {
            return _municipalities;
        }

        public async Task<Municipality> GetMunicipalityAsync(string municipalityName)
        {
            var result = _municipalities.SingleOrDefault(x => x.Name == municipalityName);
            return result;

        }

        public async Task<IEnumerable<TaxEntry>> GetTaxEntriesAsync(int municipalityId, DateTime date)
        {
            var municipalityFound = _municipalities.SingleOrDefault(x => x.Id == municipalityId);
            if (municipalityFound == null)
                return null;
            var taxEntriesForDate = _taxEntries.Where(x => x.DateFrom <= date && x.DateTo >= date).ToList();

            return taxEntriesForDate;
        }



    }}

    
