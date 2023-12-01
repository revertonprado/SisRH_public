using SisRH.Classes;
using System;
using System.Drawing;
using System.Windows.Forms;
using SisRH.Properties;

namespace SisRH.Telas_Cadastro
{
    public partial class GerarFolhadePagamento : Form
    {
        public GerarFolhadePagamento()
        {
            InitializeComponent();
        }

        private void picOn_MouseEnter(object sender, EventArgs e)
        {
            pictureBox2.Image = Resources.power_on__2_;

        }
        private void picOn_MouseLeave(object sender, EventArgs e)
        {
            pictureBox2.Image = Resources.power_on__3_;

        }
        private void AbrirHome(Object sender, EventArgs e)
        {
            SisRH.Menu menu = new SisRH.Menu();
            Hide();
            menu.Show();

        }
        private void picHome_MouseEnter(object sender, EventArgs e)
        {
            pictureBox1.Image = Resources.home__1_;

        }
        private void picHome_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Image = Resources.home__2_;

        }
        private void Fechar(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("Você deseja realmente fechar o Sistema?", "SisRH", MessageBoxButtons.YesNo);

            if (resultado == DialogResult.Yes)
            {
                Session.Instance.EncerrarSessao();
                Application.Exit();
            }
            else
            {

            }

        }
        private void FecharCan(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("Você deseja realmente cancelar a geração?", "SisRH", MessageBoxButtons.YesNo);

            if (resultado == DialogResult.Yes)
            {
                SisRH.Menu menu = new SisRH.Menu();
                Hide();
                menu.Show();
            }
            else
            {

            }

        }
        private void GerarFolhaPagamento(object sender, EventArgs e)
        {
            Classes.FolhaPonto fp = new FolhaPonto();
            int ano;
            int mes;

            ano = DateTime.Now.Year;
            mes = DateTime.Now.Month;

            if (txtMatricula.Text == "CONFIRMAR" )
            {
                DialogResult resultado = MessageBox.Show("Você deseja realmente GERAR a Folha de Pagamento?", +mes + "/" + ano, MessageBoxButtons.YesNo);

                if (resultado == DialogResult.Yes)
                {
                    fp.GerarFolhaPagamento(mes, ano);
                }
                else
                {
                    MessageBox.Show("A folha de pagamento do " + mes + "/" + ano + " foi gerada com sucesso!");
                }
            }
            else
            {
                MessageBox.Show("A folha de pagamento do " + mes + "/" + ano + " já está gerada!");
            }

        }

        private void MudarCorBox(object sender, EventArgs e)
        {
            string conf = "CONFIRMAR";
            if (txtMatricula.Text == conf)
            {
                panel2.BackColor = Color.FromArgb(0, 252, 168);
                panel4.BackColor = Color.FromArgb(0, 252, 168);
                panel14.BackColor = Color.FromArgb(0, 252, 168);
                panel13.BackColor = Color.FromArgb(0, 252, 168);
            }
            else
            {
                panel2.BackColor = Color.DarkRed;
                panel4.BackColor = Color.DarkRed;
                panel14.BackColor = Color.DarkRed;
                panel13.BackColor = Color.DarkRed;
            }

        }

        private void btnCad_Enter(object sender, EventArgs e)
        {
            btnConfirmar.ForeColor = Color.Black;
            btnConfirmar.BackColor = Color.FromArgb(28, 218, 255);
        }
        private void btnCad_Leave(object sender, EventArgs e)
        {
            btnConfirmar.ForeColor = Color.FromArgb(28, 218, 255);
            btnConfirmar.BackColor = Color.Transparent;
        }

        private void btnCanc_Enter(object sender, EventArgs e)
        {
            button2.ForeColor = Color.White;
            button2.BackColor = Color.DarkRed;
        }
        private void btnCanc_Leave(object sender, EventArgs e)
        {
            button2.ForeColor = Color.DarkRed;
            button2.BackColor = Color.Transparent;
        }

    }
}
