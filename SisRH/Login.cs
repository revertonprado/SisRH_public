using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SisRH.Properties;

namespace SisRH
{
    public partial class Login : Form
    {
        Transform transform = new Transform();

        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void picOlho_MouseEnter(object sender, EventArgs e)
        {
            picOlho.Image = Resources.visibility2;
            txtSenha.UseSystemPasswordChar = true;

        }
        private void picOlho_MouseLeave(object sender, EventArgs e)
        {
            picOlho.Image = Resources.visible_blue;
            txtSenha.UseSystemPasswordChar = false;
        }
        private void picOn_MouseEnter(object sender, EventArgs e)
        {
            pcOn.Image = Resources.power_on__2_;

        }
        private void picOn_MouseLeave(object sender, EventArgs e)
        {
            pcOn.Image = Resources.power_on__3_;

        }
        private void Encerrar(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
