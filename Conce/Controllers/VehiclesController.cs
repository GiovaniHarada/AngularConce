using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Conce.Models;
using Conce.Controllers.Resources;
using AutoMapper;
using Conce.Data;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Conce.Controllers
{
    [Route("api/[controller]")]
    public class VehiclesController : Controller
    {
        private readonly ConceContext ctx;
        private readonly IMapper mapper;

        public VehiclesController(ConceContext ctx, IMapper mapper)
        {
            this.ctx = ctx;
            this.mapper = mapper;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var vehicle = await ctx.Vehicles
                .Include(v => v.Features)
                    .ThenInclude(vf => vf.Feature)
                .Include(v => v.Model)
                    .ThenInclude(m => m.Make)
                .Where(v => v.Id == id).SingleOrDefaultAsync();

            if (vehicle == null)
                return NotFound();

            var vehicleResoure = mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(vehicleResoure);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SaveVehicleResource vehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource);
            vehicle.LastUpdate = DateTime.Now;
            ctx.Vehicles.Add(vehicle);
            await ctx.SaveChangesAsync();
            var result = mapper.Map<Vehicle, SaveVehicleResource>(vehicle);
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] SaveVehicleResource vehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = await ctx.Vehicles.Include(v => v.Features).SingleOrDefaultAsync(v => v.Id == id);

            if (vehicle == null)
                return NotFound();

            mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource, vehicle);
            vehicle.LastUpdate = DateTime.Now;
            await ctx.SaveChangesAsync();
            var result = mapper.Map<Vehicle, SaveVehicleResource>(vehicle);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await ctx.Vehicles.FindAsync(id);

            if (vehicle == null)
                return NotFound();

            ctx.Remove(vehicle);
            await ctx.SaveChangesAsync();

            return Ok(id);
        }
    }
}
