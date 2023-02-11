using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CsharpDBConnect.Entidades
{
    public class BancoDeDadosOracle : ABSBancoDeDadosComando<Oracle.ManagedDataAccess.Client.OracleCommand>, IDisposable
    {
        public BancoDeDadosOracle() : base()
        {
            this.Comando = new Oracle.ManagedDataAccess.Client.OracleCommand();
            this.Comando.Connection = new ConexaoOracle("127.0.0.1", 1234, "usuario", "senha").GetConexao();
        }
        public BancoDeDadosOracle(string ComandoSQL) : this()
        {
            this.ComandoSQL = ComandoSQL;
        }
    }
}
