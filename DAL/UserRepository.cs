using Dapper;
using FirebirdSql.Data.FirebirdClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyTask4.DAL
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository()
        {
            _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FirebirdDB"].ConnectionString;
        }

        public async Task AddUserAsync(Users user)
        {
            using (var connection = new FbConnection(_connectionString))
            {
                await connection.OpenAsync();

                var existingUser = await connection.QuerySingleOrDefaultAsync<Users>(
                "SELECT * FROM USERS WHERE Email = @Email", new { Email = user.Email });

                if (existingUser != null)
                {
                    throw new Exception("Электронная почта уже существует.");
                }


                var sql = "INSERT INTO USERS (ID, Name, Email, Password) VALUES (NEXT VALUE FOR USERS_SEQ, @Name, @Email, @Password)";
                await connection.ExecuteAsync(sql, new { user.Name, user.Email, user.Password });
            }
        }

        public async Task<Users> GetUserByEmailAndPasswordAsync(string email, string password)
        {
            using (var connection = new FbConnection(_connectionString))
            {
                await connection.OpenAsync();
                return await connection.QuerySingleOrDefaultAsync<Users>(
                    "SELECT * FROM USERS WHERE Email = @Email AND Password = @Password",
                    new { Email = email, Password = password });
            }
        }

        public async Task<List<Users>> GetAllUsersAsync()
        {
            using (var connection = new FbConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = "SELECT * FROM USERS"; // Запрос для получения всех пользователей
                return (await connection.QueryAsync<Users>(sql)).ToList();
            }
        }
    }
}