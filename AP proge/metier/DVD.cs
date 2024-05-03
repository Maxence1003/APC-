using System;

namespace Mediateq_AP_SIO2.metier
{
    class DVD : Document
    {

        
        private string realisateur;
        private string synopsis;
        private int duree;

        public DVD(int unId, string unTitre, string unsynopsis, string unrealisateur, int uneDuree, string uneImage) : base(unId, unTitre, uneImage)
        {
            synopsis = unsynopsis;
            realisateur = unrealisateur;
            duree = uneDuree;

        }


        public string Synopsis { get => synopsis; set => synopsis = value; }
        public string Realisateur { get => realisateur; set => realisateur = value; }
        public int Duree { get => duree; set => duree = value; }
    }
}

