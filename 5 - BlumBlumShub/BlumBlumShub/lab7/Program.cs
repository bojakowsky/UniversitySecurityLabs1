using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace lab7
{
    class Program
    {

        static bool IsCongruent(BigInteger value)
        {
            if ((value - 3) % 4 == 0) return true;
            else return false;
        }

        public static BigInteger GCD(BigInteger a, BigInteger b) // Greatest common divisor
        {
            BigInteger pom;

            while (b != 0)
            {
                pom = b;
                b = a % b;
                a = pom;
            }

            return a;
        }

        public static bool IsPrime(BigInteger n)
        {
            if (n == 1) return false;
            else if (n == 2) return true;

            for (BigInteger i = 2; i < n; i++)
            {
                if (n % i == 0)
                    return false;
            }
            return true;
        }

        public static bool Coprime(BigInteger x0, BigInteger M)
        {
            if (GCD(x0, M) == 1)
                return true;
            else return false;

        }

        public static void RandomCongruentPrimes(out long p, out long q, long? range = null)
        {
            p = 0;
            q = 0;
            List<long> primeToRoll = new List<long>();

            long upperPrimeBracket = range == null ? 1000000 : (long)range;
            long[] erastotenesArray = new long[upperPrimeBracket];

            long SquareRootPrimeBracket = (long)Math.Floor(Math.Sqrt(upperPrimeBracket));
            for (long i = 1; i <= SquareRootPrimeBracket; i++) erastotenesArray[i] = i;

            long j = 0;
            for (long i = 2; i < SquareRootPrimeBracket; i++)
            {
                if (erastotenesArray[i] != 0)
                {
                    j = i + i;
                    while (j <= SquareRootPrimeBracket)
                    {
                        erastotenesArray[j] = 0;
                        j += i;
                    }
                }
            }

            for (long i = 2; i < SquareRootPrimeBracket; i++)
            {
                //Is number concument check (value - 3 % 4 == 0) and is different than zero
                if (erastotenesArray[i] != 0 && IsCongruent(erastotenesArray[i]))
                    primeToRoll.Add(erastotenesArray[i]);
            }

            Random random = new Random();
            p = primeToRoll[random.Next(0, primeToRoll.Count - 1)];
            q = primeToRoll[random.Next(0, primeToRoll.Count - 1)];
        }

        //Seed needs to be a Co-prime of M
        static public long GetRandomSeed(long M)
        {
            List<long> seedToRoll = new List<long>();
            for (long i = 1; i < 1000000; i++)
            {
                if (GCD(i, M) == 1)
                    seedToRoll.Add(i);
            }
            Random random = new Random();
            return seedToRoll[(random.Next(0, seedToRoll.Count - 1))];
        }

        //Bits balance test
        static public void PrintBitsRatio(string ratio)
        {
            double zeroBits = 0;
            double onebits = 0;
            for (int i = 0; i < ratio.Length; i++)
            {
                if (ratio[i] == '0') zeroBits++;
                else onebits++;
            }

            Console.WriteLine("Zero bits: " + (double)((zeroBits * 100)/ratio.Length)+"%");
            Console.WriteLine("One bits: " + (double)((onebits * 100) / ratio.Length)+"%");
        }

        //Bits series test, and long bits series test
        static public void PrintBitsSeries(string ratio)
        {
            Dictionary<int, int> seriesDict = new Dictionary<int, int>();
            for (int i = 1; i < 30; i++)
            {
                seriesDict.Add(i, 0);
            }

            int series = 1;
            for (int i = 1; i < ratio.Count(); i++)
            {

                if (ratio[i - 1] == ratio[i]) series++;
                else
                {
                    seriesDict[series]++;
                    series = 1;
                }
            }
            
            foreach (var s in seriesDict)
            {
                Console.WriteLine(s.Key + ": " + s.Value);
            }
        }

        static public void PrintPokerBitsTest(string text)
        {
            Dictionary<string, int> gaussianDistribution = new Dictionary<string, int>();
            for (int i = 0; i <= 15; i++)
            {
                gaussianDistribution.Add(Convert.ToString(i, 2).PadLeft(4,'0'), 0);
            }
            for (int i = 0; i < text.Count(); i += 4)
            {
                gaussianDistribution[text.Substring(i, 4)]++;
            }

            foreach (var gauss in gaussianDistribution)
            {
                Console.WriteLine(gauss.Key + ": " + gauss.Value);
            }

        }

        public static void PrintTitle(string title)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("");
            Console.WriteLine("_______________");
            Console.WriteLine(title);
            Console.WriteLine("_______________");
            Console.ResetColor();
        }
        public static void BlumBlumShubGenerator()
        {
            PrintTitle("BlumBlumShub");
            //https://en.wikipedia.org/wiki/Blum_Blum_Shub
            long p;
            long q;
            RandomCongruentPrimes(out p, out q);

            long M = p * q;
            long x = GetRandomSeed(M);

            string lastBitOfEveryNumber = "";
            List<BigInteger> generatedSequence = new List<BigInteger>();

            generatedSequence.Add(BigInteger.ModPow(x, 2, M));
            string bufor = Convert.ToString((long)generatedSequence[0], 2);
            lastBitOfEveryNumber += bufor[bufor.Count() - 1];
            for (int i = 1; i < 20000; i++)
            {
                generatedSequence.Add(BigInteger.ModPow(generatedSequence[generatedSequence.Count() - 1], 2, M));
                bufor = Convert.ToString((long)generatedSequence[i], 2);
                lastBitOfEveryNumber = bufor[bufor.Count() - 1] + lastBitOfEveryNumber;
            }

            PrintBitsRatio(lastBitOfEveryNumber);
            PrintBitsSeries(lastBitOfEveryNumber);
            PrintPokerBitsTest(lastBitOfEveryNumber);
        }

        static public void ElipticCurvesGenerator()
        {
            BigInteger p = new BigInteger();
            p = BigInteger.Pow(2, 256) - BigInteger.Pow(2, 224) + BigInteger.Pow(2, 96) - 1;
            BigInteger a = BigInteger.Parse("115792089210356248762697446949407573530086143415290314195533631308867097853948");
            BigInteger b = BigInteger.Parse("41058363725152142129326129780047268409114441015993725554835256314039467401291");

            BigInteger xP = BigInteger.Parse("8439561293906451759052585252797914202762949526041747995844080717082404635286");
            BigInteger yP = BigInteger.Parse("6134250956749795798585127919587881956611106672985015071877198253568414405109");

            BigInteger xQ = BigInteger.Parse("91120319633256209954638481795610364441930342474826146651283703640232629993874");
            BigInteger yQ = BigInteger.Parse("80764272623998874743522585409326200078679332703816718187804498579075161456710");


        }

        static public void GeneratorBasedOnMouseMoves()
        {
            PrintTitle("RandomMouseGenerator");
            long p;
            long q;
            RandomCongruentPrimes(out p, out q, 30000);
            long M = p * q;

            int x;
            int y;

            string lastBits = "";
            List<long> numbers = new List<long>();

            Console.WriteLine("Swipe your mouse now!");
            Thread.Sleep(1000);
            while (lastBits.Count() <= 20000)
            {
                x = Cursor.Position.X;
                y = Cursor.Position.Y;
                string bufor = BigInteger.ModPow(x, y, M).ToString();
                lastBits = Convert.ToString(bufor[bufor.Count() - 1], 2) + lastBits;
                Thread.Sleep(2);
            }
            Console.WriteLine("Stop swiping!");

            PrintBitsRatio(lastBits);
            PrintBitsSeries(lastBits);
            PrintPokerBitsTest(lastBits);
        }

        static void Main(string[] args)
        {
            BlumBlumShubGenerator();
            GeneratorBasedOnMouseMoves();
            Console.ReadKey();
        }

        
    }
}
