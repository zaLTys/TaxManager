using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaxManager.Api.Contexts;
using TaxManager.Core.Models;

namespace TaxManager.Api.DataAccess
{
    public class TaxRepository : ITaxRepository
    {
        private readonly TaxRepositoryContext _context;

        public TaxRepository(TaxRepositoryContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public async Task<List<MunicipalityDto>> GetAllMunicipalitiesAsync()
        {
            return await new Task<List<MunicipalityDto>>(GetAllMunicipalities);
        }

        public List<MunicipalityDto> GetAllMunicipalities()
        {
            return _context.Municipalities.OrderBy(c => c.Name).ToList();
        }

        public async Task<MunicipalityDto> GetMunicipalityAsync(int id)
        {
            return await new Task<MunicipalityDto>(() => GetMunicipality(id));
        }

        public async Task<IEnumerable<TaxEntryDto>> GetMunicipalityTaxesForDate(string municipality, DateTime date)
        {
            var municipalityFound = await _context.Municipalities.SingleOrDefaultAsync(x => x.Name == municipality);
            if (municipalityFound == null)
                return null;
            var taxEntriesForDate = await _context.TaxEntries.Where(x => x.DateFrom <= date && x.DateTo >= date).ToListAsync();
            return taxEntriesForDate;
        }

        public MunicipalityDto GetMunicipality(int municipalityId)
        {
            return new MunicipalityDto(1, "Vilnius");
        }




    }}

    
