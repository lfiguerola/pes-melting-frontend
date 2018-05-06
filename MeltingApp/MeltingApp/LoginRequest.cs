namespace MeltingApp
{
    public class LoginRequest
    {
        public bool IsLogged { get; set; }
        public int LoggedUserIdDb { get; set; }
        public int LoggedUserIdBackend { get; set; }
        public int LoggedUserId { get; set; }

    }
}