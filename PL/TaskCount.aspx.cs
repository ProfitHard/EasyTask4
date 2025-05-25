using EasyTask4.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EasyTask4.PL
{
    public partial class TaskCount : System.Web.UI.Page
    {
        private readonly TaskService _taskService;

        public TaskCount()
        {
            _taskService = Global.Get<TaskService>();
        }
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var taskCounts = await _taskService.GetTaskCountByStatusAsync();
                gridTaskCount.DataSource = taskCounts;
                gridTaskCount.DataBind();
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("Tasks.aspx");
        }
    }
}