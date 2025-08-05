using EasyTask4.BLL;
using EasyTask4.DAL;
using System;
using System.Threading.Tasks;
using System.Web.UI;

namespace EasyTask4.PL
{
    public partial class AddTask : Page
    {
        private readonly TaskService _taskService;
        private readonly UserService _userService;
        private readonly StatusService _statusService;

        public AddTask()
        {
            _taskService = Global.Get<TaskService>();
            _userService = Global.Get<UserService>(); // Получаем UserService
            _statusService = Global.Get<StatusService>();
        }

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Загружаем пользователей в выпадающие списки
                await LoadUsersAsync();
                await LoadStatusesAsync();
            }
        }

        private async Task LoadUsersAsync()
        {
            var users = await _userService.GetAllUsersAsync(); // Получаем всех пользователей
            cbCreatedBy.DataSource = users;
            cbCreatedBy.TextField = "Name"; // Имя поля, отображаемого в выпадающем списке
            cbCreatedBy.ValueField = "Id"; // Имя поля, которое будет использоваться как значение
            cbCreatedBy.DataBind(); // Привязываем данные к выпадающему списку

            cbAssignedTo.DataSource = users;
            cbAssignedTo.TextField = "Name"; // Имя поля, отображаемого в выпадающем списке
            cbAssignedTo.ValueField = "Id"; // Имя поля, которое будет использоваться как значение
            cbAssignedTo.DataBind(); // Привязываем данные к выпадающему списку
        }

        private async Task LoadStatusesAsync()
        {
            var statuses = await _statusService.GetAllStatusesAsync(); // Метод для получения всех статусов
            cbStatus.DataSource = statuses;
            cbStatus.TextField = "Stat"; // Убедитесь, что у Вас есть поле Status в классе Status
            cbStatus.ValueField = "StatusId"; // Убедитесь, что у Вас есть поле StatusId в классе Status
            cbStatus.DataBind();
        }

        protected async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Получаем данные из полей ввода
                string title = txtTitle.Text;
                string description = txtDescription.Text;
                //   int createdBy = Convert.ToInt32(cbCreatedBy.SelectedItem.Value); // Получаем ID созданного пользователем
                //    int assignedTo = Convert.ToInt32(cbAssignedTo.SelectedItem.Value); // Получаем ID назначенного пользователем
                string createdBy = cbCreatedBy.SelectedItem.Value.ToString(); // Получаем имя создавшего пользователя
                string assignedTo = cbAssignedTo.SelectedItem.Value.ToString();
                int statusId = Convert.ToInt32(cbStatus.SelectedItem.Value);

                // Создаем новый объект UserTask
                UserTask newTask = new UserTask
                {
                    Title = title,
                    Description = description,
                    Status = statusId.ToString(),
                    CreatedBy = createdBy, // Используем ID
                    Assignedto = assignedTo, // Используем ID
                    CreationDate = DateTime.Now,
                    UpdateDat = DateTime.Now
                };

                // Добавляем задачу в базу данных
                await _taskService.CreateTaskAsync(newTask);

                // Перенаправляем обратно на страницу задач
                Response.Redirect("Tasks.aspx");
            }
            catch (Exception ex)
            {
                // Обработка исключений
                Console.WriteLine($"Error saving task: {ex.Message}");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            // Перенаправляем обратно на страницу задач без сохранения
            Response.Redirect("Tasks.aspx");
        }
    }
}