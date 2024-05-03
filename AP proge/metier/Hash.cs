using System;
using System.Security.Cryptography;
using System.Text;

namespace AP_proge.metier
{
    internal static class Hash
    {
        public static string CalculerHashSHA256(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);
                string hashString = BitConverter.ToString(hashBytes).Replace("-", String.Empty);
                return hashString;
            }
        }

        public static bool VerifHashSHA256(string mdp, string mdphash)
        {
            // Calculer le hash du mot de passe fourni
            string hashMdp = CalculerHashSHA256(mdp);

            // Comparer les deux hashs pour vérifier s'ils correspondent
            return string.Equals(hashMdp, mdphash, StringComparison.OrdinalIgnoreCase);
        }
    }
}
