using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using Amazon.Runtime;
using SisRH.Classes;
using System.Data.Common;
using System.Data;
using System.Diagnostics;
using Amazon.S3.Transfer;
using Amazon.S3;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;
using Org.BouncyCastle.Crypto.Macs;

namespace SisRH.Handler
{
    internal class Teste
    {
        public void GerarPDFH(int mat, int mes, int ano, int email, string rec)
        {
        string Nome;
        int Matricula;
        DateTime DtHoje = DateTime.Now;
        int Horas;
        int Extras;
        decimal INSS;
        decimal IRPF;
        decimal Salario_Bruto;
        decimal Salario;
        


        FolhaPonto fp = new FolhaPonto();

            fp.Mes_fp1 = mes;
            fp.Ano_fp1 = ano;


            System.Data.SqlClient.SqlDataReader ddr;
            ddr = fp.ListarFolhaPagamentoDR(mat);
            ddr.Read();


            if (ddr.HasRows)
            {

                Nome = ddr["Nome"].ToString();
                Matricula = Convert.ToInt32(ddr["Matricula"]);
                Horas = Convert.ToInt32(ddr["Horas_Trabalhadas"]);
                Extras = Convert.ToInt32(ddr["Horas_Extras"]);
                INSS = Convert.ToDecimal(ddr["INSS"]);
                IRPF = Convert.ToDecimal(ddr["IRPF"]);
                Salario = Convert.ToDecimal(ddr["Salario_Liquido"]);
                Salario_Bruto = Convert.ToDecimal(ddr["Salario_Base"]);

                Document document = new Document();
                PdfWriter.GetInstance(document, new FileStream("Holerite"+Matricula+".pdf", FileMode.Create));
                document.Open();

                PdfPTable table = new PdfPTable(2);
                table.AddCell(Nome);
                table.AddCell(Matricula.ToString());
                table.AddCell(Horas.ToString());
                table.AddCell(Extras.ToString());
                table.AddCell(INSS.ToString());
                table.AddCell(IRPF.ToString());
                table.AddCell(Salario.ToString());
                table.AddCell(Salario_Bruto.ToString());
                document.Add(table);
                document.Close();
               
                if (email == 1)
                {
                    EnviarEmailComAnexo(rec, Path.Combine(Environment.CurrentDirectory, "Holerite" + Matricula + ".pdf"));
                    MessageBox.Show("Holerite enviado para o email!");
                }
                else
                {
                    FazerUploadDeFotoParaS3("Holerite" + Matricula + ".pdf", Matricula);
                    Process.Start("chrome.exe", Path.Combine(Environment.CurrentDirectory, "Holerite" + Matricula + ".pdf"));
                }

            }

        }
        private void FazerUploadDeFotoParaS3(string caminhoLocalDaFoto, int mat)
        {
            int Ano = DateTime.Now.Year;
            int Mes = DateTime.Now.Month;
            int Dia = DateTime.Now.Day;

            var s3Client = new AmazonS3Client("AKIAVM3YRTJTT6A2QL7R", "qK4yVYchMMYZVBymyUU93SQAzX+EhKyDuNwClSEU", Amazon.RegionEndpoint.USEast1);
            var transferUtility = new TransferUtility(s3Client);

            string bucketName = "docspim4semestre";
            string objectKey = "Holerites/"+ mat + "/" +  Mes + "-" + Ano;

            using (var fileStream = new FileStream(caminhoLocalDaFoto, FileMode.Open))
            {
                transferUtility.Upload(fileStream, bucketName, objectKey);
            }

            MessageBox.Show("Holerite enviado para S3 com sucesso!");
        }

        static void EnviarEmailComAnexo(string destinatario, string caminhoAnexo)
        {
            try
            {
                Attachment anexo = new Attachment(caminhoAnexo);
                // Configuração do e-mail
                MailMessage mailMessage = new MailMessage();
                {
                    var smtpClient = new SmtpClient("smtp.outlook.com",587);
                    smtpClient.EnableSsl = true;
                    smtpClient.Timeout = 60 * 60;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential("reverton.carmo@aluno.unip.br","Pr@do@2002@");
                    mailMessage.From = new MailAddress("reverton.carmo@aluno.unip.br", "SisRH");
                    mailMessage.Body = "Envio de Holeite referente ao mes de";
                    mailMessage.Subject = "Envio Holerite";
                    mailMessage.IsBodyHtml = true;
                    mailMessage.Priority = MailPriority.Normal;
                    mailMessage.To.Add(destinatario);
                    mailMessage.Attachments.Add(anexo);
                    smtpClient.Send(mailMessage);
                    
                };

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao enviar e-mail: {ex.Message}");
            }
        }




    }



}
