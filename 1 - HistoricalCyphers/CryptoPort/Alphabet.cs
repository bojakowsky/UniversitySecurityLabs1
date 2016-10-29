using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HistoricalCypher
{
    public class Alphabet
    {
        private string[] defaultAlphabet = new string[]
        { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L",
          "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X",
          "Y", "Z", "_", " ", ".", ",","(", ")", "\n", "\t","1","2",
          "3","4","5","6","7","8","9","10", "]" , "[" , "/" , "\"",
          "\\", "{", "}","'", "\"", ";","-", ":", "=", "+", "<", ">",
          "!","@","#","$","%","^","&","*" };

        static public string[,] polibushAlphabet = new string[,]
        {
          { "K", "L", "U", "C", "Z" },
          { "A", "B", "D", "E", "F" },
          { "G","H", "IJ", "M", "N" },
          { "O", "P", "Q", "R", "S" },
          { "T", "V", "W", "X", "Y" }
        };

        static public string[,] baconAlphabet = new string[,]
        {
            { "*****", "****B", "***B*", "***BB", "**B**", "**B*B", "**BB*","**BBB",
            "*B***", "*B**B", "*B*B*", "*B*BB", "*BB**", "*BB*B", "*BBB*", "*BBBB",
            "B****", "B***B", "B**B*", "B**BB", "B*B**", "B*B*B", "B*BB*", "B*BBB",
            "BB***", "BB**B" },

            { "A", "B", "C", "D", "E", "F", "G", "H",
              "I", "J", "K", "L", "M", "N", "O", "P",
              "Q", "R", "S", "T", "U", "V", "W", "X",
              "Y", "Z" }
        };

        private string[] definedAlphabet;
        private string[] standardAlphabet;

        public string[] AlphabetGetter
        {
            get
            {
                if (definedAlphabet == null) return standardAlphabet;
                else return definedAlphabet;
            }
            private set { }
        }

        public Alphabet()
        {
            standardAlphabet = defaultAlphabet;
        }

        public Alphabet(string[] alphabet)
        {
            definedAlphabet = alphabet;
        }

        static public string GetCesarEncodedSign(Alphabet alphabet, string sign, int shift)
        {
            for (int i = 0; i < alphabet.AlphabetGetter.Length; ++i)
            {
                if (sign.ToUpper() == alphabet.AlphabetGetter[i])
                    return alphabet.AlphabetGetter[(i + shift) % alphabet.AlphabetGetter.Length];
            }
            return "-1";
        }

        static public string GetCesarDecryptedSign(Alphabet alphabet, string sign, int shift)
        {
            for (int i = 0; i < alphabet.AlphabetGetter.Length; ++i)
            {
                if (sign.ToUpper() == alphabet.AlphabetGetter[i])
                {
                    int index = i - shift;
                    if (i - shift < 0) index = alphabet.AlphabetGetter.Length + i - shift;
                    return alphabet.AlphabetGetter[(index) % alphabet.AlphabetGetter.Length];
                }
            }
            return "-1";
        }

        static public string GetAtBashEncryptedSign(Alphabet alphabet, string sign)
        {
            string[] reversedAlphabet = alphabet.standardAlphabet.Reverse().ToArray();
            for (int i = 0; i < alphabet.standardAlphabet.Length; ++i)
            {
                if (alphabet.standardAlphabet[i] == sign.ToUpper()) return reversedAlphabet[i];
            }
            return "-1";
        }

        static public string GetAtBashDecryptedSign(Alphabet alphabet, string sign)
        {
            string[] reversedAlphabet = alphabet.standardAlphabet.Reverse().ToArray();
            for (int i = 0; i < reversedAlphabet.Length; ++i)
            {
                if (reversedAlphabet[i] == sign.ToUpper()) return alphabet.standardAlphabet[i];
            }
            return "-1";
        }

        static public string GetPolibushEncryptedSign(string sign)
        {
            string key = "";
            for (int i = 0; i < 5; ++i)
            {
                for (int j = 0; j < 5; ++j)
                {
                    if (Alphabet.polibushAlphabet[i, j].Contains(sign.ToUpper())) return key = (j + 1).ToString() + (i + 1).ToString();
                }
            }
            return key;
        }

        static public string GetPolibushDecryptedSign(string sign)
        {
            try
            {
                int j = int.Parse(sign[0].ToString());
                int i = int.Parse(sign[1].ToString());
                return Alphabet.polibushAlphabet[i - 1, j - 1];
            }
            catch (Exception)
            {
                return "";
            }
        }

        static public string GetBaconEncryptedSign(string sign)
        {
            for (int i = 0; i < baconAlphabet.Length / 2; ++i)
            {
                if (sign.ToUpper() == baconAlphabet[1, i]) return baconAlphabet[0, i];
            }
            return "";
        }

        static public string GetBaconDecryptedSign(string sign)
        {
            for (int i = 0; i < baconAlphabet.Length / 2; ++i)
            {
                if (sign.ToUpper() == baconAlphabet[0, i]) return baconAlphabet[1, i];
            }
            return "";
        }



        static public string GetDiagonalColumnEncryptedSigns(int column, string[,] array, int width, int heigth)
        {
            string toReturn = "";
            for (int i = 0; i < heigth; ++i)
            {
                toReturn += array[i, (column + i) % width];
                //toReturn += array[(column + i) % width, i];
            }
            return toReturn;
        }

        static public string GetDiagonalColumnDecryptedSignPosition(int index, string text)
        {
            try
            {
                return text[index].ToString();
            }
            catch (Exception)
            {
                return "A";
            }
        }
    }
}
