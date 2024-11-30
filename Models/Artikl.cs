namespace Models
{
    public class Artikl
    {
        public int ID { get; set; }
        public string Naziv { get; set; }
        public decimal Cijena { get; set; }
        public int KategorijaID { get; set; }
        public string Opis { get; set; }
        public string URLSlike { get; set; }
    }
}
