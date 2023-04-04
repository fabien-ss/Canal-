using System.Data.SqlClient;

public class UserBouquets{
     // UserBouquets ub = new UserBouquets();
                // ub.dateDebut = DateTime.ParseExact(dateDebut, "yyyy-MM-dd'T'HH:mm", CultureInfo.InvariantCulture);
                // ub.idBouquet = int.Parse(o);
                // ub.idUtilisateur = int.Parse(idUtilisateur);
                // ub.dateFin = ub.dateDebut.AddMonths(int.Parse(nombreDeMois));
                // ub.etat = 1;
                // ub.insert();
    public int idUtilisateur { get; set;}
    public int idBouquet { get; set;}
    public DateTime dateDebut { get; set;}
    public DateTime dateFin { get; set;}
    public int etat { get; set;}
    public double prix { get;set;}
    public int idRechargement {get;set;}
    public UserBouquets(){}
    public UserBouquets(int idU, int idB, DateTime dDebut, DateTime dFin, int e){
        idUtilisateur = idU;
        idBouquet = idB;
        dateDebut = dDebut;
        dateFin = dFin;
        etat = e;
    }
    public void verifierAbonnement(){
        
        string connectionString = "Server=(localdb)\\mssqllocaldb;Database=Canal;Trusted_Connection=True;";
        using (SqlConnection connection = new SqlConnection(connectionString)) {
            connection.Open();
            string sql = "update userbouquets set etat = 0 where datefin < GETDATE()";
            using (SqlCommand command = new SqlCommand(sql, connection)) {
                
                int x = command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
    public void insert(){
       
        string connectionString = "Server=(localdb)\\mssqllocaldb;Database=Canal;Trusted_Connection=True;";
        
        using (SqlConnection connection = new SqlConnection(connectionString)) {
              string sql = "INSERT INTO USERBOUQUETS(idutilisateur, idbouquet, datedebut, datefin, etat, prix)"+
        " values(@idUtilisateur, @idBouquet, @dateDebut, @dateFin, @etat, @prix)";
             using (SqlCommand command = new SqlCommand(sql, connection)) {
                // Ajout des paramètres à la commande SQL
                command.Parameters.AddWithValue("@idUtilisateur", idUtilisateur);
                command.Parameters.AddWithValue("@idBouquet", idBouquet);
                SqlParameter param = new SqlParameter("@dateDebut", System.Data.SqlDbType.DateTime);
                param.Value = dateDebut;
                command.Parameters.Add(param);

                SqlParameter param2 = new SqlParameter("@dateFin", System.Data.SqlDbType.DateTime);
                param2.Value = dateFin;
                command.Parameters.Add(param2);

                // command.Parameters.AddWithValue("@dateDebut", dateDebut);
                // command.Parameters.AddWithValue("@dateFin", dateFin);
                command.Parameters.AddWithValue("@etat", etat);
                command.Parameters.AddWithValue("@prix", prix);
                Console.WriteLine(dateDebut);
                Console.WriteLine(dateFin);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
    public UserBouquets getLastContract(){
        
        //Ny date vaovao nouvel abonnement zany le date fin anio farany io
        string connectionString = "Server=(localdb)\\mssqllocaldb;Database=Canal;Trusted_Connection=True;";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string sql = "select * from userbouquets where idRechargement = @idRechargement and etat =1";

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@idRechargement", idRechargement);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        UserBouquets ub = new UserBouquets();
                        ub.dateDebut = (DateTime) reader["dateDebut"];
                        ub.dateFin = (DateTime) reader["dateFin"];
                        ub.idUtilisateur = (int) reader["idUtilisateur"];
                        ub.idBouquet = (int) reader["idBouquet"];
                        ub.etat = (int) reader["etat"];
                        ub.idRechargement = (int) reader["idRechargement"];
                        return ub;
                    }
                }
            }
        }
        return null;
    }
}