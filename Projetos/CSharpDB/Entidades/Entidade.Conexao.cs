using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace CsharpDBConnect.Entidades
{
    internal abstract class ABSConexaoBancoDeDados<T> where T : DbConnection
    {
        internal string stringConexao { get; set; }
        internal T Conexao { get; set; }

        public virtual T GetConexao()
        {
            if (this.Conexao.State != System.Data.ConnectionState.Open)
                this.Conexao.Open();

            return this.Conexao;
        }
    }
}
