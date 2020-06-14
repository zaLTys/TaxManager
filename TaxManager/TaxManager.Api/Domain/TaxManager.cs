using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TaxManager.Api.DataAccess;
using TaxManager.Api.Entities;
using TaxManager.Api.Models;

namespace TaxManager.Api.Domain
{
    public class TaxManager : ITaxManager
    {
        private readonly ITaxRepository _taxRepository;
        private readonly IMapper _mapper;
        public TaxManager(ITaxRepository taxRepository, IMapper mapper)
        {
            _taxRepository = taxRepository ??
                             throw new ArgumentNullException(nameof(_taxRepository)); ;
            _mapper = mapper ??
                      throw new ArgumentNullException(nameof(_mapper)); ;
        }


        public async Task<IEnumerable<MunicipalityDto>> GetMunicipalitiesAsync()
        {
            var result = await _taxRepository.GetAllMunicipalitiesAsync();

            return _mapper.Map<IEnumerable<MunicipalityDto>>(result); ;

        }

        public async Task<ResultDto> GetMunicipalityTaxForDateAsync(string municipalityName, string date)
        {

            var result = new ResultDto(0, null); //nesvaru
            if (!municipalityName.Any() || date == null)
            {
                result.ErrorMessage += "Incorrect input";
                return result;
            }

            var dateAsDateTime = Convert.ToDateTime(date);
            
            var municipalityTaxesForDate = await GetMunicipalityTaxesForDate(municipalityName, dateAsDateTime);
            if (municipalityTaxesForDate == null)
            {
                result.ErrorMessage += $"No tax entries found for {municipalityName}";
                return result;
            }

            var appliedTaxEntry = GetTaxByPriority(municipalityTaxesForDate);

            if (result.ErrorMessage == null && appliedTaxEntry !=null)
            {
                result.TaxApplied = appliedTaxEntry.TaxValue;
            }

            return result;
        }

        public async Task<TaxEntryDto> InsertTaxEntryAsync(string municipalityName, TaxEntryCreateDto taxEntry)
        {
            var municipality = _taxRepository.GetMunicipalityAsync(municipalityName);
            if (municipality == null)
            {
                
            }

            var entity = _mapper.Map<TaxEntry>(taxEntry);
            return _mapper.Map<TaxEntryDto>(await _taxRepository.InsertTaxEntryAsync(entity)); 

        }

        private static TaxEntryDto GetTaxByPriority(IEnumerable<TaxEntryDto> municipalityTaxesForDate)
        {
            var taxes = municipalityTaxesForDate.ToList();
            if (!taxes.Any())
            {
                return null;
            }
            var municipalityTaxByPriority = taxes?.OrderByDescending(x => (int)x.TaxType).ToList();
            //to fix null ref
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

        public async Task<TaxEntryDto> InsertTaxEntryAsync(TaxEntryCreateDto taxEntryToInsert)
        {
            var entity = _mapper.Map<TaxEntry>(taxEntryToInsert);
            var insertedTaxEntry = await _taxRepository.InsertTaxEntryAsync(entity);
            return _mapper.Map<TaxEntryDto>(insertedTaxEntry); ;
        }
    }
}