using Conce.Core;
using Conce.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conce.Persistence
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly ConceContext ctx;

        public PhotoRepository(ConceContext ctx)
        {
            this.ctx = ctx;
        }
        public async Task<IEnumerable<Photo>> GetPhotos(int vehicleId)
        {
            return await ctx.Photos
                .Where(p => p.VehicleId == vehicleId)
                .ToListAsync();
        }
    }
}
