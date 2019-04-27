using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiSupermercado.infra;
using WebApiSupermercado.dominio.entidade;
using System.Data.SqlClient;

namespace WebApiSupermercado.dominio.negocio
{
    public class ProdutoNegocio
    {
        AcessaDadosSqlServer acessaDadosSqlServer = new AcessaDadosSqlServer();
        SqlConnection conexao;
        SqlCommand cmd;

        public string InserirNovoProduto(Produto produto)
        {            
            try
            {
                conexao = acessaDadosSqlServer.criarConexaoBanco();
                conexao.Open();

                cmd = new SqlCommand();

                cmd.CommandText = "INSERT INTO tblProduto(codEmpresa, descricao, precoVenda, custo, estoque)" +
                    " VALUES(@codEmpresa, @descricao, @precoVenda, @custo, @estoque)";

                cmd.Parameters.AddWithValue("@codEmpresa", produto.codEmpresa);
                cmd.Parameters.AddWithValue("@descricao", produto.descricao);
                cmd.Parameters.AddWithValue("@precoVenda", produto.precoVenda);
                cmd.Parameters.AddWithValue("@custo", produto.custo);
                cmd.Parameters.AddWithValue("@estoque", produto.estoque);

                cmd.Connection = conexao;

                cmd.ExecuteNonQuery();

                return "Produto " + produto.descricao + " inserido com sucesso.";
            }
            catch (Exception e)
            {
                return "Não foi possivel inserir o Produto " + produto.descricao + ".";
            }
        }
    }
}