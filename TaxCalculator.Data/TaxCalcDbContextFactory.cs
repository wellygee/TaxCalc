using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace TaxCalculator.Data
{
    // private readonly ILogger<SqlConnectionFactory> logger;

    //public SqlConnectionFactory(IConfiguration rootConfiguration/*, ILogger<SqlConnectionFactory> logger*/)
    //{
    //    this.rootConfiguration = rootConfiguration;
    //    // this.logger = logger;
    //}

    //public SqlConnection GetConnection(string key)
    //{
    //    if (string.IsNullOrWhiteSpace(key))
    //    {
    //        key = DefaultConnectionKey;
    //    }

    //    // var connectionString = this.rootConfiguration[$"repository:connections:{key}"];
    //    var connectionString = this.rootConfiguration[$"Db:Connections:{key}"];

    //    if (string.IsNullOrWhiteSpace(connectionString))
    //    {
    //        throw new InvalidOperationException($"A connection could not be found for the key {key}");
    //    }

    //    return new SqlConnection(connectionString);
    //}

    public class TaxCalcDbContextFactory : ITaxCalcDbContextFactory, IDisposable
    {
        public TaxCalcDbContextFactory(IOptions<DbContextSettings> settings)
        {
            var conn = "Server=(localdb)\\MSSQLLocalDB;Database=TaxCalc;User Id=taxCal-App;password=egk{a{petshsskzvasj{tgkimsFT7_&#$!~<d|hop9U`thab;";
            //var options = new DbContextOptionsBuilder<TaxCalcDbContext>().UseSqlServer(
            //    // "Db:Connections:taxCalc"
            //    // settings.Value.DbConnectionString

            //    ).Options;
            DbContext = new TaxCalcDbContext();// options);
        }

        ~TaxCalcDbContextFactory()
        {
            Dispose();
        }

        public TaxCalcDbContext DbContext { get; private set; }


        public void Dispose()
        {
            DbContext?.Dispose();
        }

        //public SqlConnection GetConnection(string key)
        //{
        //    if (string.IsNullOrWhiteSpace(key))
        //    {
        //        key = DefaultConnectionKey;
        //    }

        //    var connectionString = this.rootConfiguration[$"Db:Connections:{key}"];

        //    if (string.IsNullOrWhiteSpace(connectionString))
        //    {
        //        throw new InvalidOperationException($"A connection could not be found for the key {key}");
        //    }

        //    if (!connectionString.Contains("{SQLSERVER}"))
        //    {
        //        return new SqlConnection(connectionString);
        //    }

        //    var sqlServer = Environment.GetEnvironmentVariable("SQLSERVER");

        //    if (string.IsNullOrWhiteSpace(sqlServer))
        //    {
        //        sqlServer = "localhost";
        //    }

        //    connectionString = connectionString.Replace("{SQLSERVER}", sqlServer);

        //    this.logger.Info(connectionString);

        //    return new SqlConnection(connectionString);
        //}
    }
}