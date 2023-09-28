using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisRH.Classes
{
    public class Cargo
    {
        private string instrucaoSql;
        Classes.Conexao c = new Classes.Conexao();

        public DataSet ListarCargos()
        {
            try
            {
                //instrucaoSql = "SELECT * FROM CLIENTE WHERE COD_CLI='" + _CodCli;
                instrucaoSql = "select * from tbCargo";

                return c.RetornarDataSet(instrucaoSql);
            }

            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataSet ListarSalario(int cod)
        {
            try
            {
                //instrucaoSql = "SELECT * FROM CLIENTE WHERE COD_CLI='" + _CodCli;
                instrucaoSql = "select salario_cargo from tbCargo where id_cargo =" + cod;

                return c.RetornarDataSet(instrucaoSql);
            }

            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataSet ListarDepartamento(int codcargo)
        {
            try
            {
                //instrucaoSql = "SELECT * FROM CLIENTE WHERE COD_CLI='" + _CodCli;
                instrucaoSql = "select nome_dep from tbDepartamento d with(nolock)\r\n" +
                    "inner join tbCargoFunc cf with(nolock)\r\n" +
                    "on cf.fk_depart = d.id_dep\r\n" +
                    "inner join tbCargo c with(nolock)\r\n" +
                    "on c.id_cargo = cf.fk_cargo\r\nwhere c.id_cargo = '" + codcargo +"'";

                return c.RetornarDataSet(instrucaoSql);
            }

            catch (Exception ex)
            {

                throw ex;
            }
        }



    }
}
