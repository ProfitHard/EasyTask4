using Dapper;
using DevExpress.Web.Internal.XmlProcessor;
using EasyTask4.PL;
using FirebirdSql.Data.FirebirdClient;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EasyTask4.DAL
{
    public class TaskRepository : ITaskRepository
    {
        private readonly string _connectionString;
        private readonly ILogger _logger;

        public TaskRepository(ILogger logger)
        {
            _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["FirebirdDB"].ConnectionString;
            _logger = logger;
        }

           public async Task<IEnumerable<UserTask>> GetAllTasksAsync()
         {
            using (var connection = new FbConnection(_connectionString))
            {
                await connection.OpenAsync();
                _logger.Information("Извлечение всех задач из базы данных.");
                var sql = @"
                SELECT
                    t.Id,
                    t.Title,
                    t.Description,
                    s.Stat AS Status,
                    uc.Name AS CreatedBy,
                    ua.Name AS Assignedto,
                    t.CreationDate,
                    t.UpdateDat
                FROM Tasks t
                JOIN Status s ON t.Status = s.StatusId
                LEFT JOIN Users uc ON t.CreatedBy = uc.Id
                LEFT JOIN Users ua ON t.Assignedto = ua.Id";

                 return await connection.QueryAsync<UserTask>(sql);
            }
         }

        public async Task<IEnumerable<UserTask>> GetTasksCreatedLastWeekAsync()
        {
            using (var connection = new FbConnection(_connectionString))
            {
                await connection.OpenAsync();
                _logger.Information("Выборка задач, созданных за последнюю неделю");
                var sql = @"
            SELECT
                    t.Id,
                    t.Title,
                    t.Description,
                    s.Stat AS Status,
                    uc.Name AS CreatedBy,
                    ua.Name AS Assignedto,
                    t.CreationDate,
                    t.UpdateDat
                FROM Tasks t
                JOIN Status s ON t.Status = s.StatusId
                LEFT JOIN Users uc ON t.CreatedBy = uc.Id
                LEFT JOIN Users ua ON t.Assignedto = ua.Id
            WHERE CreationDate >= CURRENT_DATE-7";
                return await connection.QueryAsync<UserTask>(sql);
            }
        }

        public async Task<IEnumerable<StatusCount>> GetTaskCountByStatusAsync()
        {
            using (var connection = new FbConnection(_connectionString))
            {
                await connection.OpenAsync();
                _logger.Information("Выборка количества задач по статусу.");
                var sql = @"
            SELECT 
                s.Stat AS Status,
                COUNT(t.Id) AS TaskCount
            FROM 
                Tasks t
            JOIN 
                Status s ON t.Status = s.StatusId
            GROUP BY 
                s.Stat";

                return await connection.QueryAsync<StatusCount>(sql);
            }
        }

        public async Task<UserTask> GetTaskByIdAsync(int id)
        {
            using (var connection = new FbConnection(_connectionString))
            {
                await connection.OpenAsync();
                _logger.Information("Выборка задачи с идентификатором  {TaskId}.", id);
                return await connection.QuerySingleOrDefaultAsync<UserTask>("SELECT * FROM Tasks WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task AddTaskAsync(UserTask task)
        {
            using (var connection = new FbConnection(_connectionString))
            {
                await connection.OpenAsync();
                _logger.Information("Добавление новой задачи: {@Task}", task);

                var sql = "INSERT INTO Tasks (ID, Title, Description, Status, CreatedBy, Assignedto) VALUES (NEXT VALUE FOR TASKS_SEQ, @Title, @Description, @Status, @CreatedBy, @Assignedto)";

                await connection.ExecuteAsync(sql, task);
                _logger.Information("Задача успешно выполнена.");
            }
        }

        public async Task UpdateTaskAsync(UserTask task)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(task);
            if (!Validator.TryValidateObject(task, validationContext, validationResults, true))
            {
                throw new ValidationException(string.Join(", ", validationResults.Select(v => v.ErrorMessage)));
            }
            using (var connection = new FbConnection(_connectionString))
            {
                await connection.OpenAsync();
                _logger.Information("Обновляем задачу.");
                var sql = @"
            UPDATE Tasks 
            SET 
                Title = @Title, 
                Description = @Description, 
                Status = @Status, 
                CreatedBy = @CreatedBy, 
                Assignedto = @Assignedto, 
                UpdateDat = @UpdateDat 
            WHERE Id = @Id";

                // Преобразуем статус, CreatedBy и Assignedto в int, если они были переданы как строки
                int statusId = int.TryParse(task.Status, out int tempStatusId) ? tempStatusId : 0;
                int createdById = int.TryParse(task.CreatedBy, out int tempCreatedById) ? tempCreatedById : 0; 
                int assignedToId = int.TryParse(task.Assignedto, out int tempassignedToId) ? tempassignedToId : 0; 

                await connection.ExecuteAsync(sql, new
                {
                    task.Title,
                    task.Description,
                    Status = statusId,
                    CreatedBy = createdById,
                    Assignedto = assignedToId,
                    UpdateDat = DateTime.Now,
                    task.Id
                });
            }
        }
        public async Task DeleteTaskAsync(int id)
        {
            using (var connection = new FbConnection(_connectionString))
            {
                await connection.OpenAsync();
                _logger.Information("Удаляем задачу.");
                await connection.ExecuteAsync("DELETE FROM Tasks WHERE Id = @Id", new { Id = id });
            }
        }
    }
}