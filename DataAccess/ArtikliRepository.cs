using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using Dapper;
using Models;

namespace DataAccess
{
    public class ArtikliRepository
    {
        private readonly string _connectionString;

        public ArtikliRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Artikl> GetAll()
        {
            using var connection = new SqlConnection(_connectionString);
            string query = "SELECT * FROM Artikli";
            return connection.Query<Artikl>(query);
        }

        public Artikl GetById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            string query = "SELECT * FROM Artikli WHERE ID = @ID";
            return connection.QuerySingleOrDefault<Artikl>(query, new { ID = id });
        }

        public void Add(Artikl artikl)
        {
            using var connection = new SqlConnection(_connectionString);
            string query = "INSERT INTO Artikli (Naziv, Cijena, KategorijaID, Opis, URLSlike) VALUES (@Naziv, @Cijena, @KategorijaID, @Opis, @URLSlike)";
            connection.Execute(query, artikl);
        }

        public IEnumerable<Artikl> GetAllArtikli()
        {
            using var connection = new SqlConnection(_connectionString);
            return connection.Query<Artikl>("SELECT * FROM Artikli");
        }

        public Artikl GetArtiklById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            return connection.QuerySingleOrDefault<Artikl>("SELECT * FROM Artikli WHERE ID = @ID", new { ID = id });
        }

        public void AddArtikl(Artikl artikl)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Execute("INSERT INTO Artikli (Naziv, Cijena, KategorijaID, Opis, URLSlike) VALUES (@Naziv, @Cijena, @KategorijaID, @Opis, @URLSlike)", artikl);
        }

        public void UpdateArtikl(Artikl artikl)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Execute("UPDATE Artikli SET Naziv = @Naziv, Cijena = @Cijena, KategorijaID = @KategorijaID, Opis = @Opis, URLSlike = @URLSlike WHERE ID = @ID", artikl);
        }

        public void DeleteArtikl(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Execute("DELETE FROM Artikli WHERE ID = @ID", new { ID = id });
        }
    }
}
