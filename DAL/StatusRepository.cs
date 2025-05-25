using Dapper;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EasyTask4.DAL
{
    public class StatusRepository : IStatusRepository
    {
        private readonly string _connectionString;

        public StatusRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Status>> GetAllStatusesAsync()
        {
            using (var connection = new FbConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = "SELECT StatusId, Stat FROM Status";
                return (await connection.QueryAsync<Status>(sql)).ToList();
            }
        }
    }
}