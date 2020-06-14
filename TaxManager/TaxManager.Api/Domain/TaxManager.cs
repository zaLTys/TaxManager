using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxManager.Api.DataAccess;
using TaxManager.Api.Models;
using TaxManager.Core.Models;

namespace TaxManager.Api.Domain
{
    public class TaxManager : ITaxManager
    {
        #region fields
        private readonly ITaxRepository _taxRepository;
        #endregion

        #region ctor
        public TaxManager(ITaxRepository taxRepository)
        {
            _taxRepository = taxRepository;
        }
        #endregion  


        #region methods

        public async Task<IEnumerable<MunicipalityDto>> GetMunicipalitiesAsync()
        {
            var result = await _taxRepository.GetAllMunicipalitiesAsync();
            return result;
        }

        public IEnumerable<MunicipalityDto> GetMunicipalities()
        {
            return _taxRepository.GetAllMunicipalities();
            
        }


        public async Task<ResultDto> GetMunicipalityTaxForDateAsync(string municipalityName, string date)
        {
            var dateAsDateTime = Convert.ToDateTime(date);
            var municipalityTaxesForDate =  await GetMunicipalityTaxesForDate(municipalityName, dateAsDateTime);
            if (municipalityTaxesForDate == null)
            {
                return null;
            }

            var result = GetTaxByPriority(municipalityTaxesForDate);
            if (result == null)
            {
                return null;
            }

            return new ResultDto(result.TaxValue);
        }

        private static TaxEntryDto GetTaxByPriority(IEnumerable<TaxEntryDto> municipalityTaxesForDate)
        {
            var municipalityTaxByPriority = municipalityTaxesForDate?.OrderByDescending(x => (int) x.TaxType).ToList();
            var result = municipalityTaxByPriority?[0];
            return result;
        }


        public async Task<IEnumerable<TaxEntryDto>> GetMunicipalityTaxesForDate(string municipalityName, DateTime date)
        {
            var municipality = await _taxRepository.GetMunicipalityAsync(municipalityName);
            if (municipality == null)
                return null;

            var taxEntries = await _taxRepository.GetTaxEntriesAsync(municipality.Id, date);
            
            var taxEntriesForDate = taxEntries.Where(x => date >= x.DateFrom && date < x.DateTo).ToList();
            return taxEntriesForDate;
        }

        #endregion
    }
}