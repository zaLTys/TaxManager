using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using TaxManager.Core.DataAccess;
using Xunit;
using TaxManager.Core.Domain;
using TaxManager.Core.Models;


namespace TaxManager.Tests
{
    public class TaxManagerShould
    {
        private readonly Core.Domain.TaxManager _taxManager;
        private readonly Mock<ITaxRepository> _taxRepositoryMock;
        private readonly List<Municipality> _municipalities;


        public TaxManagerShould()
        {
            _municipalities = new List<Municipality>
            {
                new Municipality (1,"Vilnius"),
                new Municipality (2,"Kaunas")

            };


            _taxRepositoryMock = new Mock<ITaxRepository>();
            _taxRepositoryMock.Setup(x => x.GetAllMunicipalities())
                .Returns(_municipalities);

            _taxManager = new Core.Domain.TaxManager(_taxRepositoryMock.Object);
        }

        [Fact]
        public void ShouldReturnAllMunicipalities()
        {
            _taxManager.
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
