using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiSupermercado.dominio.entidade
{
    public class Produto
    {
        public int codigo { get; set; }
        public int codEmpresa { get; set; }
        public string descricao { get; set; }
        public decimal precoVenda { get; set; }
        public decimal custo { get; set; }
        public float estoque { get; set; }
    }
}