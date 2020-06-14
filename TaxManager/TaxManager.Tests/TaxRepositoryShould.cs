using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TaxManager.Api.DataAccess;
using TaxManager.Core.Models;
using Xunit;

namespace TaxManager.Tests
{
    public class RaxRepositoryShould
    {

        private readonly Mock<ITaxRepository> _taxRepositoryMock;
        private readonly List<MunicipalityDto> _municipalities;


        public RaxRepositoryShould()
        {
            _municipalities = new List<MunicipalityDto>
            {
                new MunicipalityDto (1,"Vilnius"),
                new MunicipalityDto (2,"Kaunas")

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
                .Returns(Task.FromResult(_municipalities.SingleOrDefault(x => x.Name == municipalityName)));

            var result = _taxRepositoryMock.Object.GetMunicipalityAsync(municipalityName).Result;
            Assert.Equal(expectedId, result.Id);
        }


    }
}
