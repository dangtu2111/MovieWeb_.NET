﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace MSSQL.Access
{
    public partial class SqlContext : IDisposable
    {
        private bool disposedValue;
        private SqlData sqlData;

        public SqlContext()
        {
            sqlData = new SqlData();
            try
            {
                sqlData.Connect();
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Lỗi khi kết nối đến Database: " + ex.Message);
                Console.WriteLine("🔍 Chi tiết lỗi: " + ex.ToString());
                sqlData.Dispose();
                sqlData = null;
                throw new Exception("Database connection error", ex);
            }

            disposedValue = false;
        }

        protected SqlAccess<T> InitSqlAccess<T>(ref SqlAccess<T> sqlAccess)
        {
            if (sqlAccess == null)
                sqlAccess = new SqlAccess<T>(sqlData);
            return sqlAccess;
        }

        protected void DisposeSqlAccess<T>(ref SqlAccess<T> sqlAccess)
        {
            if (sqlAccess != null)
            {
                sqlAccess = null;
            }
        }

        public int ExecuteNonQuery(string commandText, CommandType commandType, params SqlParameter[] sqlParameters)
        {
            if (string.IsNullOrEmpty(commandText))
                throw new Exception("@'commandText' must not be null or empty");

            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.CommandText = commandText;
                sqlCommand.CommandType = commandType;
                if (sqlParameters != null)
                    sqlCommand.Parameters.AddRange(sqlParameters);

                return sqlData.ExecuteNonQuery(sqlCommand);
            }
        }

        public object ExecuteScalar(string commandText, CommandType commandType, params SqlParameter[] sqlParameters)
        {
            if (string.IsNullOrEmpty(commandText))
                throw new Exception("@'commandText' must not be null or empty");

            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.CommandText = commandText;
                sqlCommand.CommandType = commandType;
                if (sqlParameters != null)
                    sqlCommand.Parameters.AddRange(sqlParameters);

                return sqlData.ExecuteScalar(sqlCommand);
            }
        }

        public object Execute_ToOriginalData(string commandText, CommandType commandType, params SqlParameter[] sqlParameters)
        {
            if (string.IsNullOrEmpty(commandText))
                throw new Exception("@'commandText' must not be null or empty");

            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.CommandText = commandText;
                sqlCommand.CommandType = commandType;
                if (sqlParameters != null)
                    sqlCommand.Parameters.AddRange(sqlParameters);

                return sqlData.ToOriginalData(sqlCommand);
            }
        }

        public T Execute_To<T>(string commandText, CommandType commandType, params SqlParameter[] sqlParameters)
        {
            if (string.IsNullOrEmpty(commandText))
                throw new Exception("@'commandText' must not be null or empty");

            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.CommandText = commandText;
                sqlCommand.CommandType = commandType;
                if (sqlParameters != null)
                    sqlCommand.Parameters.AddRange(sqlParameters);

                return sqlData.To<T>(sqlCommand);
            }
        }

        public List<T> Execute_ToList<T>(string commandText, CommandType commandType, params SqlParameter[] sqlParameters)
        {
            if (string.IsNullOrEmpty(commandText))
                throw new Exception("@'commandText' must not be null or empty");

            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.CommandText = commandText;
                sqlCommand.CommandType = commandType;
                if (sqlParameters != null)
                    sqlCommand.Parameters.AddRange(sqlParameters);

                return sqlData.ToList<T>(sqlCommand);
            }
        }

        public Dictionary<string, object> Execute_ToDictionary(string commandText, CommandType commandType, params SqlParameter[] sqlParameters)
        {
            if (string.IsNullOrEmpty(commandText))
                throw new Exception("@'commandText' must not be null or empty");

            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.CommandText = commandText;
                sqlCommand.CommandType = commandType;
                if (sqlParameters != null)
                    sqlCommand.Parameters.AddRange(sqlParameters);

                return sqlData.ToDictionary(sqlCommand);
            }
        }

        public List<Dictionary<string, object>> Execute_ToDictionaryList(string commandText, CommandType commandType, params SqlParameter[] sqlParameters)
        {
            if (string.IsNullOrEmpty(commandText))
                throw new Exception("@'commandText' must not be null or empty");

            using (SqlCommand sqlCommand = new SqlCommand())
            {
                sqlCommand.CommandText = commandText;
                sqlCommand.CommandType = commandType;
                if (sqlParameters != null)
                    sqlCommand.Parameters.AddRange(sqlParameters);

                return sqlData.ToDictionaryList(sqlCommand);
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    sqlData.Disconnect();
                    sqlData.Dispose();
                    sqlData = null;
                }
                disposedValue = true;
            }
        }

        ~SqlContext()
        {
            Dispose(disposing: false);
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
