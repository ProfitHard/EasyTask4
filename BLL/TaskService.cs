using EasyTask4.DAL;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyTask4.BLL
{
    public class TaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public async Task<IEnumerable<UserTask>> GetTasksCreatedLastWeekAsync()
        {
            return await _taskRepository.GetTasksCreatedLastWeekAsync();
        }

        public async Task<IEnumerable<UserTask>> GetTasksAsync()
        {
            return await _taskRepository.GetAllTasksAsync();
        }

        public async Task CreateTaskAsync(UserTask task)
        {
            await _taskRepository.AddTaskAsync(task);
        }

        public async Task UpdateTaskAsync(UserTask task)
        {
            await _taskRepository.UpdateTaskAsync(task);
        }
        public async Task<IEnumerable<StatusCount>> GetTaskCountByStatusAsync()
        {
            return await _taskRepository.GetTaskCountByStatusAsync();
        }


        public async Task DeleteTaskAsync(int id)
        {
            await _taskRepository.DeleteTaskAsync(id);
        }
    }
}