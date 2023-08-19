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
    public partial class FrmHome : Form
    {
        public FrmHome()
        {
            InitializeComponent();
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            int soma = 0;
            for(int i = 1; i <= 1000; i+=2)
            {
                soma += i;
            }
            txtSoma.Text = soma.ToString();
        }
        //Também poderia utilizar uma progressão aritmética para esse caso. Resolvi utilizar desse métodos, pois demonstra a utilização de
        //ferramentas da programação, como o laço de repetição for.
    }
}
