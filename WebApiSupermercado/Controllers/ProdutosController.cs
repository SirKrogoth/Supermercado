using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiSupermercado.dominio.entidade;
using WebApiSupermercado.dominio.negocio;

namespace WebApiSupermercado.Controllers
{
    public class ProdutosController : ApiController
    {
        // GET: api/Produtos
        public IEnumerable<string> Get()
        {
            return new string[] { "João Rafael Menezes", "Pedro Geromel" };
        }

        // GET: api/Produtos/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Produtos
        public string Post([FromBody]Produto produto)
        {
            ProdutoNegocio pn = new ProdutoNegocio();

            string retorno = pn.InserirNovoProduto(produto);

            return retorno;
        }

        // PUT: api/Produtos/5
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE: api/Produtos/5
        public void Delete(int id)
        {

        }
    }
}
