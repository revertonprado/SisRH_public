using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisRH.Classes
{
    internal class Bancos
    {
        private string instrucaoSql;
        Classes.Conexao c = new Classes.Conexao();

        public DataSet ListarBancos()
        {
            try
            {
                
                instrucaoSql = "select * from tbbancos";

                return c.RetornarDataSet(instrucaoSql);
            }

            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
