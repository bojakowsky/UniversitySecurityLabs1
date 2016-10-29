using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Diagnostics;

namespace Trololo
{
    class Program
    {
        #region encryption and decryption methods
        static void Decrypt(string Id)
        {
            DecryptTextFromFile("Encrypted_" + Id);  
        }
        static void DecryptTextFromFile(string Id)
        {
            try
            {
                byte[] totalBytes = BytesFromFile(Id);
                int numberOfBlocks = totalBytes.Length / 24;
                List<byte[]> blocks = new List<byte[]>();
                for (int i = 0; i < numberOfBlocks; i += 1)
                    blocks.Add(totalBytes.Skip(i * 24).Take(24).ToArray<byte>());

                string result = "";

                byte[] lastBlock = blocks[0];

                ICryptoTransform decrypt = provider.CreateDecryptor(provider.Key, provider.IV);
                lastBlock = decrypt.TransformFinalBlock(lastBlock, 0, lastBlock.Length);
                byte[] lastXor = XOR(lastBlock, FromStringToByteArray(initialKey));
                result += FromByteArrayToString(lastXor);

                for (int i = 1; i < blocks.Count; i++)
                {
                    lastBlock = decrypt.TransformFinalBlock(blocks[i], 0, blocks[i].Length);
                    lastXor = XOR(blocks[i - 1], lastBlock);
                    result += FromByteArrayToString(lastXor);
                }


                StringToFile("Decrypted_" + Id, result);


            }
            catch (Exception e)
            {
                Console.WriteLine("\nException occured: " + e.Message + "\n");
            }
        }
        static void Encrypt(string Id)
        {
            string plaintext = StringFromFile(Id + ".txt");
            List<byte[]> blocksOfText = TextToByteArrays(plaintext);

            #region WorkingTests
            //byte[] input = blocksOfText[0];
            //byte[] output = encrypt.TransformFinalBlock(input, 0, input.Length);
            //BytesToFile("Encrypted", output);
            //Console.WriteLine(DecryptTextFromFile("Encrypted", provider.Key, provider.IV));
            #endregion

            byte[] lastXor = XOR(FromStringToByteArray(initialKey), blocksOfText[0]);
            byte[] lastOutput = encrypt.TransformFinalBlock(lastXor, 0, lastXor.Length);

            List<byte> totalBytes = new List<byte>();
            totalBytes.AddRange(lastOutput);

            if (blocksOfText.Count > 1)
            {
                for (int i = 1; i < blocksOfText.Count; i++)
                {
                    lastXor = XOR(lastOutput, blocksOfText[i]);
                    lastOutput = encrypt.TransformFinalBlock(lastXor, 0, lastXor.Length);
                    totalBytes.AddRange(lastOutput);
                }
            }

            BytesToFile("Encrypted_" + Id, totalBytes.ToArray());

        }
        #endregion

        #region encryption and decryption helper methods
        static int GetNumberOfBlocksAndFillPadding(ref string text)
        {
            if (text.Length <= 8)
            {
                text = text.PadRight(8);
                return 1;
            }
            else if (text.Length % 8 == 0)
            {
                return text.Length / 8;
            }
            else
            {
                int approxBlocks = text.Length / 8;
                int realBlocks = approxBlocks + 1;
                text = text.PadRight(realBlocks * 8);
                return realBlocks;
            }
        }
        static List<byte[]> TextToByteArrays(string plaintext)
        {
            int numberOfBlocks = GetNumberOfBlocksAndFillPadding(ref plaintext);
            List<byte[]> blocksOfText = new List<byte[]>();
            for (int i = 0; i < numberOfBlocks; i++)
            {
                int start = i == 0 ? 0 : i * 8;
                int length = 8;
                blocksOfText.Add(FromStringToByteArray(plaintext.Substring(start, length)));
            }
            return blocksOfText;
        }
        static byte[] XOR(byte[] result, byte[] input)
        {
            byte[] xor = new byte[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                xor[i] = byte.Parse((result[i] ^ input[i]).ToString());
            }
            return xor;
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
        static void StringToFile(string FileName, string result)
        {
            using (var bw = new StreamWriter(File.Open(FileName, FileMode.OpenOrCreate)))
            {
                bw.Write(result);
            }
        }
        static string StringFromFile(string FileName)
        {
            using (var bw = new StreamReader(File.Open(FileName, FileMode.Open)))
            {
                return bw.ReadToEnd();
            }
        }
        #endregion

        #region Test methods
        public static string StartEncryptionTest(string FileName)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            Encrypt(FileName);
            watch.Stop();
            return FileName + " Encryption tests completed in [ms]:" + watch.ElapsedMilliseconds.ToString();
            
        }
        public static string StartDecryptionTest(string FileName)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            Decrypt(FileName);
            watch.Stop();
            return FileName + " Decryption tests completed in [ms]:" + watch.ElapsedMilliseconds.ToString();
        }
        public static void StartTest(string FileName)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();

