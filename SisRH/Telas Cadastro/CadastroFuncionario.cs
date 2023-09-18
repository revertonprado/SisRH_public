using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SisRH.Telas_Cadastro
{
    public partial class CadastroFuncionario : Form
    {
        Classes.EstadoCivil ec = new Classes.EstadoCivil();

        public CadastroFuncionario()
        {
            InitializeComponent(); 
            CarregarEstadoCivil();
        }

        protected void CarregarEstadoCivil()
        {
            cmbEc.DataSource = ec.ListarEstadoCivil().Tables[0];
            cmbEc.DisplayMember = "desc_ec";
            cmbEc.ValueMember = "id_ec";
            cmbEc.SelectedIndex = 1;

        }



        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void CadastroFuncionario_Load(object sender, EventArgs e)
        {

        }

        private void textBox22_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }
    }
}
