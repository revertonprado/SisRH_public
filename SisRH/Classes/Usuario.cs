using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.CodeDom;

namespace SisRH.Classes
{
    public class Usuario
    {
        Classes.Conexao c = new Classes.Conexao();
        private static string instrucaoSql;


        private int Codigo;
        private string Usu;
        private int Matricula;
        private string Senha;
        private int fkfunc;
        private byte TrocaSenha;

        public int Codigo1 { get => Codigo; set => Codigo = value; }
        public string Usu1 { get => Usu; set => Usu = value; }
        public string Senha1 { get => Senha; set => Senha = value; }
        public int Fkfunc { get => fkfunc; set => fkfunc = value; }
        public int NivelAcesso { get => nivelAcesso; set => nivelAcesso = value; }
        public byte TrocaSenha1 { get => TrocaSenha; set => TrocaSenha = value; }
        public int Matricula1 { get => Matricula; set => Matricula = value; }

        private int nivelAcesso;

        public bool Logar()
        {
            try
            {
                instrucaoSql = "EXEC Logar_select '" + Usu1 + "', '" + Senha1 + "'";
                SqlDataReader dr;
                List<SqlParameter> parametros = new List<SqlParameter>();
                dr = c.RetornarDataReader(instrucaoSql);
                if (dr.HasRows == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                     
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool VerificaTrocaSenha()
        {
            try
            {
                instrucaoSql = "EXEC VerificaTrocaSenha '" + Usu1 + "', '" + Senha1 + "'";
                SqlDataReader dr;
                List<SqlParameter> parametros = new List<SqlParameter>();
                dr = c.RetornarDataReader(instrucaoSql);
                if (dr.HasRows == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataSet ListarUsuarios()
        {
            try
            {
                instrucaoSql = "EXEC ListaUusarios '" + Matricula1 + "'";
                return c.RetornarDataSet(instrucaoSql);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public void UpdateComParametro(int matricula, string senha)
        {
            try
            {
                instrucaoSql = "EXEC AlterarSenha'" + matricula + "', '" + senha +"'";
                c.ExecutarComando(instrucaoSql);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void LiberarSenha()
        {
            try
            {
                instrucaoSql = "EXEC LiberarTrocaSenha'" + Matricula1 + "'";
                c.ExecutarComando(instrucaoSql);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void DeletarUsuario()
        {
            try
            {
                instrucaoSql = "EXEC ExcluirUsuario'" + Matricula1 + "'";
                c.ExecutarComando(instrucaoSql);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void AtivarUsuario()
        {
            try
            {
                instrucaoSql = "EXEC AtivarUsuario'" + Matricula1 + "'";
                c.ExecutarComando(instrucaoSql);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void IncluirComParametro()
        {
            try
            {
                SqlParameter[] listaComParametros = {

                   new SqlParameter("@fkfunc",SqlDbType.Int) {Value = Fkfunc},
                   new SqlParameter("@nivelacesso",SqlDbType.Int) {Value = NivelAcesso},
                   new SqlParameter("@trocasenha",SqlDbType.Bit) {Value = 1},
                 

                };

                instrucaoSql = "INSERT INTO tbLogin(fk_func,nivel_Acesso_login,TrocaSenha) VALUES (@fkfunc,@nivelacesso,@trocasenha)";
                c.ExecutarComandoParametro(instrucaoSql, listaComParametros);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
}
