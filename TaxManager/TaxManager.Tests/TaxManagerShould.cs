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
using TaxManager.Api.Profiles;
using Xunit;

namespace TaxManager.Tests
{
    public class TaxManagerShould
    {

        private readonly IMapper _mapper;
        private readonly Api.Domain.TaxManager _taxManager;
        private readonly Mock<ITaxRepository> _taxRepositoryMock;
        private readonly TestRepository _testRepository;
        private readonly List<Municipality> _municipalities;
        private readonly List<TaxEntry> _taxEntries;

        public TaxManagerShould()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new TaxEntryProfiler());
            });
            _mapper = config.CreateMapper();

            #region CreateTestData
            _municipalities = new List<Municipality>
            {
                new Municipality (1,"Vilnius"),
                new Municipality (2,"Kaunas")

            };

            _taxEntries = new List<TaxEntry>
            {
                new TaxEntry(1, new DateTime(2016,1,1), new DateTime(2017,01,1), 1, (int)TaxTypes.Yearly,0.2m),
                new TaxEntry(2, new DateTime(2016,5,1), new DateTime(2016,6,1), 1, (int)TaxTypes.Monthly, 0.4m),
                new TaxEntry(3, new DateTime(2016,1,1), new DateTime(2016,1,2), 1, (int)TaxTypes.Daily, 0.1m),
                new TaxEntry(4, new DateTime(2016,12,25), new DateTime(2016,12,26), 1, (int)TaxTypes.Daily, 0.1m)
            };
            #endregion

            _taxRepositoryMock = new Mock<ITaxRepository>();
            _taxRepositoryMock.Setup(x => x.GetAllMunicipalities())
                .Returns(_municipalities);

            _testRepository= new TestRepository(_municipalities, _taxEntries, _mapper);
            _taxManager = new Api.Domain.TaxManager(_taxRepositoryMock.Object, _mapper);
        }

        [Fact]
        public async Task ReturnErrorMessageIfInputIsIncorrect()
        {
            var result = await _taxManager.GetMunicipalityTaxForDateAsync("", null);
            Assert.Equal("Incorrect input", result.ErrorMessage);
        }


        [Fact]
        public async Task ReturnErrorMessageIfDataNotFound()
        {
            var result = await _taxManager.GetMunicipalityTaxForDateAsync("Manchester", "2020.01.01");
            Assert.Equal("No tax entries found for Manchester", result.ErrorMessage);
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
            var taxManager = new Api.Domain.TaxManager(_testRepository, _mapper);
            var result = await taxManager.GetMunicipalityTaxForDateAsync(municipalityName, date);

            AssertResult(expectedTax, result);
        }



        [Theory]
        [InlineData("2016.04.04", "2016.04.05",1,TaxTypes.Daily, 0.5, "Vilnius")]
        [InlineData("2016.03.03", "2016.03.04",1,TaxTypes.Daily, 0.7, "Vilnius")]
        public async Task BeAbleToAddTaxEntry(string dateFrom, string dateTo, int municipalityId, TaxTypes taxType, decimal taxApplied, string municipalityName )
        {
            var dateFromAsDate = Convert.ToDateTime(dateFrom);
            var dateToAsDate = Convert.ToDateTime(dateTo);
            var taxEntryToInsert = new TaxEntryCreateDto(dateFromAsDate, dateToAsDate, municipalityId, (int)taxType, taxApplied);
            var taxManager = new Api.Domain.TaxManager(_testRepository, _mapper);

            var result = await taxManager.InsertTaxEntryAsync(taxEntryToInsert);

            Assert.Equal(taxEntryToInsert.TaxType, result.TaxType);
            Assert.Equal(taxEntryToInsert.DateFrom, result.DateFrom);
            Assert.Equal(taxEntryToInsert.DateTo, result.DateTo);
            Assert.Equal(taxEntryToInsert.MunicipalityId, result.MunicipalityId);
            Assert.Equal(taxEntryToInsert.TaxValue, result.TaxValue);
            Assert.NotNull(result.Id);

            var check = await taxManager.GetMunicipalityTaxForDateAsync(municipalityName, dateFrom);

            AssertResult(taxEntryToInsert.TaxValue, check);

        }

        private static void AssertResult(decimal expectedTax, ResultDto result)
        {
            Assert.Equal(expectedTax, result.TaxApplied);
            Assert.Null(result.ErrorMessage);
        }

    }
}
