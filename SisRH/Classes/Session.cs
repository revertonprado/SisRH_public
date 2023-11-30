using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SisRH.Classes
{
    internal class Session
    {
        private static Session instance;
        private Session() { }

        public static Session Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Session();
                }
                return instance;
            }
        }
        public string Matricula { get; private set; }
        // Adicione outras propriedades de sessão, se necessário

        // Método para iniciar uma nova sessão
        public void IniciarSessao(string matricula)
        {
            Matricula = matricula;
            // Adicione outras inicializações de sessão, se necessário
        }

        // Método para encerrar a sessão
        public void EncerrarSessao()
        {
            Matricula = null;
            // Limpe outras variáveis de sessão, se necessário
        }
    }
}
