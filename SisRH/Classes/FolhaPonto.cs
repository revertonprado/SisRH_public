using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Data.SqlTypes;
using System.Security.Cryptography;

namespace SisRH.Classes
{
    internal class FolhaPonto
    {

        public string instrucaoSql;
        Classes.Conexao c = new Classes.Conexao();
        Classes.Funcionario f = new Classes.Funcionario();

        private int id_fp;
        private int fk_func;
        private DateTime dt_apont_fp;
        private TimeSpan hr_apont_fp_E1;
        private byte atraso_fp;
        private byte adv_fp;
        private string desc_adv_fp;
        private string lat_fp;
        private string long_fp;
        private int fk_arq;
        private int AlterarFolha;
        private TimeSpan hr_apont_fp_E2;
        private TimeSpan hr_apont_fp_S1;
        private TimeSpan hr_apont_fp_S2;
        private TimeSpan hr_apont_fp_E3;
        private TimeSpan hr_apont_fp_S3;
        private int Mes_fp;
        private int Ano_fp;
        private int Dia_fp;
        private int Matricula_fp;

        public int Id_fp { get => id_fp; set => id_fp = value; }
        public int Fk_func { get => fk_func; set => fk_func = value; }
        public DateTime Dt_apont_fp { get => dt_apont_fp; set => dt_apont_fp = value; }
        public TimeSpan Hr_apont_fp_E1 { get => hr_apont_fp_E1; set => hr_apont_fp_E1 = value; }
        public byte Atraso_fp { get => atraso_fp; set => atraso_fp = value; }
        public byte Adv_fp { get => adv_fp; set => adv_fp = value; }
        public string Desc_adv_fp { get => desc_adv_fp; set => desc_adv_fp = value; }
        public string Lat_fp { get => lat_fp; set => lat_fp = value; }
        public string Long_fp { get => long_fp; set => long_fp = value; }
        public int Fk_arq { get => fk_arq; set => fk_arq = value; }
        public int AlterarFolha1 { get => AlterarFolha; set => AlterarFolha = value; }
        public TimeSpan Hr_apont_fp_E2 { get => hr_apont_fp_E2; set => hr_apont_fp_E2 = value; }
        public TimeSpan Hr_apont_fp_S1 { get => hr_apont_fp_S1; set => hr_apont_fp_S1 = value; }
        public TimeSpan Hr_apont_fp_S2 { get => hr_apont_fp_S2; set => hr_apont_fp_S2 = value; }
        public TimeSpan Hr_apont_fp_E3 { get => hr_apont_fp_E3; set => hr_apont_fp_E3 = value; }
        public TimeSpan Hr_apont_fp_S3 { get => hr_apont_fp_S3; set => hr_apont_fp_S3 = value; }
        public int Mes_fp1 { get => Mes_fp; set => Mes_fp = value; }
        public int Ano_fp1 { get => Ano_fp; set => Ano_fp = value; }
        public int Dia_fp1 { get => Dia_fp; set => Dia_fp = value; }
        public int Matricula_fp1 { get => Matricula_fp; set => Matricula_fp = value; }

        public DataSet ListarFolhaPonto(int func)
        {
            try
            {
                instrucaoSql = "EXEC ConsultarFolhaPonto'" + func + "', '" + Mes_fp1 + "','" + Dia_fp1 + "','" + Ano_fp1 + "'";
                return c.RetornarDataSet(instrucaoSql);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public SqlDataReader ListarFolhaPagamentoDR(int func)
        {
            try
            {
                instrucaoSql = "EXEC ListarFolhaPagamento'" + Mes_fp + "', '" + Ano_fp1 + "','" + func + "'";
                return c.RetornarDataReader(instrucaoSql);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public DataSet ListarFolhaPagamento(int func)
        {
            try
            {
                instrucaoSql = "EXEC ListarFolhaPagamento'" + Mes_fp + "', '" + Ano_fp1 + "','" + func + "'";
                return c.RetornarDataSet(instrucaoSql);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool VerificarFolhaPonto(int mes, int ano)
        {
            try
            {
                instrucaoSql = "select * from tbFolhaPonto where AlterarFolha_fp = 1 and mes_fp = '" + mes + "' and ano_fp = '"+ ano +"'";

                if (c.RetornarDataReader(instrucaoSql).HasRows == false)
                {
                    return false;
                }
                return true;
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool VerificarFolhaPagamento(int mes, int ano)
        {
            try
            {
                instrucaoSql = "select * from FolhaPagamentoCompleta where mes_fpgc = '" + mes + "' and ano_fpgc = '" + ano + "'";

                if (c.RetornarDataReader(instrucaoSql).HasRows == false)
                {
                    return false;
                }
                return true;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



        public void UpdateComParametro(int id, TimeSpan e1, TimeSpan e2, TimeSpan e3, TimeSpan s1, TimeSpan s2, TimeSpan s3, string obs)
        {
            try
            {
                instrucaoSql = "EXEC AtualizarFolhaPonto'" + id + "', '" + e1 + "','" + e2 + "','" + e3 + "','" + s1 + "','" + s2 + "','" + s3 +"','" + obs + "'";
                c.ExecutarComando(instrucaoSql);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void FecharFolhaPonto(int mes, int ano)
        {
            try
            {
                instrucaoSql = "EXEC FecharFolhaPonto'" + mes + "', '" + ano +"'";
                c.ExecutarComando(instrucaoSql);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void GerarFolhaPagamento(int mes, int ano)
        {
            try
            {
                instrucaoSql = "EXEC GerarFolhaPagamentov2'" + ano + "', '" + mes + "'";
                c.ExecutarComando(instrucaoSql);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
}
