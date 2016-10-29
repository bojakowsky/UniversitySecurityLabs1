using CryptoPort.Cyphers;
using HistoricalCypher.Cyphers;
using System;
using System.IO;

namespace HistoricalCypher
{
    class Program
    {
        //Tests
        #region TestData
        public static string x = @"-1-1-1-1-1-1-1-1-1-1CKUO-1CRSB-1USBB-1DZYX-1CRO-1LAYF-1-1-1-1-1-1-1-1-1-1-1KXN-1-1SX-1ZKACSXQ-1PAYW-1HYD-1XYF-1-1-1-1-1-1-1-1-1-1-1CRDB-1WDMR-1VOC-1WO-1KEYF-1-1-1-1-1-1-1-1-1-1-1-1HYD-1KAO-1XYC-1FAYXQ-1-1FRY-1NOOW-1-1-1-1-1-1-1-1-1-1CRKC-1WH-1NKHB-1RKEO-1LOOX-1K-1NAOKW-1-1-1-1-1-1-1-1-1-1-1HOC-1SP-1RYZO-1RKB-1PVYFX-1KFKH-1-1-1-1-1-1-1-1-1-1SX-1K-1XSQRC-1-1YA-1SX-1K-1NKH-1-1-1-1-1-1-1-1-1-1-1SX-1K-1ESBSYX-1-1YA-1SX-1XYXO-1-1-1-1-1-1-1-1-1-1-1SB-1SC-1CROAOPYAO-1CRO-1VOBB-1QYXO-1-1-1-1-1-1-1-1-1-1-1KVV-1CRKC-1FO-1BOO-1YA-1BOOW-1-1-1-1-1-1-1-1-1-1SB-1LDC-1K-1NAOKW-1FSCRSX-1K-1NAOKW-1-1-1-1-1-1-1-1-1-1-1-1-1S-1BCKXN-1KWSN-1CRO-1AYKA-1-1-1-1-1-1-1-1-1-1YP-1K-1BDAP-1CYAWOXCON-1BRYAO-1-1-1-1-1-1-1-1-1-1-1KXN-1S-1RYVN-1FSCRSX-1WH-1RKXN-1-1-1-1-1-1-1-1-1-1QAKSXB-1YP-1CRO-1QYVNOX-1BKXN-1-1-1-1-1-1-1-1-1-1-1-1RYF-1POF-1-1HOC-1RYF-1CROH-1MAOOZ-1-1-1-1-1-1-1-1-1-1CRAYDQR-1WH-1PSXQOAB-1CY-1CRO-1NOOZ-1-1-1-1-1-1-1-1-1-1-1FRSVO-1S-1FOOZ-1-1FRSVO-1S-1FOOZ-1-1-1-1-1-1-1-1-1-1-1Y-1QYN-1-1MKX-1S-1XYC-1QAKBZ-1-1-1-1-1-1-1-1-1-1CROW-1FSCR-1K-1CSQRCOA-1MVKBZ-1-1-1-1-1-1-1-1-1-1-1Y-1QYN-1-1MKX-1S-1XYC-1BKEO-1-1-1-1-1-1-1-1-1-1YXO-1PAYW-1CRO-1ZSCSVOBB-1FKEO-1-1-1-1-1-1-1-1-1-1-1SB-1KVV-1CRKC-1FO-1BOO-1YA-1BOOW-1-1-1-1-1-1-1-1-1-1LDC-1K-1NAOKW-1FSCRSX-1K-1NAOKW-1-1-1-1-1-1-1-1-1-1-1-1-1-1-1";
        public static string y = @"B**BB******B*B***B**B**BB**BBB*B***B**B**B*B**B***B**B*B**B*B*B***BBBB*BBB**BB*BB**BB**BBB**B******BB***B*BBB*B*BB*******BB*B***BB*B****BB*B*BBBB*****B***BB**BB*B****BB*B**BB***B*BB***B*BBB**BB**BB****BBB*B*B***BB*B*BBB*B*BB*B**BB**BBBB*B**B**B**BB**B*B*****B***BBB*B*BB**B**B**BB*BB****B*******B*B*B*BBB*B*BB*BB****BBB*B*B*******B***B**B***BB*B*BBB*B**BBB*BB*B***B*BBB**BB*B**BB*B*BB***BBB*BBB****BB**B****B***BB**B**BB**BBB*****B**BB*BB**BB******BB*****BB***B**B***BBB*****B*B*B**B******B**B****B***BB*B********BBB***B**B********BB**BB*****B**B**BB*B*****B*B**BBB*BBB**BBBB**B****BBB*****B**B***B*B*B*BB*BBB*B*BB**BB*B*****B*BB******BB****B****BB*B******BB*B*B*****BB***BBBB**BB*BBB*B***B*B****BB*B********BB*****BB****B****BB*B*****B*B*B*B***B**B**B****BBB**BB*B*BBB*B***B*B****BB*B*BB*B*BBB**BB*B**B***B***B**B**B***B**BBB**BB**BBB**B**B***B**B****B*B*BBB*B***B**B**B**BB**BBB**B***B*BB**B**B**B*B**B***BB**BBB**BB*B**B********B*BB*B*BBB**BB**BBB*****B**BBB*BB***B**B**B***B****B***BBB*B***BB**B***B****B***BB***B***B**B*****BB*B**B**BB********BBB***B**B********BB**B*BB**B***B**BB**BBB*B****BB*B********BBB***B**B********BB***B***B**B*B**BB******BB*B***BB******BB***B******BBB**BB**BBB**B**B***B*BBB******B***B*BBB***B*B*****B**B*B*B**B***B**B*BB**BB*BBB*B***B*BB****B***BB*BB**BB**B*****BBB**B***BBB*BBB*B***B**B********BB*B***BB*B*****BBB*BBB**B*BB***BBB*BB**B***B**BB**BBB*B****BB*B*BB**BB*****BBB******BB*B***BB**BB*B***B******B****BB*BB**B**BBB***B*BB**BB**BBB**B****BB**BBB**B*BB***BB**B***BB*BB**B*******BB*B***BB**BBB*BBB*B*BB***B*B**B**B*BB*BB*****B**B**BB**BBB*BBB*B*BB*B**BB**BBB**B**BB******B*B***B**B****B***BBBBB**BB**BBBB***B*BBB*B*B****BB***BBB*BB**BB*****B*B*B****BB*B**BB***B**B***BB**B*B**BB*BBB*B**BB**BBB**B*****BB**B****B***BBBBB*BB***BBB*B****B*BB**B***B***B*BB***B****B***BBBBB*BB***BBB*B****B*BB**B***B***B*BB***B****B***BBBB*BBB***BB**BBB****BB***B*******BB*B*B****BB*B*BBB*B**BB**BB*B***B*****B**B**BBBBB**BB**BBB**B***BB**B*BB**B***B**BB**BBB*****B**BB*B*****BB***BBBB**BB**B**B***B***B**B*BB*****B**B**BBBB*BBB***BB**BBB****BB***B*******BB*B*B****BB*B*BBB*B**BBB**B******B*B*B**B***BBB**BB*B**B****B*BB***B*BBB**BB**B**BB**BBB**B***BBBB*B***B**BB*B****B*BB**B**B**B*B**B*B*BB******B*B*B**B***B***B**B*******B*BB*B*BBB**BB**BBB*****B**BBB*BB***B**B**B***B****B***BBB*B***BB**B***B****B***BB******BB*B**B**BB********BBB***B**B********BB**B*BB**B***B**BB**BBB*B****BB*B********BBB***B**B********BB**";
        public static string z = @"-1-1-1-1-1-1-1-1-1-1H_QW-1HTSI-1QSII-1GLMN-1HTW-1ZJME-1-1-1-1-1-1-1-1-1-1-1_NX-1-1SN-1L_JHSNU-1VJMO-1CMG-1NME-1-1-1-1-1-1-1-1-1-1-1HTGI-1OGYT-1PWH-1OW-1_FME-1-1-1-1-1-1-1-1-1-1-1-1CMG-1_JW-1NMH-1EJMNU-1-1ETM-1XWWO-1-1-1-1-1-1-1-1-1-1HT_H-1OC-1X_CI-1T_FW-1ZWWN-1_-1XJW_O-1-1-1-1-1-1-1-1-1-1-1CWH-1SV-1TMLW-1T_I-1VPMEN-1_E_C-1-1-1-1-1-1-1-1-1-1SN-1_-1NSUTH-1-1MJ-1SN-1_-1X_C-1-1-1-1-1-1-1-1-1-1-1SN-1_-1FSISMN-1-1MJ-1SN-1NMNW-1-1-1-1-1-1-1-1-1-1-1SI-1SH-1HTWJWVMJW-1HTW-1PWII-1UMNW-1-1-1-1-1-1-1-1-1-1-1_PP-1HT_H-1EW-1IWW-1MJ-1IWWO-1-1-1-1-1-1-1-1-1-1SI-1ZGH-1_-1XJW_O-1ESHTSN-1_-1XJW_O-1-1-1-1-1-1-1-1-1-1-1-1-1S-1IH_NX-1_OSX-1HTW-1JM_J-1-1-1-1-1-1-1-1-1-1MV-1_-1IGJV-1HMJOWNHWX-1ITMJW-1-1-1-1-1-1-1-1-1-1-1_NX-1S-1TMPX-1ESHTSN-1OC-1T_NX-1-1-1-1-1-1-1-1-1-1UJ_SNI-1MV-1HTW-1UMPXWN-1I_NX-1-1-1-1-1-1-1-1-1-1-1-1TME-1VWE-1-1CWH-1TME-1HTWC-1YJWWL-1-1-1-1-1-1-1-1-1-1HTJMGUT-1OC-1VSNUWJI-1HM-1HTW-1XWWL-1-1-1-1-1-1-1-1-1-1-1ETSPW-1S-1EWWL-1-1ETSPW-1S-1EWWL-1-1-1-1-1-1-1-1-1-1-1M-1UMX-1-1Y_N-1S-1NMH-1UJ_IL-1-1-1-1-1-1-1-1-1-1HTWO-1ESHT-1_-1HSUTHWJ-1YP_IL-1-1-1-1-1-1-1-1-1-1-1M-1UMX-1-1Y_N-1S-1NMH-1I_FW-1-1-1-1-1-1-1-1-1-1MNW-1VJMO-1HTW-1LSHSPWII-1E_FW-1-1-1-1-1-1-1-1-1-1-1SI-1_PP-1HT_H-1EW-1IWW-1MJ-1IWWO-1-1-1-1-1-1-1-1-1-1ZGH-1_-1XJW_O-1ESHTSN-1_-1XJW_O-1-1-1-1-1-1-1-1-1-1-1-1-1-1-1";
        public static string v = @"151211421523335411335454312414531523422244143512533233532412441533531352441443551431531435152331544331412321421543421225143555143112444253141535441453133523143242424315231215435532125554231225422242425312324442124355421533522314244223125452211435531235125533531253331323151444335312321255335312253354331453144433535314534233543315152342444252144442152342214254541314534212212115231215354254424214445442424333542231151232444212433533152333531232444212433354151253321243333215234244141244145212543144521514444342531542325423144442125332332314213235331523335343552312533213441233535414521523421314213242535412533223143552423555421523143515234255414442422415234414311323435552335313424454151415234232424224352333214233354242243523332142333542422414131432411253335314151344125424152342433533152312153313231542444121125424141314324112533353141554122542145342524414431523422433153321425454351225423354122121152312153542544242144454424243223115123244421243353315233353123244421243";
        #endregion

