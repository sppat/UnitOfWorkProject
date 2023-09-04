using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;
using UnitOfWork.Application.Interfaces.Repositories;
using UnitOfWork.Infrastructure.TypeHandlers;

namespace UnitOfWork.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDbConnection _connection;
        private IDbTransaction _transaction;
        private IProductRepository _productRepository;

        public UnitOfWork(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Default");

            SqlMapper.AddTypeHandler(new ProductIdTypeHandler());

            _connection = new NpgsqlConnection(connectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public IProductRepository ProductRepository => _productRepository ??= new ProductRepository(_connection);

        public void Dispose() =>_connection.Dispose();

        public void Save()
        {
            try
            {
                _transaction.Commit();
            }
            catch (Exception ex)
            {
                _transaction.Rollback();
                throw new Exception(ex.Message);
            }
            finally
            { 
                _connection.Dispose();
            }
        }

        public static async Task MigrateAsync(IConfiguration configuration)
        {
            await CreateDatabase(configuration.GetConnectionString("Initial")!);
            await CreateTable(configuration.GetConnectionString("Default")!);
        }

        public static async Task CreateTable(string connectionString)
        {
            using var connection = new NpgsqlConnection(connectionString);

            const string query = @"
                CREATE TABLE IF NOT EXISTS Products (
                    Id UUID PRIMARY KEY,
                    Name VARCHAR(200),
                    Description VARCHAR(200)
                );
            ";

            await connection.ExecuteAsync(query);
        }

        public static async Task CreateDatabase(string connectionString)
        {
            using var connection = new NpgsqlConnection(connectionString);
            
            const string dbCountQuery = @"
                SELECT COUNT(*)
                FROM pg_database
                WHERE datname = 'UnitOfWorkExample'
            ";

            var dbCount = await connection.ExecuteScalarAsync<int>(dbCountQuery);

            if (dbCount is default(int)) 
            {
                const string query = "CREATE DATABASE \"UnitOfWorkExample\"";

                await connection.ExecuteAsync(query);
            }
        }
    }
}
