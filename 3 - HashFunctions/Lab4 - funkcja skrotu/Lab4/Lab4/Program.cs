using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Diagnostics;
using HistoricalCypher.Cyphers;
using CryptoPort.Cyphers;
using HistoricalCypher;

namespace Lab4
{
    class Program
    {
        static void PrintBytesInConsole(string Title, byte[] array)
        {
            Console.WriteLine("_______" + Title + " _______");
            foreach (var b in array)
            {
                Console.Write("[" + b.ToString() + "]\t");
            }
            Console.WriteLine();
        }

        public static void PrintBitsInConsole(string Title, byte[] array)
        {
            Console.WriteLine("_______" + Title + " _______");
            foreach (var b in array)
            {
                Console.Write("[" + Convert.ToString(b, 2).PadLeft(8, '0') + "]\t");
            }
            Console.WriteLine();
        }

        static void PrintLine()
        {
            Console.WriteLine("###################################### \n");
        }

        static void NextTextLine()
        {
            Console.WriteLine("\n\n\n");
        }

        public static Stopwatch timer = new Stopwatch();

        public static void PrintDifference(byte[] ok, byte[] error)
        {

            double difference = 0;
            double total = 0;
            Console.WriteLine("");
            try
            {
                for (int i = 0; i < ok.Count(); i++)
                {

                    string okBits = Convert.ToString(ok[i], 2).PadLeft(8, '0');
                    string errorBits = Convert.ToString(error[i], 2).PadLeft(8, '0');

                    for (int j = 0; j < 8; j++)
                    {
                        if (okBits[j] != errorBits[j])
                        {
                            difference++;
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.Write(okBits[j] + "\t");
                        }
                        else
                        {
                            Console.ResetColor();
                            Console.Write(okBits[j] + "\t");
                        }
                        total++;
                    }
                    Console.ResetColor();
                }
            }
            catch(Exception)
            { }
            Console.WriteLine("\n");

            Console.WriteLine("Percantage of bits that have changed: " + ((double)(difference / total)) + "%");

        }

        public static void MD5Test(string FileName, byte[] inputBytes, byte[] inputErrorBytes)
        {
            #region MD5
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            timer.Start();
            byte[] md5_hash = md5.ComputeHash(inputBytes);
            Console.WriteLine("OK: MD5 " + timer.ElapsedMilliseconds.ToString() + " [ms]");
            timer.Restart();

            byte[] md5_hash_error = md5.ComputeHash(inputErrorBytes);
            Console.WriteLine("ERROR: MD5 " + timer.ElapsedMilliseconds.ToString() + " [ms]");
            Console.WriteLine();
            timer.Stop();
            timer.Reset();

            PrintBytesInConsole("md5_hash_" + FileName, md5_hash);
            PrintBitsInConsole("md5_hash_" + FileName, md5_hash);
            PrintLine();
            PrintBytesInConsole("md5_hash_error_" + FileName, md5_hash_error);
            PrintBitsInConsole("md5_hash_error_" + FileName, md5_hash_error);

            PrintDifference(md5_hash, md5_hash_error);
            PrintLine();

            BytesToFile("MD5_hashed_" + FileName + ".txt", md5_hash);
            BytesToFile("MD5_hashed_error_" + FileName + ".txt", md5_hash_error);
            NextTextLine();

            #endregion
        }

        public static void SHA1Test(string FileName, byte[] inputBytes, byte[] inputErrorBytes)
        {
            #region SHA1
            SHA1 sha1 = System.Security.Cryptography.SHA1.Create();
            timer.Start();
            byte[] sha1_hash = sha1.ComputeHash(inputBytes);
            Console.WriteLine("OK: SHA1 " + timer.ElapsedMilliseconds.ToString() + " [ms]");
            timer.Restart();

            byte[] sha1_hash_error = sha1.ComputeHash(inputErrorBytes);
            Console.WriteLine("ERROR: SHA1 " + timer.ElapsedMilliseconds.ToString() + " [ms]");
            Console.WriteLine();
            timer.Stop();
            timer.Reset();

            PrintBytesInConsole("sha1_hash_" + FileName, sha1_hash);
            PrintBitsInConsole("sha1_hash_" + FileName, sha1_hash);
            PrintLine();
            PrintBytesInConsole("sha1_hash_error_" + FileName, sha1_hash_error);
            PrintBitsInConsole("sha1_hash_error_" + FileName, sha1_hash_error);

            PrintDifference(sha1_hash, sha1_hash_error);
            PrintLine();

            BytesToFile("SHA1_hashed_" + FileName + ".txt", sha1_hash);
            BytesToFile("SHA1_hashed_error_" + FileName + ".txt", sha1_hash_error);
            #endregion
        }

