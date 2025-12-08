using ActivitiesAndTasksAPI.Enums;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;

namespace ActivitiesAndTasksAPI.Data
{
    public class AppDbContext : DbContext
    {
        private DbConnection DbConnection => this.Database.GetDbConnection();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Low-level helper: executes a stored procedure and returns a DbDataReader.
        /// Caller must dispose the reader.
        /// </summary>
        public async Task<DbDataReader> DbExecuteReaderAsync(
            SPs spEnum,
            List<SqlParameter>? parameters = null)
        {
            var connection = DbConnection;

            if (connection.State != ConnectionState.Open)
                await connection.OpenAsync();

            var command = connection.CreateCommand();
            command.CommandText = spEnum.GetStringValue();
            command.CommandType = CommandType.StoredProcedure;

            if (parameters != null)
            {
                foreach (var p in parameters)
                {
                    command.Parameters.Add(p);
                }
            }

            // Connection will be closed when reader is disposed
            var reader = await command.ExecuteReaderAsync(CommandBehavior.CloseConnection);
            return reader;
        }

        /// <summary>
        /// Maps result set to List&lt;T&gt; using a mapper function.
        /// </summary>
        public async Task<List<T>> DbExecuteReaderMapAsync<T>(
            SPs spEnum,
            List<SqlParameter>? parameters,
            Func<DbDataReader, T> map)
        {
            var results = new List<T>();

            await using var reader = await DbExecuteReaderAsync(spEnum, parameters);

            while (await reader.ReadAsync())
            {
                results.Add(map(reader));
            }

            return results;
        }

        /// <summary>
        /// Executes a stored procedure that does not return rows (INSERT/UPDATE/DELETE).
        /// Returns affected rows count.
        /// </summary>
        public async Task<int> DbExecuteNonQueryAsync(
            SPs spEnum,
            List<SqlParameter>? parameters = null)
        {
            var connection = DbConnection;

            if (connection.State != ConnectionState.Open)
                await connection.OpenAsync();

            await using var command = connection.CreateCommand();
            command.CommandText = spEnum.GetStringValue();
            command.CommandType = CommandType.StoredProcedure;

            if (parameters != null)
            {
                foreach (var p in parameters)
                {
                    command.Parameters.Add(p);
                }
            }

            var affected = await command.ExecuteNonQueryAsync();

            if (connection.State == ConnectionState.Open)
                await connection.CloseAsync();

            return affected;
        }

        /// <summary>
        /// Executes a stored procedure and returns a single scalar value.
        /// </summary>
        public async Task<object?> DbExecuteScalarAsync(
            SPs spEnum,
            List<SqlParameter>? parameters = null)
        {
            var connection = DbConnection;

            if (connection.State != ConnectionState.Open)
                await connection.OpenAsync();

            await using var command = connection.CreateCommand();
            command.CommandText = spEnum.GetStringValue();
            command.CommandType = CommandType.StoredProcedure;

            if (parameters != null)
            {
                foreach (var p in parameters)
                {
                    command.Parameters.Add(p);
                }
            }

            var result = await command.ExecuteScalarAsync();

            if (connection.State == ConnectionState.Open)
                await connection.CloseAsync();

            return result is DBNull ? null : result;
        }
    }
}
