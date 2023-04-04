using System.Data.SqlClient;
public class Bouquet
{
    public int IdBouquet { get; set; }

    private string nom;

    private List<Chaine> chaines;
    
    public string GetNom()
    {
        return nom;
    }

    public void SetNom(string value)
    {
        nom = value;
    }

    private double totalPrice;

    public double GetTotalPrice()
    {
        return totalPrice;
    }

    public void SetTotalPrice(double value)
    {
        totalPrice = value;
    }
    public List<Bouquet> GetValideBouquets(int idUtilisateur)
    {
        Utilisateur user = new Utilisateur();
        user.IdUtilisateur = idUtilisateur;
        user = user.GetUserById();
        Region region = new Region();
        region.idRegion = user.IdRegion;
        region = region.GetRegionById();
        List<Bouquet> bouquets = new List<Bouquet>();

        string connectionString = "Server=(localdb)\\mssqllocaldb;Database=Canal;Trusted_Connection=True;";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string sql = "SELECT TotalPrice,IdBouquet, Nom FROM FETCHBOUQUET";

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Bouquet bouquet = new Bouquet();
                        bouquet.SetTotalPrice((double)reader["TotalPrice"]);
                        bouquet.IdBouquet = (int)reader["IdBouquet"];
                        bouquet.SetNom((string)reader["Nom"]);
                        Chaine c = new Chaine();
                        List<Chaine> cs = c.getChaineById(bouquet.IdBouquet);
                        bouquet.chaines = cs;
                        bool ajouter = true;
                        foreach(Chaine ch in cs){
                            if(ch.Frequence > region.frequence){
                                ajouter = false;
                            }
                        }
                        if(ajouter) bouquets.Add(bouquet);
                    }
                }
            }
            connection.Close();
        }
        return bouquets;
    }
    public List<Bouquet> GetAllBouquets()
    {
        List<Bouquet> bouquets = new List<Bouquet>();

        string connectionString = "Server=(localdb)\\mssqllocaldb;Database=Canal;Trusted_Connection=True;";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string sql = "SELECT TotalPrice,IdBouquet, Nom FROM FETCHBOUQUET";

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Bouquet bouquet = new Bouquet();
                        bouquet.SetTotalPrice((double)reader["TotalPrice"]);
                        bouquet.IdBouquet = (int)reader["IdBouquet"];
                        bouquet.SetNom((string)reader["Nom"]);
                        bouquets.Add(bouquet);
                    }
                }
            }
        }
        return bouquets;
    }
    public Bouquet GetBouquetById()
    {

        string connectionString = "Server=(localdb)\\mssqllocaldb;Database=Canal;Trusted_Connection=True;";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string sql = "SELECT TotalPrice,IdBouquet, Nom FROM FETCHBOUQUET where idBouquet = @idBouquet";

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@idBouquet", IdBouquet);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Bouquet bouquet = new Bouquet();
                        bouquet.SetTotalPrice((double)reader["TotalPrice"]);
                        bouquet.IdBouquet = (int)reader["IdBouquet"];
                        bouquet.SetNom((string)reader["Nom"]);
                        // bouquets.Add(bouquet);
                        return bouquet;
                    }
                }
            }
        }
        return null;
    }
}