using System;

namespace TaxManager.Core.Models
{
    public class TaxEntryDto
    {
        public TaxEntryDto(int id, DateTime dateFrom, DateTime dateTo, int municipalityId, TaxTypes taxType, decimal taxValue)
        {
            Id = id;
            DateFrom = dateFrom;
            DateTo = dateTo;
            MunicipalityId = municipalityId;
            TaxType = taxType;
            TaxValue = taxValue;
        }
        public int Id { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int MunicipalityId { get; set; }
        public TaxTypes TaxType { get; }
        public decimal TaxValue { get; set; }
    }
}