using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisRH.Classes
{
    public class EstadoCivil
    {
        private string instrucaoSql;
        Classes.Conexao c = new Classes.Conexao();

        public DataSet ListarEstadoCivil()
        {
            try
            {
                //instrucaoSql = "SELECT * FROM CLIENTE WHERE COD_CLI='" + _CodCli;
                instrucaoSql = "select * from tbEstadoCivil";

                return c.RetornarDataSet(instrucaoSql);
            }

            catch (Exception ex)
            {

                throw ex;
            }
        }




    }
}
