using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaxManager.Core.DataAccess;

namespace TaxManager.Api.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class TaxController : ControllerBase
    {
        private readonly ITaxRepository _taxRepository;
        private readonly IMapper _mapper;

        public TaxController(ITaxRepository taxRepository,
            IMapper mapper)
        {
            _taxRepository = taxRepository ??
                             throw new ArgumentNullException(nameof(taxRepository));
            _mapper = mapper ??
                      throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public IActionResult GetMunicipalities()
        {
            var cityEntities = _taxRepository.GetAllMunicipalities();

            return Ok(_mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(cityEntities));
        }

    }
}
