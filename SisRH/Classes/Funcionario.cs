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
    public class Funcionario
    {
        public string instrucaoSql;
        Classes.Conexao c = new Classes.Conexao();

       
        string Sobrenome_Func;
        string UltimoNome_Func;
        string PrimeroNome_Func;
        string NomeConjunge_Func;
        int Matricula_Func;
        DateTime? Data_Nasc_Func;
        int Data_Nasc_FuncInt;
        byte Sexo_Func;
        string Raca_Func;
        string Tipo_Sang_Func;
        string Nm_Mae_Func;
        string Nm_Pai_Func;
        string EstCivil_Func;
        string CidadeNasc_Func;
        int Num_Res_Func;
        string Compl_Func;
        string Tipo_Res_Func;
        string Cel_Func;
        byte Whatsapp_func;
        string Tel_Func;
        string Email_Func;
        string Email_Corp_Func;
        string Num_Agenc_Func;
        string Num_Conta_Func;
        int Fk_Cargo;
        int Fk_Cep;
        int Fk_Banco;
        string CPF_Func;
        string Rg_Func;
        DateTime Dt_Emissao_Func;
        string Orgao_Emissor_Func;
        string Reservista_Func;
        string Titulo_Eleitor_Func;
        string Zona_Eleitoral_Func;
        string Sessao_Eleitoral_Func;
        string Cidade_Eleitoral_Func;
        string UF_Eleitoral_Func;
        string Escolaridade_Func;
        string CNS_Func;
        byte Cert_Nasc_Func;
        byte Cert_Casament_Func;
        string Comprov_Residencia_Func;
        string Nacionalidade_Func;
        int St_Status_Func;
        int Fk_Dep;
        string Jornada_Func;

        public string Sobrenome_Func1 { get => Sobrenome_Func; set => Sobrenome_Func = value; }
        public string UltimoNome_Func1 { get => UltimoNome_Func; set => UltimoNome_Func = value; }
        public string PrimeroNome_Func1 { get => PrimeroNome_Func; set => PrimeroNome_Func = value; }
        public string NomeConjunge_Func1 { get => NomeConjunge_Func; set => NomeConjunge_Func = value; }
        public int Matricula_Func1 { get => Matricula_Func; set => Matricula_Func = value; }
        
        public byte Sexo_Func1 { get => Sexo_Func; set => Sexo_Func = value; }
        public string Raca_Func1 { get => Raca_Func; set => Raca_Func = value; }
        public string Tipo_Sang_Func1 { get => Tipo_Sang_Func; set => Tipo_Sang_Func = value; }
        public string Nm_Mae_Func1 { get => Nm_Mae_Func; set => Nm_Mae_Func = value; }
        public string Nm_Pai_Func1 { get => Nm_Pai_Func; set => Nm_Pai_Func = value; }
        public string EstCivil_Func1 { get => EstCivil_Func; set => EstCivil_Func = value; }
        public string CidadeNasc_Func1 { get => CidadeNasc_Func; set => CidadeNasc_Func = value; }
        public int Num_Res_Func1 { get => Num_Res_Func; set => Num_Res_Func = value; }
        public string Compl_Func1 { get => Compl_Func; set => Compl_Func = value; }
        public string Tipo_Res_Func1 { get => Tipo_Res_Func; set => Tipo_Res_Func = value; }
        public string Cel_Func1 { get => Cel_Func; set => Cel_Func = value; }
        public byte Whatsapp_func1 { get => Whatsapp_func; set => Whatsapp_func = value; }
        public string Tel_Func1 { get => Tel_Func; set => Tel_Func = value; }
        public string Email_Func1 { get => Email_Func; set => Email_Func = value; }
        public string Email_Corp_Func1 { get => Email_Corp_Func; set => Email_Corp_Func = value; }
        public string Num_Agenc_Func1 { get => Num_Agenc_Func; set => Num_Agenc_Func = value; }
        public string Num_Conta_Func1 { get => Num_Conta_Func; set => Num_Conta_Func = value; }
        public int Fk_Cargo1 { get => Fk_Cargo; set => Fk_Cargo = value; }
        public int Fk_Cep1 { get => Fk_Cep; set => Fk_Cep = value; }
        public int Fk_Banco1 { get => Fk_Banco; set => Fk_Banco = value; }
        public string CPF_Func1 { get => CPF_Func; set => CPF_Func = value; }
        public string Rg_Func1 { get => Rg_Func; set => Rg_Func = value; }
        public DateTime Dt_Emissao_Func1 { get => Dt_Emissao_Func; set => Dt_Emissao_Func = value; }
        public string Orgao_Emissor_Func1 { get => Orgao_Emissor_Func; set => Orgao_Emissor_Func = value; }
        public string Reservista_Func1 { get => Reservista_Func; set => Reservista_Func = value; }
        public string Titulo_Eleitor_Func1 { get => Titulo_Eleitor_Func; set => Titulo_Eleitor_Func = value; }
        public string Zona_Eleitoral_Func1 { get => Zona_Eleitoral_Func; set => Zona_Eleitoral_Func = value; }
        public string Sessao_Eleitoral_Func1 { get => Sessao_Eleitoral_Func; set => Sessao_Eleitoral_Func = value; }
        public string Cidade_Eleitoral_Func1 { get => Cidade_Eleitoral_Func; set => Cidade_Eleitoral_Func = value; }
        public string UF_Eleitoral_Func1 { get => UF_Eleitoral_Func; set => UF_Eleitoral_Func = value; }
        public string Escolaridade_Func1 { get => Escolaridade_Func; set => Escolaridade_Func = value; }
        public string CNS_Func1 { get => CNS_Func; set => CNS_Func = value; }
        public byte Cert_Nasc_Func1 { get => Cert_Nasc_Func; set => Cert_Nasc_Func = value; }
        public byte Cert_Casament_Func1 { get => Cert_Casament_Func; set => Cert_Casament_Func = value; }
        public string Comprov_Residencia_Func1 { get => Comprov_Residencia_Func; set => Comprov_Residencia_Func = value; }
        public string Nacionalidade_Func1 { get => Nacionalidade_Func; set => Nacionalidade_Func = value; }
        public int St_Status_Func1 { get => St_Status_Func; set => St_Status_Func = value; }
        public int Fk_Dep1 { get => Fk_Dep; set => Fk_Dep = value; }
        public int Data_Nasc_FuncInt1 { get => Data_Nasc_FuncInt; set => Data_Nasc_FuncInt = value; }
        public string Jornada_Func1 { get => Jornada_Func; set => Jornada_Func = value; }
        public DateTime? Data_Nasc_Func1 { get => Data_Nasc_Func; set => Data_Nasc_Func = value; }

        public void IncluirComParametro()
        {
            try
            {
                SqlParameter[] listaComParametros = {

                   new SqlParameter("@primeironome",SqlDbType.VarChar) {Value = PrimeroNome_Func1},
                   new SqlParameter("@sobrenome",SqlDbType.VarChar) {Value = Sobrenome_Func1},
                   new SqlParameter("@ultimonome",SqlDbType.VarChar) {Value = UltimoNome_Func1},
                   new SqlParameter("@nomeconjunge",SqlDbType.VarChar) {Value = NomeConjunge_Func1},
                   new SqlParameter("@matricula",SqlDbType.Int) {Value = Matricula_Func1},
                   new SqlParameter("@DataNasc",SqlDbType.Date) {Value = Data_Nasc_Func1},
                   new SqlParameter("@sexofunc",SqlDbType.Bit) {Value = Sexo_Func1},
                   new SqlParameter("@raca",SqlDbType.VarChar) {Value = Raca_Func1},
                   new SqlParameter("@tiposangue",SqlDbType.VarChar) {Value = Tipo_Sang_Func1},
                   new SqlParameter("@nomemae",SqlDbType.VarChar) {Value = Nm_Mae_Func1},
                   new SqlParameter("@nomepai",SqlDbType.VarChar) {Value = Nm_Pai_Func1},
                   new SqlParameter("@estadocivil",SqlDbType.VarChar) {Value = EstCivil_Func1},
                   new SqlParameter("@cidadenasc",SqlDbType.VarChar) {Value = CidadeNasc_Func1},
                   new SqlParameter("@numres",SqlDbType.Int) {Value = Num_Res_Func1},
                   new SqlParameter("@compl",SqlDbType.VarChar) {Value = Compl_Func1},
                   new SqlParameter("@tipores",SqlDbType.VarChar) {Value = Tipo_Res_Func1},
                   new SqlParameter("@cel",SqlDbType.VarChar) {Value = Cel_Func1},
                   new SqlParameter("@whatsapp",SqlDbType.Bit) {Value = Whatsapp_func1},
                   new SqlParameter("@tel",SqlDbType.VarChar) {Value = Tel_Func1},
                   new SqlParameter("@email",SqlDbType.VarChar) {Value = Email_Func1},
                   new SqlParameter("@emailcorp",SqlDbType.VarChar) {Value = Email_Corp_Func1},
                   new SqlParameter("@numagenc",SqlDbType.VarChar) {Value = Num_Agenc_Func1},
                   new SqlParameter("@numconta",SqlDbType.VarChar) {Value = Num_Conta_Func1},
                   new SqlParameter("@fkcargo",SqlDbType.Int) {Value = Fk_Cargo1},
                   new SqlParameter("@fkbanco",SqlDbType.Int) {Value = Fk_Banco1},
                   new SqlParameter("@cpf",SqlDbType.VarChar) {Value = CPF_Func1},
                   new SqlParameter("@rg",SqlDbType.VarChar) {Value = Rg_Func1},
                   new SqlParameter("@dataemissao",SqlDbType.Date) {Value = Dt_Emissao_Func1},
                   new SqlParameter("@orgaoe",SqlDbType.VarChar) {Value = Orgao_Emissor_Func1},
                   new SqlParameter("@reservista",SqlDbType.VarChar) {Value = Reservista_Func1},
                   new SqlParameter("@titeleitor",SqlDbType.VarChar) {Value = Titulo_Eleitor_Func1},
                   new SqlParameter("@zonaele",SqlDbType.VarChar) {Value = Zona_Eleitoral_Func1},
                   new SqlParameter("@sessaoele",SqlDbType.VarChar) {Value = Sessao_Eleitoral_Func1},
                   new SqlParameter("@cidadeele",SqlDbType.VarChar) {Value = Cidade_Eleitoral_Func1},
                   new SqlParameter("@escolaridade",SqlDbType.VarChar) {Value = Escolaridade_Func1},
                   new SqlParameter("@cns",SqlDbType.VarChar) {Value = CNS_Func1},
                   new SqlParameter("@certnasc",SqlDbType.Bit) {Value = Cert_Nasc_Func1},
                   new SqlParameter("@certCas",SqlDbType.Bit) {Value = Cert_Casament_Func1},
                   new SqlParameter("@comprovanteres",SqlDbType.VarChar) {Value = Comprov_Residencia_Func1},
                   new SqlParameter("@nacionalidade",SqlDbType.VarChar) {Value = Nacionalidade_Func1},
                   new SqlParameter("@status",SqlDbType.Int) {Value = 1},
                   new SqlParameter("@fkdep",SqlDbType.Int){Value = Fk_Dep1},
                   new SqlParameter("@fkcep",SqlDbType.Int){Value = Fk_Cep1},
                   new SqlParameter("@jornada",SqlDbType.VarChar){Value = Jornada_Func1}

                };

                instrucaoSql = "INSERT INTO tbFuncionario VALUES (@primeironome ,@sobrenome, @ultimonome, @matricula, @DataNasc, @sexofunc ,@raca, @tiposangue, @nomemae,@nomepai,@estadocivil,@nomeconjunge,@cidadenasc,@numres,@compl,@tipores,@cel,@whatsapp,@tel,@email,@emailcorp,@numagenc,@numconta,@fkcargo,@fkbanco,@cpf,@rg,@dataemissao,@orgaoe,@reservista,@titeleitor,@zonaele,@sessaoele,@cidadeele,@escolaridade,@cns,@certnasc,@certCas,@comprovanteres,@nacionalidade,@status, @fkdep, @fkcep, @jornada, 1)";
                c.ExecutarComandoParametro(instrucaoSql, listaComParametros);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public SqlDataReader ListarUltimo()
        {
            try
            {
                instrucaoSql = "select max(id_func)as id_func from tbFuncionario";
                return c.RetornarDataReader(instrucaoSql);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public SqlDataReader ListarUltimoMatricula()
        {
            try
            {
                instrucaoSql = "select matricula_func, primeiro_nm_func from tbfuncionario where id_func = (select max(id_func)as id_func from tbFuncionario)";
                return c.RetornarDataReader(instrucaoSql);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public DataSet ListarFunc()
        {
            try
            {
                instrucaoSql = "EXEC ListarFunc_Select '" + Matricula_Func1 + "', '" + PrimeroNome_Func1 + "','" + CPF_Func1 + "','" + Data_Nasc_Func1 + "','" + Data_Nasc_FuncInt1 + "','" + Fk_Cargo1 + "','" + Fk_Dep1 +"','" + Jornada_Func1 + "'";
                return c.RetornarDataSet(instrucaoSql);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }





    }
}
