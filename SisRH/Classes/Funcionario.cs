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
    public class Funcionario
    {
        public string instrucaoSql;
        Classes.Conexao c = new Classes.Conexao();

        int id_func;

        int fk_end;
        string cpf_func;
        int fk_contato;
        int fk_contabancaria;
        int fk_cor;
        byte status_func;
        string foto_func;
        byte sexo_func;
        int fk_vt;
        int fk_ec;
        int fk_tiposanguineo;
        int Matricula_func;
        string Sobrenome_Func;
        string UltimoNome_Func;
        string PrimeroNome_Func;
        string NomeConjunge_func;

        public int Id_func { get => id_func; set => id_func = value; }
        public int Fk_end { get => fk_end; set => fk_end = value; }
        public string Cpf_func { get => cpf_func; set => cpf_func = value; }
        public int Fk_contato { get => fk_contato; set => fk_contato = value; }
        public int Fk_contabancaria { get => fk_contabancaria; set => fk_contabancaria = value; }
        public int Fk_cor { get => fk_cor; set => fk_cor = value; }
        public byte Status_func { get => status_func; set => status_func = value; }
        public string Foto_func { get => foto_func; set => foto_func = value; }
        public byte Sexo_func { get => sexo_func; set => sexo_func = value; }
        public int Fk_vt { get => fk_vt; set => fk_vt = value; }
        public int Fk_ec { get => fk_ec; set => fk_ec = value; }
        public int Fk_tiposanguineo { get => fk_tiposanguineo; set => fk_tiposanguineo = value; }
        public int Matricula_func1 { get => Matricula_func; set => Matricula_func = value; }
        public string Sobrenome_Func1 { get => Sobrenome_Func; set => Sobrenome_Func = value; }
        public string UltimoNome_Func1 { get => UltimoNome_Func; set => UltimoNome_Func = value; }
        public string PrimeroNome_Func1 { get => PrimeroNome_Func; set => PrimeroNome_Func = value; }
        public string NomeConjunge_func1 { get => NomeConjunge_func; set => NomeConjunge_func = value; }

        

       




    }
}
