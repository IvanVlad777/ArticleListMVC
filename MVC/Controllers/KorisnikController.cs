using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Models;
using MVC.Services;

namespace MVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KorisnikController : ControllerBase
    {
        private readonly KorisnikRepository _repository;
        private readonly PasswordService _passwordService;
        private readonly JwtTokenService _jwtTokenService;

        public KorisnikController(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            _repository = new KorisnikRepository(connectionString);
            _passwordService = new PasswordService();
            _jwtTokenService = new JwtTokenService(configuration);
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] Korisnik korisnik)
        {
            korisnik.HashiranaLozinka = _passwordService.HashPassword(korisnik.HashiranaLozinka);
            _repository.AddKorisnik(korisnik);
            return Ok("Korisnik uspješno registriran.");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var korisnik = _repository.GetByKorisnickoIme(request.KorisnickoIme);

            if (korisnik == null || !_passwordService.VerifyPassword(korisnik.HashiranaLozinka, request.Lozinka))
            {
                return Unauthorized("Neispravno korisničko ime ili lozinka.");
            }

            var token = _jwtTokenService.GenerateToken(korisnik.KorisnickoIme, korisnik.Uloga.ToString());

            return Ok(new { Token = token });
        }
    }

    public class LoginRequest
    {
        public string KorisnickoIme { get; set; }
        public string Lozinka { get; set; }
    }
}