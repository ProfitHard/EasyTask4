
using EasyTask4.DAL;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EasyTask4.BLL
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task RegisterUserAsync(Users user)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(user);
            if (!Validator.TryValidateObject(user, validationContext, validationResults, true))
            {
                throw new ValidationException(string.Join(", ", validationResults.Select(v => v.ErrorMessage)));
            }
            await _userRepository.AddUserAsync(user);
        }

        public async Task<Users> AuthenticateUserAsync(string email, string password)
        {
            return await _userRepository.GetUserByEmailAndPasswordAsync(email, password);
        }
        public async Task<List<Users>> GetAllUsersAsync() 
        {
            return await _userRepository.GetAllUsersAsync();
        }
    }
}