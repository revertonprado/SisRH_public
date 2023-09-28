﻿using System;
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

namespace SisRH.Telas_Cadastro
{

    public partial class CadastroFuncionario : Form
    {
        private readonly string awsAccessKeyId;
        private readonly string awsSecretAccessKey;
        private readonly string bucketName;
        private readonly string objectKey;

        Classes.EstadoCivil ec = new Classes.EstadoCivil();

        List<string> nacionalidades = new List<string>
        {
            "Brasileira",
            "Americana",
            "Canadense",
            "Britânica",
            "Alemã",
    // Adicione mais nacionalidades conforme necessário
        };

        List<string> ufsBrasil = new List<string>
        {
            "Selecione",
            "AC","AL","AM","AP","BA","CE","DF","ES",
            "GO","MA","MG","MS","MT","PA","PB","PE",
            "PI","PR","RJ","RN","RO","RR","RS","SC",
            "SE","SP","TO"
        };

        List<string> tiposEscolaridade = new List<string>
        {
            "Selecione",
            "Fundamental Incompleto",
            "Fundamental Completo",
            "Médio Incompleto",
            "Médio Completo",
            "Técnico",
            "Superior Incompleto",
            "Superior Completo",
            "Pós-Graduação",
            "Mestrado",
            "Doutorado"
        };

        List<string> tiposComprovanteResidencia = new List<string>
        {
            "Selecione",
            "Conta de Luz",
            "Conta de Água",
            "Conta de Gás",
            "Conta de Telefone",
            "Fatura de Cartão de Crédito",
            "Contrato de Locação",
            "Declaração de Residência",
            "Recibo de Aluguel",
            "Escritura de Imóvel",
            "Contrato de Compra e Venda",
            "Extrato Bancário",
            "Outro"
        };

        public CadastroFuncionario()
        {
            InitializeComponent();

            ddlSexo.Items.Clear();
            ddlSexo.Items.Add("Selecione");
            ddlSexo.Items.Add("M");
            ddlSexo.Items.Add("F");
            ddlSexo.SelectedIndex = 0; // Define o primeiro item (Selecione) como selecionado por padrão

            // Para ddlCor
            ddlCor.Items.Clear();
            ddlCor.Items.Add("Selecione");
            ddlCor.Items.Add("Branco");
            ddlCor.Items.Add("Negro");
            ddlCor.Items.Add("Pardo");
            ddlCor.Items.Add("Amarelo");
            ddlCor.Items.Add("Indigena");
            ddlCor.SelectedIndex = 0; // Define o primeiro item (Selecione) como selecionado por padrão

            // Para ddlTs
            ddlTs.Items.Clear();
            ddlTs.Items.Add("Selecione");
            ddlTs.Items.Add("A+");
            ddlTs.Items.Add("A-"); 
            ddlTs.Items.Add("B+");
            ddlTs.Items.Add("B-");
            ddlTs.Items.Add("AB+");
            ddlTs.Items.Add("AB-");
            ddlTs.Items.Add("O-");
            ddlTs.Items.Add("O+");
            ddlTs.SelectedIndex = 0;

            foreach (string nacionalidade in nacionalidades)
            {
                ddlNac.Items.Add(nacionalidade);
            }

            ddlNac.SelectedIndex = 0;

            foreach (string uf in ufsBrasil)
            {
                ddlUF.Items.Add(uf);
            }

            ddlUF.SelectedIndex = 0;

            foreach (string uf in ufsBrasil)
            {
                ddlUF2.Items.Add(uf);
            }

            ddlUF2.SelectedIndex = 0;

            ddlEc.Items.Add("Selecione");
            ddlEc.Items.Add("Solteiro");
            ddlEc.Items.Add("Casado");
            ddlEc.Items.Add("Divorciado");
            ddlEc.Items.Add("Viúvo");
            ddlEc.SelectedIndex = 0;

            ddlTipoMorad.Items.Add("Selecione");
            ddlTipoMorad.Items.Add("Casa");
            ddlTipoMorad.Items.Add("Apartamento");
            ddlTipoMorad.Items.Add("Sítio");
            ddlTipoMorad.Items.Add("Chácara");
            ddlTipoMorad.SelectedIndex = 0;

            foreach(string tipo in tiposEscolaridade)
            {
                ddlEscolaridade.Items.Add(tipo);
            }

            ddlEscolaridade.SelectedIndex = 0;

            foreach (string tipo in tiposComprovanteResidencia)
            {
                ddlComprRes.Items.Add(tipo);
            }

            ddlComprRes.SelectedIndex = 0;

            
            CarregarBanco();
            CarregarCargo();
            CarregarDepartamento();
        }

