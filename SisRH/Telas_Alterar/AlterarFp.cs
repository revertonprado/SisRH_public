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
using SisRH.Classes;
using Amazon.Runtime;
using Amazon.S3.Model;
using Amazon.S3;
using System.IO;
using Amazon;
using Amazon.S3.Transfer;

namespace SisRH.Telas_Alterar
{
    public partial class AlterarFp : Form
    {
        int id;
        TimeSpan e1, e2, e3;
        TimeSpan s1, s2, s3;
        string obs;

        FolhaPonto fp = new FolhaPonto();   
        public AlterarFp()
        {
            InitializeComponent();
            
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

        private void btnCad_Enter(object sender, EventArgs e)
        {
            btnCadastrar.ForeColor = Color.Black;
            btnCadastrar.BackColor = Color.FromArgb(28, 218, 255);
        }
        private void btnCad_Leave(object sender, EventArgs e)
        {
            btnCadastrar.ForeColor = Color.FromArgb(28, 218, 255);
            btnCadastrar.BackColor = Color.Transparent;
        }

        private void btnCanc_Enter(object sender, EventArgs e)
        {
            btnCancelar.ForeColor = Color.White;
            btnCancelar.BackColor = Color.DarkRed;
        }
        private void btnCanc_Leave(object sender, EventArgs e)
        {
            btnCancelar.ForeColor = Color.DarkRed;
            btnCancelar.BackColor = Color.Transparent;
        }

        private void AtualizarFP(object sender, EventArgs e)
        {
            if (txtObs.Text == "")
            {
                MessageBox.Show("Adicione uma justificativa para a alteração");
            }
            else
            {
                id = Convert.ToInt32(txtId.Text);
                TimeSpan.TryParse(Convert.ToString(txtE1.Text), out e1);
                TimeSpan.TryParse(Convert.ToString(txtE2.Text), out e2);
                TimeSpan.TryParse(Convert.ToString(txtE3.Text), out e3);
                TimeSpan.TryParse(Convert.ToString(txtS1.Text), out s1);
                TimeSpan.TryParse(Convert.ToString(txtS2.Text), out s2);
                TimeSpan.TryParse(Convert.ToString(txtS3.Text), out s3);
                obs = Convert.ToString(txtObs.Text);

                fp.UpdateComParametro(id, e1, e2, e3, s1, s2, s3, obs);
                MessageBox.Show("Alteração realizada com sucesso");

                SisRH.Telas_Consulta.ConsultaFolhaPonto fp1 = new SisRH.Telas_Consulta.ConsultaFolhaPonto();
                fp1.Show();
                this.Hide();
            }
            

        }

        private void FazerUploadDeFotoParaS3Declaracao(string caminhoLocalDaFoto)
        {
            var s3Client = new AmazonS3Client("AKIAVM3YRTJTT6A2QL7R", "qK4yVYchMMYZVBymyUU93SQAzX+EhKyDuNwClSEU", Amazon.RegionEndpoint.USEast1);
            var transferUtility = new TransferUtility(s3Client);

            string bucketName = "docspim4semestre";
            string objectKey = "DocsAtestadosDeclaracoes/" + "Declaracoes/" + txtMatricula.Text + "/" + "AnexoFP "+ txtData.Text.Replace("/","-").Replace("00:00:00", ""); // Substitua pelo nome desejado para a foto no S3, incluindo a extensão

            using (var fileStream = new FileStream(caminhoLocalDaFoto, FileMode.Open))
            {
                transferUtility.Upload(fileStream, bucketName, objectKey);
            }
        }

        private void SelecionarDeclaracao(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Arquivos de Imagem|*.jpg;*.jpeg;*.png;*.gif;*.bmp"; // Filtre os tipos de imagem que deseja permitir

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string caminhoLocalDaFotoCas = openFileDialog.FileName; // Caminho da foto selecionada pelo usuário

                    try
                    {
                        FazerUploadDeFotoParaS3Declaracao(caminhoLocalDaFotoCas);
                        MessageBox.Show("Foto Carregada com sucesso");
                        btnDeclaracao.ForeColor = Color.Black;
                        btnDeclaracao.BackColor = Color.FromArgb(28, 218, 255);
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }// Execute o código de upload aqui usando o caminho da foto selecionada

                }
            }
        }

        private void FazerUploadDeFotoParaS3Atestado(string caminhoLocalDaFoto)
        {
            var s3Client = new AmazonS3Client("AKIAVM3YRTJTT6A2QL7R", "qK4yVYchMMYZVBymyUU93SQAzX+EhKyDuNwClSEU", Amazon.RegionEndpoint.USEast1);
            var transferUtility = new TransferUtility(s3Client);

            string bucketName = "docspim4semestre";
            string objectKey = "DocsAtestadosDeclaracoes/" + "Atestado/" + txtMatricula.Text + "/" + "AnexoFP" + txtData.Text.Replace("/", "-").Replace("00:00:00",""); // Substitua pelo nome desejado para a foto no S3, incluindo a extensão

            using (var fileStream = new FileStream(caminhoLocalDaFoto, FileMode.Open))
            {
                transferUtility.Upload(fileStream, bucketName, objectKey);
            }
        }

        private void SelecionarAtestado(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Arquivos de Imagem|*.jpg;*.jpeg;*.png;*.gif;*.bmp"; // Filtre os tipos de imagem que deseja permitir

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string caminhoLocalDaFotoCas = openFileDialog.FileName; // Caminho da foto selecionada pelo usuário

                    try
                    {
                        FazerUploadDeFotoParaS3Atestado(caminhoLocalDaFotoCas);
                        MessageBox.Show("Foto Carregada com sucesso");
                        btnAtestado.ForeColor = Color.Black;
                        btnAtestado.BackColor = Color.FromArgb(28, 218, 255);
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }// Execute o código de upload aqui usando o caminho da foto selecionada

                }
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
        private void AbrirHome(Object sender, EventArgs e)
        {
            SisRH.Menu menu = new SisRH.Menu();
            Hide();
            menu.Show();
        }
        private void AbrirCancelar(Object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show("Você deseja realmente cancelar?", "SisRH", MessageBoxButtons.YesNo);

            if (resultado == DialogResult.Yes)
            {
                SisRH.Menu menu = new SisRH.Menu();
                Hide();
                menu.Show();
            }
            else
            {

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
        private void AlterarFp_Load(object sender, EventArgs e)
        {

        }
    }
}
