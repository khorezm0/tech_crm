﻿using System.Data;
using Microsoft.Extensions.Options;
using Npgsql;
using TC.AspNetCore.Configurations;
using TC.DAL.Abstractions.Data;

namespace TC.DAL.Data
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;
        
        public DbConnectionFactory(IOptions<ConnectionStrings> connectionStringOptions)
        {
            _connectionString = connectionStringOptions.Value.DefaultConnection;
        }

        public IDbConnection Open()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}
