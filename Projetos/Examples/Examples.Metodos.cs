using CsharpDBConnect.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Examples
{
    /// <summary>
    /// Classe com Metodos de Exemplo
    /// </summary>
    public class ExampleMetodo : IDisposable
    {
        public List<Pessoa> Consultar(PessoaParametros Parametro)
        {
            try
            {
                using (BancoDeDadosOracle Comando = new BancoDeDadosOracle())
                {
                    Comando.ComandoSQL = @"SELECT Id, Nome FROM Pessoa";

                    if (Parametro != null)
                    {
                        if (Parametro.Id != null)
                            Comando.ComandoSQLIncluirWhereAnd = string.Concat("Id = ", Parametro.Id);

                        if (Parametro.Nome != null)
                            Comando.ComandoSQLIncluirWhereAnd = string.Concat("Nome = ", Parametro.Nome);
                    }

                    return Comando.ObterLista<Pessoa>();
                }
            }
            catch (Exception Problema)
            {
                throw Problema;
            }
        }

        public void Dispose() => GC.SuppressFinalize(this);
    }
}
