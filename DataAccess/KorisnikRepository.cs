using Dapper;
using Models;
using Microsoft.Data.SqlClient;
using System;

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
            try
            {
                using var connection = new SqlConnection(_connectionString);

                var parameters = new
                {
                    KorisnickoIme = korisnik.KorisnickoIme,
                    HashiranaLozinka = korisnik.HashiranaLozinka,
                    Uloga = korisnik.Uloga.ToString().ToLower() // Osiguraj pravilnu formu
                };

                connection.Execute(
                    "INSERT INTO Korisnici (KorisnickoIme, HashiranaLozinka, Uloga) VALUES (@KorisnickoIme, @HashiranaLozinka, @Uloga)",
                    parameters);

            } 
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}