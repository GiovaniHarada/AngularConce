using Conce.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conce.Persistence
{
    public class ProdutoRepository
    {
        private readonly ConceContext _ctx;

        public ProdutoRepository(ConceContext ctx)
        {
            this._ctx = ctx;
        }
        public async Task<IEnumerable<Produto>> GetProdutos()
        {
            return await _ctx.Produto.ToListAsync();
        }
        public async Task CreateProduto
    }
}
