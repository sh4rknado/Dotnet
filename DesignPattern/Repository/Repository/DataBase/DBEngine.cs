using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using Npgsql;
using Repository.Interfaces;

namespace Repository.Model
{
    public class DBEngine : IGetDBConnection
    {
        private readonly DBConfig dbConfig;


        public DBEngine(DBConfig dbConfig)
        {
            this.dbConfig = dbConfig;
        }


        public bool TryGetDBConnection(out IDbConnection dbConnection)
        {
            dbConnection = null;

            switch (dbConfig.Type)
            {
                case DBType.MySQL:
                    dbConnection = new MySqlConnection($"Server={dbConfig.IP};Database={dbConfig.DBName};Uid={dbConfig.Username};Pwd={dbConfig.Password};");
                    break;
                case DBType.SQL:
                    dbConnection = new SqlConnection($"Server={dbConfig.IP};Database={dbConfig.DBName};User Id={dbConfig.Username};Password={dbConfig.Password};");
                    break;
                case DBType.SQLITE:
                    dbConnection = new MySqlConnection($"Server={dbConfig.IP};Database={dbConfig.DBName};Uid={dbConfig.Username};Pwd={dbConfig.Password};");
                    break;
                case DBType.PSQL:
                    dbConnection = new NpgsqlConnection($"User ID={dbConfig.Username};Password={dbConfig.Password};Host={dbConfig.IP};Port={dbConfig.Port};Database={dbConfig.DBName};");
                    break;
            }

            return dbConnection != null;
        }
    }
}
