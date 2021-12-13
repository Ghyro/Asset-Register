using System;
using System.Data.SqlClient;
using System.Data;


namespace Platform.API.Utilities
{
    using Dapper;    

    internal static class DbPreparer
    {
        const string SQL_CREATE_TABLE = 
            "IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Platform' and xtype='U')" +
            "BEGIN" +
            "    CREATE TABLE [dbo].Platform(" +
            "            [Id] [int] IDENTITY(1,1) NOT NULL," +
            "            [Title] [nvarchar](250) NOT NULL," +
            "            [Publisher] [nvarchar](100) NOT NULL," +
            "            [Cost] [nvarchar](50) NOT NULL," +
            "            [CreatedAt] [datetime2](7) NOT NULL," +
            "            [ModifiedAt] [datetime2](7) NOT NULL)" +
            "END;";

        internal static void Prepare(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException($"The database connection is not specified.");
            }

            Console.WriteLine("Seeding data...");

            try
            {
                using IDbConnection connection = new SqlConnection(connectionString);
                connection.Execute(SQL_CREATE_TABLE);
                Console.WriteLine("The database is initialized successfully.");
            }
            catch (Exception)
            {
                Console.WriteLine("The database is not initialized.");
            }       
        }
    }
}
