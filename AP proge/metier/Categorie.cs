

namespace Mediateq_AP_SIO2.metier
{
    class Categorie
    {
        private string id;
        private string libelle;

        public Categorie(string id, string libelle)
        {
            this.id = id;
            this.libelle = libelle;
        }

        public string Id { get => id; set => id = value; }
        public string Libelle { get => libelle; set => libelle = value; }
    }
}
