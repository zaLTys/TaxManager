using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaxManager.Api.Models;

namespace TaxManager.Api.Entities
{
    public class TaxEntry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("MunicipalityId")]
        public Municipality Municipality { get; set; }
        public int MunicipalityId { get; set; }
        [Required]
        public int TaxType { get; set; }

        [Required]
        public decimal TaxValue { get; set; }
        [Required]
        public DateTime DateFrom { get; set; }
        [Required]
        public DateTime DateTo { get; set; }

        public TaxEntry()
        {
            
        }

        public TaxEntry(int id, DateTime dateFrom, DateTime dateTo, int municipalityId, int taxType, decimal taxValue)
        {
            Id = id;
            DateFrom = dateFrom;
            DateTo = dateTo;
            MunicipalityId = municipalityId;
            TaxType = taxType;
            TaxValue = taxValue;
        }
    }
}
