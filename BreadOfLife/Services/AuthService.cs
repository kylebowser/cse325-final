using BreadOfLife.Models;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace BreadOfLife.Services
{
    public class AuthService
    {
        private readonly IMongoCollection<Baker> _bakersCollection;

        public bool IsLoggedIn { get; private set; }
        public string CurrentBakerEmail { get; private set; }

        public event Action OnAuthStateChanged;

        // Automatically takes the database connection
        public AuthService(IMongoDatabase database)
        {
            _bakersCollection = database.GetCollection<Baker>("baker");
        }

        public async Task<bool> RegisterAsync(BakerRegistrationModel model)
        {
            var existingBaker = await _bakersCollection.Find(b => b.Email == model.Email).FirstOrDefaultAsync();
            if (existingBaker != null) return false; // Email already exists

            var newBaker = new Baker
            {
                Email = model.Email,
                // Hash the password before assigning it to the new Baker object
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password), 
                ZipCode = model.ZipCode
            };

            await _bakersCollection.InsertOneAsync(newBaker);
            LoginLocal(newBaker.Email);
            return true;
        }

        public async Task<bool> LoginAsync(string email, string password)
        {
            // Find the user strictly by email first
            var baker = await _bakersCollection.Find(b => b.Email == email).FirstOrDefaultAsync();
            
            // If baker exists, verify the plain text password against the hashed database password
            if (baker != null && BCrypt.Net.BCrypt.Verify(password, baker.Password))
            {
                LoginLocal(baker.Email);
                return true;
            }
            
            return false;
        }

        private void LoginLocal(string email)
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