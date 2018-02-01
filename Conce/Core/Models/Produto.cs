using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Conce.Core.Models
{
    public class Produto
    {
        public int Id { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string Descricao { get; set; }
        [Column(TypeName = "varchar(255)")]
        public string Codigo { get; set; }
        public Marca Marca { get; set; }
        public int MarcaId { get; set; }
        public CategoriaProduto CategoriaProduto { get; set; }
        public int CategoriaProdutoId { get; set; }
    }
}
