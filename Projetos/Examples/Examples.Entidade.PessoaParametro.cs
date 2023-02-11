using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Examples
{
    /// <summary>
    /// Classe representando os parametros de pesquisa de uma pessoa.
    /// </summary>
    public class PessoaParametros
    {
        public object Id, Nome;

        public PessoaParametros(object id)
        {
            Id = id;
        }
        public PessoaParametros(object id, object nome) : this(id)
        {
            Nome = nome;
        }
    }
}
