using System.Data.SqlClient;

public class Chaine
{
    public int IdChaine { get; set; }
    public string Nom { get; set; }
    public double Prix { get; set; }
    public double Frequence { get; set; }

    public List<Chaine> getListeChaine(int idRegion){
        Region region = new Region();
        region.idRegion = idRegion;
        region = region.GetRegionById();
        List<Chaine> listeChaines = getAllChaines();
        List<Chaine> retour = new List<Chaine>();
        Console.WriteLine(listeChaines);
        foreach(var ch in listeChaines){
            if(ch.Frequence <= region.frequence){
                retour.Add(ch);
            }
        }
        return retour;
    }
    public List<Chaine> getChaineById(int idBouquet){
        string connectionString = "Server=(localdb)\\mssqllocaldb;Database=Canal;Trusted_Connection=True;";
        
        string query = "SELECT idChaine, nom, prix, frequence FROM Bouquetdetails where idbouquet = @idbouquet";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
           
            List<Chaine> chaines = new List<Chaine>();
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@idbouquet", idBouquet);
            connection.Open();

            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Chaine chaine = new Chaine();
                chaine.IdChaine = (int)reader["idChaine"];
                chaine.Nom = (string)reader["nom"];
                chaine.Prix = (double)reader["prix"];
                chaine.Frequence = (double)reader["frequence"];
                chaines.Add(chaine);
            }
            return chaines;
        }
    }
    public List<Chaine> getAllChaines()
    {
        string connectionString = "Server=(localdb)\\mssqllocaldb;Database=Canal;Trusted_Connection=True;";
        
        string query = "SELECT * FROM Chaine";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            List<Chaine> chaines = new List<Chaine>();
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Chaine chaine = new Chaine();
                chaine.IdChaine = (int)reader["idChaine"];
                chaine.Nom = (string)reader["nom"];
                chaine.Prix = (double)reader["prix"];
                chaine.Frequence = (double)reader["frequence"];
                chaines.Add(chaine);
            }
            connection.Close();
            return chaines;
        }
    }
}