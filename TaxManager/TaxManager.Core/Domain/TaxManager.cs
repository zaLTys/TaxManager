using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaxManager.Core.DataAccess;
using TaxManager.Core.Models;

namespace TaxManager.Core.Domain
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

        public async Task<List<Municipality>> GetMunicipalities()
        {
            return await _taxRepository.GetAllMunicipalities();
        }

        public List<TaxEntry> GetMunicipalityTaxesForDate(string municipality, string date)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}