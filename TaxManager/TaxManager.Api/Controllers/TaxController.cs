﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaxManager.Api.DataAccess;
using TaxManager.Api.Domain;
using TaxManager.Api.Models;

namespace TaxManager.Api.Controllers
{
    [ApiController]
    public class TaxController : ControllerBase
    {
        private readonly ITaxManager _taxManager;
        private readonly IMapper _mapper;

        public TaxController(ITaxManager taxManager, IMapper mapper)
        {
            _taxManager = taxManager??
                             throw new ArgumentNullException(nameof(_taxManager));
            _mapper = mapper ??
                      throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("/municipalities")]
        public async Task<IActionResult> GetMunicipalities()
        {
            var municipalities = await _taxManager.GetMunicipalitiesAsync();

            return Ok(_mapper.Map<IEnumerable<MunicipalityDto>>(municipalities));
        }

        
        [HttpGet("/municipalities/{municipalityName}/date/{date}")]
        public async Task<IActionResult> GetTaxForMunicipalityByDate(string municipalityName, string date)
        {
            var result = await _taxManager.GetMunicipalityTaxForDateAsync(municipalityName, date);
            if (result.ErrorMessage != null)
            {
                return NotFound(result.ErrorMessage);
            }

            return Ok(result);
        }

    }
}
