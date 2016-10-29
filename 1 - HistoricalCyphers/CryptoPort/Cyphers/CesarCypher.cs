using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoricalCypher.Cyphers
{
    static public class CesarClassicCypher
    {
        public static string Encryption(int shift, string Text, Alphabet alphabet)
        {
            string encryptedText = "";
            for (int i = 0; i < Text.Length; ++i)
            {
                encryptedText += Alphabet.GetCesarEncodedSign(alphabet, Text[i].ToString(), shift);
            }
            return encryptedText;
        }

        public static string Decryption(int shift, string Text, Alphabet alphabet)
        {
            string decryptedText = "";
            for (int i = 0; i < Text.Length; ++i)
            {
                decryptedText += Alphabet.GetCesarDecryptedSign(alphabet, Text[i].ToString(), shift);
            }
            return decryptedText;
        }
    }
}
