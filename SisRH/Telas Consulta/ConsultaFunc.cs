using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Amazon.Runtime;
using Amazon.S3.Model;
using Amazon.S3;
using SisRH.Classes;
using SisRH.Properties;
using Amazon.S3.Transfer;
using MetroFramework.Drawing.Html;
using Amazon;


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
            ddlCargo.SelectedValue = 5;
            ddlDepart.SelectedValue = 5;
            label13.Text = Session.Instance.Matricula;
            ExibirImagemButton_Click();




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

            if ((int)ddlCargo.SelectedValue == 5)
            {
                func.Fk_Cargo1 = -1;
            }
            else
            {
                func.Fk_Cargo1 = Convert.ToInt32(ddlCargo.SelectedValue.ToString());
            } 


            if (ddlJornada.SelectedIndex == 0)
            {
                func.Jornada_Func1 = "-1";
            }
            else
            {
                func.Jornada_Func1 = ddlJornada.SelectedItem.ToString();
            }

            if ((int)ddlDepart.SelectedValue == 5)
            {
                func.Fk_Dep1 = -1;
            }
            else
            {
                func.Fk_Dep1 = Convert.ToInt32(ddlDepart.SelectedValue.ToString());
            }
            

            dgvFunc.DataSource = func.ListarFunc().Tables[0];
        }

        private void ExibirImagemButton_Click()
        {
            string accessKey = "AKIAVM3YRTJTT6A2QL7R";
            string secretKey = "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";
            string bucketName = "docspim4semestre";
            string objectKey = "Fotos-Perfil/" + label13.Text;

            var credentials = new BasicAWSCredentials(accessKey, secretKey);
            var config = new AmazonS3Config
            {
                RegionEndpoint = RegionEndpoint.USEast2 // Substitua pela região desejada
            };

            using (var client = new AmazonS3Client(credentials, config))
            {
                try
                {
                    var getObjectRequest = new GetObjectRequest
                    {
                        BucketName = bucketName,
                        Key = objectKey
                    };

                    using (var response = client.GetObject(getObjectRequest))
                    using (var responseStream = response.ResponseStream)
                    {
                        if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                        {
                            // Ler a imagem em um MemoryStream
                            var memoryStream = new MemoryStream();
                            responseStream.CopyTo(memoryStream);

                            // Criar uma imagem a partir do MemoryStream
                            var imagem = Image.FromStream(memoryStream);

                            // Definir a imagem na PictureBox
                            pictureBox1.Image = imagem;
                            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage; // Ajuste o modo de exibição conforme necessário
                        }
                        else
                        {
                            MessageBox.Show("Erro ao acessar o objeto no S3.");
                        }
                    }
                }
                catch (AmazonS3Exception ex)
                {
                    MessageBox.Show("Erro ao acessar o S3: " + ex.Message);
                }
            }
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

        private void AbrirHome(Object sender, EventArgs e)
        {
            SisRH.Menu menu = new SisRH.Menu();
            Hide();
            menu.Show();

        }

        private void CarregarCargo()
        {

            Classes.Cargo cargo = new Classes.Cargo();
            ddlCargo.DataSource = cargo.ListarCargos().Tables[0];
            ddlCargo.DisplayMember = "desc_cargo";
            ddlCargo.ValueMember = "id_cargo";
            ddlCargo.SelectedIndex = 0;
        }

        private void picOn_MouseEnter(object sender, EventArgs e)
        {
            pcOn.Image = Resources.power_on__2_;

        }
        private void picOn_MouseLeave(object sender, EventArgs e)
        {
            pcOn.Image = Resources.power_on__3_;

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

        private void ConsultaFunc_Load(object sender, EventArgs e)
        {

        }
    }
}
