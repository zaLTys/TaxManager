using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace TaxManager.Api.Profiles
{
    public class TaxEntryProfiler : Profile
    {
        public TaxEntryProfiler()
        {
            CreateMap<Entities.TaxEntry, Models.TaxEntryDto>();
            
        }
    }
}
