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

public class Program
{
    public string Nome;
    public int Matricula;
    public DateTime DtHoje = DateTime.Now;
    public int Ano = DateTime.Now.Year;
    public int Mes = DateTime.Now.Month;
    public int Dia = DateTime.Now.Day;
    public int Horas;
    public int Extras;
    public decimal INSS;
    public decimal IRPF;
    public decimal Salario_Bruto;
    public decimal Salario;
    public void GerarPDFH(int mat)
    {

        FolhaPonto fp = new FolhaPonto();

        fp.Mes_fp1 = Mes;
        fp.Ano_fp1 = Ano;


        System.Data.SqlClient.SqlDataReader ddr;
        ddr = fp.ListarFolhaPagamentoDR(40896);
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
        }

        Document document = new Document();
        PdfWriter.GetInstance(document, new FileStream("Holerite-"+ Matricula+ Mes+"/"+Ano+".pdf", FileMode.Create));
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

       // Process.Start("chrome.exe", @"C:\caminho\para\seu\arquivo.pdf");


    }
}
