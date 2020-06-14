using System;

namespace TaxManager.Core.Models
{
    public class TaxEntryDto
    {
        public int Id { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int MunicipalityId { get; set; }
        public TaxTypes TaxType { get; }
    }
}