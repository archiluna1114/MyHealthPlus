using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class DapperRepository
    {
        #region Constructors
        public DapperRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetValue<string>("DatabaseConnection:ConnectionString");
        }

        #region Fields
        private string connectionString;
        #endregion

        #region Private Methods
        public DbConnection CreateDbConnection
        {
            get
            {
                return new SqlConnection(connectionString);
            }
        }
        #endregion

        #region Protected Methods
        #region Connection Helpers
        /// <summary>
        /// Connection helper method for accessing the database asynchronously
        /// that is used for use for buffered queries that return a type
        /// </summary>
        /// <typeparam name="T">Result data type</typeparam>
        /// <param name="getData">Delegate that mandates how data is retrieved</param>
        /// <returns>Asynchronous data accessing task with the data results</returns>
        protected async Task<T> WithConnection<T>(Func<IDbConnection, Task<T>> getData)
        {
            using (var connection = CreateDbConnection)
            {
                await connection.OpenAsync();
                return await getData(connection);
            }
        }

        /// <summary>
        /// Connection helper method for accessing the database asynchronously
        /// that is used for buffered queries that do not return a type
        /// </summary>
        /// <param name="getData">Delegate that mandates that data access</param>
        /// <returns>Asynchronous data accessing task</returns>
        protected async Task WithConnection(Func<IDbConnection, Task> getData)
        {
            using (var connection = CreateDbConnection)
            {
                await connection.OpenAsync();
                await getData(connection);
            }
        }

        protected async Task<TResult> WithConnection<TRead, TResult>(
            Func<IDbConnection,
            Task<TRead>> getData,
            Func<TRead, Task<TResult>> process)
        {
            using (var connection = CreateDbConnection)
            {
                await connection.OpenAsync();
                var data = await getData(connection);
                return await process(data);
            }
        }
        #endregion

        #region  Querying and CRUD Helpers
        /// <summary>
        /// Method used for executing a stored procedure in the database without parameters
        /// </summary>
        /// <param name="storedProcedureName">Stored procedure name</param>
        protected void Execute(string storedProcedureName)
        {
            Execute(storedProcedureName, null);
        }

        /// <summary>
        /// Method used for executing a stored procedure with parameters
        /// </summary>
        /// <param name="storedProcedureName">Stored procedure name</param>
        /// <param name="parameterSet">Parameters</param>
        protected void Execute(string storedProcedureName, object parameterSet)
        {
            using (var dbConnection = CreateDbConnection)
            {
                dbConnection.Execute(
                    storedProcedureName,
                    parameterSet,
                    commandType: CommandType.StoredProcedure,
                    transaction: null
                );
                dbConnection.Close();
            }
        }

        /// <summary>
        /// Method used for executing a stored procedure in the database asynchronously without parameters
        /// </summary>
        /// <param name="storedProcedureName">Stored procedure name</param>
        /// <returns>Asynchronous data accessing task</returns>
        protected async Task ExecuteAsync(string storedProcedureName)
        {
            await ExecuteAsync(storedProcedureName, null);
        }

        /// <summary>
        /// Method used for executing a stored procedure asynchronously with parameters
        /// </summary>
        /// <param name="storedProcedureName">Stored procedure name</param>
        /// <param name="parameterSet">Parameters</param>
        /// <returns>Asynchronous data accessing task</returns>
        protected async Task ExecuteAsync(string storedProcedureName, object parameterSet)
        {
            await WithConnection(async c =>
            {
                await c.ExecuteAsync(
                            storedProcedureName,
                            parameterSet,
                            commandType: CommandType.StoredProcedure
                        );
            });
        }

        /// <summary>
        /// Method used for querying the database for a single value without parameters
        /// </summary>
        /// <typeparam name="T">Result data type</typeparam>
        /// <param name="storedProcedureName">Stored procedure name</param>
        /// <returns>Value result</returns>
        public T QueryScalar<T>(string storedProcedureName)
        {
            return QueryScalar<T>(storedProcedureName, null);
        }

        /// <summary>
        /// Method used for querying the database for a single value with parameters
        /// </summary>
        /// <typeparam name="T">Result data type</typeparam>
        /// <param name="storedProcedureName">Stored procedure name</param>
        /// <param name="parameterSet">Parameters</param>
        /// <returns></returns>
        public T QueryScalar<T>(string storedProcedureName, object parameterSet)
        {
            using (var dbConnection = CreateDbConnection)
            {
                return dbConnection.Query<T>(
                           storedProcedureName,
                           parameterSet,
                           commandType: CommandType.StoredProcedure,
                           commandTimeout: 120
                       ).FirstOrDefault();
            }
        }

        /// <summary>
        /// Method used for querying the database asynchronously for a single value without parameters
        /// </summary>
        /// <typeparam name="T">Result data type</typeparam>
        /// <param name="storedProcedureName">Stored procedure name</param>
        /// <returns>Asynchronous data accessing task with the value result</returns>
        protected async Task<T> QueryScalarAsync<T>(string storedProcedureName)
        {
            return await QueryScalarAsync<T>(storedProcedureName, null);
        }

        /// <summary>
        /// Method used for querying the database asynchronously for a single value with parameters
        /// </summary>
        /// <typeparam name="T">Result data type</typeparam>
        /// <param name="storedProcedureName">Stored procedure name</param>
        /// <param name="parameterSet">Parameters</param>
        /// <returns>Asynchronous data accessing task with the value result</returns>
        protected async Task<T> QueryScalarAsync<T>(string storedProcedureName, object parameterSet)
        {
            return await WithConnection(async c =>
            {
                var result = await c.QueryAsync<T>(
                                         storedProcedureName,
                                         parameterSet,
                                         commandType: CommandType.StoredProcedure
                                     );

                return result.FirstOrDefault();
            });
        }

        /// <summary>
        /// Method used for querying the database for a list without parameters
        /// </summary>
        /// <typeparam name="T">Result data type</typeparam>
        /// <param name="storedProcedureName">Stored procedure name</param>
        /// <returns>List of result value</returns>
        public List<T> QueryList<T>(string storedProcedureName)
        {
            return QueryList<T>(storedProcedureName, null);
        }

        /// <summary>
        /// Method used for querying the database for a list with parameters
        /// </summary>
        /// <typeparam name="T">Result data type</typeparam>
        /// <param name="storedProcedureName">Stored procedure name</param>
        /// <param name="parameterSet">Parameters</param>
        /// <returns>List of result value</returns>
        public List<T> QueryList<T>(string storedProcedureName, object parameterSet)
        {
            using (var dbConnection = CreateDbConnection)
            {
                return dbConnection.Query<T>(
                           storedProcedureName,
                           parameterSet,
                           commandType: CommandType.StoredProcedure,
                           commandTimeout: 180
                       ).ToList();
            }
        }

        /// <summary>
        /// Method used for querying the database for a list with dynamic parameters
        /// </summary>
        /// <typeparam name="T">Result data type</typeparam>
        /// <param name="storedProcedureName">Stored procedure name</param>
        /// <param name="parameterSet">Parameters</param>
        /// <returns>List of result value</returns>
        public List<T> QueryList<T>(string storedProcedureName, DynamicParameters parameterSet)
        {
            using (var dbConnection = CreateDbConnection)
            {
                return dbConnection.Query<T>(
                           storedProcedureName,
                           parameterSet,
                           commandType: CommandType.StoredProcedure,
                           commandTimeout: 120
                       ).ToList();
            }
        }

        /// <summary>
        /// Method used for querying the database asynchronously for a list without parameters
        /// </summary>
        /// <typeparam name="T">Result data type</typeparam>
        /// <param name="storedProcedureName">Stored procedure name</param>
        /// <returns>Asynchronous data accessing task with the list of result value</returns>
        protected async Task<List<T>> QueryListAsync<T>(string storedProcedureName)
        {
            return await QueryListAsync<T>(storedProcedureName, null);
        }

        /// <summary>
        /// Method used for querying the database asynchronously for a list with parameters
        /// </summary>
        /// <typeparam name="T">Result data type</typeparam>
        /// <param name="storedProcedureName">Stored procedure name</param>
        /// <param name="parameterSet">Parameters</param>
        /// <returns>Asynchronous data accessing task with the list of result value</returns>
        protected async Task<List<T>> QueryListAsync<T>(string storedProcedureName, object parameterSet)
        {
            return await WithConnection(async c =>
            {
                var result = await c.QueryAsync<T>(
                                         storedProcedureName,
                                         parameterSet,
                                         commandType: CommandType.StoredProcedure
                                     );

                return result.ToList();
            });
        }
        #endregion
        #endregion
        #endregion
    }
}
