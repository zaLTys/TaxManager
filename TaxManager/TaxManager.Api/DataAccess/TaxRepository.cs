using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaxManager.Api.Contexts;
using TaxManager.Api.Entities;

namespace TaxManager.Api.DataAccess
{
    public class TaxRepository : ITaxRepository
    {
        private readonly TaxContext _context;

        public TaxRepository(TaxContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public async Task<List<Municipality>> GetAllMunicipalitiesAsync()
        {
            return await _context.Municipalities.OrderBy(c => c.Name).ToListAsync();
        }

        public List<Municipality> GetAllMunicipalities()
        {
            return _context.Municipalities.OrderBy(c => c.Name).ToList();
        }

        public async Task<Municipality> GetMunicipalityAsync(string municipalityName)
        {
            return await _context.Municipalities.SingleOrDefaultAsync(x => x.Name == municipalityName);
        }

        public async Task<IEnumerable<TaxEntry>> GetTaxEntriesAsync(int municipalityId, DateTime date)
        {
            var municipalityFound = await _context.Municipalities.SingleOrDefaultAsync(x => x.Id == municipalityId);
            if (municipalityFound == null)
                return null;
            var taxEntriesForDate = await _context.TaxEntries.Where(x => x.DateFrom <= date && x.DateTo >= date).ToListAsync();
            return taxEntriesForDate;
        }
        
    }}

    
