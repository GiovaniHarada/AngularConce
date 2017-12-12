using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Conce.Core;

namespace Conce.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ConceContext ctx;

        public UnitOfWork(ConceContext ctx)
        {
            this.ctx = ctx;
        }
        public async Task CompleteAsync()
        {
            await ctx.SaveChangesAsync();
        }
    }
}
