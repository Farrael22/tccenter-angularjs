using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
//using NLog;
using System.Linq;
using System.Text;
using System.Threading;
using balcao.offline.api.Domain.Enum;

namespace balcao.offline.api.Helpers.DataAccess
{
    public class TransactionHelperBaseGerente : TransactionHelperBase
    {
        public TransactionHelperBaseGerente()
            : base(ConfigurationManager.ConnectionStrings["CSMBLCGERDBS"].ConnectionString)
        {

        }
    }

    public abstract class TransactionHelperBase : IDisposable
    {
        //private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly ConfigCtor _configCtor;
        private readonly IEnumerable<string> _bancosExternos;

        protected TransactionHelperBase(string connectionString, IsolationLevel isolationLevel = IsolationLevel.ReadUncommitted, IEnumerable<string> bancosExternos = null)
        {
            _configCtor = new ConfigCtor();
            _configCtor.SetConnection(new SqlConnection(connectionString));
            _configCtor.GetConnection().Open();
            _configCtor.SetTransaction(_configCtor.GetConnection().BeginTransaction(isolationLevel));
            _bancosExternos = bancosExternos;
        }

        public T GetModel<T>()
        {
            return (T)Activator.CreateInstance(typeof(T), _configCtor);
        }

        public void Execute(Action action)
        {
            try
            {
                action();

                _configCtor.GetTransaction().Commit();
            }
            catch (Exception)
            {
                _configCtor.GetTransaction().Rollback();
                throw;
            }
            finally
            {
                Dispose();
            }
        }

        public int Execute(string sql, object param = null)
        {
            try
            {
                var retorno = _configCtor.GetConnection().Execute(sql, param, _configCtor.GetTransaction());
                _configCtor.GetTransaction().Commit();
                return retorno;
            }
            catch (Exception ex)
            {
                _configCtor.GetTransaction().Rollback();
                LogarErro(sql, param, ex);
                throw;
            }
            finally
            {
                Dispose();
            }
        }

        public int ExecuteWithoutCommit(string sql, object param = null)
        {
            try
            {
                return _configCtor.GetConnection().Execute(sql, param, _configCtor.GetTransaction());
            }
            catch (Exception ex)
            {
                _configCtor.GetTransaction().Rollback();
                LogarErro(sql, param, ex);
                throw;
            }
        }

        public TRetorno ExecuteScalar<TRetorno>(string sql, object param = null)
        {
            try
            {
                var retorno = _configCtor.GetConnection().ExecuteScalar<TRetorno>(RecuperarQuery(sql), param, _configCtor.GetTransaction());
                _configCtor.GetTransaction().Commit();
                return retorno;
            }
            catch (Exception ex)
            {
                _configCtor.GetTransaction().Rollback();
                LogarErro(RecuperarQuery(sql), param, ex);
                throw;
            }
            finally
            {
                Dispose();
            }
        }

        public TRetorno ExecuteScalarWithoutCommit<TRetorno>(string sql, object param = null)
        {
            try
            {
                var retorno = _configCtor.GetConnection().ExecuteScalar<TRetorno>(RecuperarQuery(sql), param, _configCtor.GetTransaction());
                return retorno;
            }
            catch (Exception ex)
            {
                _configCtor.GetTransaction().Rollback();
                LogarErro(RecuperarQuery(sql), param, ex);
                throw;
            }
        }

        public IEnumerable<TRetorno> Query<TRetorno>(string sql, object param = null, int? tentativa = null)
        {
            try
            {
                return _configCtor.GetConnection().Query<TRetorno>(RecuperarQuery(sql), param, _configCtor.GetTransaction());
            }
            catch (SqlException ex)
            {
                if (ex.Number == Convert.ToInt32(SqlExceptionEnum.FatalErrorException)
                    && ex.Class == Convert.ToInt32(SqlExceptionEnum.FatalErrorClassException))
                {
                    if (!tentativa.HasValue)
                        tentativa = 0;

                    if (tentativa.HasValue && tentativa.Value < 3)
                    {
                        Thread.Sleep(1000);
                        return Query<TRetorno>(sql, param, tentativa++);
                    }
                }
                LogarErro(RecuperarQuery(sql), param, ex);
                throw ex;
            }
            catch (Exception ex)
            {
                LogarErro(RecuperarQuery(sql), param, ex);
                throw ex;
            }
        }

        public IEnumerable<TRetorno> Query<TFirst, TSecond, TRetorno>(string sql, Func<TFirst, TSecond, TRetorno> map, string splitOn, object param = null)
        {
            try
            {
                return _configCtor.GetConnection().Query(RecuperarQuery(sql), map, param, _configCtor.GetTransaction());
            }
            catch (Exception ex)
            {
                LogarErro(RecuperarQuery(sql), param, ex);
                throw;
            }
        }

        public IEnumerable<TRetorno> Query<TFirst, TSecond, TThird, TRetorno>(string sql, Func<TFirst, TSecond, TThird, TRetorno> map, string splitOn, object param = null)
        {
            try
            {
                return _configCtor.GetConnection().Query(RecuperarQuery(sql), map, param, _configCtor.GetTransaction(), splitOn: splitOn);
            }
            catch (Exception ex)
            {
                LogarErro(RecuperarQuery(sql), param, ex);
                throw;
            }
        }

        public TRetorno QueryFirstOrDefault<TRetorno>(string sql, object param = null)
        {
            try
            {
                return _configCtor.GetConnection().QueryFirstOrDefault<TRetorno>(RecuperarQuery(sql), param, _configCtor.GetTransaction());
            }
            catch (Exception ex)
            {
                LogarErro(RecuperarQuery(sql), param, ex);
                throw;
            }
        }

        public TRetorno QueryFirstOrDefault<TFirst, TSecond, TRetorno>(string sql, Func<TFirst, TSecond, TRetorno> map, string splitOn, object param = null)
        {
            try
            {
                return _configCtor.GetConnection().Query(RecuperarQuery(sql), map, param, _configCtor.GetTransaction()).FirstOrDefault();
            }
            catch (Exception ex)
            {
                LogarErro(RecuperarQuery(sql), param, ex);
                throw;
            }
        }

        public TRetorno QueryFirstOrDefault<TFirst, TSecond, TThird, TRetorno>(string sql, Func<TFirst, TSecond, TThird, TRetorno> map, string splitOn, object param = null)
        {
            try
            {
                return _configCtor.GetConnection().Query(RecuperarQuery(sql), map, param, _configCtor.GetTransaction(), splitOn: splitOn).FirstOrDefault();
            }
            catch (Exception ex)
            {
                LogarErro(RecuperarQuery(sql), param, ex);
                throw;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~TransactionHelperBase()
        {
            Dispose(false);
        }
        public ConfigCtor getConfig()
        {
            return _configCtor;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && _configCtor.GetConnection() != null)
            {
                _configCtor.GetConnection().Close();
                _configCtor.GetConnection().Dispose();
            }
        }

        private string RecuperarQuery(string sql)
        {
            if (_bancosExternos != null)
            {
                foreach (var banco in _bancosExternos)
                    sql = string.Format(sql, banco);
            }
            return sql;
        }

        private void LogarErro(string sql, object param, Exception ex)
        {
            //Logger.Error(new
            //{
            //    Sql = sql,
            //    Parametros = SimpleJson.SimpleJson.SerializeObject(param),
            //    Erro = ex
            //});
        }
    }
}