            string logs = "";
            logs = StartEncryptionTest(FileName);
            logs += "\n" + StartDecryptionTest(FileName) + "\n";

            logs += FileName + " Decryption and Encryption tests completed in [ms]:" 
                + watch.ElapsedMilliseconds.ToString();
            Console.WriteLine("\n" + logs);
            StringToFile(FileName + "_Tests", logs);

            watch.Stop();
        }
        #endregion Test methods

        static void MainActions()
        {
            MainMenuTextLabel:

            Console.WriteLine("1. Encrypt data in \"Input\" file, result data will be in \"Encrypted\"");
            Console.WriteLine("2. Decrypt data from \"Encrypted\" file, result will be in \"Decrypted\"");
            Console.WriteLine("3. Run combined with massive tests");
            Console.WriteLine("4. Exit");
            ConsoleKeyInfo key = Console.ReadKey();
            if (key.KeyChar == '1')
            {
                try
                {
                    File.Delete("Encrypted_9_error");
                }
                catch (Exception)
                {
                    //ok
                }
                Encrypt("9_error");
                Console.Clear();
                Console.WriteLine("Encryption successful\n");
                goto MainMenuTextLabel;

            }

            else if (key.KeyChar == '2')
            {
                try
                {
                    File.Delete("Decrypted_Encrypted_9_error");
                }
                catch (Exception)
                {
                    //ok
                }
                Decrypt("9_error");
                Console.Clear();
                Console.WriteLine("Decryption successful\n");
                goto MainMenuTextLabel;
            }

            else if (key.KeyChar == '3')
            {
                Stopwatch MainWatch = new Stopwatch();
                MainWatch.Start();

                StartTest("1");
                StartTest("2");
                StartTest("3");
                StartTest("4");
                StartTest("5");
                StartTest("6");
                StartTest("7");
                StartTest("8");


                Console.Clear();
                Console.WriteLine("All tests completed in [ms]: " + MainWatch.ElapsedMilliseconds.ToString());
                MainWatch.Stop();
                Console.WriteLine("Tests successful\n");
                goto MainMenuTextLabel;
            }

            else if (key.KeyChar == '4')
            {
                return;
            }

            else
            {
                Console.Clear();
                goto MainMenuTextLabel;
            }

        }

        #region globals per run living
        public static DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
        public static ICryptoTransform encrypt;
        public static string initialKey = "KEY#_POD";
        #endregion

        //CBC Mode implementation
        static void Main(string[] args)
        {
            provider.Mode = CipherMode.ECB;
            encrypt = provider.CreateEncryptor(provider.Key, provider.IV);
            MainActions();
        }

        #region Helper methods
        static byte[] FromStringToByteArray(string str)
        {
            //return System.Text.Encoding.Unicode.GetBytes(str);
            return UnicodeEncoding.Unicode.GetBytes(str);
        }
        static string FromByteArrayToString(byte[] bytes)
        {
            //return System.Text.Encoding.Unicode.GetString(bytes, 0, bytes.Length);
            return UnicodeEncoding.Unicode.GetString(bytes);
        }
        #endregion
    }
}




