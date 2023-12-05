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
using SisRH.Properties;

namespace SisRH.Telas_Cadastro
{
    public partial class CadastroDocs : Form
    {
        public CadastroDocs()
        {
            InitializeComponent();
            PegarFunc();
            
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
                        ExibirCPF();
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
                        ExibirRG();
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
                        ExibirNasc();
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
                        certcas.BackColor = Color.FromArgb(0, 252, 168);
                        ExibirCas();
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }// Execute o código de upload aqui usando o caminho da foto selecionada

                }
            }
        }

        private void SelecionarRes(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Arquivos de Imagem|*.jpg;*.jpeg;*.png;*.gif;*.bmp"; // Filtre os tipos de imagem que deseja permitir

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string caminhoLocalDaFotoRes = openFileDialog.FileName; // Caminho da foto selecionada pelo usuário

                    try
                    {
                        FazerUploadDeFotoParaS3Res(caminhoLocalDaFotoRes);
                        MessageBox.Show("Foto Carregada com sucesso");
                        // ExibirImagemButton_Click(sender, e);
                        panelReservista.BackColor = Color.FromArgb(0, 252, 168);
                        ExibirRes();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }
            }
        }

        private void SelecionarCTPS(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Arquivos de Imagem|*.jpg;*.jpeg;*.png;*.gif;*.bmp"; // Filtre os tipos de imagem que deseja permitir

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string caminhoLocalDaFotoctps = openFileDialog.FileName; // Caminho da foto selecionada pelo usuário

                    try
                    {
                        FazerUploadDeFotoParaS3ctps(caminhoLocalDaFotoctps);
                        MessageBox.Show("Foto Carregada com sucesso");
                        // ExibirImagemButton_Click(sender, e);
                        panelCTPS.BackColor = Color.FromArgb(0, 252, 168);
                        ExibirCTPS();
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }// Execute o código de upload aqui usando o caminho da foto selecionada

                }
            }
        }

        private void SelecionarEscolaridade(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Arquivos de Imagem|*.jpg;*.jpeg;*.png;*.gif;*.bmp"; // Filtre os tipos de imagem que deseja permitir

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string caminhoLocalDaFotoEscolaridade = openFileDialog.FileName; // Caminho da foto selecionada pelo usuário

                    try
                    {
                        FazerUploadDeFotoParaS3Escolaridade(caminhoLocalDaFotoEscolaridade);
                        MessageBox.Show("Foto Carregada com sucesso");
                        // ExibirImagemButton_Click(sender, e);
                        panelEscolaridade.BackColor = Color.FromArgb(0, 252, 168);
                        ExibirEscolaridade();
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }// Execute o código de upload aqui usando o caminho da foto selecionada

                }
            }
        }

        private void SelecionarComprRes(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Arquivos de Imagem|*.jpg;*.jpeg;*.png;*.gif;*.bmp"; // Filtre os tipos de imagem que deseja permitir

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string caminhoLocalDaFotoComprRes = openFileDialog.FileName; // Caminho da foto selecionada pelo usuário

                    try
                    {
                        FazerUploadDeFotoParaS3ComprRes(caminhoLocalDaFotoComprRes);
                        MessageBox.Show("Foto Carregada com sucesso");
                        // ExibirImagemButton_Click(sender, e);
                        PanelComprovRes.BackColor = Color.FromArgb(0, 252, 168);
                        ExibirComprovRes();
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
            string accessKey = "";
            string secretKey = "";
            string bucketName = "docspim4semestre";
            string objectKey = "Fotos-Perfil/" + txtMatricula.Text;

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

        private void ExibirCPF()
        {
            string accessKey = "";
            string secretKey = "";
            string bucketName = "docspim4semestre";
            string objectKey = "Docs/" + txtMatricula.Text + "/" + "CPF";

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
                            picCPF.Image = imagem;
                            picCPF.SizeMode = PictureBoxSizeMode.StretchImage; // Ajuste o modo de exibição conforme necessário
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

        private void ExibirRG()
        {
            string accessKey = "";
            string secretKey = "";
            string bucketName = "docspim4semestre";
            string objectKey = "Docs/" + txtMatricula.Text + "/"+ "RG";

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
                            picRG.Image = imagem;
                            picRG.SizeMode = PictureBoxSizeMode.StretchImage; // Ajuste o modo de exibição conforme necessário
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

        private void ExibirNasc()
        {
            string accessKey = "";
            string secretKey = "";
            string bucketName = "docspim4semestre";
            string objectKey = "Docs/" + txtMatricula.Text + "/" + "Certidao-Nascimento";

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
                            picNasc.Image = imagem;
                            picNasc.SizeMode = PictureBoxSizeMode.StretchImage; // Ajuste o modo de exibição conforme necessário
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

        private void ExibirCas()
        {
            string accessKey = "";
            string secretKey = "";
            string bucketName = "docspim4semestre";
            string objectKey = "Docs/" + txtMatricula.Text + "/" + "Certidao-Casamento";

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
                            picCasam.Image = imagem;
                            picCasam.SizeMode = PictureBoxSizeMode.StretchImage; // Ajuste o modo de exibição conforme necessário
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

        private void ExibirRes()
        {
            string accessKey = "";
            string secretKey = "";
            string bucketName = "docspim4semestre";
            string objectKey = "Docs/" + txtMatricula.Text + "/" + "Reservista";

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
                            picReserv.Image = imagem;
                            picReserv.SizeMode = PictureBoxSizeMode.StretchImage; // Ajuste o modo de exibição conforme necessário
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


        private void ExibirCTPS()
        {
            string accessKey = "";
            string secretKey = "";
            string bucketName = "docspim4semestre";
            string objectKey = "Docs/" + txtMatricula.Text + "/" + "CTPS";

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
                            picCTPS.Image = imagem;
                            picCTPS.SizeMode = PictureBoxSizeMode.StretchImage; // Ajuste o modo de exibição conforme necessário
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

        private void ExibirEscolaridade()
        {
            string accessKey = "";
            string secretKey = "";
            string bucketName = "docspim4semestre";
            string objectKey = "Docs/" + txtMatricula.Text + "/" + "Escolaridade";

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
                            PICEscolaridade.Image = imagem;
                            PICEscolaridade.SizeMode = PictureBoxSizeMode.StretchImage; // Ajuste o modo de exibição conforme necessário
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

        private void ExibirComprovRes()
        {
            string accessKey = "";
            string secretKey = "";
            string bucketName = "docspim4semestre";
            string objectKey = "Docs/" + txtMatricula.Text + "/" + "Comprov-Residencia";

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
                            picCompRes.Image = imagem;
                            picCompRes.SizeMode = PictureBoxSizeMode.StretchImage; // Ajuste o modo de exibição conforme necessário
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
            var s3Client = new AmazonS3Client("", "", Amazon.RegionEndpoint.USEast1);
            var transferUtility = new TransferUtility(s3Client);

            string bucketName = "docspim4semestre";
            string objectKey = "Docs/" + txtMatricula.Text +"/" + "CPF"; // Substitua pelo nome desejado para a foto no S3, incluindo a extensão

            using (var fileStream = new FileStream(caminhoLocalDaFoto, FileMode.Open))
            {
                transferUtility.Upload(fileStream, bucketName, objectKey);
            }
        }

        private void FazerUploadDeFotoParaS3RG(string caminhoLocalDaFoto)
        {
            var s3Client = new AmazonS3Client("", "+", Amazon.RegionEndpoint.USEast1);
            var transferUtility = new TransferUtility(s3Client);

            string bucketName = "docspim4semestre";
            string objectKey = "Docs/" + txtMatricula.Text + "/" + "RG"; // Substitua pelo nome desejado para a foto no S3, incluindo a extensão

            using (var fileStream = new FileStream(caminhoLocalDaFoto, FileMode.Open))
            {
                transferUtility.Upload(fileStream, bucketName, objectKey);
            }
        }

        private void FazerUploadDeFotoParaS3Nasc(string caminhoLocalDaFoto)
        {
            var s3Client = new AmazonS3Client("", "+", Amazon.RegionEndpoint.USEast1);
            var transferUtility = new TransferUtility(s3Client);

            string bucketName = "docspim4semestre";
            string objectKey = "Docs/" + txtMatricula.Text + "/" + "Certidao-Nascimento"; // Substitua pelo nome desejado para a foto no S3, incluindo a extensão

            using (var fileStream = new FileStream(caminhoLocalDaFoto, FileMode.Open))
            {
                transferUtility.Upload(fileStream, bucketName, objectKey);
            }
        }

        private void FazerUploadDeFotoParaS3Cas(string caminhoLocalDaFoto)
        {
            var s3Client = new AmazonS3Client("", "+", Amazon.RegionEndpoint.USEast1);
            var transferUtility = new TransferUtility(s3Client);

            string bucketName = "docspim4semestre";
            string objectKey = "Docs/" + txtMatricula.Text + "/" + "Certidao-Casamento"; // Substitua pelo nome desejado para a foto no S3, incluindo a extensão

            using (var fileStream = new FileStream(caminhoLocalDaFoto, FileMode.Open))
            {
                transferUtility.Upload(fileStream, bucketName, objectKey);
            }
        }

        private void FazerUploadDeFotoParaS3Res(string caminhoLocalDaFoto)
        {
            var s3Client = new AmazonS3Client("", "", Amazon.RegionEndpoint.USEast1);
            var transferUtility = new TransferUtility(s3Client);

            string bucketName = "docspim4semestre";
            string objectKey = "Docs/" + txtMatricula.Text + "/" + "Reservista"; // Substitua pelo nome desejado para a foto no S3, incluindo a extensão

            using (var fileStream = new FileStream(caminhoLocalDaFoto, FileMode.Open))
            {
                transferUtility.Upload(fileStream, bucketName, objectKey);
            }
        }

        private void FazerUploadDeFotoParaS3ctps(string caminhoLocalDaFoto)
        {
            var s3Client = new AmazonS3Client("", "", Amazon.RegionEndpoint.USEast1);
            var transferUtility = new TransferUtility(s3Client);

            string bucketName = "docspim4semestre";
            string objectKey = "Docs/" + txtMatricula.Text + "/" + "CTPS"; // Substitua pelo nome desejado para a foto no S3, incluindo a extensão

            using (var fileStream = new FileStream(caminhoLocalDaFoto, FileMode.Open))
            {
                transferUtility.Upload(fileStream, bucketName, objectKey);
            }
        }

        private void FazerUploadDeFotoParaS3Escolaridade(string caminhoLocalDaFoto)
        {
            var s3Client = new AmazonS3Client("", "", Amazon.RegionEndpoint.USEast1);
            var transferUtility = new TransferUtility(s3Client);

            string bucketName = "docspim4semestre";
            string objectKey = "Docs/" + txtMatricula.Text + "/" + "Escolaridade"; // Substitua pelo nome desejado para a foto no S3, incluindo a extensão

            using (var fileStream = new FileStream(caminhoLocalDaFoto, FileMode.Open))
            {
                transferUtility.Upload(fileStream, bucketName, objectKey);
            }
        }

        private void FazerUploadDeFotoParaS3ComprRes(string caminhoLocalDaFoto)
        {
            var s3Client = new AmazonS3Client("", "", Amazon.RegionEndpoint.USEast1);
            var transferUtility = new TransferUtility(s3Client);

            string bucketName = "docspim4semestre";
            string objectKey = "Docs/" + txtMatricula.Text + "/" + "Comprov-Residencia"; // Substitua pelo nome desejado para a foto no S3, incluindo a extensão

            using (var fileStream = new FileStream(caminhoLocalDaFoto, FileMode.Open))
            {
                transferUtility.Upload(fileStream, bucketName, objectKey);
            }
        }

        private void PegarFunc()
        {
            Classes.Funcionario f = new Classes.Funcionario();
            System.Data.SqlClient.SqlDataReader ddr;
            ddr = f.ListarUltimoMatricula();
            ddr.Read();
            if (ddr.HasRows)
            {
                if (ddr["matricula_func"].ToString() == "")
                {
                    txtMatricula.Text = "1";
                }
                else
                {
                    lblFunc.Text = ddr["primeiro_nm_func"].ToString().Trim() + " - " + ddr["matricula_func"].ToString() ;
                    txtMatricula.Text = ddr["matricula_func"].ToString();
                    
                }
            }
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

        private void AbrirDadosFunc(object sender, EventArgs e)
        {
            MessageBox.Show("Já foi realizado o cadastro do funcionario, finalize o cadastro dos documentos");
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
        private void btnCad_Enter(object sender, EventArgs e)
        {
            btnCadastrar.ForeColor = Color.Black;
            btnCadastrar.BackColor = Color.FromArgb(28,218,255);
        }
        private void btnCad_Leave(object sender, EventArgs e)
        {
            btnCadastrar.ForeColor = Color.FromArgb(28, 218, 255);
            btnCadastrar.BackColor = Color.Transparent;
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
