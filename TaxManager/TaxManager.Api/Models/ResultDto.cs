using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaxManager.Api.Models
{
    public class ResultDto
    {
        public ResultDto(decimal taxApplied, string errorMessage)
        {
            TaxApplied = taxApplied;
        }
        public decimal TaxApplied { get; set; }
        public string ErrorMessage { get; set; }
    }
}