        private void ExibirImagemButton_Click(object sender, EventArgs e)
        {
            string accessKey = "AKIAVM3YRTJTT6A2QL7R";
            string secretKey = "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";
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


        private void SelecionarFotoButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Arquivos de Imagem|*.jpg;*.jpeg;*.png;*.gif;*.bmp"; // Filtre os tipos de imagem que deseja permitir

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string caminhoLocalDaFoto = openFileDialog.FileName; // Caminho da foto selecionada pelo usuário

                    try
                    {
                        FazerUploadDeFotoParaS3(caminhoLocalDaFoto);
                        MessageBox.Show("Foto Carregada com sucesso");
                        ExibirImagemButton_Click(sender, e);
                    }
                    catch (Exception ex)
                    {

                        throw ex;
                    }// Execute o código de upload aqui usando o caminho da foto selecionada
                   


                }
            }
        }


        private void FazerUploadDeFotoParaS3(string caminhoLocalDaFoto)
        {
            var s3Client = new AmazonS3Client("AKIAVM3YRTJTT6A2QL7R", "qK4yVYchMMYZVBymyUU93SQAzX+EhKyDuNwClSEU", Amazon.RegionEndpoint.USEast1);
            var transferUtility = new TransferUtility(s3Client);

            string bucketName = "docspim4semestre";
            string objectKey = "Fotos-Perfil/"+txtMatricula.Text; // Substitua pelo nome desejado para a foto no S3, incluindo a extensão

            using (var fileStream = new FileStream(caminhoLocalDaFoto, FileMode.Open))
            {
                transferUtility.Upload(fileStream, bucketName, objectKey);
            }
        }

        private void CarregarBanco()
        {
            ddlBanco.Items.Add("Selecione");
            Classes.Bancos bancos = new Classes.Bancos();    
            ddlBanco.DataSource = bancos.ListarBancos().Tables[0];
            ddlBanco.DisplayMember = "banco";
            ddlBanco.ValueMember = "id_banco";
            ddlBanco.SelectedIndex = 0;
        }

        private void CarregarCargo()
        {
            ddlCargo.Items.Add("Selecione");
            Classes.Cargo cargo = new Classes.Cargo();
            ddlCargo.DataSource = cargo.ListarCargos().Tables[0];
            ddlCargo.DisplayMember = "desc_cargo";
            ddlCargo.ValueMember = "id_cargo";
            ddlCargo.SelectedIndex = 0;
        }

        protected void ddlCargo_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cod1 = ddlCargo.SelectedValue.ToString(); // Obtém o valor selecionado no DropDownList
            int cod = Convert.ToInt32(cod1);

            Classes.Cargo cargo = new Classes.Cargo();
            DataTable salarioTable = cargo.ListarSalario(cod).Tables[0];

            if (salarioTable.Rows.Count > 0)
            {
                // Se houver linhas no DataTable, atualize o TextBox com o salário
                txtSalario.Text = salarioTable.Rows[0]["salario_cargo"].ToString();
            }
            else
            {
                // Caso contrário, o salário não foi encontrado para a seleção atual
                txtSalario.Text = "Salário não encontrado";
            }
        }

        private void CarregarDepartamento()
        {
            ddlDepart.Items.Add("Selecione");
            Classes.Departamento dep = new Classes.Departamento();
            ddlDepart.DataSource = dep.ListarDepartamentos().Tables[0];
            ddlDepart.DisplayMember = "nome_dep";
            ddlDepart.ValueMember = "id_dep";

        }


        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void CadastroFuncionario_Load(object sender, EventArgs e)
        {

        }

        private void textBox22_TextChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }
    }
}
