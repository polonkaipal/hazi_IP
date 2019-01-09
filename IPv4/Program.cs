using System;

namespace TesztIP
{
    class Program
    {


        static void Main(string[] args)
        {
            Char m;
            menuDisplay();
            do
            {
                m = Console.ReadKey().KeyChar;
                switch (m)
                {
                    case '1':
                        testIPV4();
                        menuDisplay();
                        break;
                    case '2':
                        testIPV4error();
                        menuDisplay();
                        break;
                    case '3':
                        TestIPV4Utils();
                        menuDisplay();
                        break;

                    case 'k':
                    case 'K':
                        break;
                    default:
                        Console.WriteLine("Érvénytelen választás!");
                        break;
                }

            } while (m != 'k' && m != 'K');
        }

        private static void menuDisplay()
        {
            Console.Clear();
            Console.WriteLine("[1] IPV4 osztály tesztelése (konstruktor, get, set)");
            Console.WriteLine("[2] IPV4 osztály tesztelése hibákra");
            Console.WriteLine("[3] IPV4Utils osztály tesztelése");
            Console.WriteLine("[k] Kilépés\n");
            Console.WriteLine("Kérem válasszon!");
        }


        static void MindentKiir(IPV4 ipszam, string tesztNeve)
        {
            System.Console.WriteLine("TESZT:" + tesztNeve + "\n");
            System.Console.WriteLine("Bináris cím egyben:" + ipszam.AddrBinary);
            System.Console.WriteLine("Bináris cím pontokkal:" + ipszam.AddrBinaryDotted);
            System.Console.WriteLine("Decimális cím pontokkal:" + ipszam.AddrDecimalDotted);
            System.Console.WriteLine("Az eszköz leírása:" + ipszam.Comment);
            System.Console.Write("A decimális cím tömbjében lévő elemek:");
            int cik = 0;
            foreach (byte tag in ipszam.AddrMembersArray)
                System.Console.Write("t[" + cik++ + "]=" + tag + "; ");
            System.Console.WriteLine("\n" + new string('-', 80) + "\n");
            return;
        }

        private static void testIPV4error()
        {
            Console.Clear();
            Console.WriteLine("IPV4 osztály tesztje hibákra:\n");
            IPV4 cim = new IPV4(10, 105, 225, 15);

            try
            {
                cim.AddrDecimalDotted = "260.200.100.0";
            }
            catch (System.Exception hiba)
            {
                System.Console.WriteLine(hiba.Message);
            }

            try
            {
                cim.AddrDecimalDotted = "2x20.200.100.0";
            }
            catch (System.Exception hiba)
            {
                System.Console.WriteLine(hiba.Message);
            }

            try
            {
                cim.AddrDecimalDotted = "260.200.0";
            }
            catch (System.Exception hiba)
            {
                System.Console.WriteLine(hiba.Message);
            }

            try
            {
                cim = new IPV4("100110010.10110100.11100100.00010110", "Notebook");
            }
            catch (System.Exception hiba)
            {
                System.Console.WriteLine(hiba.Message);
            }

            try
            {
                cim = new IPV4("10012010.10110100.11100100.00010110", "WebCam");
            }
            catch (System.Exception hiba)
            {
                System.Console.WriteLine(hiba.Message);
            }

            Console.WriteLine("\n Nyomjon le egy gombot a visszalépéshez!");
            Console.ReadKey();
        }

        private static void testIPV4()
        {
            Console.Clear();
            Console.WriteLine("IPV4 osztály tesztje:\n");

            IPV4 cimA = new IPV4(192, 168, 20, 1);
            MindentKiir(cimA, "Létrehozó konstruktor: IPV4(byte d31, byte d23, byte d15, byte d7)");

            cimA = new IPV4(10, 0, 224, 17, "Hálózati nyomtató");
            MindentKiir(cimA, "Létrehozó konstruktor: IPV4(byte d31, byte d23, byte d15, byte d7, string leiro)");
            cimA.AddrBinaryDotted = "10101111.11110000.10110000.00000000";
            MindentKiir(cimA, "Cím módosítása [AddrBinaryDotted] használata után");


            cimA = new IPV4("10110010.10110100.11100100.00010110", "Access Point");
            MindentKiir(cimA, "Létrehozó konstruktor: IPV4(string ipaddress, string leiro)");
            cimA.AddrDecimalDotted = "176.14.224.128";
            MindentKiir(cimA, "Cím módosítása [AddrDecimalDotted] használata után");

            Console.WriteLine("\n Nyomjon le egy gombot a visszalépéshez!");
            Console.ReadKey();


        }
        private static void TestIPV4Utils()
        {
            Console.Clear();
            Console.WriteLine("IPV4Utils osztály tesztje:\n");
            Console.WriteLine("Kérem adja meg az IP számot! (pontokkal elválasztva, decimális formában)");
            String sor = Console.ReadLine();
            IPV4 cim = new IPV4(sor, "");
            Console.WriteLine("Kérem adja meg a hálózat bitjeinek a számát! (8..30)");
            sor = Console.ReadLine();
            IPV4 maszk = IPV4Utils.GetSubnetMask(Convert.ToInt16(sor));

            MindentKiir(cim, "Megadott IPv4 szám");
            MindentKiir(maszk, "Megadott alhálózati maszk");
            MindentKiir(IPV4Utils.GetNetworkFromIPV4(cim, maszk), "A megadott IP szám hálózata");

            IPV4 firstHost = IPV4Utils.GetFirstHostWithIPV4(cim, maszk);
            MindentKiir(firstHost, "A megadott IP számhoz tartozó hálózatban kiosztható legelső IP szám");

            IPV4 lastHost = IPV4Utils.GetLastHostWithIPV4(cim, maszk);
            MindentKiir(lastHost, "A megadott IP számhoz tartozó hálózatban kiosztható legutolsó IP szám");

            IPV4 prevNet = IPV4Utils.GetPreviousNetworkWithIPV4(cim, maszk);
            MindentKiir(prevNet, "A megadott IP szám hálózata előtti hálózat");
            IPV4 nextNet = IPV4Utils.GetNextNetworkWithIPV4(cim, maszk);
            MindentKiir(nextNet, "A megadott IP szám hálózata utáni hálózat");

            Console.WriteLine("\n Nyomjon le egy gombot a visszalépéshez!");
            Console.ReadKey();
        }

    }
}
