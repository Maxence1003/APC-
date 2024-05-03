using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;

namespace AP_proge.metier
{
    internal class Compte
    {
        private int compte_id;
        private string mdp;
        private string nom;
        private string prenom;
        private string service;
        private string identifiant;

        public Compte(int unCompteId, string unIdentifiant, string unPrenom, string unNom,  string unMdp, string unService) 
        {
            compte_id = unCompteId;
            mdp = unMdp;
            service = unService;
            nom = unNom;
            prenom = unPrenom;
            identifiant = unIdentifiant;
        }
        public int CompteId { get => compte_id; set => compte_id = value; }
        public string Mdp { get => mdp; set => mdp = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public string Service { get => service; set => service = value; }
        public string Identifiant { get => identifiant; set => identifiant = value; }
    }
}
