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
using SisRH.Properties;
using SisRH.Classes;
using Amazon;

namespace SisRH
{
    public partial class Menu : Form
    {
        public Menu()
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
        private void picOn_MouseEnter(object sender, EventArgs e)
        {
            pcOn.Image = Resources.power_on__2_;

        }
        private void picOn_MouseLeave(object sender, EventArgs e)
        {
            pcOn.Image = Resources.power_on__3_;

        }
        private void AbrirCadFunc(Object sender, EventArgs e)
        {
            SisRH.Telas_Cadastro.CadastroFuncionario func = new Telas_Cadastro.CadastroFuncionario();
            Hide();
            func.Show();

        }
        private void AbrirFolhaPagConsulta(Object sender, EventArgs e)
        {
            SisRH.Telas_Consulta.ConsultarFolhaPagamento cfp = new Telas_Consulta.ConsultarFolhaPagamento();
            Hide();
            cfp.Show();

        }
        private void AbrirConsultaFP(Object sender, EventArgs e)
        {
            SisRH.Telas_Consulta.ConsultaFolhaPonto fp = new SisRH.Telas_Consulta.ConsultaFolhaPonto();
            Hide();
            fp.Show();

        }
        private void AbrirFolhaPag(Object sender, EventArgs e)
        {
            int ano;
            int mes;

            ano = DateTime.Now.Year;
            mes = DateTime.Now.Month;

            SisRH.Classes.FolhaPonto fp = new FolhaPonto();
            if (fp.VerificarFolhaPagamento(mes, ano) == false)
            {
                SisRH.Telas_Cadastro.GerarFolhadePagamento gfp = new SisRH.Telas_Cadastro.GerarFolhadePagamento();
                Hide();
                gfp.Show();
            }
            else
            {
                MessageBox.Show("Folha de Pagamento"+mes+"/"+ano+" já gerada.");
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

        private void FecharFolhaPonto(object sender, EventArgs e)
        {
            Classes.FolhaPonto fp = new FolhaPonto();
            int ano;
            int mes;

            ano = DateTime.Now.Year;
            mes = DateTime.Now.Month;

            if (fp.VerificarFolhaPonto(mes, ano) == true)
            {
                DialogResult resultado = MessageBox.Show("Você deseja realmente fechar a Folha Ponto?", +mes+"/"+ano, MessageBoxButtons.YesNo);

                if (resultado == DialogResult.Yes)
                {
                    fp.FecharFolhaPonto(mes, ano);
                }
                else
                {
                    MessageBox.Show("A folha ponto do " + mes + "/" + ano + " foi fechada com sucesso!");
                }
            }
            else
            {
                MessageBox.Show("A folha ponto do "+ mes +"/"+ ano +" já está fechada!");
            }
            
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void Menu_Load(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
