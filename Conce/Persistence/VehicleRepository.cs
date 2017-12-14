using Conce.Core.Models;
using Conce.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Conce.Extensions;

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
        public async Task<QueryResult<Vehicle>> GetVehicles(VehicleQuery queryObj)
        {
            var result = new QueryResult<Vehicle>();

            var query = ctx.Vehicles
                .Include(v => v.Model)
                    .ThenInclude(m => m.Make)
                .Include(v => v.Features)
                    .ThenInclude(vf => vf.Feature)
                .AsQueryable();

            if (queryObj.MakeId.HasValue)
                query = query.Where(v => v.Model.MakeId == queryObj.MakeId.Value);

            if (queryObj.ModelId.HasValue)
                query = query.Where(v => v.Model.Id == queryObj.ModelId.Value);

            var columnsMap = new Dictionary<string, Expression<Func<Vehicle, object>>>()
            {
                ["make"] = v => v.Model.Make.Name,
                ["model"] = v => v.Model.Name,
                ["contactName"] = v => v.ContactName
            };
            query = query.ApplyOrdering(queryObj, columnsMap);

            result.TotalItens = await query.CountAsync();
            query = query.ApplyPaging(queryObj);

            result.Itens = await query.ToListAsync();

            return result;
        }

    }
}
