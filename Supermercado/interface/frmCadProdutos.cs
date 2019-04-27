using Supermercado.dominio.negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            produto.descricao = txtDescricao.Text;
            produto.precoVenda = Convert.ToDecimal(txtPrecoVenda.Text);
            produto.custo = Convert.ToDecimal(txtCusto.Text);
            produto.codEmpresa = 1;
            produto.estoque = Convert.ToSingle(txtEstoque.Text);

            pnd.InserirProdutoWebServices(produto);

            MessageBox.Show("Produto inserido com sucesso");
        }
    }
}
