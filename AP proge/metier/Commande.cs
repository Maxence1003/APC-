using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP_proge.metier
{
    internal class Commande
    {
        private int id;
        private int nbExemplaire;
        private DateTime dateCommande;
        private double montant;
        private int idDocument;
        private int etat_commande;

        public Commande(int unId, int nbExemplaire, DateTime dateCommande, double montant, int idDocument, int etat_commande)
        {
            this.id = unId;
            this.nbExemplaire = nbExemplaire;
            this.dateCommande = dateCommande;
            this.montant = montant;
            this.idDocument = idDocument;
            this.etat_commande = etat_commande;
        }

        public int getId() { return id; }
        public int getNbExemplaire() { return nbExemplaire; }
        public DateTime getDateCommande() { return dateCommande; }
        public double getMontant() { return montant; }
        public int getIdDocument() { return idDocument; }
        public int getEtat_Commande() { return etat_commande; }
    }
}
