using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace SisRH.Classes
{
    internal class Departamento
    {
        public string instrucaoSql;
        Classes.Conexao c = new Classes.Conexao();

        int if_dep;
        string nome_dep;
        int fk_chefe_dep;

        public int If_dep { get => if_dep; set => if_dep = value; }
        public string Nome_dep { get => nome_dep; set => nome_dep = value; }
        public int Fk_chefe_dep { get => fk_chefe_dep; set => fk_chefe_dep = value; }

        public DataSet ListarDepartamentos()
        {
            try
            {
                //instrucaoSql = "SELECT * FROM CLIENTE WHERE COD_CLI='" + _CodCli;
                instrucaoSql = "select * from tbDepartamento";

                return c.RetornarDataSet(instrucaoSql);
            }

            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
}
