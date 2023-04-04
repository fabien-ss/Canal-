using System.Data.SqlClient;

public class UserValideBouquet {
    public int idBouquet {get; set;}
    public string nom {get;set;}
    public double remise {get;set;}
    public DateTime dateDebut {get;set;}
    public DateTime dateFin {get;set;}
    public int idUtilisateur {get;set;}
    public double prix {get;set;}
    public int idRechargement {get;set;}
    public UserValideBouquet(){}
    
    public List<UserValideBouquet> GetUserValideBouquet(){
        string sql = "Select * from uservalidebouquet where etat = 1 and idUtilisateur = @idUtilisateur";
        List<UserValideBouquet> bouquets = new List<UserValideBouquet>();

        string connectionString = "Server=(localdb)\\mssqllocaldb;Database=Canal;Trusted_Connection=True;";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@idUtilisateur", idUtilisateur);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        UserValideBouquet usv = new UserValideBouquet();
                        usv.dateDebut = (DateTime) reader["dateDebut"];
                        usv.dateFin = (DateTime) reader["dateFin"];
                        usv.idBouquet = (int) reader["idBouquet"];
                        usv.nom = (string) reader["nom"];
                        usv.remise = (double) reader["remise"];
                        usv.idUtilisateur = (int) reader["idUtilisateur"];
                        usv.prix = (double) reader["prix"];
                        usv.idRechargement  = (int) reader["idRechargement"];
                        bouquets.Add(usv);
                    }
                }
            }
        }
        return bouquets;
    }
}