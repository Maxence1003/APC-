using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediateq_AP_SIO2.metier
{
    class Exemplaire
    {
        private int idDocument;
        private int numero;
        private DateTime dateAchat;
        private int idRayon;
        private int idEtat;
        private string nomDoc;

        public Exemplaire(int unId, int unNumero, DateTime unedateAchat, int unidRayon, int unidEtat, string unNomDoc=null)
        {
            idDocument = unId;
            numero = unNumero;
            dateAchat = unedateAchat;
            idRayon = unidRayon;
            idEtat = unidEtat;
            nomDoc = unNomDoc;
        }


        public int IdDoc { get => idDocument; set => idDocument = value; }
        public int Numero { get => numero; set => numero = value; }
        public DateTime DateAchat { get => dateAchat; set => dateAchat = value; }
        public int IdRayon { get => idRayon; set => idRayon = value; }
        public int IdEtat { get => idEtat; set => idEtat = value; }

        public string NomDoc { get => nomDoc; set => nomDoc = value; }

    }
}
