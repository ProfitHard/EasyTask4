using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyTask4.DAL
{
    public interface ITaskRepository
    {
        Task<IEnumerable<UserTask>> GetAllTasksAsync();
        Task<UserTask> GetTaskByIdAsync(int id);
        Task AddTaskAsync(UserTask task);
        Task UpdateTaskAsync(UserTask task);
        Task DeleteTaskAsync(int id);
        Task<IEnumerable<UserTask>> GetTasksCreatedLastWeekAsync();
        Task<IEnumerable<StatusCount>> GetTaskCountByStatusAsync();
    }
}