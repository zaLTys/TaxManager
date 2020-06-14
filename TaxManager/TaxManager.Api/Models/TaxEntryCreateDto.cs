using System;

namespace TaxManager.Api.Models
{
    public class TaxEntryCreateDto
    {
        public TaxEntryCreateDto(DateTime dateFrom, DateTime dateTo, int municipalityId, TaxTypes taxType, decimal taxValue)
        {
            DateFrom = dateFrom;
            DateTo = dateTo;
            MunicipalityId = municipalityId;
            TaxType = taxType;
            TaxValue = taxValue;
        }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int MunicipalityId { get; set; }
        public TaxTypes TaxType { get; }
        public decimal TaxValue { get; set; }
    }
}