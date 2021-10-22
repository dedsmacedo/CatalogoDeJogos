using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICatalogoDeJogos.Exceptions
{
    public class JogoNaoCadastradoExpection : Exception
    {
        public JogoNaoCadastradoExpection()
            : base("este jogo não está cadastrado") 
        { }
    }
}
