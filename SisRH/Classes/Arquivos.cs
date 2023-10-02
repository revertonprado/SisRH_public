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
    internal class Arquivos
    {
        public string instrucaoSql;
        Classes.Conexao c = new Classes.Conexao();

        int id_arq;
        string caminho_arq;
        string tipo_arq;
        string tipo_doc_arq;
        byte st_ativo;
        int fk_func;

        public int Id_arq { get => id_arq; set => id_arq = value; }
        public string Caminho_arq { get => caminho_arq; set => caminho_arq = value; }
        public string Tipo_arq { get => tipo_arq; set => tipo_arq = value; }
        public string Tipo_doc_arq { get => tipo_doc_arq; set => tipo_doc_arq = value; }
        public byte St_ativo { get => st_ativo; set => st_ativo = value; }
        public int Fk_func { get => fk_func; set => fk_func = value; }

        public void IncluirComParametro()
        {
            try
            {
                SqlParameter[] listaComParametros = {

                   new SqlParameter("@caminho",SqlDbType.VarChar) {Value = Caminho_arq},
                   new SqlParameter("@tipo_arq",SqlDbType.VarChar) {Value = Tipo_arq},
                   new SqlParameter("@tipo_doc_arq",SqlDbType.VarChar) {Value = Tipo_doc_arq},
                   new SqlParameter("@st_ativo_arq",SqlDbType.Int) {Value = St_ativo},
                   new SqlParameter("@fk_func",SqlDbType.Int) {Value = Fk_func},

                };

                instrucaoSql = "INSERT INTO tbArquivos VALUES (@caminho, @tipo_arq, @tipo_doc_arq, @st_ativo_arq, @fk_func)";
                c.ExecutarComandoParametro(instrucaoSql, listaComParametros);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
}
