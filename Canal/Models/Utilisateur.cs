using System.Data.SqlClient;

public class Utilisateur {
    public int IdUtilisateur { get; set; }
    public string Nom { get; set; }
    public string Prenom { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int IdRegion { get; set; }

    public Utilisateur(string nom, string prenom, string email, string password, int idRegion) {
        Nom = nom;
        Prenom = prenom;
        Email = email;
        Password = password;
        IdRegion = idRegion;
    }

    public Utilisateur()
    {
    }
    public Utilisateur GetUserById()
    {
        List<Bouquet> bouquets = new List<Bouquet>();

        string connectionString = "Server=(localdb)\\mssqllocaldb;Database=Canal;Trusted_Connection=True;";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {

            string sql = "SELECT * FROM UTILISATEUR WHERE idUtilisateur = @id";

            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@id", IdUtilisateur);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read()) {
                    // Un utilisateur a été trouvé avec les identifiants donnés
                    Utilisateur utilisateur = new Utilisateur(
                        (string)reader["nom"],
                        (string)reader["prenom"],
                        (string)reader["email"],
                        (string)reader["password"],
                        (int)reader["idregion"]
                    );
                    utilisateur.IdUtilisateur = (int)reader["idutilisateur"];
                    connection.Close();
                    return utilisateur;
                } else {
                    return null;
                }
            }
        }

    }
    public void insert() {
        string connectionString = "Server=(localdb)\\mssqllocaldb;Database=Canal;Trusted_Connection=True;";
        using (SqlConnection connection = new SqlConnection(connectionString)) {
            string sql = "insert into utilisateur(nom, prenom, email, password, idregion) values(@Nom, @Prenom, @Email, @Password, @IdRegion)";

            using (SqlCommand command = new SqlCommand(sql, connection)) {
                // Ajout des paramètres à la commande SQL
                command.Parameters.AddWithValue("@Nom", Nom);
                command.Parameters.AddWithValue("@Prenom", Prenom);
                command.Parameters.AddWithValue("@Email", Email);
                command.Parameters.AddWithValue("@Password", Password);
                command.Parameters.AddWithValue("@IdRegion", IdRegion);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
    public Utilisateur VerifierUtilisateur(string email, string password) {
        string connectionString = "Server=(localdb)\\mssqllocaldb;Database=Canal;Trusted_Connection=True;";
        using (SqlConnection connection = new SqlConnection(connectionString)) {
            string sql = "select * from utilisateur where email=@Email and password=@Password";

            using (SqlCommand command = new SqlCommand(sql, connection)) {
                // Ajout des paramètres à la commande SQL
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Password", password);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read()) {
                    // Un utilisateur a été trouvé avec les identifiants donnés
                    Utilisateur utilisateur = new Utilisateur(
                        (string)reader["nom"],
                        (string)reader["prenom"],
                        (string)reader["email"],
                        (string)reader["password"],
                        (int)reader["idregion"]
                    );
                    utilisateur.IdUtilisateur = (int)reader["idutilisateur"];
                    connection.Close();
                    return utilisateur;
                } else {
                    return null;
                }
                
            }
        }
    }
}
