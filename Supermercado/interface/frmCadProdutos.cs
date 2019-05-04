using Supermercado.dominio.negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using WebApiSupermercado.dominio.entidade;

namespace Supermercado
{
    public partial class frmCadProdutos : Form
    {
        public frmCadProdutos()
        {
            InitializeComponent();
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            ProdutoNegocioDesktop pnd = new ProdutoNegocioDesktop();
            Produto produto = new Produto();

            if (btnSalvar.Text == "Salvar")
            {
                produto.descricao = txtDescricao.Text;
                produto.precoVenda = Convert.ToDecimal(txtPrecoVenda.Text);
                produto.custo = Convert.ToDecimal(txtCusto.Text);
                produto.codEmpresa = 1;
                produto.estoque = Convert.ToSingle(txtEstoque.Text);

                pnd.InserirProdutoWebServices(produto);

                BuscarTodosProdutos();

                MessageBox.Show("Produto inserido com sucesso");
            }
            else
            {
                produto.codigo = Convert.ToInt32(txtCodigo.Text);
                produto.descricao = txtDescricao.Text;
                produto.precoVenda = Convert.ToDecimal(txtPrecoVenda.Text);
                produto.custo = Convert.ToDecimal(txtCusto.Text);
                produto.codEmpresa = 1;
                produto.estoque = Convert.ToSingle(txtEstoque.Text);

                pnd.AtualizarProdutoWebServices(produto);

                BuscarTodosProdutos();

                MessageBox.Show("Produto atualizado com sucesso");
            }
        }

        private void frmCadProdutos_Load(object sender, EventArgs e)
        {
            BuscarTodosProdutos();            ;
        }

        public async void BuscarTodosProdutos()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:53660/api/Produtos");

                try
                {
                    var resposta = await client.GetAsync("");
                    string dados = await resposta.Content.ReadAsStringAsync();
                    List<Produto> produtos = new JavaScriptSerializer().Deserialize<List<Produto>>(dados);
                    dgvProdutos.AutoGenerateColumns = false;
                    dgvProdutos.DataSource = produtos;
                }
                catch (Exception)
                {
                    
                }                                                               
             }
        }

        private void btnSincronizarAgora_Click(object sender, EventArgs e)
        {
            BuscarTodosProdutos();
        }

        private void btnDeletarProduto_Click(object sender, EventArgs e)
        {
            ProdutoNegocioDesktop pnd = new ProdutoNegocioDesktop();

            pnd.DeletarProduto(Convert.ToInt32(txtCodigo.Text));

            BuscarTodosProdutos();

            MessageBox.Show("Produto excluido com sucesso.", "Excluido com sucesso",MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dgvProdutos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            Produto produtoSelecionado = new Produto();

            produtoSelecionado = (dgvProdutos.SelectedRows[0].DataBoundItem as Produto);

            PreencherCampos(produtoSelecionado);

            btnSalvar.Text = "Atualizar";
        }   
        
        public void PreencherCampos(Produto produto)
        {
            txtCodigo.Text = produto.codigo.ToString();
            txtDescricao.Text = produto.descricao.ToString();
            txtCusto.Text = produto.custo.ToString();
            txtEstoque.Text = produto.estoque.ToString();
            txtPrecoVenda.Text = produto.precoVenda.ToString();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            btnSalvar.Text = "Salvar";
        }
    }
}
