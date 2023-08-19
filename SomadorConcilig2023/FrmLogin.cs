using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace SomadorConcilig2023
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (Banco.ValidaLogin(mtxtCpf.Text, txtSenha.Text))
            {
                FrmHome frmHome = new FrmHome();
                frmHome.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Usuário ou senha incorreta!");
                txtSenha.Text = null;
            }
        }

        private void btnNovaConta_Click(object sender, EventArgs e)
        {
            FrmCadastro frmCadastro = new FrmCadastro();
            frmCadastro.Show();
            this.Hide();
        }
        private void cbMostrarSenha_CheckedChanged_1(object sender, EventArgs e)
        {
            txtSenha.PasswordChar = cbMostrarSenha.Checked ? '\0' : '*';
        }
    }
}
