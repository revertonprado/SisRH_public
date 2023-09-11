using SisRH.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SisRH
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        //private void MudarCor(Object sender, EventArgs e)
        //{
        //    if (btnEntrar.MouseHover += true)
        //    {
        //        btnEntrar.ForeColor = Color.FromArgb(52, 73, 94);
        //    }
        //    else
        //    {
        //        btnEntrar.ForeColor = Color.FromArgb(255, 215, 0);
        //    }

        //}
        private void btnEntrar_MouseEnter(object sender, EventArgs e)
        {
            btnEntrar.ForeColor = Color.FromArgb(52, 73, 94);
        }

        private void btnEntrar_MouseLeave(object sender, EventArgs e)
        {
            btnEntrar.ForeColor = Color.FromArgb(255, 215, 0);
        }

        private void picSair_MouseLeave(object sender, EventArgs e)
        {
            picSair.Image = Resources.switch__1_;
        }

        private void picSair_MouseEnter(object sender, EventArgs e)
        {
            picSair.Image = Resources.switch__2_;

        }
        private void picOlho_MouseEnter(object sender, EventArgs e)
        {
            picOlho.Image = Resources.hide;

        }
        private void picOlho_MouseLeave(object sender, EventArgs e)
        {
            picOlho.Image = Resources.visibility;

        }

    }
}
