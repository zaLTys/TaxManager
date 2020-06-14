using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaxManager.Api.DataAccess;
using TaxManager.Api.Domain;
using TaxManager.Api.Models;
using TaxManager.Core.Models;
using Xunit;

namespace TaxManager.Tests
{
    public class TaxManagerShould
    {

        private readonly Api.Domain.TaxManager _taxManager;
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

            _taxEntries = new List<TaxEntryDto>
            {
                new TaxEntryDto(1, DateTime.Today, DateTime.Today.AddDays(1), 1, TaxTypes.Daily),
                new TaxEntryDto(2, DateTime.Today, DateTime.Today.AddDays(1), 1, TaxTypes.Daily),
                new TaxEntryDto(3, DateTime.Today, DateTime.Today.AddDays(1), 1, TaxTypes.Daily),
                new TaxEntryDto(4, DateTime.Today, DateTime.Today.AddDays(1), 1, TaxTypes.Daily)
            };

            
            _taxManagerMock = new Mock<ITaxManager>();
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
        public void ShouldThrowExceptionIfInputIsIncorrect()
        {
            var exception = Assert.ThrowsAsync<ArgumentNullException>(() => _taxManager.GetMunicipalityTaxForDate("manchester", null));

            Assert.Equal("request", exception.Result.ParamName);
        }

        [Fact]
        public void ReturnNotFoundIfDataForNonExistingMunicipalityIsRequested()
        {

            Assert.ThrowsAsync<NotImplementedException>(() => _taxManager.GetMunicipalityTaxForDate("Manchester", "201.01.01"));
        }

        [Theory]
        [InlineData("Vilnius", "2016.01.01", 0.1)]
        [InlineData("Vilnius", "2016.05.02", 0.4)]
        [InlineData("Vilnius", "2016.07.10", 0.2)]
        [InlineData("Vilnius", "2016.03.16", 0.2)]

        public void GetTaxForMunicipalityAndDate(string municipalityName, string date, decimal expectedTax)
        {
            var sut = _taxManagerMock;
            sut.Setup(x => x.GetMunicipalityTaxForDate(municipalityName, date))
                .Returns(Task.FromResult<ResultDto>(new ResultDto(){TaxApplied = 0}));
                    //_taxEntries.Where(x => x.MunicipalityId == _municipalities.SingleOrDefault(y => y.Name == municipalityName).Id).SingleOrDefault());


        }


    }
}
