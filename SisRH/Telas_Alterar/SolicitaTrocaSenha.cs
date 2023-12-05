using Amazon.Runtime;
using Amazon.S3.Model;
using Amazon.S3;
using SisRH.Classes;
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
using Amazon;
using SisRH.Properties;

namespace SisRH.Telas_Alterar
{
    public partial class SolicitaTrocaSenha : Form
    {
        public SolicitaTrocaSenha()
        {
            InitializeComponent();
            label13.Text = Session.Instance.Matricula;
            ExibirImagemButton_Click();
        }
        private void Fechar(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("Você deseja realmente fechar o Sistema?", "SisRH", MessageBoxButtons.YesNo);

            if (resultado == DialogResult.Yes)
            {
                Application.Exit();
            }
            else
            {

            }
        }

        private void ExibirImagemButton_Click()
        {
            string accessKey = "";
            string secretKey = "";
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

        private void DeletarUsu(object sender, EventArgs e)
        {
            try
            {
                if (dgvFolhaPonto.SelectedRows.Count > 0)
                {
                    Usuario u = new Usuario();
                    u.Matricula1 = Convert.ToInt32(dgvFolhaPonto.SelectedRows[0].Cells["Matricula"].Value);
                    u.DeletarUsuario();
                    MessageBox.Show("Usuario deletado com sucesso");
                }
                else
                {
                    MessageBox.Show("Não há registros");
                }
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        private void LiberarSenhaUsu(object sender, EventArgs e)
        {
            try
            {
                if (dgvFolhaPonto.SelectedRows.Count > 0)
                {
                    Usuario u = new Usuario();
                    u.Matricula1 = Convert.ToInt32(dgvFolhaPonto.SelectedRows[0].Cells["Matricula"].Value);
                    u.LiberarSenha();
                    MessageBox.Show("Troca de Senha habilitada");
                }
                else
                {
                    MessageBox.Show("Não há registros");
                }
            }
            catch (Exception)
            {

                throw;
            }

        }


        private void AtivarUsu(object sender, EventArgs e)
        {
            try
            {
                if (dgvFolhaPonto.SelectedRows.Count > 0)
                {
                    Usuario u = new Usuario();
                    u.Matricula1 = Convert.ToInt32(dgvFolhaPonto.SelectedRows[0].Cells["Matricula"].Value);
                    u.AtivarUsuario();
                    MessageBox.Show("Usuario Ativado com sucesso");
                }
                else
                {
                    MessageBox.Show("Não há registros");
                }
            }
            catch (Exception)
            {

                throw;
            }

        }


        private void picHome_MouseEnter(object sender, EventArgs e)
        {
            picHome.Image = Resources.home__1_;

        }
        private void picHome_MouseLeave(object sender, EventArgs e)
        {
            picHome.Image = Resources.home__2_;

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
        private void Pesquisar(object o, EventArgs e)
        {
            Usuario u = new Usuario();
            if (txtMatricula.Text == "")
            {
                u.Matricula1 = -1;
            }
            else
            {
                u.Matricula1 = Convert.ToInt32(txtMatricula.Text);
            }
            dgvFolhaPonto.DataSource = u.ListarUsuarios().Tables[0];

        }
        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvFolhaPonto.ClearSelection();
                dgvFolhaPonto.Rows[e.RowIndex].Selected = true;

                contextMenuStrip1.Show(Cursor.Position);
            }
        }

    }
}
