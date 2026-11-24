using System;
using System.Collections.Generic;
using System.Text;

namespace Cadastro_de_Jogos
{
    internal class Jogo
    {
        public string titulo = "";
        public string console = "";
        public int ano;
        public int ranking;
        public Emprestimo emprestimo = new Emprestimo();
    }
}
