using Microsoft.Extensions.Configuration;

namespace TrolleyApi.User
{
    public interface IUserService
    {
        public UserResponse Get();
    }

    public class UserService : IUserService
    {
        private readonly IConfiguration _configuration;

        public UserService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public UserResponse Get()
        {
            return new UserResponse("Ram Anam", _configuration["UserToken"]);
        }
    }
}
