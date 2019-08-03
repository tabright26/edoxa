// Filename: InMemoryDbContextFactory.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Data.Common;

using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace eDoxa.Seedwork.Infrastructure.Factories
{
    public sealed class InMemoryDbContextFactory<TDbContext> : IDisposable
    where TDbContext : DbContext
    {
        private const string ConnectionString = "DataSource=:memory:";

        private DbConnection? _connection;

        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
        }

        public TDbContext CreateContext()
        {
            if (_connection == null)
            {
                _connection = new SqliteConnection(ConnectionString);
                _connection.Open();

                using var context = this.CreateInstance();

                context.Database.EnsureCreated();
            }

            return this.CreateInstance();
        }

        private TDbContext CreateInstance()
        {
            return (TDbContext) Activator.CreateInstance(typeof(TDbContext), this.CreateOptions());
        }

        private DbContextOptions<TDbContext> CreateOptions()
        {
            return new DbContextOptionsBuilder<TDbContext>().UseSqlite(_connection).Options;
        }
    }
}
