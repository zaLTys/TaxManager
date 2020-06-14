using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxManager.Api.DataAccess;
using TaxManager.Api.Domain;
using TaxManager.Core.Models;
using Xunit;

namespace TaxManager.Tests
{
    public class TaxManagerShould
    {

        private readonly Api.Domain.TaxManager _taxManager;
        private readonly TestRepository _testRepository;

        private readonly Mock<ITaxManager> _taxManagerMock;

        private readonly Mock<ITaxRepository> _taxRepositoryMock;
        private readonly List<MunicipalityDto> _municipalities;
        private readonly List<TaxEntryDto> _taxEntries;

        public TaxManagerShould()
        {
            _municipalities = new List<MunicipalityDto>
            {
                new MunicipalityDto (1,"Vilnius"),
                new MunicipalityDto (2,"Kaunas")

            };

            //[InlineData("Vilnius", "2016.01.01", 0.1)]
            //[InlineData("Vilnius", "2016.05.02", 0.4)]
            //[InlineData("Vilnius", "2016.07.10", 0.2)]
            //[InlineData("Vilnius", "2016.03.16", 0.2)]


            _taxEntries = new List<TaxEntryDto>
            {
                new TaxEntryDto(1, new DateTime(2016,1,1), new DateTime(2017,01,1), 1, TaxTypes.Yearly,0.2m),
                new TaxEntryDto(2, new DateTime(2016,5,1), new DateTime(2016,6,1), 1, TaxTypes.Monthly, 0.4m),
                new TaxEntryDto(3, new DateTime(2016,1,1), new DateTime(2016,1,2), 1, TaxTypes.Daily, 0.1m),
                new TaxEntryDto(4, new DateTime(2016,12,25), new DateTime(2016,12,26), 1, TaxTypes.Daily, 0.1m)
            };
            _testRepository = new TestRepository(_municipalities, _taxEntries);

            
            _taxManagerMock = new Mock<ITaxManager>();
            _taxRepositoryMock = new Mock<ITaxRepository>();
            _taxRepositoryMock.Setup(x => x.GetAllMunicipalities())
                .Returns(_municipalities);

            _taxManager = new Api.Domain.TaxManager(_taxRepositoryMock.Object);
        }

        [Fact]
        public void ShouldThrowExceptionIfInputIsIncorrect()
        {
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() => _taxManager.GetMunicipalityTaxForDateAsync("manchester", null));

            Assert.Equal("request", exception.Result.ParamName);
        }

        [Fact]
        public void ReturnNotFoundIfDataForNonExistingMunicipalityIsRequested()
        {

              Assert.ThrowsAsync<NotImplementedException>(() => _taxManager.GetMunicipalityTaxForDateAsync("Manchester", "201.01.01"));
        }

        [Theory]
        [InlineData("Vilnius", "2016.01.01", 0.1)]
        [InlineData("Vilnius", "2016.01.02", 0.2)]
        [InlineData("Vilnius", "2016.05.02", 0.4)]
        [InlineData("Vilnius", "2016.07.10", 0.2)]
        [InlineData("Vilnius", "2016.03.16", 0.2)]
        [InlineData("Vilnius", "2016.12.25", 0.1)]
        [InlineData("Vilnius", "2016.12.26", 0.2)]

        public async Task GetTaxForMunicipalityAndDate(string municipalityName, string date, decimal expectedTax)
        {
            var taxManager = new Api.Domain.TaxManager(_testRepository);

            var result = await taxManager.GetMunicipalityTaxForDateAsync(municipalityName, date);

            Assert.Equal(expectedTax, result.TaxApplied);
        }


    }
}
