using AP_proge.metier;
using AP_proge.modele;
using Mediateq_AP_SIO2.metier;
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace Mediateq_AP_SIO2
{
    public partial class FrmMediateq : Form
    {
        #region Variables globales

        static List<Categorie> lesCategories;
        static List<Descripteur> lesDescripteurs;
        static List<Revue> lesRevues;
        static List<Livre> lesLivres;
        static List<DVD> lesDvd;
        static List<Document> lesDocuments;
        static List<Exemplaire> lesExemplaires;
        private List<Commande> lesCommandes;
        private List<Compte> lesComptes;


        #endregion


        #region Procédures évènementielles

        public FrmMediateq()
        {
            InitializeComponent();
        }

        private void FrmMediateq_Load(object sender, EventArgs e)
        {
            // Création de la connexion avec la base de données
            DAOFactory.creerConnection();

            // Chargement des objets en mémoire
            lesDescripteurs = DAODocuments.getAllDescripteurs();
            lesRevues = DAOPresse.getAllRevues();

            lesComptes = DAODocuments.getAllCompte();


            TabAllPage.TabPages.Clear();
            TabAllPage.TabPages.Add(TabConnexion);
        }

        #endregion


        #region Parutions
        //-----------------------------------------------------------
        // ONGLET "PARUTIONS"
        //------------------------------------------------------------
        private void tabParutions_Enter(object sender, EventArgs e)
        {
            cbxTitres.DataSource = lesRevues;
            cbxTitres.DisplayMember = "titre";
        }

        private void cbxTitres_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Parution> lesParutions;

            Revue titreSelectionne = (Revue)cbxTitres.SelectedItem;
            lesParutions = DAOPresse.getParutionByTitre(titreSelectionne);

            // ré-initialisation du dataGridView
            dgvParutions.Rows.Clear();

            // Parcours de la collection des titres et alimentation du datagridview
            foreach (Parution parution in lesParutions)
            {
                dgvParutions.Rows.Add(parution.Numero, parution.DateParution, parution.Photo);
            }

        }
        #endregion


        #region Revues
        //-----------------------------------------------------------
        // ONGLET "TITRES"
        //------------------------------------------------------------
        private void tabTitres_Enter(object sender, EventArgs e)
        {
            cbxDomaines.DataSource = lesDescripteurs;
            cbxDomaines.DisplayMember = "libelle";
        }

        private void cbxDomaines_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Objet Domaine sélectionné dans la comboBox
            Descripteur domaineSelectionne = (Descripteur)cbxDomaines.SelectedItem;

            // ré-initialisation du dataGridView
            dgvTitres.Rows.Clear();

            // Parcours de la collection des titres et alimentation du datagridview
            foreach (Revue revue in lesRevues)
            {
                if (revue.IdDescripteur == domaineSelectionne.Id)
                {
                    dgvTitres.Rows.Add(revue.Id, revue.Titre, revue.Empruntable, revue.DateFinAbonnement, revue.DelaiMiseADispo);
                }
            }
        }
        #endregion


        #region Livres
        //-----------------------------------------------------------
        // ONGLET "LIVRES"
        //-----------------------------------------------------------

        private void tabLivres_Enter(object sender, EventArgs e)
        {
            // Chargement des objets en mémoire
            lesCategories = DAODocuments.getAllCategories();
            lesDescripteurs = DAODocuments.getAllDescripteurs();
            lesLivres = DAODocuments.getAllLivres();
        }

        private void btnRechercher_Click(object sender, EventArgs e)
        {
            // On réinitialise les labels
            lblNumero.Text = "";
            lblTitre.Text = "";
            lblAuteur.Text = "";
            lblCollection.Text = "";
            lblISBN.Text = "";
            lblImage.Text = "";

            // On recherche le livre correspondant au numéro de document saisi.
            // S'il n'existe pas: on affiche un popup message d'erreur
            bool trouve = false;
            foreach (Livre livre in lesLivres)
            {
                if (livre.IdDoc.ToString() == txbNumDoc.Text)
                {
                    lblNumero.Text = livre.IdDoc.ToString();
                    lblTitre.Text = livre.Titre;
                    lblAuteur.Text = livre.Auteur;
                    lblCollection.Text = livre.LaCollection;
                    lblISBN.Text = livre.ISBN1;
                    lblImage.Text = livre.Image;
                    trouve = true;
                }
            }
            if (!trouve)
                MessageBox.Show("Document non trouvé dans les livres");
        }

        private void txbTitre_TextChanged(object sender, EventArgs e)
        {
            dgvLivres.Rows.Clear();

            // On parcourt tous les livres. Si le titre matche avec la saisie, on l'affiche dans le datagrid.
            foreach (Livre livre in lesLivres)
            {
                // on passe le champ de saisie et le titre en minuscules car la méthode Contains
                // tient compte de la casse.
                string saisieMinuscules;
                saisieMinuscules = txbTitre.Text.ToLower();
                string titreMinuscules;
                titreMinuscules = livre.Titre.ToLower();

                //on teste si le titre du livre contient ce qui a été saisi
                if (titreMinuscules.Contains(saisieMinuscules))
                {
                    dgvLivres.Rows.Add(livre.IdDoc, livre.Titre, livre.Auteur, livre.ISBN1, livre.LaCollection);
                }
            }
        }
        #endregion

        #region DVD
        //-----------------------------------------------------------
        // ONGLET "LIVRES"
        //-----------------------------------------------------------

        private void tabDVD_Enter(object sender, EventArgs e)
        {
            // Chargement des objets en mémoire
            lesCategories = DAODocuments.getAllCategories();
            lesDescripteurs = DAODocuments.getAllDescripteurs();
            lesLivres = DAODocuments.getAllLivres();
            lesDvd = DAODocuments.getAllDvd();

        }

        private void btnRechercherDVD_Click(object sender, EventArgs e)
        {
            // On réinitialise les labels
            label18.Text = "";
            label14.Text = "";
            label20.Text = "";
            label19.Text = "";
            label27.Text = "";
            lblImage.Text = "";

            // On recherche le livre correspondant au numéro de document saisi.
            // S'il n'existe pas: on affiche un popup message d'erreur
            bool trouve = false;
            foreach (DVD dvd in lesDvd)
            {
                if (dvd.IdDoc == Convert.ToInt32(txbdvdDoc.Text))
                {
                    label18.Text = dvd.IdDoc.ToString();
                    label14.Text = dvd.Titre;
                    label20.Text = dvd.Synopsis;
                    label19.Text = dvd.Realisateur;
                    label27.Text = Convert.ToInt32(dvd.Duree).ToString();
                    lblImage.Text = dvd.Image;
                    trouve = true;
                }
            }
            if (!trouve)
                MessageBox.Show("Document non trouvé dans les livres");
        }

        private void txbTitreDVD_TextChanged(object sender, EventArgs e)
        {
            dvgDVD.Rows.Clear();

            // On parcourt tous les livres. Si le titre matche avec la saisie, on l'affiche dans le datagrid.
            foreach (DVD DVD in lesDvd)
            {

                // on passe le champ de saisie et le titre en minuscules car la méthode Contains
                // tient compte de la casse.
                string saisieMinuscules;
                saisieMinuscules = TxTitre.Text.ToLower();
                string titreMinuscules;
                titreMinuscules = DVD.Titre.ToLower();

                //on teste si le titre du livre contient ce qui a été saisi
                if (titreMinuscules.Contains(saisieMinuscules))
                {
                    dvgDVD.Rows.Add(DVD.IdDoc, DVD.Titre, DVD.Synopsis, DVD.Realisateur, DVD.Duree);
                }
            }
        }
        #endregion


        private void Tab_Exemplaire_Enter(object sender, EventArgs e)
        {
            // Chargement des objets en mémoire
            lesCategories = DAODocuments.getAllCategories();
            lesDescripteurs = DAODocuments.getAllDescripteurs();
            lesLivres = DAODocuments.getAllLivres();
            lesDvd = DAODocuments.getAllDvd();
            lesCommandes = DAOCommande.GetCommandes();
            lesDocuments = DAODocuments.getDocuments();
            lesExemplaires = DAODocuments.getAllExemplaire();

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();

            lesExemplaires = DAODocuments.getExemplairesByTitre(textBox5.Text);

            // On parcourt tous les livres. Si le titre matche avec la saisie, on l'affiche dans le datagrid.
            foreach (Exemplaire exemplaire in lesExemplaires)
            {

                dataGridView1.Rows.Add(exemplaire.IdDoc, exemplaire.NomDoc, exemplaire.Numero, exemplaire.DateAchat, exemplaire.IdRayon, exemplaire.IdEtat);

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabOngletsApplication_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CreeExemplaire_Click(object sender, EventArgs e)
        {
            try
            {
                // On récupère les valeurs des TextBox
                string unId = textBox1.Text;
                int unNumero = Convert.ToInt32(textBoxNumero.Text);
                DateTime uneDateAchat = dateTimePicker1.Value;
                int idRayon = Convert.ToInt32(textBoxIdRayon.Text);

                // Appel de la méthode pour insérer un exemplaire
                if (DAODocuments.InsertExemplaire(unId, idRayon, unNumero, uneDateAchat))
                {
                    // Si l'insertion réussit, afficher un message de confirmation
                    MessageBox.Show("Exemplaire créé avec succès.");
                    // Rafraîchir la liste des exemplaires
                    lesExemplaires = DAODocuments.getAllExemplaire();
                }
                else
                {
                    // Sinon, afficher un message d'erreur
                    MessageBox.Show("Erreur lors de la création de l'exemplaire. Veuillez réessayer plus tard.");
                }
            }
            catch (FormatException)
            {
                // Capturer une exception si la conversion de texte en entier échoue
                MessageBox.Show("Erreur de format : Vérifiez que les champs numériques sont corrects.");
            }
            catch (Exception ex)
            {
                // Capturer toutes les autres exceptions non prévues
                MessageBox.Show("Une erreur s'est produite : " + ex.Message);
            }
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void CréerCommande_Click(object sender, EventArgs e)
        {

            try
            {
                int idDocument = 0;
                if (cbxIdDoc.SelectedItem != null)
                {
                    idDocument = Convert.ToInt32(cbxIdDoc.SelectedItem);
                }

                int nombreExemplaires = Convert.ToInt32(txtBoxNBex.Value);
                decimal prixUnitaire = PrixUnit.Value;
                DateTime datecommande = dtPCommande.Value;

                // Créer la commande
                if (DAOCommande.CreateCommand(nombreExemplaires, datecommande, prixUnitaire, idDocument))
                {
                    MessageBox.Show("Commande créée avec succès");
                    // Mettre à jour les données si nécessaire
                    lesCommandes = DAOCommande.GetCommandes();
                    // Par exemple, vous pouvez mettre à jour l'affichage des commandes dans une liste
                }
                else
                {
                    MessageBox.Show("Erreur lors de la création de la commande");
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Erreur de format : Vérifiez que les champs numériques sont corrects.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur s'est produite : " + ex.Message);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Récupérer les IDs d'exemplaires depuis la DAO
            List<int> exemplaireIDs = DAODocuments.getExemplaireIDS();

            // Effacer la liste déroulante existante
            cbxIdDoc.Items.Clear();

            // Ajouter les IDs d'exemplaires à la liste déroulante
            foreach (int id in exemplaireIDs)
            {
                cbxIdDoc.Items.Add(id.ToString());
            }
        }

        private void Tab1_ENTER(object sender, EventArgs e)
        {
            montTextBox.Enabled = false;

            lesCommandes = DAOCommande.GetCommandes();

        }

        private void label34_Click(object sender, EventArgs e)
        {

        }

        #region commande

        private void tabCreateCommande_Enter(object sender, EventArgs e)
        {
            lesCommandes = DAOCommande.GetCommandes();
        }

        private void ConnexionUser_Click(object sender, EventArgs e)
        {
            lesCommandes = DAOCommande.GetCommandes();

        }



        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            dgvCommande.Rows.Clear();

            foreach (Commande commande in lesCommandes)
            {
                string saisieMinuscules;
                saisieMinuscules = textBox10.Text.ToLower();
                string titreMinuscules;
                titreMinuscules = DAOCommande.GetCommandebyId(commande.getId()).getId().ToString().ToLower();

                if (titreMinuscules.Contains(saisieMinuscules))
                {
                    dgvCommande.Rows.Add(commande.getId(), commande.getNbExemplaire(), commande.getDateCommande(), commande.getMontant(), commande.getIdDocument(), commande.getEtat_Commande());
                }
            }
        }
        #endregion commande

        private void cbxIdDoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Récupérer les IDs des documents depuis la DAO
            List<int> documentIDs = DAODocuments.getDocumentIDS();

            // Effacer la liste déroulante existante
            cbxIdDoc.Items.Clear();

            // Ajouter les IDs des documents à la liste déroulante
            foreach (int id in documentIDs)
            {
                cbxIdDoc.Items.Add(id.ToString());
            }
        }

        private void Tab1_(object sender, EventArgs e)
        {

        }
        #region Commande
        private void add(object sender, EventArgs e)
        {
            // Récupérer les IDs des documents depuis la DAO
            List<int> documentIDs = DAODocuments.getDocumentIDS();
            List<string> etat_commande = DAODocuments.getEtat_commande();

            List<Commande> lesCommandes = DAOCommande.GetCommandes();

            // Effacer la liste déroulante existante
            cbxIdDoc.Items.Clear();
            comboBoxEtat.Items.Clear();

            // Ajouter les IDs des documents à la liste déroulante
            foreach (int id in documentIDs)
            {
                cbxIdDoc.Items.Add(id.ToString());
            }

            foreach (string id in etat_commande)
            {
                comboBoxEtat.Items.Add(id.ToString());
            }



        }

        private void montTextBox_TextChanged(object sender, EventArgs e)
        {

        }


        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            int Montant = Convert.ToInt32(txtBoxNBex.Value) * Convert.ToInt32(PrixUnit.Value);
            montTextBox.Text = Montant.ToString();
        }

        private void txtBoxNBex_ValueChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            int idCommande = Convert.ToInt32(textBox10.Text); //on récupère lId 
            int idEtat_Commande = (comboBoxEtat.SelectedIndex + 1); //on récupère l etat 
            try
            {
                DAOCommande.UpdateCommande(idCommande, idEtat_Commande);
                dgvCommande.Rows.Clear();
                Commande commande = DAOCommande.GetCommandebyId(idCommande);
                dgvCommande.Rows.Add(commande.getId(), commande.getNbExemplaire(), commande.getDateCommande(), commande.getMontant(), commande.getIdDocument(), commande.getEtat_Commande());
                MessageBox.Show("L'état de la commande a été modifié avec succès !");


            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur s'est produite lors de la modification de l'état de la commande : " + ex.Message);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();

            foreach (Exemplaire exemplaire in lesExemplaires)
            {
                string saisieMinuscules;
                saisieMinuscules = textBox2.Text.ToLower();
                string titreMinuscules;
                titreMinuscules = exemplaire.IdRayon.ToString().ToLower();

                if (titreMinuscules.Contains(saisieMinuscules))
                {
                    dataGridView1.Rows.Add(exemplaire.IdDoc, DAODocuments.GetDocumentsByIdExemplaire(exemplaire.IdDoc).Titre, exemplaire.Numero, exemplaire.DateAchat, exemplaire.IdRayon, exemplaire.IdEtat);
                }
            }

        }

        private void textBox10_TextChanged_1(object sender, EventArgs e)
        {
            dgvCommande.Rows.Clear();

            foreach (Commande commande in lesCommandes)
            {
                string saisieMinuscules;
                saisieMinuscules = textBox10.Text.ToLower();
                string titreMinuscules;
                titreMinuscules = DAOCommande.GetCommandebyId(commande.getId()).getId().ToString().ToLower();

                if (titreMinuscules.Contains(saisieMinuscules))
                {
                    dgvCommande.Rows.Add(commande.getId(), commande.getNbExemplaire(), commande.getDateCommande(), commande.getMontant(), commande.getIdDocument(), commande.getEtat_Commande());
                }
            }

        }

        private void cbxIdDoc_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }


        //Code du bouton valider la connexion
        private void BtnConnexion_Click(object sender, EventArgs e)
        {
            // Récupérer le login et le mot de passe saisis par l'utilisateur
            string login = txbLogin.Text;
            string mdp = txbMdp.Text;

            //string NewMDPhash = Hash.CalculerHashSHA256(mdp); 

            // Parcourir la liste des comptes pour vérifier les identifiants
            foreach (Compte c1 in lesComptes)
            {
                if (login == c1.Identifiant)
                {
                    if (Hash.VerifHashSHA256(mdp, c1.Mdp))
                    {
                        // Si les identifiants sont valides, effectuer les actions appropriées
                        TabAllPage.TabPages.Remove(TabConnexion);

                        switch (c1.Service.ToString())
                        {
                            case "Admin":
                                // Ajouter les onglets pour l'administration
                                TabAllPage.TabPages.Add(tabTitres);
                                TabAllPage.TabPages.Add(TabCommande);
                                TabAllPage.TabPages.Add(GestionExemplaire);
                                TabAllPage.TabPages.Add(tabParutions);
                                TabAllPage.TabPages.Add(tabLivres);
                                TabAllPage.TabPages.Add(tabDVD);
                                break;

                            case "Culture":
                            case "Administratif":
                            case "Prets":
                                // Ajouter les onglets pour les autres services
                                TabAllPage.TabPages.Add(tabParutions);
                                TabAllPage.TabPages.Add(tabTitres);
                                TabAllPage.TabPages.Add(tabLivres);
                                TabAllPage.TabPages.Add(tabDVD);
                                break;
                        }
                    }
                    else
                    {
                        // Afficher un message d'erreur en cas de mot de passe incorrect
                        MessageBox.Show("Erreur de mot de passe ou de login");
                    }
                }
            }


        }
    }
}
//La fonction qui effecture le hash du mot de passe




#endregion