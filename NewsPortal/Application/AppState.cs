using NewsPortal.Entity;

namespace NewsPortal.Application
{
    public class AppState
    {
        public bool IsLoggedIn { get; private set; } = false;
        public string? UserEmail { get; private set; }

        public RegisterUser? currentUser { get; private set; }

        public void Login(RegisterUser user)
        {
            IsLoggedIn = true;
            currentUser = user;
        }

        public void Logout()
        {
            IsLoggedIn = false;
            currentUser = null;
        }
        //public void Login(string email)
        //{
        //    IsLoggedIn = true;
        //    UserEmail = email;
        //}

        //public void Logout()
        //{
        //    IsLoggedIn = false;
        //    UserEmail = null;
        //}
    }
}
