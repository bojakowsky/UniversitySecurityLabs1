using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {

        public static string SomeTextToEncrypt = "Some very random T3xt To 3ncrypt, just f0 f4n ;)";

        public static long e = 0;
        public static List<long> eToRoll = new List<long>();
        public static Random random = new Random();

        public static bool IsPrime(long n)
        {
            if (n == 1) return false;
            else if (n == 2) return true;

            for (long i = 2; i < n; i++)
            {
                if (n % i == 0)
                    return false;
            }
            return true;
        }
        public static long FindPseudoPrime(long phi)
        {
            for (long i = 1; i < phi; i++)
            {
                if (GCD(i, phi) == 1)
                    eToRoll.Add(i);
            }
            long ret = eToRoll[(random.Next(0, eToRoll.Count - 1))];
            if (ret < phi) return ret;
            while (ret >= phi)
            {
                ret = eToRoll[(random.Next(0, eToRoll.Count - 1))];
            } return ret;
        }
        public static long GCD(long a, long b)
        {
            long pom;

            while (b != 0)
            {
                pom = b;
                b = a % b;
                a = pom;
            }

            return a;
        }

        public static long x = 1, y = 0; // Euklides extended variables
        static void euklidesExtended(long a, long b)
        {
            if (b != 0)
            {
                euklidesExtended(b, a % b);
                long pom = y;
                y = x - a / b * y;
                x = pom;
            }
        }

        public static long FindD(long phi)
        {
            x = 1;
            y = 0;
            euklidesExtended(phi, e);

            try {
                long d = phi - y;
                if ((e * d - 1) % phi != 0)
                    throw new Exception("D value is really wrong!");
                return d;
            }
            catch(Exception)
            {
                long d = phi + y;
                if ((e * d - 1) % phi != 0)
                    throw new Exception("D value is really wrong!");
                return d;
            }
            
        }

        public static void RandomPrimes(out long p, out long q)
        {
            p = 0;
            q = 0;
            List<long> primeToRoll = new List<long>();
            for (long i = 1; i < 1000; i++)
            {
                if (IsPrime(i))
                {
                    primeToRoll.Add(i);
                }
            }
            p = primeToRoll[random.Next(0, primeToRoll.Count - 1)];
            q = primeToRoll[random.Next(0, primeToRoll.Count - 1)];
        }

        public static string Encrypt(long n, long e, long d)
        {
            string encrypted = "";
            for (int i = 0; i < SomeTextToEncrypt.Length; i++)
            {
                BigInteger result = SomeTextToEncrypt[i];
                //result = BigInteger.Pow(result, (int)e) % n;
                result = BigInteger.ModPow(result, (int)e, n);
                encrypted += result + " ";
            }
            return encrypted;
        }

        public static string Decrypt(long n, long e, long d, string encrypted)
        {
            
            Dictionary<int, string> decrypting = new Dictionary<int, string>();
            string[] chunks = encrypted.Split(' ');
            Task[] task = new Task[chunks.Count() - 1];

            //Parallel.For(0, chunks.Count() - 1, i =>
            // {
            //     BigInteger result = BigInteger.Parse(chunks[i]);
            //     //result = BigInteger.Pow(result, (int)d) % n;
            //     result = BigInteger.ModPow(result, (int)d, n);
            //     decrypting.Add(i, ((char)result).ToString());
            // });

            for (int i = 0; i < chunks.Count() -1 ; i++)
            {
                BigInteger result = BigInteger.Parse(chunks[i]);
                //result = BigInteger.Pow(result, (int)d) % n;
                result = BigInteger.ModPow(result, (int)d, n);
                decrypting.Add(i, ((char)result).ToString());
            }

            string decrypted = "";
            for (int i = 0; i < decrypting.Count(); i++)
            {
                decrypted += decrypting[i];
            }
            return decrypted;
        }

        static void Main(string[] args)
        {
            for (int i = 0; i < 5; i++)
            {
                long p, q;

                //Prime numbers
                RandomPrimes(out p, out q);


                long n = p * q;
                long phi = (p - 1) * (q - 1);

                //E musi byc mniejsza od PHI
                e = FindPseudoPrime(phi);


                 Console.WriteLine(string.Format("p:{0}, q:{1}, phi:{2}, e:{3}", p, q, phi, e));
                try
                {
                    long d;
                    try {
                        d = FindD(phi);
                    }
                    catch(Exception)
                    {
                        e = FindPseudoPrime(phi);
                        d = FindD(phi);
                    }
                    Console.WriteLine(string.Format("d:{0}", d));

                    //public key : e, n
                    //private key : d, n
                    Stopwatch watch = new Stopwatch();
                    watch.Start();
                    string encrypted = Encrypt(n, e, d); //Stark is dead
                    Console.WriteLine("\nEncrypted stuff: \n" + encrypted);
                    string decrypted = Decrypt(n, e, d, encrypted);
                    Console.WriteLine("\nDecrypted stuff: \n" + decrypted);
                    Console.WriteLine("Calculation time: " + watch.ElapsedMilliseconds + " [ms]\n\n");
                    watch.Stop();

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

            }

            Console.WriteLine("\n\nThe end");
            while (Console.ReadKey().Key != ConsoleKey.Escape) { }
            Console.ReadKey();
        }
    }
}