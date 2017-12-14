using Conce.Core.Models;
using Conce.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conce.Persistence
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly ConceContext ctx;

        public VehicleRepository(ConceContext ctx)
        {
            this.ctx = ctx;
        }
        public async Task<Vehicle> GetVehicle(int id, bool includeRelated = true)
        {
            if (!includeRelated)
                return await ctx.Vehicles.FindAsync(id);

            return await ctx.Vehicles
                .Include(v => v.Features)
                    .ThenInclude(vf => vf.Feature)
                .Include(v => v.Model)
                    .ThenInclude(m => m.Make)
                .Where(v => v.Id == id).SingleOrDefaultAsync();
        }

        public void Add(Vehicle vehicle)
        {
            ctx.Vehicles.Add(vehicle);
        }
        public void Remove(Vehicle vehicle)
        {
            ctx.Remove(vehicle);
        }
        public async Task<IEnumerable<Vehicle>> GetVehicles()
        {
            return await ctx.Vehicles
                .Include(v => v.Model)
                    .ThenInclude(m => m.Make)
                .Include(v => v.Features)
                    .ThenInclude(vf => vf.Feature)
                .ToListAsync();
        }
    }
}
