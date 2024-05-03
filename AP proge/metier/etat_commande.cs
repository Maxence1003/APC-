using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP_proge.metier
{
    internal class etat_commande
    {
        public int Id { get; set; }
        public string Libelle { get; set; }

        public etat_commande(int unId, string unLibelle)
        {
            Id = unId;
            Libelle = unLibelle;
        }
    }
}
