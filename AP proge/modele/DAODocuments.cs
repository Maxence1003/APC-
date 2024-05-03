using AP_proge.metier;
using Mediateq_AP_SIO2.metier;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Mediateq_AP_SIO2
{
    class DAODocuments
    {

        public static List<Categorie> getAllCategories()
        {
            List<Categorie> lesCategories = new List<Categorie>();
            string req = "Select * from public";

            DAOFactory.connecter();

            MySqlDataReader reader = DAOFactory.execSQLRead(req);

            while (reader.Read())
            {
                Categorie categorie = new Categorie(reader[0].ToString(), reader[1].ToString());
                lesCategories.Add(categorie);
            }
            DAOFactory.deconnecter();
            return lesCategories;
        }


        public static List<Descripteur> getAllDescripteurs()
        {
            List<Descripteur> lesGenres = new List<Descripteur>();
            string req = "Select * from descripteur";

            DAOFactory.connecter();

            MySqlDataReader reader = DAOFactory.execSQLRead(req);

            while (reader.Read())
            {
                Descripteur genre = new Descripteur(reader[0].ToString(), reader[1].ToString());
                lesGenres.Add(genre);
            }
            DAOFactory.deconnecter();
            return lesGenres;
        }

        public static List<Livre> getAllLivres()
        {
            List<Livre> lesLivres = new List<Livre>();
            string req = "Select l.idDocument, l.ISBN, l.auteur, d.titre, d.image, l.collection from livre l join document d on l.idDocument=d.id";

            DAOFactory.connecter();

            MySqlDataReader reader = DAOFactory.execSQLRead(req);

            while (reader.Read())
            {
                // On ne renseigne pas le genre et la catégorie car on ne peut pas ouvrir 2 dataReader dans la même connexion
                Livre livre = new Livre(Int32.Parse(reader[0].ToString()), reader[3].ToString(), reader[1].ToString(),
                    reader[2].ToString(), reader[5].ToString(), reader[4].ToString());
                lesLivres.Add(livre);
            }
            DAOFactory.deconnecter();

            return lesLivres;
        }

        public static List<DVD> getAllDvd()
        {
            List<DVD> lesDvd = new List<DVD>();
            string req = "SELECT d.id, d.titre, dvd.synopsis, dvd.réalisateur, dvd.duree, d.image FROM dvd JOIN document AS d ON dvd.idDocument = d.id;";

            DAOFactory.connecter();

            MySqlDataReader reader = DAOFactory.execSQLRead(req);

            while (reader.Read())
            {
                // On ne renseigne pas le genre et la catégorie car on ne peut pas ouvrir 2 dataReader dans la même connexion
                DVD dvd = new DVD(Int32.Parse(reader[0].ToString()), reader[1].ToString(), reader[2].ToString(),
                    reader[3].ToString(), Int32.Parse(reader[4].ToString()), reader[5].ToString());
                lesDvd.Add(dvd);
            }
            DAOFactory.deconnecter();

            return lesDvd;
        }
        public static List<Exemplaire> getAllExemplaire()
        {
            List<Exemplaire> lesExemplaires = new List<Exemplaire>();
            string req = "SELECT d.id, d.titre, exemplaire.numero, exemplaire.DateAchat, exemplaire.idEtat FROM exemplaire JOIN document AS d ON exemplaire.idDocument = d.id;";

            DAOFactory.connecter();

            MySqlDataReader reader = DAOFactory.execSQLRead(req);

            while (reader.Read())
            {
                int id, idDocument, idEtat, idRayon;
                string titre;
                DateTime dateAchat;

                if (int.TryParse(reader[0].ToString(), out id) && int.TryParse(reader[1].ToString(), out idDocument) &&
                    DateTime.TryParse(reader[2].ToString(), out dateAchat) && int.TryParse(reader[3].ToString(), out idRayon) && int.TryParse(reader[4].ToString(), out idEtat))
                {
                    Exemplaire exemplaire = new Exemplaire(id, idDocument, dateAchat, idRayon, idEtat);
                    lesExemplaires.Add(exemplaire);
                }
                else
                {
                    // Gérer le cas où la conversion échoue
                    // Vous pouvez ignorer cette ligne ou journaliser un avertissement
                }
            }
            DAOFactory.deconnecter();

            return lesExemplaires;
        }
        public static List<Document> getDocuments()
        {
            List<Document> lesDocuments = new List<Document>();
            string req = "SELECT id, titre, image from document";

            DAOFactory.connecter();

            MySqlDataReader reader = DAOFactory.execSQLRead(req);

            while (reader.Read())
            {
                Document document = new Document(Int32.Parse(reader[0].ToString()), reader[1].ToString(), reader[2].ToString());

                lesDocuments.Add(document);
            }

            DAOFactory.deconnecter();

            return lesDocuments;
        }

        public static List<Exemplaire> getExemplairesByTitre( string titre)
        {
            List<Exemplaire> lesExemplaires = new List<Exemplaire>();
            string req = "SELECT * FROM document JOIN exemplaire ON document.id = exemplaire.idDocument WHERE document.titre LIKE '%" + titre + "%';";

            DAOFactory.connecter();

            MySqlDataReader reader = DAOFactory.execSQLRead(req);

            while (reader.Read())
            {
                Exemplaire exemplaire = new Exemplaire(Convert.ToInt32(reader[5]), Convert.ToInt32(reader[6]), Convert.ToDateTime(reader[7]), Convert.ToInt32(reader[8]), Convert.ToInt32(reader[9]), reader[1].ToString());
                lesExemplaires.Add(exemplaire);
            }

            DAOFactory.deconnecter();

            return lesExemplaires;
        }

        public static Categorie getCategorieByLivre(Livre pLivre)
        {
            Categorie categorie;
            string req = "Select p.id,p.libelle from public p,document d where p.id = d.idPublic and d.id='";
            req += pLivre.IdDoc + "'";

            DAOFactory.connecter();

            MySqlDataReader reader = DAOFactory.execSQLRead(req);

            if (reader.Read())
            {
                categorie = new Categorie(reader[0].ToString(), reader[1].ToString());
            }
            else
            {
                categorie = null;
            }
            DAOFactory.deconnecter();
            return categorie;
        }
        public static bool InsertExemplaire(string idDocument, int idRayon, int unNumero, DateTime uneDateAchat)
        {

            string req = "INSERT INTO exemplaire (idDocument, idRayon, numero, dateAchat ) " +
                "VALUES (" + Convert.ToInt32(idDocument) + ", " + idRayon + ", " + unNumero + ", '" + uneDateAchat.ToString("yyyy-MM-dd") + "')";

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

        // Méthode pour créer une commande dans la base de données
       

        public static List<int> getExemplaireIDS()
        {
            List<int> exemplarIDs = new List<int>();

            string req = "SELECT exemplaire.id FROM exemplaire JOIN document AS d ON exemplaire.idDocument = d.id";

            DAOFactory.connecter();

            using (MySqlDataReader reader = DAOFactory.execSQLRead(req))
            {
                try
                {
                    while (reader.Read())
                    {
                        int exemplarID = reader.GetInt32(0); // Récupération de l'ID de l'exemplaire
                        exemplarIDs.Add(exemplarID); // Ajout de l'ID à la liste
                    }
                }
                catch (Exception ex)
                {
                    // Gestion des exceptions (à adapter selon vos besoins)
                    Console.WriteLine("Une erreur s'est produite : " + ex.Message);
                }
            }

            DAOFactory.deconnecter();
            return exemplarIDs;
        }

        public static List<int> getDocumentIDS()
        {
            List<int> documentIDs = new List<int>();

            string req = "SELECT id FROM document";

            DAOFactory.connecter();

            using (MySqlDataReader reader = DAOFactory.execSQLRead(req))
            {
                try
                {
                    while (reader.Read())
                    {
                        int documentID = reader.GetInt32(0); // Récupération de l'ID du document
                        documentIDs.Add(documentID); // Ajout de l'ID à la liste
                    }
                }
                catch (Exception ex)
                {
                    // Gestion des exceptions (à adapter selon vos besoins)
                    Console.WriteLine("Une erreur s'est produite : " + ex.Message);
                }
            }

            DAOFactory.deconnecter();
            return documentIDs;
        }

        public static List<string> getEtat_commande()
        {
            List<string> etat_commande = new List<string>();

            string req = "SELECT libelle FROM etat";

            DAOFactory.connecter();

            using (MySqlDataReader reader = DAOFactory.execSQLRead(req))
            {
                try
                {
                    while (reader.Read())
                    {
                        string libelle = reader.GetString(0); // Récupération du libellé de l'état
                        etat_commande.Add(libelle); // Ajout du libellé à la liste
                    }
                }
                catch (Exception ex)
                {
                    // Gestion des exceptions (à adapter selon vos besoins)
                    Console.WriteLine("Une erreur s'est produite : " + ex.Message);
                }
            }

            DAOFactory.deconnecter();
            return etat_commande;
        }


        public static Document GetDocumentsByIdExemplaire(int idDocument)
        {
            Document document;
            string req = "Select id,titre,image from document Where id =" + idDocument;

            DAOFactory.connecter();

            MySqlDataReader reader = DAOFactory.execSQLRead(req);
            if (reader.Read())
            {

                document = new Document(Int32.Parse(reader[0].ToString()), reader[1].ToString(), reader[2].ToString());
            }

            else
            {
                document = null;
            }
            DAOFactory.deconnecter();
            return document;
        }

        public static List<Compte> getAllCompte()
        {
            List<Compte> lesComptes = new List<Compte>();
            string req = "Select * from compte";

            DAOFactory.connecter();

            MySqlDataReader reader = DAOFactory.execSQLRead(req);

            while (reader.Read())
            {
                Compte categorie = new Compte(Int32.Parse(reader[0].ToString()), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString());
                lesComptes.Add(categorie);
            }
            DAOFactory.deconnecter();
            return lesComptes;
        }


    }
}

