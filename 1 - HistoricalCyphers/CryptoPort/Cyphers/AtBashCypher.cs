using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoricalCypher.Cyphers
{
    static public class AtBashCypher
    {
        public static string Encryption(string Text, Alphabet alphabet)
        {
            string encryptedText = "";
            for (int i = 0; i < Text.Length; ++i)
            {
                encryptedText += Alphabet.GetAtBashEncryptedSign(alphabet, Text[i].ToString());
            }
            return encryptedText;
        }

        public static string Decryption(string Text, Alphabet alphabet)
        {
            string decryptedText = "";
            for (int i = 0; i < Text.Length; ++i)
            {
                decryptedText += Alphabet.GetAtBashDecryptedSign(alphabet, Text[i].ToString());
            }
            return decryptedText;
        }
    }
}