        static void Main(string[] args)
        {
            Alphabet alphabet = new Alphabet();
            //string[] text = LoadNormalTextFromFile("Tekst.txt");
            //for (int i = 0; i < text.Length; ++i)
            //{
            //    if (!string.IsNullOrWhiteSpace(text[i]))
            //        SaveCipherToFile("Szyfrogram_S5", PolibushCypher.Encryption(text[i]));
            //}

            //LoadCiphersFromFile("Szyfrogram_S4");


            while (true)
            {
                Console.WriteLine("Wpisz tekst do zaszyfrowania");
                string encrypted = Console.ReadLine();
                string encr = PolibushCypher.Encryption(encrypted);

                Console.WriteLine(encr);

                Console.WriteLine();
                Console.WriteLine("Nacisnij dowolny przycisk by kontynuować");
                Console.ReadKey();
                Console.Clear();
            }
            //CyphersTest();
        }

        #region tests
        static public void CyphersTest()
        {
            Alphabet alphabet = new Alphabet();

            //Console.WriteLine(CesarClassicCypher.Encryption(25, "abzy_", alphabet));
            //Console.WriteLine(CesarClassicCypher.Decryption(25, "z_xwy", alphabet));
            //Console.WriteLine(AtBashCypher.Encryption("abc_xyz", alphabet));
            //Console.WriteLine(AtBashCypher.Decryption("_zya", alphabet));
            //Console.WriteLine(PolibushCypher.Encryption("SPOTKANIE JEST JUTRO"));
            //Console.WriteLine(PolibushCypher.Decryption("542414151112533342334254153331154414"));
            //Console.WriteLine(BaconCypher.Encryption("DOM"));
            //Console.WriteLine(BaconCypher.Decryption("***BB*BBB**BB**"));

            //CyphersTestHelper(PolibushCypher.Encryption(SzyfrogramS1), CypherAction.Encrypt, CypherTypes._Polibush);
            //CyphersTestHelper(PolibushCypher.Decryption(v), CypherAction.Decrypt, CypherTypes._Polibush);

            //CyphersTestHelper(AtBashCypher.Encryption(SzyfrogramS1, alphabet), CypherAction.Encrypt, CypherTypes._AtBash);
            //CyphersTestHelper(AtBashCypher.Encryption(z, alphabet), CypherAction.Decrypt, CypherTypes._AtBash);

            //CyphersTestHelper(CesarClassicCypher.Encryption(10, SzyfrogramS1, alphabet), CypherAction.Encrypt, CypherTypes._Cesar);
            //CyphersTestHelper(CesarClassicCypher.Decryption(10, x, alphabet), CypherAction.Decrypt, CypherTypes._Cesar);

            //CyphersTestHelper(BaconCypher.Encryption(SzyfrogramS1), CypherAction.Encrypt, CypherTypes._Bacon);
            //CyphersTestHelper(BaconCypher.Decryption(y), CypherAction.Decrypt, CypherTypes._Bacon);

            //CyphersTestHelper(DiagonalColumnCypher.Encryption("ALA_MA_KOTA_KOT_MA_ALE", alphabet, "KOTEK"), CypherAction.Encrypt, CypherTypes._Diagonal);
            //CyphersTestHelper(DiagonalColumnCypher.Decryption("_TAMAA_K_AMA_AALKOALAOT_E", alphabet, "KOTEK"), CypherAction.Decrypt, CypherTypes._Diagonal);
        }
        #endregion


