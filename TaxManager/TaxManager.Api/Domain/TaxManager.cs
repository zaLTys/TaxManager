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



        public async Task<ResultDto> GetMunicipalityTaxForDate(string municipality, string date)
        {
            var municipalityTaxesForDate =  await _taxRepository.GetMunicipalityTaxesForDate(municipality, date);

            var municipalityTaxByPriority = municipalityTaxesForDate?.OrderBy(x => (int) x.TaxType).FirstOrDefault();
            if (municipalityTaxByPriority == null)
            {
                return null;
            }

            return new ResultDto(municipalityTaxByPriority.TaxValue);
        }
        #endregion
    }
}