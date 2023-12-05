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
    public partial class ConsultaFolhaPonto : Form
    {
        Classes.FolhaPonto fp = new Classes.FolhaPonto();
        public ConsultaFolhaPonto()
        {
            InitializeComponent();
            label13.Text = Session.Instance.Matricula;
            ExibirImagemButton_Click();

           
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

                 dgvFolhaPonto.DataSource = fp.ListarFolhaPonto(mat).Tables[0];
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }
        private void ExibirImagemButton_Click()
        {
            
            string accessKey = "";
            string secretKey = "+";
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
        private void visualizarDetalhesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvFolhaPonto.SelectedRows.Count > 0)
            {
                FolhaPonto fp = new FolhaPonto();

                TimeSpan E1;
                TimeSpan E2;
                TimeSpan E3;
                TimeSpan S1;
                TimeSpan S2;
                TimeSpan S3;

                int matricula;

                string nome;

                fp.Id_fp = Convert.ToInt32(dgvFolhaPonto.SelectedRows[0].Cells["ID"].Value);

                fp.Dt_apont_fp = Convert.ToDateTime(dgvFolhaPonto.SelectedRows[0].Cells["Data1"].Value);

                TimeSpan.TryParse(Convert.ToString(dgvFolhaPonto.SelectedRows[0].Cells["E1"].Value), out E1);
                TimeSpan.TryParse(Convert.ToString(dgvFolhaPonto.SelectedRows[0].Cells["S1"].Value), out S1);
                TimeSpan.TryParse(Convert.ToString(dgvFolhaPonto.SelectedRows[0].Cells["E2"].Value), out E2);
                TimeSpan.TryParse(Convert.ToString(dgvFolhaPonto.SelectedRows[0].Cells["S2"].Value), out S2);
                TimeSpan.TryParse(Convert.ToString(dgvFolhaPonto.SelectedRows[0].Cells["E3"].Value), out E3);
                TimeSpan.TryParse(Convert.ToString(dgvFolhaPonto.SelectedRows[0].Cells["S3"].Value), out S3);

                fp.Matricula_fp1 = Convert.ToInt32(dgvFolhaPonto.SelectedRows[0].Cells["Matricula"].Value);

                nome = Convert.ToString(dgvFolhaPonto.SelectedRows[0].Cells["Nome"].Value);

                fp.Hr_apont_fp_E1 = E1;
                fp.Hr_apont_fp_E2 = E2;
                fp.Hr_apont_fp_E3 = E3;

                fp.Hr_apont_fp_S1 = S1;
                fp.Hr_apont_fp_S2 = S2;
                fp.Hr_apont_fp_S3 = S3;

                matricula = fp.Matricula_fp1;

                fp.Desc_adv_fp = Convert.ToString(dgvFolhaPonto.SelectedRows[0].Cells["Observacao"].Value);


                Telas_Alterar.AlterarFp afp = new Telas_Alterar.AlterarFp();
                this.Hide();
                afp.txtId.Text = fp.Id_fp.ToString();
                afp.txtE1.Text = fp.Hr_apont_fp_E1.ToString();
                afp.txtE2.Text = fp.Hr_apont_fp_E2.ToString();
                afp.txtE3.Text = fp.Hr_apont_fp_E3.ToString();
                afp.txtS1.Text = fp.Hr_apont_fp_S1.ToString();
                afp.txtS2.Text = fp.Hr_apont_fp_S2.ToString();
                afp.txtS3.Text = fp.Hr_apont_fp_S3.ToString();
                afp.txtData.Text = fp.Dt_apont_fp.ToString();
                afp.txtObs.Text = fp.Desc_adv_fp.ToString();
                afp.txtMatricula.Text = Convert.ToString(matricula);
                afp.lblFunc.Text = nome;

                try
                {
                    string accessKey = "AKIAVM3YRTJTT6A2QL7R";
                    string secretKey = "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";
                    string bucketName = "docspim4semestre";
                    string objectKey = "Fotos-Perfil/" + matricula;

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
                                    afp.picFoto.Image = imagem;
                                    afp.picFoto.SizeMode = PictureBoxSizeMode.StretchImage; // Ajuste o modo de exibição conforme necessário
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
               catch(Exception ex) 
                {
                    MessageBox.Show("Erro ao acessar o S3: " + ex.Message);
                    throw ex;
                }    
                    
                afp.Show();
                
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
    }
}
