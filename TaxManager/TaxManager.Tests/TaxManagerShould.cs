using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TaxManager.Api.DataAccess;
using TaxManager.Api.Domain;
using TaxManager.Api.Entities;
using TaxManager.Api.Models;
using Xunit;

namespace TaxManager.Tests
{
    public class TaxManagerShould
    {

        private readonly Api.Domain.TaxManager _taxManager;
        private readonly TestRepository _testRepository;
        private readonly IMapper _mapper;

        private readonly Mock<ITaxRepository> _taxRepositoryMock;
        private readonly List<Municipality> _municipalities;
        private readonly List<TaxEntry> _taxEntries;

        public TaxManagerShould()
        {

            _municipalities = new List<Municipality>
            {
                new Municipality (1,"Vilnius"),
                new Municipality (2,"Kaunas")

            };

            _taxEntries = new List<TaxEntry>
            {
                new TaxEntry(1, new DateTime(2016,1,1), new DateTime(2017,01,1), 1, TaxTypes.Yearly,0.2m),
                new TaxEntry(2, new DateTime(2016,5,1), new DateTime(2016,6,1), 1, TaxTypes.Monthly, 0.4m),
                new TaxEntry(3, new DateTime(2016,1,1), new DateTime(2016,1,2), 1, TaxTypes.Daily, 0.1m),
                new TaxEntry(4, new DateTime(2016,12,25), new DateTime(2016,12,26), 1, TaxTypes.Daily, 0.1m)
            };
            _testRepository = new TestRepository(_municipalities, _taxEntries, null);

            _taxRepositoryMock = new Mock<ITaxRepository>();
            _taxRepositoryMock.Setup(x => x.GetAllMunicipalities())
                .Returns(_municipalities);

            _taxManager = new Api.Domain.TaxManager(_taxRepositoryMock.Object, _mapper);
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
            var taxManager = new Api.Domain.TaxManager(_testRepository, null);

            var result = await taxManager.GetMunicipalityTaxForDateAsync(municipalityName, date);

            Assert.Equal(expectedTax, result.TaxApplied);
        }


    }
}
