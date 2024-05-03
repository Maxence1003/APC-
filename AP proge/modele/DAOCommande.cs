using AP_proge.metier;
using Mediateq_AP_SIO2;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP_proge.modele
{
    internal class DAOCommande
    {

        public static bool CreateCommand(int nbExemplaire, DateTime DateCommande, decimal montant, int idDocument)
        {
            string req = "INSERT INTO commande ( nbExemplaire,DateCommande ,  montant, idDocument ) " +
                         "VALUES ( " + nbExemplaire + ",'" + DateTime.Now.ToString("yyyy-MM-dd") + "', " + montant + "," + idDocument + ")";

            try
            {
                DAOFactory.connecter();
                DAOFactory.execSQLWrite(req);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                DAOFactory.deconnecter();
            }
        }
        public static void UpdateCommande(int idCommande, int idEtat_Commande)
        {
            string req = "UPDATE commande SET etat_commande = " + idEtat_Commande + " WHERE id = " + idCommande + ";";
            DAOFactory.connecter();

            DAOFactory.execSQLWrite(req);
            DAOFactory.deconnecter();
            
        }

        public static List<Commande> GetCommandes()
        {
            List<Commande> lesCommandes = new List<Commande>();
            string req = "SELECT commande.id, commande.nbExemplaire, commande.dateCommande, commande.montant, commande.idDocument, commande.etat_commande FROM Commande";

            DAOFactory.connecter();

            MySqlDataReader reader = DAOFactory.execSQLRead(req);

            while (reader.Read())
            {
                Commande ex = new Commande(int.Parse(reader[0].ToString()), int.Parse(reader[1].ToString()),reader.GetDateTime(2), double.Parse(reader[3].ToString()),int.Parse(reader[4].ToString()), int.Parse(reader[5].ToString()));
                lesCommandes.Add(ex);
            }
            DAOFactory.deconnecter();
            return lesCommandes;
        }

        public static Commande GetCommandebyId(int id)
        {
            Commande commande;
            string req = "SELECT commande.id, commande.nbExemplaire, commande.dateCommande, commande.montant, commande.idDocument, commande.etat_commande FROM Commande Where id =" + id;

            DAOFactory.connecter();

            MySqlDataReader reader = DAOFactory.execSQLRead(req);
            if (reader.Read())
            {

                commande = new Commande(Int32.Parse(reader[0].ToString()), Int32.Parse(reader[1].ToString()), reader.GetDateTime(2), Int32.Parse(reader[3].ToString()), Int32.Parse(reader[4].ToString()), Int32.Parse(reader[5].ToString()));
            }

            else
            {
                commande = null;
            }
            DAOFactory.deconnecter();
            return commande;
        }

        public static List<etat_commande> GetEtat_Commande()
        {
            List<etat_commande> lesEtat_Commande = new List<etat_commande>();
            string req = "SELECT id, libelle from etat_commande";

            DAOFactory.connecter();

            MySqlDataReader reader = DAOFactory.execSQLRead(req);

            while (reader.Read())
            {
                etat_commande ex = new etat_commande(Int32.Parse(reader[0].ToString()), reader[1].ToString());
                lesEtat_Commande.Add(ex);
            }
            DAOFactory.deconnecter();
            return lesEtat_Commande;
        }

        
    }
}

