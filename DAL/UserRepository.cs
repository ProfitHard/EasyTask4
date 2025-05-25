using Dapper;
using DevExpress.Persistent.Base.Security;
using FirebirdSql.Data.FirebirdClient;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EasyTask4.DAL
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;
        private readonly ILogger _logger;

        public UserRepository()
        {
            _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FirebirdDB"].ConnectionString;
            _logger = Log.ForContext<UserRepository>();
        }

        public async Task AddUserAsync(Users user)
        {
            ValidateEmail(user.Email); // Валидация email

            using (var connection = new FbConnection(_connectionString))
            {
                await connection.OpenAsync();
                Log.Information("Проверка существования пользователя с email: {Email}", user.Email);

                var existingUser = await connection.QuerySingleOrDefaultAsync<Users>(
                    "SELECT * FROM USERS WHERE Email = @Email", new { Email = user.Email });

                if (existingUser != null)
                {
                    Log.Warning("Электронная почта уже существует: {Email}", user.Email);
                    throw new Exception("Электронная почта уже существует.");
                }

                var sql = "INSERT INTO USERS (ID, Name, Email, Password) VALUES (NEXT VALUE FOR USERS_SEQ, @Name, @Email, @Password)";
                await connection.ExecuteAsync(sql, new { user.Name, user.Email, user.Password });
                Log.Information("Пользователь успешно добавлен: {Email}", user.Email);
            }
        }
        private void ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new Exception("Email не может быть пустым.");
            }

            // Регулярное выражение для проверки формата email
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(email, emailPattern))
            {
                throw new Exception("Некорректный формат email.");
            }
        }


        public async Task<Users> GetUserByEmailAndPasswordAsync(string email, string password)
        {
            _logger.Information("Попытка получить пользователя с email: {Email}", email);
            using (var connection = new FbConnection(_connectionString))
            {
                await connection.OpenAsync();
                Log.Information("Попытка входа для пользователя с email: {Email}", email);
                return await connection.QuerySingleOrDefaultAsync<Users>(
                    "SELECT * FROM USERS WHERE Email = @Email AND Password = @Password",
                    new { Email = email, Password = password });
            }
        }

        public async Task<List<Users>> GetAllUsersAsync()
        {
            _logger.Information("Получение всех пользователей.");
            using (var connection = new FbConnection(_connectionString))
            {
                await connection.OpenAsync();
                var sql = "SELECT * FROM USERS";
                _logger.Information("Успешно");
                return (await connection.QueryAsync<Users>(sql)).ToList();
            }
        }
    }
}