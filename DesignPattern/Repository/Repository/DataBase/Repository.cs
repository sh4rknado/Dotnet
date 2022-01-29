using Dapper;
using Repository.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using Repository.Utils;
using System.Threading.Tasks;

namespace Repository
{
    public class Repository<T> where T : AModel
    {
        private readonly DBEngine dbEngine;
        private readonly IShowMessage showMessage;
        private IDbConnection db;
        private IDbTransaction tran;
        
        public Repository(DBEngine dbEngine,  IShowMessage showMessage = null)
        {
            this.dbEngine = dbEngine;
            this.showMessage = showMessage;
            CanExecute = TryGetDbConnection();
        }

        #region PROPERTIES
        
        public bool CanExecute { get; private set; }
        public bool isBusy { get; private set; }
        
        #endregion

        #region SYNC

        public void Insert(T model)
        {
            if (!CanExecute)
                return;

            string tableName = RepositoryUtils<T>.GetTableNameFromModel(model);
            string sql = $"INSERT INTO {tableName}";
            db.Query<T>(sql, model);
        }

        public void Delete(T model)
        {
            if (!CanExecute)
                return;

            string tableName = RepositoryUtils<T>.GetTableNameFromModel(model);            
            string sql = $"DELETE FROM {tableName} WHERE id = {model.ID}";
            db.Query<T>(sql, model);
        }

        public void Update(T model)
        {
            if (!CanExecute)
                return;

            string tableName = RepositoryUtils<T>.GetTableNameFromModel(model);
            string update = RepositoryUtils<T>.BuildUpdateRequest(model);
            string sql = $"UPDATE {tableName} {update} WHERE id = {model.ID}";
            db.Query<T>(sql, model);
        }


        public void InsertRange(IEnumerable<T> models)
        {
            if (!CanExecute)
                return;

            foreach (T model in models)
                Insert(model);
        }

        public void DeleteRange(IEnumerable<T> models)
        {
            if (!CanExecute)
                return;

            foreach (T model in models)
                Delete(model);
        }

        public void UpdateRange(IEnumerable<T> models)
        {
            if (!CanExecute)
                return;

            foreach (T model in models)
                Update(model);
        }

        #endregion

        #region ASYNC

        public async Task InsertAsync(T model)
        {
            if (!CanExecute)
                return;

            string tableName = RepositoryUtils<T>.GetTableNameFromModel(model);
            string sql = $"INSERT INTO {tableName}";
            await db.QueryAsync<T>(sql, model);
        }

        public async Task DeleteAsync(T model)
        {
            if (!CanExecute)
                return;

            string tableName = RepositoryUtils<T>.GetTableNameFromModel(model);
            string sql = $"DELETE FROM {tableName} WHERE id = {model.ID}";
            await db.QueryAsync<T>(sql, model);
        }

        public async Task UpdateAsync(T model)
        {
            if (!CanExecute)
                return;

            string tableName = RepositoryUtils<T>.GetTableNameFromModel(model);
            string update = RepositoryUtils<T>.BuildUpdateRequest(model);
            string sql = $"UPDATE {tableName} {update} WHERE id = {model.ID}";
            await db .QueryAsync<T>(sql, model);
        }


        public async Task InsertRangeAsync(IEnumerable<T> models)
        {
            if (!CanExecute)
                return;

            foreach (T model in models)
                await InsertAsync(model);
        }

        public async Task DeleteRangeAsync(IEnumerable<T> models)
        {
            if (!CanExecute)
                return;

            foreach (T model in models)
               await DeleteAsync(model);
        }

        public async Task UpdateRangeAsync(IEnumerable<T> models)
        {
            if (!CanExecute)
                return;

            foreach (T model in models)
               await UpdateAsync(model);
        }

        #endregion

        private bool TryGetDbConnection()
        {
            try
            {
                if (!this.dbEngine.TryGetDBConnection(out this.db))
                    throw new Exception("Unable to get the DBConnection");
                
                db.Open();
                tran = db.BeginTransaction();
                return true;
            }
            catch (Exception ex)
            {
                this.showMessage?.ShowError($"Error when Commit the transaction with the reason : {ex.Message}");
                return false;
            }
        }

        public bool TryCommit()
        {
            try
            {
                if (tran == null)
                    throw new Exception("transaction is null");

                tran.Commit();
                return true;
            }
            catch (Exception ex)
            {
                tran?.Rollback();
                this.showMessage?.ShowError($"Error when Commit the transaction with the reason : {ex.Message}");
                return false;
            }
        }

    }
}
