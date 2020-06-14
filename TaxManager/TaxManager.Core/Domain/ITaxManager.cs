using System.Collections.Generic;
using TaxManager.Core.Models;

namespace TaxManager.Core.Domain
{
    public interface ITaxManager
    {
        public List<Municipality> GetMunicipalities();
        public List<TaxEntry> GetMunicipalityTaxesForDate(string municipality, string date);
        
    }
}