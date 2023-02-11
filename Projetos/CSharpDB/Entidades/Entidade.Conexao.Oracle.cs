using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CsharpDBConnect.Entidades
{
    internal class ConexaoOracle : ABSConexaoBancoDeDados<Oracle.ManagedDataAccess.Client.OracleConnection>
    {
        public ConexaoOracle(string Host, int Port, string User, string Password)
        {
            this.stringConexao = string.Format(@"Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={0})(PORT={1})))
                    (CONNECT_DATA=(SERVICE_NAME=orcl))); User Id={2}; Password={3};Min Pool Size=1;Connection Lifetime=120;Connection Timeout=60;Incr Pool Size=1;Decr Pool Size=1;", Host, Port, User, Password);

            this.Conexao = new OracleConnection(this.stringConexao);
        }
    }
}
