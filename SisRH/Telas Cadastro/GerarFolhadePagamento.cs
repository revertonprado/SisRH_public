using SisRH.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            pcOn.Image = Resources.power_on__2_;

        }
        private void picOn_MouseLeave(object sender, EventArgs e)
        {
            pcOn.Image = Resources.power_on__3_;

        }
        private void AbrirHome(Object sender, EventArgs e)
        {
            SisRH.Menu menu = new SisRH.Menu();
            Hide();
            menu.Show();

        }
        private void picHome_MouseEnter(object sender, EventArgs e)
        {
            picHome.Image = Resources.home__1_;

        }
        private void picHome_MouseLeave(object sender, EventArgs e)
        {
            picHome.Image = Resources.home__2_;

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


    }
}
