using System;

namespace TaxManager.Api.Models
{
    public class TaxEntryDto : TaxEntryCreateDto
    {
        public TaxEntryDto(int id, DateTime dateFrom, DateTime dateTo, int municipalityId, TaxTypes taxType, decimal taxValue) 
            : base(dateFrom, dateTo, municipalityId, taxType, taxValue)
        {
            Id = id;
        }
        public int Id { get; set; }
    }
}