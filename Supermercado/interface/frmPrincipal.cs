using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Supermercado
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void SairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EmpresaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ProdutosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCadProdutos fcp = new frmCadProdutos();

            fcp.Show();
        }
    }
}
