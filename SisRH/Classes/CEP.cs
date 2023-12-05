using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Data.SqlTypes;

namespace SisRH.Classes
{
    internal class CEP
    {
        public string instrucaoSql;
        Classes.Conexao c = new Classes.Conexao();

        int Id_cep;
        string Cep;
        string Cidade;
        string Bairro;
        string UF;

        public int Id_cep1 { get => Id_cep; set => Id_cep = value; }
        public string Cep1 { get => Cep; set => Cep = value; }
        public string Cidade1 { get => Cidade; set => Cidade = value; }
        public string Bairro1 { get => Bairro; set => Bairro = value; }
        public string UF1 { get => UF; set => UF = value; }

        public SqlDataReader Consultar()
        {
            try
            {
                instrucaoSql = "SELECT  ID_CEP, CEP, UF, CIDADE, LOGRADOURO, BAIRRO FROM CEP WHERE CEP = '" + Cep1 + "'";
         
                return c.RetornarDataReader(instrucaoSql);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



    }
}
