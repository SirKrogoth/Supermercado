using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApiSupermercado.dominio.entidade;

namespace Supermercado.dominio.negocio
{
    public class ProdutoNegocioDesktop
    {
        public async void InserirProdutoWebServices(Produto produto)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:53660/api/Produtos");

                try
                {
                    HttpResponseMessage resposta = await client.PostAsJsonAsync("", produto);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }    
            }
        }
    }
}
