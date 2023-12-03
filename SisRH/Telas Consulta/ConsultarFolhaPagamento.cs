using Amazon.Runtime;
using Amazon.S3.Model;
using Amazon.S3;
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
using SisRH.Classes;
using SisRH.Properties;
using System.Net.Mail;
using System.Net;

namespace SisRH.Telas_Consulta
{
    public partial class ConsultarFolhaPagamento : Form
    {
        Classes.FolhaPonto fp = new Classes.FolhaPonto();
        public ConsultarFolhaPagamento()
        {
            InitializeComponent();
            label13.Text = Session.Instance.Matricula;
            ExibirImagemButton_Click();
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

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                dgvFolhaPonto.ClearSelection();
                dgvFolhaPonto.Rows[e.RowIndex].Selected = true;

                contextMenuStrip1.Show(Cursor.Position);
            }
        }

        private void GerarPDF(Object sender, EventArgs e)
        {
            int matricula = Convert.ToInt32(dgvFolhaPonto.SelectedRows[0].Cells["Matricula"].Value);
            int mes = Convert.ToInt32(dgvFolhaPonto.SelectedRows[0].Cells["Mes"].Value);
            int ano = Convert.ToInt32(dgvFolhaPonto.SelectedRows[0].Cells["Ano"].Value);
            SisRH.Handler.Teste t = new Handler.Teste();
            t.GerarPDFH(matricula, mes, ano, 0);
            MessageBox.Show("PDF GERADO");
        }

        private void EnviarPDFeMAIL(Object sender, EventArgs e)
        {
            int matricula = Convert.ToInt32(dgvFolhaPonto.SelectedRows[0].Cells["Matricula"].Value);
            int mes = Convert.ToInt32(dgvFolhaPonto.SelectedRows[0].Cells["Mes"].Value);
            int ano = Convert.ToInt32(dgvFolhaPonto.SelectedRows[0].Cells["Ano"].Value);
            SisRH.Handler.Teste t = new Handler.Teste();
            t.GerarPDFH(matricula, mes, ano, 1);
            
        }



        private void Pesquisar(object sender, EventArgs e)
        {
            try
            {
                Classes.Funcionario func = new Classes.Funcionario();

                int mat;

                if (txtMatricula.Text == "")
                {
                    mat = -1;
                }
                else
                {
                    mat = Convert.ToInt32(txtMatricula.Text);
                }
                if (txtAno.Text == "")
                {
                    fp.Ano_fp1 = -1;
                }
                else
                {
                    fp.Ano_fp1 = Convert.ToInt32(txtAno.Text);
                }

                if (txtDia.Text == "")
                {
                    fp.Dia_fp1 = -1;
                }
                else
                {
                    fp.Dia_fp1 = Convert.ToInt32(txtDia.Text);
                }
                if (txtMes.Text == "")
                {
                    fp.Mes_fp1 = -1;
                }
                else
                {
                    fp.Mes_fp1 = Convert.ToInt32(txtMes.Text);
                }

                dgvFolhaPonto.DataSource = fp.ListarFolhaPagamento(mat).Tables[0];
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }




    }
}
