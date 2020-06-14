using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaxManager.Api.DataAccess;
using Xunit;

namespace TaxManager.Tests
{
    public class TaxRepositoryShould
    {
        private readonly ITaxRepository _taxRepository;

        public TaxRepositoryShould(ITaxRepository taxRepository)
        {
            _taxRepository = taxRepository;
        }



        [Fact]
        public void ReturnNotFoundIfDataForNonExistingMunicipalityIsRequested()
        {

            Assert.Throws<InvalidOperationException>(() =>_taxRepository.GetAllTaxEntriesForMunicipality(0));
        }
    }
}
