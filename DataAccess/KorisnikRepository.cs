using Dapper;
using Models;
using Microsoft.Data.SqlClient;

namespace DataAccess
{
    public class KorisnikRepository
    {
        private readonly string _connectionString;

        public KorisnikRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Korisnik GetByKorisnickoIme(string korisnickoIme)
        {
            using var connection = new SqlConnection(_connectionString);
            return connection.QuerySingleOrDefault<Korisnik>(
                "SELECT * FROM Korisnici WHERE KorisnickoIme = @KorisnickoIme",
                new { KorisnickoIme = korisnickoIme });
        }

        public void AddKorisnik(Korisnik korisnik)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Execute(
                "INSERT INTO Korisnici (KorisnickoIme, HashiranaLozinka, Uloga) VALUES (@KorisnickoIme, @HashiranaLozinka, @Uloga)",
                korisnik);
        }
    }
}