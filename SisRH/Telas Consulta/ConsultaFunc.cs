using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SisRH.Classes;
using SisRH.Properties;

namespace SisRH.Telas_Consulta
{
    public partial class ConsultaFunc : Form
    {
        public ConsultaFunc()
        {
            InitializeComponent();

            foreach (string tipo in tiposJornadaTrabalho)
            {
                ddlJornada.Items.Add(tipo);
            }
            ddlJornada.SelectedIndex = 0;

            CarregarDepartamento();
            CarregarCargo();
        }

        List<string> tiposJornadaTrabalho = new List<string>
        {
            "Selecione",
            "07hrs-16hrs",
            "08hrs-17hrs",
            "09hrs-18hrs",
            "08hrs-14hrs",
            "09hrs-15hrs",
            "10hrs-17hrs",
            "11hrs-18hrs",
        };

        private void Pesquisar(object sender, EventArgs e)
        {
            Classes.Funcionario func = new Classes.Funcionario();

            if (txtMatricula.Text == "")
            {
                func.Matricula_Func1 = -1;
            }
            else
            {
                func.Matricula_Func1 = Convert.ToInt32(txtMatricula.Text);
            }
            if (txtNome.Text == "")
            {
                func.PrimeroNome_Func1 = "-1";
            }
            else
            {
                func.PrimeroNome_Func1 = txtNome.Text;
            }
            if (txtCPF.Text.Replace("-", "").Replace(",", "").Replace(".", "").Trim() == "")
            {
                func.CPF_Func1 = "-1";
            }
            else
            {
                func.CPF_Func1 = txtCPF.Text.Replace("-", "").Replace(",", "").Replace(".", "").Trim();
            }
            if (txtNascimento.Text.Replace("/","").Trim() == "")
            {
                func.Data_Nasc_Func1 = null;
                func.Data_Nasc_FuncInt1 = -1;
            }
            else
            {
                func.Data_Nasc_Func1 = Convert.ToDateTime(txtNascimento.Text);
            }
            if (ddlJornada.SelectedIndex == 0)
            {
                func.Jornada_Func1 = "-1";
            }
            else
            {
                func.Jornada_Func1 = ddlJornada.SelectedItem.ToString();
            }

            func.Fk_Cargo1 = Convert.ToInt32(ddlCargo.SelectedValue.ToString());
            func.Jornada_Func1 = ddlJornada.SelectedItem.ToString();

            dgvFunc.DataSource = func.ListarFunc().Tables[0];
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void picHome_MouseEnter(object sender, EventArgs e)
        {
            picHome.Image = Resources.home__1_;

        }
        private void picHome_MouseLeave(object sender, EventArgs e)
        {
            picHome.Image = Resources.home__2_;

        }
        private void CarregarDepartamento()
        {
            ddlDepart.Items.Add("Selecione");
            Classes.Departamento dep = new Classes.Departamento();
            ddlDepart.DataSource = dep.ListarDepartamentos().Tables[0];
            ddlDepart.DisplayMember = "nome_dep";
            ddlDepart.ValueMember = "id_dep";

        }

        private void CarregarCargo()
        {

            Classes.Cargo cargo = new Classes.Cargo();
            ddlCargo.DataSource = cargo.ListarCargos().Tables[0];
            ddlCargo.DisplayMember = "desc_cargo";
            ddlCargo.ValueMember = "id_cargo";
            ddlCargo.SelectedIndex = 0;
        }

        private void ConsultaFunc_Load(object sender, EventArgs e)
        {

        }
    }
}
