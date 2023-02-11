using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CsharpDBConnect.Entidades
{
    public abstract class ABSBancoDeDadosComando<T> where T : DbCommand, IDisposable
    {
        internal T Comando;
        bool IncrementarWhere, IncrementarAnd;
        public string ComandoSQL
        {
            get { return this.Comando.CommandText; }
            set { this.Comando.CommandText = value; }
        }
        public string ComandoSQLIncluirWhereAnd
        {
            set
            {
                string novoComando = string.Empty;
                if (this.IncrementarWhere) { novoComando = " WHERE "; this.IncrementarWhere = false; }
                if (this.IncrementarAnd) { novoComando = " AND "; }

                novoComando += string.Concat(" ", value);
                this.ComandoSQL += novoComando;
                this.IncrementarAnd = true;
            }
        }

        public ABSBancoDeDadosComando()
        {
            this.IncrementarWhere = true;
            this.IncrementarAnd = false;
        }

        public virtual DataTable ObterDataTable()
        {
            try
            {
                using (DbDataReader leitor = Comando.ExecuteReader())
                {
                    if (this.Comando.Connection.State != ConnectionState.Open)
                        this.Comando.Connection.Open();

                    DataTable tb = new DataTable();
                    tb.Load(leitor);
                    return tb;
                }
            }
            catch (Exception Problema)
            {
                throw Problema;
            }
        }
        public virtual List<C> ObterLista<C>(params string[] members)
        {
            try
            {
                using (DataTable dt = ObterDataTable())
                {
                    var nomesColunas = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName.ToLower()).ToList();
                    var propriedades = typeof(C).GetProperties();
                    return dt.AsEnumerable().Select(row =>
                    {
                        var objT = Activator.CreateInstance<C>();
                        foreach (var pro in propriedades)
                        {
                            if (nomesColunas.Contains(pro.Name.ToLower()))
                            {
                                try
                                {
                                    if (pro.PropertyType.IsEnum)
                                    {
                                        Type type = pro.PropertyType;
                                        foreach (string name in Enum.GetNames(type))
                                        {
                                            object dado = row[pro.Name.ToUpper()];
                                            Type columnType = dt.Columns[pro.Name].DataType;

                                            if (columnType == typeof(string) || columnType == typeof(char))
                                                if (dado.ToString().Length.Equals(1))
                                                    columnType = typeof(char);

                                            pro.SetValue(objT, Enum.ToObject(pro.PropertyType, Convert.ChangeType(dado, columnType)), null);

                                        }
                                    }
                                    else
                                    {
                                        pro.SetValue(objT, row[pro.Name], null);
                                    }
                                }
                                catch (Exception Problema) { throw Problema; }
                            }
                        }
                        return objT;
                    }).ToList();
                }
            }
            catch (Exception Problema)
            {
                throw Problema;
            }
        }
        public virtual bool Atualizar()
        {
            try
            {
                if (this.Comando.Connection.State != ConnectionState.Open)
                    this.Comando.Connection.Open();

                return (this.Comando.ExecuteNonQuery() >= 1);
            }
            catch (Exception Problema)
            {
                throw Problema;
            }
        }
        public virtual object Executar(string NomeProcedure)
        {
            try
            {
                if (this.Comando.Connection.State != ConnectionState.Open)
                    this.Comando.Connection.Open();

                this.Comando.CommandType = CommandType.StoredProcedure;
                this.Comando.CommandText = NomeProcedure;
                this.Comando.ExecuteNonQuery();

                string parametroRetorno = string.Empty;
                foreach (DbParameter dbp in this.Comando.Parameters)
                    if (dbp.Direction == ParameterDirection.InputOutput)
                        parametroRetorno = dbp.ParameterName;

                return this.Comando.Parameters[parametroRetorno].Value;
            }
            catch (Exception Problema)
            {
                throw Problema;
            }
        }
        public virtual void Parametro(string Chave, object Valor, bool ParametroDeRetorno = false)
        {
            try
            {
                DbParameter dbp = this.Comando.CreateParameter();
                dbp.ParameterName = Chave;
                dbp.Value = Valor;
                dbp.DbType = EscontrarTipoParametro(Valor.GetType());

                if (ParametroDeRetorno)
                    dbp.Direction = ParameterDirection.InputOutput;

                this.Comando.Parameters.Add(dbp);
            }
            catch (Exception Problema)
            {
                throw Problema;
            }
        }
        public virtual void LimparParametros() => this.Comando.Parameters.Clear();

        private DbType EscontrarTipoParametro(Type Tipo)
        {
            try
            {
                Dictionary<Type, DbType> typeMap = new Dictionary<Type, DbType>();
                typeMap[typeof(byte)] = DbType.Byte;
                typeMap[typeof(sbyte)] = DbType.SByte;
                typeMap[typeof(short)] = DbType.Int16;
                typeMap[typeof(ushort)] = DbType.UInt16;
                typeMap[typeof(int)] = DbType.Int32;
                typeMap[typeof(uint)] = DbType.UInt32;
                typeMap[typeof(long)] = DbType.Int64;
                typeMap[typeof(ulong)] = DbType.UInt64;
                typeMap[typeof(float)] = DbType.Single;
                typeMap[typeof(double)] = DbType.Double;
                typeMap[typeof(decimal)] = DbType.Decimal;
                typeMap[typeof(bool)] = DbType.Boolean;
                typeMap[typeof(string)] = DbType.String;
                typeMap[typeof(char)] = DbType.StringFixedLength;
                typeMap[typeof(Guid)] = DbType.Guid;
                typeMap[typeof(DateTime)] = DbType.DateTime;
                typeMap[typeof(DateTimeOffset)] = DbType.DateTimeOffset;
                typeMap[typeof(byte[])] = DbType.Binary;
                typeMap[typeof(byte?)] = DbType.Byte;
                typeMap[typeof(sbyte?)] = DbType.SByte;
                typeMap[typeof(short?)] = DbType.Int16;
                typeMap[typeof(ushort?)] = DbType.UInt16;
                typeMap[typeof(int?)] = DbType.Int32;
                typeMap[typeof(uint?)] = DbType.UInt32;
                typeMap[typeof(long?)] = DbType.Int64;
                typeMap[typeof(ulong?)] = DbType.UInt64;
                typeMap[typeof(float?)] = DbType.Single;
                typeMap[typeof(double?)] = DbType.Double;
                typeMap[typeof(decimal?)] = DbType.Decimal;
                typeMap[typeof(bool?)] = DbType.Boolean;
                typeMap[typeof(char?)] = DbType.StringFixedLength;
                typeMap[typeof(Guid?)] = DbType.Guid;
                typeMap[typeof(DateTime?)] = DbType.DateTime;
                typeMap[typeof(DateTimeOffset?)] = DbType.DateTimeOffset;
                return typeMap[Tipo];
            }
            catch (Exception Problema)
            {
                throw Problema;
            }
        }

        public virtual void Dispose() => GC.SuppressFinalize(this);
    }
}
