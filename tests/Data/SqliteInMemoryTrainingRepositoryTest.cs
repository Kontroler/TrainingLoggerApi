using System;
using System.Data.Common;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using TrainingLogger.API.Data;


namespace Tests.Data
{
    public class SqliteInMemoryTrainingRepositoryTest: TrainingRepositoryTest, IDisposable
    {
         private readonly DbConnection _connection;

        public SqliteInMemoryTrainingRepositoryTest()
            : base(
                new DbContextOptionsBuilder<DataContext>()
                    .UseSqlite(CreateInMemoryDatabase())
                    .Options)
        {
            _connection = RelationalOptionsExtension.Extract(ContextOptions).Connection;
        }

        private static DbConnection CreateInMemoryDatabase()
        {
            var connection = new SqliteConnection("Filename=:memory:");

            connection.Open();

            return connection;
        }

        public void Dispose() => _connection.Dispose();
    }
}