        public enum CypherAction
        {
            Encrypt = 1,
            Decrypt = 2
        }

        public enum CypherTypes
        {
            _AtBash = 1,
            _Bacon = 2,
            _Cesar = 3,
            _Polibush = 4,
            _Diagonal = 5
        }

        static void CyphersTestHelper(string text, CypherAction choice, CypherTypes type)
        {
            string filename = "";
            if (choice == CypherAction.Encrypt)
            {
                filename = "encrypted" + type.ToString() + ".txt";
            }
            else
            {
                filename = "decrypted" + type.ToString() + ".txt";
            }

            FileStream filestream = new FileStream(filename, FileMode.Create);
            var streamwriter = new StreamWriter(filestream);
            streamwriter.AutoFlush = true;
            streamwriter.WriteLine(text);

            Console.SetOut(streamwriter);
            Console.SetError(streamwriter);

            System.Diagnostics.Process.Start("notepad.exe", filename);
        }

        static void SaveCipherToFile(string filename, string text_encrypted)
        {
            Alphabet alphabet = new Alphabet();
            FileStream filestream = new FileStream(filename, FileMode.Append);
            var streamwriter = new StreamWriter(filestream);
            streamwriter.AutoFlush = true;
            streamwriter.WriteLine(text_encrypted);

            Console.SetOut(streamwriter);
            Console.SetError(streamwriter);

            streamwriter.Close();
        }

        static void LoadCiphersFromFile(string filename)
        {
            Alphabet alphabet = new Alphabet();
            FileStream filestream = new FileStream(filename, FileMode.Open);
            var streamreader = new StreamReader(filestream);
            //Just for test
            Console.WriteLine(CesarClassicCypher.Decryption(24,streamreader.ReadLine(), alphabet));
            streamreader.Close();
            Console.ReadKey();
        }

        static string[] LoadNormalTextFromFile(string filename)
        {
            FileStream filestream = new FileStream(filename, FileMode.Open);
            var streamreader = new StreamReader(filestream);
            string text = streamreader.ReadToEnd();
            string[] TextLined = text.Split('\n', '\r');
            return TextLined;
        }


    }
}
