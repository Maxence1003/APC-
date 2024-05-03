using System;
using MySqlConnector;

namespace Mediateq_AP_SIO2
{
    class DAOFactory
    {
        private static MySqlConnection connexion;

        public static void creerConnection()
        {
            string serverIp = "127.0.0.1";
            string username = "root";
            string password = "";
            string databaseName = "mediateq";

            string dbConnectionString = string.Format("server={0};uid={1};pwd={2};database={3};", serverIp, username, password, databaseName);
           
            try
            {
                connexion = new MySqlConnection(dbConnectionString);
            }
            catch (Exception e)
            {
                Console.WriteLine("Erreur connexion BDD", e.Message);
            }
            
        }

        public static void connecter()
        {
            try
            {
                connexion.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void deconnecter()
        {
            connexion.Close();
        }

 
        // Exécution d'une requête de lecture ; retourne un Datareader
        public static MySqlDataReader execSQLRead(string requete)
        {
            MySqlCommand command;
            MySqlDataAdapter adapter;
            command = new MySqlCommand();
            command.CommandText = requete;
            command.Connection = connexion;

            adapter = new MySqlDataAdapter();
            adapter.SelectCommand = command;

            MySqlDataReader dataReader;
            dataReader = command.ExecuteReader();

            return dataReader;
        }

        // Exécution d'une requete d'écriture (Insert ou Update) ; ne retourne rien
        public static void execSQLWrite(string requete)
        {

            MySqlCommand command;
            command = new MySqlCommand();
            command.CommandText = requete;
            command.Connection = connexion;

            command.ExecuteNonQuery();
        }
    }
}
