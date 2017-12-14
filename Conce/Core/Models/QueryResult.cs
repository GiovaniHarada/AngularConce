using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conce.Core.Models
{
    public class QueryResult<T>
    {
        public int TotalItens { get; set; }
        public IEnumerable<T> Itens { get; set; }
    }
}
