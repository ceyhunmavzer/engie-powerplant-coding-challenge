using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SPaas.Api.Models;
using SPaas.Services.Services;

namespace SPaas.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductionPlanController : ControllerBase
    {
        private readonly PowerPlantService _powerPlantService;
        private readonly IMapper _mapper;
        
        public ProductionPlanController(PowerPlantService powerPlantService, IMapper mapper)
        {
            _powerPlantService = powerPlantService;
            _mapper = mapper;
        }
        
        [HttpPost]
        public IActionResult Calculate([FromBody] LoadRequest request)
        {
            var mappedPowerPlants= request.PowerPlants.Select(x => _mapper.Map<SPaas.Services.DataModels.PowerPlant>(x));
            var mappedFuel = _mapper.Map<SPaas.Services.DataModels.Fuel>(request.Fuels);
            return Ok(_powerPlantService.GetProductionPlan(mappedPowerPlants, mappedFuel, request.Load));
        }
    }
}