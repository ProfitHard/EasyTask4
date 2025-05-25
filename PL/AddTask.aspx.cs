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
            _userService = Global.Get<UserService>();
            _statusService = Global.Get<StatusService>();
        }

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                await LoadUsersAsync();
                await LoadStatusesAsync();
            }
        }

        private async Task LoadUsersAsync()
        {
            var users = await _userService.GetAllUsersAsync(); 
            cbCreatedBy.DataSource = users;
            cbCreatedBy.TextField = "Name"; 
            cbCreatedBy.ValueField = "Id";
            cbCreatedBy.DataBind();

            cbAssignedTo.DataSource = users;
            cbAssignedTo.TextField = "Name"; 
            cbAssignedTo.ValueField = "Id"; 
            cbAssignedTo.DataBind();
        }

        private async Task LoadStatusesAsync()
        {
            var statuses = await _statusService.GetAllStatusesAsync(); 
            cbStatus.DataSource = statuses;
            cbStatus.TextField = "Stat"; 
            cbStatus.ValueField = "StatusId"; 
            cbStatus.DataBind();
        }

        protected async void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string title = txtTitle.Text;
                string description = txtDescription.Text;
                string createdBy = cbCreatedBy.SelectedItem.Value.ToString();
                string assignedTo = cbAssignedTo.SelectedItem.Value.ToString();
                int statusId = Convert.ToInt32(cbStatus.SelectedItem.Value);

                UserTask newTask = new UserTask
                {
                    Title = title,
                    Description = description,
                    Status = statusId.ToString(),
                    CreatedBy = createdBy, 
                    Assignedto = assignedTo, 
                    CreationDate = DateTime.Now,
                    UpdateDat = DateTime.Now
                };

                await _taskService.CreateTaskAsync(newTask);

                Response.Redirect("Tasks.aspx");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving task: {ex.Message}");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("Tasks.aspx");
        }
    }
}