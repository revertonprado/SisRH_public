using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace SisRH.Classes
{
    public class Conexao
    {
        private static SqlConnection cn;
       
        private static SqlCommand cmd;
        
        private static SqlDataReader dr;
        
        private static SqlDataAdapter da;
        
        private static DataSet ds;
        
        private static DataTable dt;
        
        private static string instrucaoSql;
        
        private static string stringConexao = "server=pim4semestreteste.database.windows.net;database=SisRH;user id=masteruserteste;password=!;";

        public static SqlConnection ConectarBanco()
        {
            try
            {
                cn = new SqlConnection(stringConexao);
               
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                }
               
                return cn;
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void FecharBanco(SqlConnection minhaConexao)
        {
            try
            {
                if (minhaConexao.State == ConnectionState.Open)
                {
                    minhaConexao.Close();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static string infoBanco()
        {
            try
            {
                string msg = "";
                
                cn = new SqlConnection(stringConexao);
                if (cn.State == ConnectionState.Closed)
                {
                    cn.Open();
                    msg = "versao " + cn.ServerVersion + " fonte de dados " + cn.DataSource + ".";

                }
                return msg;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public SqlDataReader RetornarDataReader(string instrucaoSelecionar)
        {
            try
            {
                cmd = new SqlCommand(instrucaoSelecionar, ConectarBanco());
                
                dr = cmd.ExecuteReader();
                
                return dr;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable RetornarDataTable(string instrucao)
        {
            try
            {
                dt = new DataTable();
                cmd = new SqlCommand(instrucao, ConectarBanco());
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataSet RetornarDataSet(string instrucao)
        {
            try
            {
                ds = new DataSet();
                cmd = new SqlCommand(instrucao, ConectarBanco());
                da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                return ds;
                

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ExecutarComando(string instrucao)
        {
            try
            {
                
                cmd = new SqlCommand(instrucao, ConectarBanco());
                cmd.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Int32 RetornarContagem(string instrucao)
        {
            try
            {
                
                cmd = new SqlCommand(instrucao, ConectarBanco());
                return Convert.ToInt32(cmd.ExecuteScalar());
               


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public double RetornarTotal(string instrucao)
        {
            try
            {
                cmd = new SqlCommand(instrucao, ConectarBanco());
                
                return Convert.ToDouble(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public double RetornarMenorValor(string instrucao)
        {
            try
            {
                cmd = new SqlCommand(instrucao, ConectarBanco());
                
                return Convert.ToDouble(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public double RetornarMaiorValor(string instrucao)
        {
            try
            {
                cmd = new SqlCommand(instrucao, ConectarBanco());
                
                return Convert.ToDouble(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int ObterNumeroAutomaticoInserir(string instrucao)
        {
            try
            {
                cmd = new SqlCommand();
                cmd.CommandText = instrucao;
                
                cmd.CommandType = CommandType.Text;
                
                cmd.Connection = ConectarBanco();
                cmd.ExecuteNonQuery();
                cmd.CommandText = "Select @@Identity";
                
                dr = cmd.ExecuteReader();
                dr.Read();
                return Convert.ToInt32(dr[0]);
                

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public SqlParameter CriarParametro(string nomeParametro, SqlDbType tipoParametro, object valorParametro)
        {
            try
            {
                SqlParameter p = new SqlParameter();
                
                p.ParameterName = nomeParametro;
                
                p.SqlDbType = tipoParametro;
                
                if ((valorParametro == null) || (tipoParametro == SqlDbType.Char && valorParametro.ToString().Length == 0))
                {
                    p.Value = DBNull.Value;
                    
                }
                else
                {
                    p.Value = valorParametro;
                    
                }
                return p;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ExecutarComandoParametro(string instrucao, SqlParameter[] listaParametros)
        {
            try
            {
                cmd = new SqlCommand();
                cmd.CommandText = instrucao;
                
                cmd.CommandType = CommandType.Text;
                
                if (listaParametros != null)
                {
                    
                    foreach (SqlParameter item in listaParametros)
                    {
                        cmd.Parameters.Add(item);
                        
                    }
                }
                cmd.Connection = ConectarBanco();
                
                cmd.ExecuteNonQuery();
                

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ExecutarStoredProcedureParametro(string nomeProcedure, SqlParameter[] listaParametros)
        {
            try
            {
                
                cmd = new SqlCommand();
                cmd.CommandText = nomeProcedure;
                cmd.CommandType = CommandType.StoredProcedure;
                if (listaParametros != null)
                {
                    foreach (SqlParameter item in listaParametros)
                    {
                        cmd.Parameters.Add(item);
                    }
                }
                cmd.Connection = ConectarBanco();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
}
