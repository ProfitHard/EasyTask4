using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyTask4.DAL
{
    public interface IUserRepository
    {
        Task AddUserAsync(Users user);
        Task<Users> GetUserByEmailAndPasswordAsync(string email, string password);
        Task<List<Users>> GetAllUsersAsync();
    }
}