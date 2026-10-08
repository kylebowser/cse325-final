namespace BreadOfLife.Services
{
    public class AuthService
    {
        public bool IsLoggedIn { get; private set; }
        public string CurrentBakerEmail { get; private set; }

        public event Action OnAuthStateChanged;

        public void Login(string email)
        {
            IsLoggedIn = true;
            CurrentBakerEmail = email;
            NotifyStateChanged();
        }

        public void Logout()
        {
            IsLoggedIn = false;
            CurrentBakerEmail = string.Empty;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnAuthStateChanged?.Invoke();
    }
}