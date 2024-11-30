using Microsoft.AspNetCore.Identity;
using Models;

namespace MVC.Services
{
    public class PasswordService
    {
        private readonly PasswordHasher<Korisnik> _passwordHasher;

        public PasswordService()
        {
            _passwordHasher = new PasswordHasher<Korisnik>();
        }

        public string HashPassword(string plainPassword)
        {
            return _passwordHasher.HashPassword(null, plainPassword);
        }

        public bool VerifyPassword(string hashedPassword, string plainPassword)
        {
            return _passwordHasher.VerifyHashedPassword(null, hashedPassword, plainPassword) == PasswordVerificationResult.Success;
        }
    }
}