using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
using Amazon;

namespace SisRH.Telas_Consulta
{
    public partial class AlterarSenhaLogin : Form
    {
        public AlterarSenhaLogin()
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
                            pictureBox5.Image = imagem;
                            pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage; // Ajuste o modo de exibição conforme necessário
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
            pictureBox4.Image = Resources.power_on__2_;

        }
        private void picOn_MouseLeave(object sender, EventArgs e)
        {
            pictureBox4.Image = Resources.power_on__3_;

        }

        private void btnCad_Enter(object sender, EventArgs e)
        {
            button5.ForeColor = Color.Black;
            button5.BackColor = Color.FromArgb(28, 218, 255);
        }
        private void btnCad_Leave(object sender, EventArgs e)
        {
            button5.ForeColor = Color.FromArgb(28, 218, 255);
            button5.BackColor = Color.Transparent;
        }

        private void btnCanc_Enter(object sender, EventArgs e)
        {
            button2.ForeColor = Color.White;
            button2.BackColor = Color.DarkRed;
        }
        private void btnCanc_Leave(object sender, EventArgs e)
        {
            button2.ForeColor = Color.DarkRed;
            button2.BackColor = Color.Transparent;
        }

        private void AtualizarSenha(Object sender, EventArgs e)
        {
            if (txtSenha1.Text == txtConfirmar.Text)
            {
                SisRH.Classes.FuncoesGerais f = new Classes.FuncoesGerais();
                if (FuncoesGerais.IsPasswordStrong(txtSenha1.Text) == true)
                {
                    SisRH.Classes.Usuario u = new SisRH.Classes.Usuario();
                    u.UpdateComParametro(Convert.ToInt32(label13.Text), txtSenha1.Text);
                    MessageBox.Show("Senha Alterada com sucesso");
                    SisRH.Menu m = new SisRH.Menu();
                    Hide();
                    m.Show();
                    
                }
                else
                {
                    MessageBox.Show("Senha Fraca, por favor escolha uma mais segura");
                }    
               
            }
            else { MessageBox.Show("Senhas não conferem"); }

        }


    }
}
