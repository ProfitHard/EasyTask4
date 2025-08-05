using EasyTask4.BLL;
using EasyTask4.DAL;
using System;
using System.Threading.Tasks;
using System.Web.UI;

namespace EasyTask4.PL
{
    public partial class Auth : Page
    {
        private readonly IUserService _userService;
        private readonly UserRepository _userRepository;

        public Auth()
        {
            _userService = Global.Get<UserService>();
            _userRepository = Global.Get<UserRepository>();
        }

        protected async void btnRegister_Click(object sender, EventArgs e)
        {
            var user = new Users
            {
                Name = txtName.Text,
                Email = txtEmail.Text,
                Password = txtPassword.Text
            };

            try
            {
                await _userRepository.AddUserAsync(user);
                lblErrorMessage.Text = "Registration successful!";
                lblErrorMessage.Visible = true;
            }
            catch (Exception ex)
            {
                lblErrorMessage.Text = "Registration failed: " + ex.Message;
                lblErrorMessage.Visible = true;
            }
        }

        protected async void btnLogin_Click(object sender, EventArgs e)
        {
            var email = txtEmail.Text;
            var password = txtPassword.Text;

            var user = await _userService.AuthenticateUserAsync(email, password);
            if (user != null)
            {
                // Логика успешного входа
                Response.Redirect("Tasks.aspx"); // Перенаправление на страницу задач после входа
            }
            else
            {
                lblErrorMessage.Text = "Invalid email or password.";
                lblErrorMessage.Visible = true;
            }
        }
    }
}