        public static void SHA256Test(string FileName, byte[] inputBytes, byte[] inputErrorBytes)
        {
            #region SHA256
            SHA256 sha256 = System.Security.Cryptography.SHA256.Create();
            timer.Start();
            byte[] sha256_hash = sha256.ComputeHash(inputBytes);
            Console.WriteLine("OK: SHA256 " + timer.ElapsedMilliseconds.ToString() + " [ms]");
            timer.Restart();

            byte[] sha256_hash_error = sha256.ComputeHash(inputErrorBytes);
            Console.WriteLine("ERROR: SHA256 " + timer.ElapsedMilliseconds.ToString() + " [ms]");
            Console.WriteLine();
            timer.Stop();
            timer.Reset();

            PrintBytesInConsole("sha256_hash_" + FileName, sha256_hash);
            PrintBitsInConsole("sha256_hash_" + FileName, sha256_hash);
            PrintLine();
            PrintBytesInConsole("sha256_hash_error_" + FileName, sha256_hash_error);
            PrintBitsInConsole("sha256_hash_error_" + FileName, sha256_hash_error);

            PrintDifference(sha256_hash, sha256_hash_error);
            PrintLine();

            BytesToFile("SHA256_hashed_" + FileName + ".txt", sha256_hash);
            BytesToFile("SHA256_hashed_error_" + FileName + ".txt", sha256_hash_error);
            #endregion
        }

        public static void SHA512Test(string FileName, byte[] inputBytes, byte[] inputErrorBytes)
        {
            #region SHA512
            SHA512 sha512 = System.Security.Cryptography.SHA512.Create();
            timer.Start();
            byte[] sha512_hash = sha512.ComputeHash(inputBytes);
            Console.WriteLine("OK: SHA512 " + timer.ElapsedMilliseconds.ToString() + " [ms]");

            timer.Restart();
            byte[] sha512_hash_error = sha512.ComputeHash(inputErrorBytes);
            Console.WriteLine("ERROR: SHA512 " + timer.ElapsedMilliseconds.ToString() + " [ms]");
            Console.WriteLine();
            timer.Stop();
            timer.Reset();

            PrintBytesInConsole("sha512_hash_" + FileName, sha512_hash);
            PrintBitsInConsole("sha512_hash_" + FileName, sha512_hash);
            PrintLine();
            PrintBytesInConsole("sha512_hash_error_" + FileName, sha512_hash_error);
            PrintBitsInConsole("sha512_hash_error_" + FileName, sha512_hash_error);

            PrintDifference(sha512_hash, sha512_hash_error);
            PrintLine();

            BytesToFile("SHA512_hashed_" + FileName + ".txt", sha512_hash);
            BytesToFile("SHA512_hashed_error_" + FileName + ".txt", sha512_hash_error);
            #endregion
        }

