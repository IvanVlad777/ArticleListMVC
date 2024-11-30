using Models.Enums;

namespace Models
{
    public class Korisnik
    {
        public int ID { get; set; }
        public string KorisnickoIme { get; set; }
        public string HashiranaLozinka { get; set; }
        public UserRole Uloga { get; set; }
    }
}
