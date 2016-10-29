using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoricalCypher.Cyphers
{
    static public class PolibushCypher
    {
        public static string Encryption(string Text)
        {
            string encryptedText = "";
            for (int i = 0; i < Text.Length; ++i)
            {
                encryptedText += Alphabet.GetPolibushEncryptedSign(Text[i].ToString());
            }
            return encryptedText;
        }

        public static string Decryption(string Text)
        {
            string decryptedText = "";
            for (int i = 0; i < Text.Length; i+=2)
            {
                decryptedText += Alphabet.GetPolibushDecryptedSign(Text[i].ToString() + Text[i+1].ToString());
            }
            return decryptedText;
        }
    }
}
