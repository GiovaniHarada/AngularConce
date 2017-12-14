using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conce.Controllers.Resources
{
    public class QueryResultResource<T>
    {
        public int TotalItens { get; set; }
        public IEnumerable<T> Itens { get; set; }
    }
}
