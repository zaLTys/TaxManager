using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxManager.Api.DataAccess;
using TaxManager.Core.Models;
using Xunit;


namespace TaxManager.Tests
{
    public class TaxManagerShould
    {
        private readonly Api.Domain.TaxManager _taxManager;
        private readonly Mock<ITaxRepository> _taxRepositoryMock;
        private readonly List<MunicipalityDto> _municipalities;


        public TaxManagerShould()
        {
            _municipalities = new List<MunicipalityDto>
            {
                new MunicipalityDto (1,"Vilnius"),
                new MunicipalityDto (2,"Kaunas")

            };


            _taxRepositoryMock = new Mock<ITaxRepository>();
            _taxRepositoryMock.Setup(x => x.GetAllMunicipalities())
                .Returns(_municipalities);

            _taxManager = new Api.Domain.TaxManager(_taxRepositoryMock.Object);
        }

        [Fact]
        public async Task ShouldReturnAllMunicipalities()
        {
            var result = await _taxManager.GetMunicipalitiesAsync();
            Assert.Equal(2, result.ToList().Count);
        }

        [Fact]
        public void ShouldThrowExceptionIfRequestIsNull()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => _taxManager.GetMunicipalityTaxesForDate(null, null));

            Assert.Equal("request", exception.ParamName);
        }

        [Fact]
        public void ReturnNotFoundIfDataForNonExistingMunicipalityIsRequested()
        {

            Assert.Throws<NotImplementedException>(() => _taxManager.GetMunicipalityTaxesForDate("Manchester", "201.01.01"));
        }


    }
}
