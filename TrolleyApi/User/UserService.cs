namespace TrolleyApi.User
{
    public interface IUserService
    {
        public UserResponse Get();
    }

    public class UserService : IUserService
    {
        public UserResponse Get()
        {
            return new UserResponse("Ram Anam", "dab78cfc-6bd8-428f-be75-19be7bd38643");
        }
    }
}
