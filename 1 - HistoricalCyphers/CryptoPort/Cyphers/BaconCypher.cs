using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoricalCypher.Cyphers
{
    static public class BaconCypher
    {
        static public string Encryption(string Text)
        {
            string encryptedText = "";
            for (int i = 0; i < Text.Length; ++i)
            {
                encryptedText += Alphabet.GetBaconEncryptedSign(Text[i].ToString());
            }
            return encryptedText;
        } 

        static public string Decryption(string Text)
        {
            string decryptedText = "";
            for (int i = 0; i < Text.Length; i+=5)
            {
                string letter = Text[i].ToString() + Text[i + 1].ToString() +
                   Text[i + 2].ToString() + Text[i + 3].ToString() + Text[i + 4].ToString();
                decryptedText += Alphabet.GetBaconDecryptedSign(letter);
            }
            return decryptedText;
        }
    }
}
