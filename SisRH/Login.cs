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
using System.Data.SqlClient;
using SisRH.Classes;

namespace SisRH
{
    public partial class Login : Form
    {
        Classes.Usuario u = new Classes.Usuario();
        
        public Login()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void Logar(object sender, EventArgs e)
        {
            FolhaPonto pf = new FolhaPonto();   
            u.Usu1 = txtUsuario.Text;
            u.Senha1 = txtSenha.Text;
            int dia = DateTime.Now.Day;
            int mes = DateTime.Now.Month;
            int ano = DateTime.Now.Year;
            if (u.Logar() == true)
            {
                if (pf.VerificarFPDiaria(Convert.ToInt32(txtUsuario.Text), dia, mes, ano) == false)
                {
                    
                    pf.CriarFPDiaria(Convert.ToInt32(txtUsuario.Text), mes, ano, dia);
                    MessageBox.Show("Criado Campos para registro diarioos da Folha de Ponto");
                }
                Session.Instance.IniciarSessao(txtUsuario.Text);
                    if (u.VerificaTrocaSenha() == true)
                    {
                        SisRH.Telas_Consulta.AlterarSenhaLogin l = new Telas_Consulta.AlterarSenhaLogin();
                        Hide();
                        l.Show();
                    }
                    else
                    {
                        SisRH.Menu m = new SisRH.Menu();
                        Hide();
                        m.Show();
                    }
            }
            else
            {
                panel1.BackColor = Color.Red;
                panel2.BackColor = Color.Red;
                panel3.BackColor = Color.Red;
                panel4.BackColor = Color.Red;
                panel5.BackColor = Color.Red;
                panel6.BackColor = Color.Red;
                MessageBox.Show("Usuario ou Senha Invalido(s)");
                panel1.BackColor = Color.FromArgb(28, 218, 255);
                panel2.BackColor = Color.FromArgb(28, 218, 255);
                panel3.BackColor = Color.FromArgb(28, 218, 255);
                panel4.BackColor = Color.FromArgb(28, 218, 255);
                panel5.BackColor = Color.FromArgb(28, 218, 255);
                panel6.BackColor = Color.FromArgb(28, 218, 255);
            }
            
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
