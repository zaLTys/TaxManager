using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TaxManager.Api.DataAccess;
using TaxManager.Api.Models;

namespace TaxManager.Api.Domain
{
    public class TaxManager : ITaxManager
    {
        private readonly ITaxRepository _taxRepository;
        private readonly IMapper _mapper;
        public TaxManager(ITaxRepository taxRepository, IMapper mapper)
        {
            _taxRepository = taxRepository;
            _mapper = mapper;
        }


        public async Task<IEnumerable<MunicipalityDto>> GetMunicipalitiesAsync()
        {
            var result = await _taxRepository.GetAllMunicipalitiesAsync();

            return _mapper.Map<IEnumerable<MunicipalityDto>>(result); ;

        }

        public async Task<ResultDto> GetMunicipalityTaxForDateAsync(string municipalityName, string date)
        {
            var dateAsDateTime = Convert.ToDateTime(date);


            var result = new ResultDto(0,"");
            var municipalityTaxesForDate = await GetMunicipalityTaxesForDate(municipalityName, dateAsDateTime);
            if (municipalityTaxesForDate == null)
            {
                result.ErrorMessage += $"No tax entries found for {municipalityName}";
            }

            var appliedTaxEntry = GetTaxByPriority(municipalityTaxesForDate);

            if (result.ErrorMessage == "")
            {
                result.TaxApplied = appliedTaxEntry.TaxValue;
            }

            return result;
        }

        private static TaxEntryDto GetTaxByPriority(IEnumerable<TaxEntryDto> municipalityTaxesForDate)
        {
            var municipalityTaxByPriority = municipalityTaxesForDate?.OrderByDescending(x => (int)x.TaxType).ToList();
            return municipalityTaxByPriority?[0];
        }


        public async Task<IEnumerable<TaxEntryDto>> GetMunicipalityTaxesForDate(string municipalityName, DateTime date)
        {
            var municipality = await _taxRepository.GetMunicipalityAsync(municipalityName);
            if (municipality == null)
            {
                return null;
            }
                
            var taxEntries = await _taxRepository.GetTaxEntriesAsync(municipality.Id, date);

            var taxEntriesForDate = taxEntries.Where(x => date >= x.DateFrom && date < x.DateTo && x.MunicipalityId == municipality.Id).ToList();
            return  _mapper.Map<IEnumerable<TaxEntryDto>>(taxEntriesForDate); ;
        }

    }
}