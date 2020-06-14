using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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


        public async Task<IEnumerable<MunicipalityDto>> GetAllMunicipalitiesAsync()
        {
            return await new Task<IEnumerable<MunicipalityDto>>(GetAllMunicipalities);
        }

        public IEnumerable<MunicipalityDto> GetAllMunicipalities()
        {
            return _context.Municipalities.OrderBy(c => c.Name).ToList();
        }

        public async Task<IEnumerable<TaxEntryDto>> GetAllTaxEntriesForMunicipalityAsync(int municipalityId)
        {
            return await new Task<IEnumerable<TaxEntryDto>>(() =>GetAllTaxEntriesForMunicipality(municipalityId));
        }

        public IEnumerable<TaxEntryDto> GetAllTaxEntriesForMunicipality(int municipalityId)
        {
            return new List<TaxEntryDto>();
        }

        public async Task<IEnumerable<TaxEntryDto>> GetAllTaxEntriesForMunicipalityAsync(int municipalityId, DateTime date)
        {
            return await new Task<IEnumerable<TaxEntryDto>>(() => GetTaxEntriesForMunicipalityByDate(municipalityId, date));
        }


        public IEnumerable<TaxEntryDto> GetTaxEntriesForMunicipalityByDate(int municipalityId, DateTime date)
        {
            return new List<TaxEntryDto>();
        }


    }}

    
