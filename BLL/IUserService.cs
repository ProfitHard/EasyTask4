using EasyTask4.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EasyTask4.BLL
{
    public interface IUserService
    {
        Task RegisterUserAsync(Users user);
        Task<Users> AuthenticateUserAsync(string email, string password);
    }
}