        public static byte[] CustomHashFunction(string input)
        {
            Alphabet alphabet = new Alphabet();
            int blockSize = 32;
            int length = input.Length % blockSize == 0 ? input.Length / blockSize : input.Length / blockSize + 1;
            input = input.PadRight(32 * length, 'X');
            // input = input.ToUpper();

            string initial = new string(input.Skip((0) * blockSize).Take(blockSize).ToArray());
            string initialEncoded = DiagonalColumnCypher.Encryption(initial, alphabet, "Secret1!");

            for (int i = 1; i < length; i++)
            {
                string first = "";
                try
                {
                    first = new string(input.Skip((i) * blockSize).Take(blockSize).ToArray());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                byte[] bytesToEncode = XOR(FromStringToByteArray(initialEncoded), FromStringToByteArray(first));
                string stringToEncode = FromByteArrayToString(bytesToEncode);

                initialEncoded = DiagonalColumnCypher.Encryption(stringToEncode, alphabet, "Secret1!");
            }
            return FromStringToByteArray(initialEncoded);
        }

        public static byte[] CustomHashFunctionWithoutCipher(string input)
        {
            int blockSize = 32;
            int length = input.Length % blockSize == 0 ? input.Length / blockSize : input.Length / blockSize + 1;
            input = input.PadRight(32 * length, 'X');

            string initial = new string(input.Skip((0) * blockSize).Take(blockSize).ToArray());

            for (int i = 1; i < length; i++)
            {
                string first = "";
                try
                {
                    first = new string(input.Skip((i) * blockSize).Take(blockSize).ToArray());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                byte[] bytesToEncode = XOR(FromStringToByteArray(initial), FromStringToByteArray(first));
                initial = FromByteArrayToString(bytesToEncode);


            }
            return FromStringToByteArray(initial);
        }

        public static byte[] CustomHashFunctionMathemathic(string input,int type = 1)
        {
            if (type == 0)
            {
                UInt64 hashedValue = 3074457345618258791ul;

                for (int i = 0; i < input.Length; i++)
                {
                    hashedValue += input[i];
                    hashedValue *= 3074457345618258799ul;
                }
                string result = hashedValue.ToString();
                return FromStringToByteArray(result);
            }
            else
            {
                UInt64 value = 3074457345618258791ul;
                string initial = (input[0] * value).ToString();
                for (int i = 1; i < input.Length; i++)
                {
                    string buf = (input[i] * value).ToString();
                    byte[] xor = XOR(FromStringToByteArray(buf), FromStringToByteArray(initial));
                    initial = FromByteArrayToString(xor);
                }
                return FromStringToByteArray(initial);
            }
        }
        public static void CustomHashFunctionTest(string FileName)
        {
            string input = ReturnStringFromFile(FileName);
            timer.Start();
            //byte[] ok = CustomHashFunction(input);
            //byte[] ok = CustomHashFunctionWithoutCipher(input);
            byte[] ok = CustomHashFunctionMathemathic(input, 1);
            Console.WriteLine("OK: Custom " + timer.ElapsedMilliseconds.ToString() + " [ms]");

            input = "123" + input.Substring(3);
            timer.Restart();
            //byte[] error = CustomHashFunction(input);
            //byte[] error = CustomHashFunctionWithoutCipher(input);
            byte[] error = CustomHashFunctionMathemathic(input, 1);
            Console.WriteLine("ERROR: Custom " + timer.ElapsedMilliseconds.ToString() + " [ms]");
            Console.WriteLine();
            timer.Stop();
            timer.Reset();

            PrintBytesInConsole("Custom_" + FileName, ok);
            PrintBitsInConsole("Custom_" + FileName, ok);
            PrintLine();
            PrintBytesInConsole("Custom_Error_" + FileName, error);
            PrintBitsInConsole("Custom_Error_" + FileName, error);

            PrintDifference(ok, error);
            PrintLine();

            BytesToFile("Custom_" + FileName + ".txt", ok);
            BytesToFile("Custom_" + FileName + ".txt", error);

            Console.ReadKey();


        }

        static byte[] XOR(byte[] result, byte[] input)
        {
            byte[] xor;
            if (input.Length > result.Length)
                xor = new byte[result.Length];
            else xor = new byte[input.Length];

            for (int i = 0; i < xor.Length; i++)
            {
                xor[i] = byte.Parse((result[i] ^ input[i]).ToString());
            }
            return xor;
        }

        static void BuiltInHashFunction(string FileName)
        {
            byte[] inputBytes = ReturnBytesFromFile(FileName);
            byte[] inputErrorBytes = new byte[inputBytes.Length];

            //Creating shallow copy for inputBytes
            Array.ConstrainedCopy(inputBytes, 0, inputErrorBytes, 0, inputBytes.Length);
            //Changing first bit to see if it changes whole 
            inputErrorBytes[0] = 0;

            MD5Test(FileName, inputBytes, inputErrorBytes);
            SHA1Test(FileName, inputBytes, inputErrorBytes);
            SHA256Test(FileName, inputBytes, inputErrorBytes);
            SHA512Test(FileName, inputBytes, inputErrorBytes);

            Console.ReadKey();
            Console.Clear();
        }

        static void Main(string[] args)
        {
            //BuiltInHashFunction("1");
            //BuiltInHashFunction("2");
            //BuiltInHashFunction("3");
            CustomHashFunctionTest("4");
            CustomHashFunctionTest("5");
            CustomHashFunctionTest("6");
            Console.ReadKey();
        }

        #region Helper methods
        static byte[] FromStringToByteArray(string str)
        {
            return System.Text.Encoding.ASCII.GetBytes(str);
            //return UnicodeEncoding.Unicode.GetBytes(str);
        }
        static string FromByteArrayToString(byte[] bytes)
        {
            return System.Text.Encoding.ASCII.GetString(bytes, 0, bytes.Length);
            //return UnicodeEncoding.Unicode.GetString(bytes);

        }
        #endregion

        #region File operation methods
        static byte[] BytesFromFile(string FileName)
        {
            using (var bw = new BinaryReader(File.Open(FileName, FileMode.Open)))
            {
                const int bufferSize = 4096;
                using (var ms = new MemoryStream())
                {
                    byte[] buffer = new byte[bufferSize];
                    int count;
                    while ((count = bw.Read(buffer, 0, buffer.Length)) != 0)
                        ms.Write(buffer, 0, count);
                    return ms.ToArray();
                }
            }
        }
        static void BytesToFile(string FileName, byte[] partOfText)
        {
            using (var bw = new BinaryWriter(File.Open(FileName, FileMode.OpenOrCreate)))
            {
                bw.Write(partOfText.ToArray());
            }
        }
        public static byte[] ReturnBytesFromFile(string FileName)
        {
            string Message = "";
            Console.WriteLine("#### " + FileName + " ####");
            using (StreamReader reader = new StreamReader(File.Open(FileName + ".txt", FileMode.Open)))
            {
                Message = reader.ReadToEnd();
            }
            return System.Text.Encoding.ASCII.GetBytes(Message);
        }
        public static string ReturnStringFromFile(string FileName)
        {
            string Message = "";
            Console.WriteLine("#### " + FileName + " ####");
            using (StreamReader reader = new StreamReader(File.Open(FileName + ".txt", FileMode.Open)))
            {
                Message = reader.ReadToEnd();
            }
            return Message;
        }
        #endregion
    }
}
