using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Examples
{
    /// <summary>
    /// Classe Representando a Entidade Pessoa.
    /// </summary>
    public class Pessoa
    {
        #region Atributos
        private int _Id;
        private string _Nome;
        #endregion

        #region Propriedades
        public int Id { get => _Id; set => _Id = value; }
        public string Nome { get => _Nome; set => _Nome = value; }
        #endregion

        #region Metodos
        public override bool Equals(object obj)
        {
            return obj is Pessoa pessoa &&
                   _Id == pessoa._Id;
        }
        public override int GetHashCode()
        {
            return 310941579 + _Id.GetHashCode();
        }
        #endregion
    }

}
