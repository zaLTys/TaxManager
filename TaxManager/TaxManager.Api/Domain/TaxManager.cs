using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaxManager.Api.DataAccess;
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



        public List<TaxEntryDto> GetMunicipalityTaxesForDate(string municipality, string date)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}