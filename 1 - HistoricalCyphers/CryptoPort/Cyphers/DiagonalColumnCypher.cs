using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HistoricalCypher;

namespace CryptoPort.Cyphers
{
    static public class DiagonalColumnCypher
    {

        static public string Encryption(string text, Alphabet alphabet, string cypherKey)
        {
            List<char> originalKey = cypherKey.ToList();
            List<char> sortedKey = cypherKey.ToList();
            sortedKey.Sort();

            int width = originalKey.Count();
            int height = text.Length % width > 0 ? (text.Length / width) + 1 : text.Length / width;
            string[,] EncryptedArray = new string[height, width];
            for (int i = 0; i < height; ++i)
            {
                for (int j = 0; j < width; ++j)
                {
                    try
                    {
                        EncryptedArray[i, j] = text[(i * width) + j].ToString();
                    }
                    catch (Exception)
                    {
                        EncryptedArray[i, j] = "A";
                    }
                }
            }

            string encryptedText = "";
            for (int i = 0; i < sortedKey.Count; i++)
            {
                int column = originalKey.FindIndex(x => x == sortedKey[i] && x != '/');
                originalKey[column] = '/';
                encryptedText += Alphabet.GetDiagonalColumnEncryptedSigns(column, EncryptedArray, width, height);
            }

            return encryptedText;

        }

        static public string Decryption(string text, Alphabet alphabet, string cypherKey)
        {
            List<char> originalKey = cypherKey.ToList();
            List<char> sortedKey = cypherKey.ToList();
            sortedKey.Sort();

            int width = originalKey.Count();
            int height = text.Length % width > 0 ? (text.Length / width) + 1 : text.Length / width;
            string[,] DecryptedArray = new string[height, width];

            int counter = 0;
            for (int j = 0; j < sortedKey.Count; j++)
            {
                int column = originalKey.FindIndex(x => x == sortedKey[j] && x != '/');
                originalKey[column] = '/';
                for (int i = 0; i < height; i++)
                {
                    DecryptedArray[i % height, (i + column) % width] =
                        Alphabet.GetDiagonalColumnDecryptedSignPosition(counter, text);
                    counter++;
                }
            }

            string decryptedString = "";
            for (int i = 0; i < height; ++i)
            {
                for (int j = 0; j < width; ++j)
                {
                    decryptedString += DecryptedArray[i, j].ToString();
                }
            }

            return decryptedString;
        }
    }
}
