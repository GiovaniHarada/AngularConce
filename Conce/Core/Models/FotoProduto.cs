using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Conce.Core.Models
{
    public class FotoProduto
    {
        public int Id { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string Nome { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string Url { get; set; }
        public Produto Produto { get; set; }
    }
}
