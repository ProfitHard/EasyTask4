using System;
using System.Threading.Tasks;
using System.Web.UI;
using EasyTask4.BLL;
using EasyTask4.DAL;

namespace EasyTask4.PL
{
    public partial class Queries : Page
    {
        private readonly TaskService _taskService;

        public Queries()
        {
            _taskService = Global.Get<TaskService>();
        }

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                await BindGridAsync();
            }
        }

        private async Task BindGridAsync()
        {
            gridQueries.DataSource = await _taskService.GetTasksCreatedLastWeekAsync();
            gridQueries.DataBind();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Tasks.aspx");
        }
    }
}