using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using MetroFramework.Drawing.Html;
using Amazon;
using Amazon.Runtime;
using System.Xml.Serialization;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using SisRH.Classes;

namespace SisRH.Telas_Cadastro
{
    public partial class CadastroDocs : Form
    {
        public CadastroDocs()
        {
            InitializeComponent();

            ExibirImagemButton_Click();
        }

        private void SelecionarCPF(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Arquivos de Imagem|*.jpg;*.jpeg;*.png;*.gif;*.bmp"; // Filtre os tipos de imagem que deseja permitir

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string caminhoLocalDaFotoCPF = openFileDialog.FileName; // Caminho da foto selecionada pelo usuário

                    try
                    {
                        FazerUploadDeFotoParaS3CPF(caminhoLocalDaFotoCPF);
                        MessageBox.Show("Foto Carregada com sucesso");
                      //  ExibirImagemButton_Click(sender, e);
                        panelrcpf.BackColor = Color.FromArgb(0, 252, 168);
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }// Execute o código de upload aqui usando o caminho da foto selecionada

                }
            }
        }

        private void SelecionarRG(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Arquivos de Imagem|*.jpg;*.jpeg;*.png;*.gif;*.bmp"; // Filtre os tipos de imagem que deseja permitir

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string caminhoLocalDaFotoRG = openFileDialog.FileName; // Caminho da foto selecionada pelo usuário

                    try
                    {
                        FazerUploadDeFotoParaS3RG(caminhoLocalDaFotoRG);
                        MessageBox.Show("Foto Carregada com sucesso");
                      //  ExibirImagemButton_Click(sender, e);
                        panelrg.BackColor = Color.FromArgb(0, 252, 168);
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }// Execute o código de upload aqui usando o caminho da foto selecionada

                }
            }
        }

        private void SelecionarNasc(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Arquivos de Imagem|*.jpg;*.jpeg;*.png;*.gif;*.bmp"; // Filtre os tipos de imagem que deseja permitir

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string caminhoLocalDaFotoNasc = openFileDialog.FileName; // Caminho da foto selecionada pelo usuário

                    try
                    {
                        FazerUploadDeFotoParaS3Nasc(caminhoLocalDaFotoNasc);
                        MessageBox.Show("Foto Carregada com sucesso");
                       // ExibirImagemButton_Click(sender, e);
                        panelnasc.BackColor = Color.FromArgb(0, 252, 168);
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }// Execute o código de upload aqui usando o caminho da foto selecionada

                }
            }
        }

        private void SelecionarCas(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Arquivos de Imagem|*.jpg;*.jpeg;*.png;*.gif;*.bmp"; // Filtre os tipos de imagem que deseja permitir

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string caminhoLocalDaFotoCas = openFileDialog.FileName; // Caminho da foto selecionada pelo usuário

                    try
                    {
                        FazerUploadDeFotoParaS3Cas(caminhoLocalDaFotoCas);
                        MessageBox.Show("Foto Carregada com sucesso");
                       // ExibirImagemButton_Click(sender, e);
                        certCas.BackColor = Color.FromArgb(0, 252, 168);
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }// Execute o código de upload aqui usando o caminho da foto selecionada

                }
            }
        }


        private void ExibirImagemButton_Click()
        {
            string accessKey = "AKIAVM3YRTJTT6A2QL7R";
            string secretKey = "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";
            string bucketName = "docspim4semestre";
            string objectKey = "Fotos-Perfil/" + lblPront.Text;

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
                            picFoto.Image = imagem;
                            picFoto.SizeMode = PictureBoxSizeMode.StretchImage; // Ajuste o modo de exibição conforme necessário
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


        private void FazerUploadDeFotoParaS3CPF(string caminhoLocalDaFoto)
        {
            var s3Client = new AmazonS3Client("AKIAVM3YRTJTT6A2QL7R", "qK4yVYchMMYZVBymyUU93SQAzX+EhKyDuNwClSEU", Amazon.RegionEndpoint.USEast1);
            var transferUtility = new TransferUtility(s3Client);

            string bucketName = "docspim4semestre";
            string objectKey = "Docs/" + txtMatricula.Text +"/" + lblcpf.Text + "/"; // Substitua pelo nome desejado para a foto no S3, incluindo a extensão

            using (var fileStream = new FileStream(caminhoLocalDaFoto, FileMode.Open))
            {
                transferUtility.Upload(fileStream, bucketName, objectKey);
            }
        }

        private void FazerUploadDeFotoParaS3RG(string caminhoLocalDaFoto)
        {
            var s3Client = new AmazonS3Client("AKIAVM3YRTJTT6A2QL7R", "qK4yVYchMMYZVBymyUU93SQAzX+EhKyDuNwClSEU", Amazon.RegionEndpoint.USEast1);
            var transferUtility = new TransferUtility(s3Client);

            string bucketName = "docspim4semestre";
            string objectKey = "Docs/" + txtMatricula.Text + "/" + lblrg.Text + "/"; // Substitua pelo nome desejado para a foto no S3, incluindo a extensão

            using (var fileStream = new FileStream(caminhoLocalDaFoto, FileMode.Open))
            {
                transferUtility.Upload(fileStream, bucketName, objectKey);
            }
        }

        private void FazerUploadDeFotoParaS3Nasc(string caminhoLocalDaFoto)
        {
            var s3Client = new AmazonS3Client("AKIAVM3YRTJTT6A2QL7R", "qK4yVYchMMYZVBymyUU93SQAzX+EhKyDuNwClSEU", Amazon.RegionEndpoint.USEast1);
            var transferUtility = new TransferUtility(s3Client);

            string bucketName = "docspim4semestre";
            string objectKey = "Docs/" + txtMatricula.Text + "/" + certnascimento.Text + "/"; // Substitua pelo nome desejado para a foto no S3, incluindo a extensão

            using (var fileStream = new FileStream(caminhoLocalDaFoto, FileMode.Open))
            {
                transferUtility.Upload(fileStream, bucketName, objectKey);
            }
        }

        private void FazerUploadDeFotoParaS3Cas(string caminhoLocalDaFoto)
        {
            var s3Client = new AmazonS3Client("AKIAVM3YRTJTT6A2QL7R", "qK4yVYchMMYZVBymyUU93SQAzX+EhKyDuNwClSEU", Amazon.RegionEndpoint.USEast1);
            var transferUtility = new TransferUtility(s3Client);

            string bucketName = "docspim4semestre";
            string objectKey = "Docs/" + txtMatricula.Text + "/" + certcasamento.Text + "/"; // Substitua pelo nome desejado para a foto no S3, incluindo a extensão

            using (var fileStream = new FileStream(caminhoLocalDaFoto, FileMode.Open))
            {
                transferUtility.Upload(fileStream, bucketName, objectKey);
            }
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
