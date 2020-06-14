using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using TaxManager.Api.DataAccess;
using TaxManager.Api.Entities;
using TaxManager.Api.Models;
using Xunit;

namespace TaxManager.Tests
{
    public class RaxRepositoryShould
    {

        private readonly Mock<ITaxRepository> _taxRepositoryMock;
        private readonly List<Municipality> _municipalities;


        public RaxRepositoryShould()
        {
            _municipalities = new List<Municipality>
            {
                new Municipality (1,"Vilnius"),
                new Municipality (2,"Kaunas")
            };

            
            _taxRepositoryMock = new Mock<ITaxRepository>();
            

        }

        [Fact]
        public void ShouldReturnAllMunicipalities()
        {
            _taxRepositoryMock
                .Setup(x => x.GetAllMunicipalities()).Returns(_municipalities);

            var result = _taxRepositoryMock.Object.GetAllMunicipalities();
            var results = result.ToList();
            Assert.Equal(2, results.Count);
            Assert.True(results.Exists(x => x.Name == "Vilnius" && x.Id == 1));
            Assert.True(results.Exists(x => x.Name == "Kaunas" && x.Id == 2));
        }

        [Fact]
        public void ShouldReturnAllMunicipalitiesAsync()
        {
            _taxRepositoryMock
                .Setup(x => x.GetAllMunicipalitiesAsync())
                .Returns(Task.FromResult(_municipalities));

            var result = _taxRepositoryMock.Object.GetAllMunicipalitiesAsync().Result;
            var results = result.ToList();
            Assert.Equal(2, results.Count);
            Assert.True(results.Exists(x => x.Name == "Vilnius" && x.Id == 1));
            Assert.True(results.Exists(x => x.Name == "Kaunas" && x.Id == 2));
        }


        [Theory]
        [InlineData(1, "Vilnius")]
        [InlineData(2, "Kaunas")]
        public void ReturnSingleMunicipalityById(int expectedId, string municipalityName)
        {
            _taxRepositoryMock
                .Setup(x => x.GetMunicipalityAsync(municipalityName))
                .Returns(Task.FromResult(_municipalities.SingleOrDefault()));

            var result = _taxRepositoryMock.Object.GetMunicipalityAsync(municipalityName).Result;
            Assert.Equal(expectedId, result.Id);
        }


    }
}
