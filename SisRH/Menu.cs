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


namespace SisRH
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
            monthCalendar1.TitleBackColor = Color.FromArgb(16, 16, 16);
            monthCalendar1.TitleForeColor = Color.FromArgb(28, 218, 255);

        }

        private void Fechar(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void picOn_MouseEnter(object sender, EventArgs e)
        {
            pcOn.Image = Resources.power_on__2_;

        }
        private void picOn_MouseLeave(object sender, EventArgs e)
        {
            pcOn.Image = Resources.power_on__3_;

        }
       

        private void flowLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void flowLayoutPanel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void Menu_Load(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
