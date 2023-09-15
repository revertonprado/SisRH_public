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
        private string Senha;

        public int Codigo1 { get => Codigo; set => Codigo = value; }
        public string Usu1 { get => Usu; set => Usu = value; }
        public string Senha1 { get => Senha; set => Senha = value; }

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


    }
}
