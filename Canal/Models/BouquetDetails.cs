using System.Data.SqlClient;
public class BouquetDetails
{
    public int IdChaine { get; set; }
    public string NomChaine { get; set; }
    public decimal PrixChaine { get; set; }
    public int Frequence { get; set; }
    public int IdBouquet { get; set; }
    public string NomBouquet { get; set; }

    public BouquetDetails GetBouquetByIdBouquet(int idBouquet)
    {
        BouquetDetails bouquetDetails = null;

        string connectionString = "Data Source=<nom du serveur>;Initial Catalog=<nom de la base de données>;User ID=<nom d'utilisateur>;Password=<mot de passe>;";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string sql = "SELECT IdChaine, Nom, Prix, Frequence, IdBouquet, NomBouquet FROM BOUQUETDETAILS WHERE IdBouquet = @IdBouquet";

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@IdBouquet", idBouquet);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        bouquetDetails = new BouquetDetails();
                        bouquetDetails.IdChaine = (int)reader["IdChaine"];
                        bouquetDetails.NomChaine = (string)reader["Nom"];
                        bouquetDetails.PrixChaine = (decimal)reader["Prix"];
                        bouquetDetails.Frequence = (int)reader["Frequence"];
                        bouquetDetails.IdBouquet = (int)reader["IdBouquet"];
                        bouquetDetails.NomBouquet = (string)reader["NomBouquet"];
                    }
                }
            }
            connection.Close();
        }
        return bouquetDetails;
    }
}
