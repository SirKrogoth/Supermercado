using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApiSupermercado.infra;
using WebApiSupermercado.dominio.entidade;
using System.Data.SqlClient;
using System.Data;

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

                conexao.Close();

                return "Produto " + produto.descricao + " inserido com sucesso.";
            }
            catch (Exception e)
            {
                return "Não foi possivel inserir o Produto " + produto.descricao + ".";
            }
        }

        public List<Produto> BuscarTodosProdutos()
        {
            try
            {
                conexao = acessaDadosSqlServer.criarConexaoBanco();
                conexao.Open();

                cmd = new SqlCommand();
                cmd.CommandText = "SELECT * FROM tblProduto";

                cmd.Connection = conexao;

                DataTable table = new DataTable();
                SqlDataReader reader = cmd.ExecuteReader();

                table.Load(reader);

                List<Produto> produtos = new List<Produto>();

                foreach (DataRow item in table.Rows)
                {
                    Produto produto = new Produto();

                    produto.codigo = Convert.ToInt32(item["codigo"]);
                    produto.codEmpresa = Convert.ToInt32(item["codEmpresa"]);
                    produto.descricao = item["descricao"].ToString();
                    produto.custo = Convert.ToDecimal(item["custo"]);
                    produto.precoVenda = Convert.ToDecimal(item["precoVenda"]);
                    produto.estoque = Convert.ToSingle(item["estoque"]);

                    produtos.Add(produto);
                }

                return produtos;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string DeletarProduto(int codigo)
        {
            try
            {
                conexao = acessaDadosSqlServer.criarConexaoBanco();
                conexao.Open();

                cmd = new SqlCommand();

                cmd.CommandText = "DELETE FROM tblProduto WHERE codigo = @codigo";
                cmd.Parameters.AddWithValue("@codigo", codigo);
                cmd.Connection = conexao;
                cmd.ExecuteNonQuery();

                conexao.Close();

                return "Produto excluido com sucesso";
            }
            catch (Exception)
            {
                return "Falha ao deletar produto: " + codigo.ToString();
            }
        }

        public string AtualizarProduto(Produto produto)
        {
            try
            {
                conexao = acessaDadosSqlServer.criarConexaoBanco();
                conexao.Open();

                cmd = new SqlCommand();

                cmd.CommandText = "UPDATE tblProduto SET codEmpresa = @codEmpresa, descricao = @descricao," +
                    " precoVenda = @precoVenda, custo = @custo, estoque = @estoque WHERE codigo = @codigo";

                cmd.Parameters.AddWithValue("@codEmpresa", produto.codEmpresa);
                cmd.Parameters.AddWithValue("@descricao", produto.descricao);
                cmd.Parameters.AddWithValue("@precoVenda", produto.precoVenda);
                cmd.Parameters.AddWithValue("@custo", produto.custo);
                cmd.Parameters.AddWithValue("@estoque", produto.estoque);
                cmd.Parameters.AddWithValue("@codigo", produto.codigo);

                cmd.Connection = conexao;

                cmd.ExecuteNonQuery();

                conexao.Close();

                return "Produto atualizado com sucesso";
            }
            catch (Exception e)
            {
                return "Não foi possível atualizar o produto";
                throw new Exception(e.Message);
            }
        }
    }
}