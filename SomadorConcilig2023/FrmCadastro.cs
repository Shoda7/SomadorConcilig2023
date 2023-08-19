using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SomadorConcilig2023
{
    public partial class FrmCadastro : Form
    {
        public FrmCadastro()
        {
            InitializeComponent();
        }

        private void cbMostrarSenha_CheckedChanged(object sender, EventArgs e)
        {
            txtSenha.PasswordChar = cbMostrarSenha.Checked ? '\0' : '*';
            txtConfirmarSenha.PasswordChar = cbMostrarSenha.Checked ? '\0' : '*';
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            //Veridica sem o campo de email esta de acordo com o modelo padrao de email.
            string strModelo = "^([0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtEmail.Text, strModelo))
            {
                MessageBox.Show("Email invalido!");
                txtEmail.Focus();
            }
            //Validação para o tamanho da senha ser maior que 6 caracteres.
            else if (txtSenha.Text.Length <= 5)
            {
                MessageBox.Show("A senha precisa ter mais de 5 dígitos!");
                txtSenha.Focus();
            }
            else if (txtSenha.Text != txtConfirmarSenha.Text)
            {
                MessageBox.Show("Senhas diferentes!");
                txtConfirmarSenha.Focus();
            }
            else if (!ValidaCPF.IsCpf(mtxtCpf.Text))
            {
                MessageBox.Show("CPF inválido!");
                mtxtCpf.Focus();
            }
            else if (Banco.LocalizaCPF(mtxtCpf.Text))
            {
                MessageBox.Show("CPF já cadastrado!");
                mtxtCpf.Focus();
            }
            else if (Banco.LocalizaEMAIL(txtEmail.Text))
            {
                MessageBox.Show("Email já cadastrado!");
                txtEmail.Focus();
            }
            else
            {
                string _hashSenha = Crypto.sha256encrypt(txtSenha.Text);
                Banco.Add(txtNome.Text, mtxtCpf.Text, txtEmail.Text, mtxtCelular.Text, _hashSenha);
                FrmLogin frmLogin = new FrmLogin();
                frmLogin.Show();
                this.Hide();
            }
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            FrmLogin frmLogin= new FrmLogin();
            frmLogin.Show();
            this.Hide();
        }

        private void txtConfirmarSenha_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtSenha.Text != txtConfirmarSenha.Text)
            {
                lblSenhasDiferente.Visible = true;
            }
            else
            {
                lblSenhasDiferente.Visible = false;
            }
        }
    }
}
