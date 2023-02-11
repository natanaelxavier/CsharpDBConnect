using CsharpDBConnect.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Examples
{
    /// <summary>
    /// Classe com Metodo de Exemplo do uso da Biblioteca de conexão.
    /// </summary>
    public class Main
    {
        public void Exemplo()
        {
            using(ExampleMetodo Metodos = new ExampleMetodo())
            {
                List<Pessoa> ListaPessoas = Metodos.Consultar(new PessoaParametros(1));
            }
        }
    }
}
