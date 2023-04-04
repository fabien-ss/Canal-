using System.Data.SqlClient;
public class Region{
    public int idRegion { get; set; }
    public string nom { get; set; }
    public double frequence { get; set; }
    public Region GetRegionById(){

        List<Bouquet> bouquets = new List<Bouquet>();

        string connectionString = "Server=(localdb)\\mssqllocaldb;Database=Canal;Trusted_Connection=True;";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string sql = "SELECT * FROM region WHERE idregion = @id";

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@id", idRegion);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                Region r = new Region();
                if (reader.Read()) {
                    // Un utilisateur a été trouvé avec les identifiants donnés
                    r.frequence = (double) reader["frequence"];
                    r.nom = (string)reader["nom"];
                    r.idRegion = idRegion;   
                    connection.Close();    
                    return r;
                } else {
                    connection.Close();
                    return null;
                }
            }
        }
    }
}