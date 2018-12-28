using Microsoft.AspNetCore.Mvc;
using WebApplicationWithCore.Models;
using AutoMapper;
using WebApplicationWithCore.Persistence;
using WebApplicationWithCore.Core;
using WebApplicationWithCore.Controllers.Resources;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
namespace WebApplicationWithCore.Controllers
{
    [Route("/api/vehicles")]
    public class VehiclesController : Controller
    {
        private readonly IMapper mapper;
        private readonly WebApplicationWithCoreDbContext context;
        private readonly IVehicleRepository repository;
        private readonly IUnitOfWork unitOfWork;
        public VehiclesController(IMapper mapper,WebApplicationWithCoreDbContext context,IVehicleRepository repository ,IUnitOfWork unitOfWork )
        {
            this.mapper = mapper;
            this.context = context;
            this.repository = repository;
            this.unitOfWork = unitOfWork;
        }
        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] SaveVehicleResources vehicleResources)
        {   
            if(!ModelState.IsValid)
            return BadRequest(ModelState);

           /*  var model = await context.Models.FindAsync(vehicleResources.ModelId);
            if(model == null)
            {
                ModelState.AddModelError("ModelId" , "Invalid ModelId ");
                return BadRequest(ModelState);
            }
            */
            var vehicle = mapper.Map<SaveVehicleResources , Vehicle>(vehicleResources);
            vehicle.LastUpdate = DateTime.Now;
            repository.Add(vehicle);
            await unitOfWork.Complete();
            vehicle = await repository.GetVehicle(vehicle.Id);
            var result = mapper.Map<Vehicle,VehicleResource>(vehicle);
            return Ok(result);
        }

      [HttpPut("{id}")] 
         public async Task<IActionResult> UpdateVehicle(int id,[FromBody] SaveVehicleResources vehicleResources)
        {
            if(!ModelState.IsValid)
            return BadRequest(ModelState);
            var  vehicle = await repository.GetVehicle(id);
            if(vehicle == null)
            return NotFound();
            mapper.Map<SaveVehicleResources,Vehicle>(vehicleResources , vehicle);
            vehicle.LastUpdate = DateTime.Now;
           
            await unitOfWork.Complete();
            vehicle = await repository.GetVehicle(vehicle.Id);
            var result = mapper.Map<Vehicle,VehicleResource>(vehicle);
            return Ok(result);
        }

        [HttpDelete("{id}")] 
         public async Task<IActionResult> DeleteVehicle(int id,[FromBody] SaveVehicleResources vehicleResources)
        {
         
            var vehicle = await repository.GetVehicle(id,includeRelated :false);
            if(vehicle == null)
            return NotFound();
            repository.Remove(vehicle);
            await unitOfWork.Complete();
          
            return Ok(id);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            //Complete representation of vehicle...
           var vehicle = await repository.GetVehicle(id);
           if(vehicle == null)
           return NotFound();
           var result = mapper.Map<Vehicle,VehicleResource>(vehicle);
           return Ok(result);
        }

    }
}