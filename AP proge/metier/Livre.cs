

namespace Mediateq_AP_SIO2.metier
{
    class Livre : Document
    {
        private string ISBN;
        private string auteur;
        private string laCollection;


        public Livre(int unId, string unTitre, string unISBN, string unAuteur, string uneCollection,string uneImage) : base(unId, unTitre, uneImage)
        {
            ISBN1 = unISBN;
            Auteur = unAuteur;
            LaCollection = uneCollection;
        }

        public string ISBN1 { get => ISBN; set => ISBN = value; }
        public string Auteur { get => auteur; set => auteur = value; }
        public string LaCollection { get => laCollection; set => laCollection = value; }
    }
}
