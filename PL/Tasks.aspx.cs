using DevExpress.Web.Data;
using DevExpress.XtraRichEdit.Model;
using EasyTask4.BLL;
using EasyTask4.DAL;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.UI;

namespace EasyTask4.PL
{
    public partial class Tasks : Page
    {
        private readonly TaskService _taskService;

        public Tasks()
        {
            _taskService = Global.Get<TaskService>();
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGridAsync();
            }
        }

        private async Task BindGridAsync()
        {
            gridTasks.DataSource = await _taskService.GetTasksAsync();
            gridTasks.DataBind();
        }

        protected async void gridTasks_RowInserting(object sender, ASPxDataInsertingEventArgs e)
        {
            var newTask = new UserTask
            {
                Title = e.NewValues["Title"].ToString(),
                Description = e.NewValues["Description"].ToString(),
                Status = e.NewValues["Status"].ToString(),
                CreatedBy = e.NewValues["CreatedBy"].ToString(),
                Assignedto = e.NewValues["Assignedto"].ToString(),
                CreationDate = DateTime.Now,
                UpdateDat = DateTime.Now
            };
            await _taskService.CreateTaskAsync(newTask);
            e.Cancel = true;
            await BindGridAsync();
        }

        protected async void gridTasks_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            var title = e.NewValues["Title"]?.ToString();
            var description = e.NewValues["Description"]?.ToString();

            var statusName = e.NewValues["Status"]?.ToString();
            var createdByName = e.NewValues["CreatedBy"]?.ToString();
            var assignedToName = e.NewValues["Assignedto"]?.ToString();

            int statusId = 0;
            int createdById = 0;
            int assignedToId = 0;
            var statusService = Global.Get<StatusService>();
            var userService = Global.Get<UserService>();

            // Получаем статус по имени
            var statuses = await statusService.GetAllStatusesAsync();
            var status = statuses.FirstOrDefault(s => string.Equals(s.Stat, statusName, StringComparison.OrdinalIgnoreCase));
            if (status != null)
                statusId = status.StatusId;
            else
                throw new Exception($"Статус '{statusName}' не найден.");

            // Получаем CreatedBy ID по имени
            var users = await userService.GetAllUsersAsync();
            var createdByUser = users.FirstOrDefault(u => string.Equals(u.Name, createdByName, StringComparison.OrdinalIgnoreCase));
            if (createdByUser != null)
                createdById = createdByUser.Id;
            else
                throw new Exception($"Пользователь создатель '{createdByName}' не найден.");

            // Получаем AssignedTo ID по имени
            var assignedToUser = users.FirstOrDefault(u => string.Equals(u.Name, assignedToName, StringComparison.OrdinalIgnoreCase));
            if (assignedToUser != null)
                assignedToId = assignedToUser.Id;
            else
                throw new Exception($"Пользователь назначенный '{assignedToName}' не найден.");

            // Создаем обновленный объект задачи
            var updatedTask = new UserTask
            {
                Id = Convert.ToInt32(e.Keys["Id"]),
                Title = title,
                Description = description,
                Status = statusId.ToString(), // Модель UserTask хранит Status как string, но в базе это int
                CreatedBy = createdById.ToString(),
                Assignedto = assignedToId.ToString(),
                UpdateDat = DateTime.Now
                // Не трогаем CreationDate, чтобы не изменять дату создания
            };

            await _taskService.UpdateTaskAsync(updatedTask);

            e.Cancel = true;
            await BindGridAsync();
        }
        protected async void gridTasks_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
            {
              int taskId = Convert.ToInt32(e.Keys["Id"]);
              await _taskService.DeleteTaskAsync(taskId);
          e.Cancel = true;
               await BindGridAsync(); 
          }

        protected void btnAddTask_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddTask.aspx");
        }
        protected void btnQueries_Click(object sender, EventArgs e)
        {
            Response.Redirect("Queries.aspx"); 
        }

        protected void btnTaskCount_Click(object sender, EventArgs e)
        {
            Response.Redirect("TaskCount.aspx"); 
        }
    }
}