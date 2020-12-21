namespace TrolleyApi.User
{
    public class UserResponse
    {
        public string Name { get; }
        public string Token { get; }

        public UserResponse(string name, string token)
        {
            Name = name;
            Token = token;
        }
    }
}
