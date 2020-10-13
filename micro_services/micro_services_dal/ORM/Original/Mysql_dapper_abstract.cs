using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;
using Dapper.Contrib.Extensions;
using Dapper;
using System.Threading.Tasks;
using System.Linq;

namespace micro_services_dal
{
    public abstract class Mysql_dapper_abstract : IDisposable
    {

        MySqlConnection conn;
        MySqlTransaction tran;
        public const int timeoutSec = 9000;

        public void Dispose()
        {
            //throw new NotImplementedException();
        }

        #region Constructor
        public Mysql_dapper_abstract(string CONNstr)
        {
            conn = new MySqlConnection(CONNstr);
        }


        #endregion Construvtor


        #region BeginTransaction
        public void BeginTransaction()
        {
            checkConnection();
            tran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
        }
        #endregion
        #region Properties
        public bool IsUsingTransaction
        {
            get
            {
                return (tran != null) && (tran.Connection != null);
            }
        }
        #endregion
        #region Query
        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, int pageNumber = 0, int recordPerPage = 0) //where T : class
        {
            IEnumerable<T> result = null;
            //Pageing yapılırkennlazım olabilir
            //if ((pageNumber > 0) && (recordPerPage > 0))
            //    sql = sqliteconn.GetPagedSql(sql, pageNumber, recordPerPage);


            result = await conn.QueryAsync<T>(sql, param, tran, commandTimeout: timeoutSec);
            return result;
        }
        public IEnumerable<T> Query<T>(string sql, object param = null, int pageNumber = 0, int recordPerPage = 0) //where T : class
        {
            IEnumerable<T> result = null;
            //paging yapılırken lazım olabilir
            //if ((pageNumber > 0) && (recordPerPage > 0))
            //    sql = dbConnection.GetPagedSql(sql, pageNumber, recordPerPage);

            result = conn.Query<T>(sql, param, tran, commandTimeout: timeoutSec);
            return result;
        }
        public IEnumerable<T> QuerySP<T>(string storedProcedureName, object param = null) where T : class
        {
            IEnumerable<T> result = null;
            result = conn.Query<T>(storedProcedureName, param, tran, commandType: CommandType.StoredProcedure);
            return result;
        }
        #endregion
        #region Execute
        public async Task<int> ExecuteAsync(string sql, object param = null)
        {
            int result = -1;
            result = await conn.ExecuteAsync(sql, param, tran, commandTimeout: timeoutSec);
            return result;
        }
        public int Execute(string sql, object param = null)
        {
            int result = -1;
            result = conn.Execute(sql, param, tran);
            return result;
        }
        public int ExecuteSP(string storedProcedureName, object param = null)
        {
            int result = -1;
            result = conn.Execute(storedProcedureName, param, tran, commandType: CommandType.StoredProcedure);
            return result;
        }
        #endregion
        #region Count
        public int Count<T>() where T : class
        {
            //var tableName = typeof(T).GetTableName(); Method

            string tname = typeof(T).Name;
            var attr = typeof(T).CustomAttributes.FirstOrDefault(w => w.AttributeType == typeof(System.ComponentModel.DataAnnotations.Schema.TableAttribute));
            if (attr != null)
                tname = attr.ConstructorArguments.First().Value.ToString();

            string sql = string.Format("SELECT COUNT(*) FROM {0}", tname);
            return conn.QueryFirst<int>(sql, tran);
        }
        #endregion
        #region Get
        public T Get<T>(long id) where T : class
        {
            return conn.Get<T>(id, tran);
        }

        /// <summary>
        /// Bu metod henüz tamamlanmadı. Geçici olarak bu şekilde bırakıldı...
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> GetAll<T>(string whereclause = "1=1") where T : class
        {
            if (whereclause != "1=1")
            {
                string tname = typeof(T).Name;
                var attr = typeof(T).CustomAttributes.FirstOrDefault(w => w.AttributeType == typeof(System.ComponentModel.DataAnnotations.Schema.TableAttribute));
                if (attr != null)
                    tname = attr.ConstructorArguments.First().Value.ToString();

                string sql = string.Format("select * from {0} Where 1=1 And {1}", tname, whereclause);

                return conn.Query<T>(sql);
            }

            return conn.GetAll<T>(tran);
        }
        #endregion

        #region Insert
        public async Task<long> InsertAsync<T>(T entity) where T : class
        {
            long result = -1;
            result = await conn.InsertAsync(entity, tran, commandTimeout: timeoutSec);
            return result;
        }
        public async Task<long> InsertAsync<T>(IEnumerable<T> entities) where T : class
        {
            long result = -1;
            result = await conn.InsertAsync(entities, tran, commandTimeout: timeoutSec);
            return result;
        }

        public long Insert<T>(T entity) where T : class
        {
            long result = -1;
            result = conn.Insert(entity, tran);
            return result;
        }

        public long Insert<T>(IEnumerable<T> entities) where T : class
        {
            long result = -1;
            result = conn.Insert(entities, tran);
            return result;
        }
        #endregion
        #region Update
        public async Task<bool> UpdateAsync<T>(T entity) where T : class
        {
            bool result = false;
            result = await conn.UpdateAsync(entity, tran, commandTimeout: timeoutSec);
            return result;
        }
        public bool Update<T>(T entity) where T : class
        {
            bool result = false;

            //result += conn.Update(entity, tran);
            result = conn.Update<T>(entity, tran);
            return result;
        }
        #endregion
        #region Delete
        public async Task<bool> DeleteAsync<T>(T entity) where T : class
        {
            bool result = false;
            result = await conn.DeleteAsync(entity, tran, commandTimeout: timeoutSec);
            return result;
        }
        public bool Delete<T>(T entity) where T : class
        {
            bool result = false;
            result = conn.Delete(entity, tran);
            return result;
        }
        public bool Delete<T>(IEnumerable<T> entities) where T : class
        {
            bool result = false;
            result = conn.Delete(entities, tran);
            return result;
        }
        #endregion
        #region Commit & RollBack
        public void Commit(bool renewTransaction = false)
        {
            if (tran == null) return;
            tran.Commit();
            if (renewTransaction)
                tran = conn.BeginTransaction(IsolationLevel.ReadCommitted);
        }
        public void RollBack()
        {
            if (tran == null || tran.Connection == null) return;
            tran.Rollback();
        }
        #endregion

        private void checkConnection()
        {
            if (conn == null) return;
            if (conn.State == ConnectionState.Closed)
                conn.Open();
        }

    }
}
