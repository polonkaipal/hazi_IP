using System;
using System.Text;

namespace TesztIP
{
    class IPV4Utils
    {
        // Előállítja az alhálózati maszk IPv4 címét a hálózati rész bithosszának ismeretében
        // Csak a 8..30 értékeket fogadja el, egyébként kivétel!
        static public IPV4 GetSubnetMask(int netLength)
        {
            while (netLength < 8 || netLength > 30)
            {
                try { throw new ArgumentException("Hibás az alhálózati maszk!"); }
                catch (ArgumentException)
                {
                    Console.WriteLine("Kérem adja meg a hálózat bitjeinek a számát! (8..30)");
                    netLength = Convert.ToInt32(Console.ReadLine());
                }
            }

            StringBuilder maszk = new StringBuilder();
            byte i = 0;
            while (i < netLength)
            {
                maszk.Append("1");
                i++;
            }
            while (maszk.Length < 32)
            {
                maszk.Append("0");
                i++;
            }
            return new IPV4(maszk.ToString(), "");
        }



        // Egy IPv4 cím és a hozzátartozó alhálózati maszk alapján visszadja a hálózati címet
        // Hibás alhűlózati maszk esetén kivétel! -> throw new System.ArgumentException("Hibás az alhálózati maszk!");
        static public IPV4 GetNetworkFromIPV4(IPV4 addr, IPV4 subnet)
        {
            uint cim = Convert.ToUInt32(addr.AddrBinary, 2);
            uint maszk = Convert.ToUInt32(subnet.AddrBinary, 2);
            string halozatiCim = Convert.ToString(cim & maszk, 2);
            halozatiCim = Padding(halozatiCim, 32);
            return new IPV4(halozatiCim, "");
        }



        // Egy IPv4 cím és a hozzátartozó alhálózati maszk alapján visszadja az adott hálózatban kiosztható legkisebbb (legelső) hálózati címet
        static public IPV4 GetFirstHostWithIPV4(IPV4 addr, IPV4 subnet)
        {
            uint cim = Convert.ToUInt32(addr.AddrBinary, 2);
            uint maszk = Convert.ToUInt32(subnet.AddrBinary, 2);
            string elsoCim = Convert.ToString((cim & maszk) + 1, 2);
            elsoCim = Padding(elsoCim, 32);
            return new IPV4(elsoCim, "");
        }

        // Egy IPv4 cím és a hozzátartozó alhálózati maszk alapján visszadja az adott hálózatban kiosztható legutolsó hálózati címet
        static public IPV4 GetLastHostWithIPV4(IPV4 addr, IPV4 subnet)
        {
            uint cim = Convert.ToUInt32(addr.AddrBinary, 2);
            uint maszk = Convert.ToUInt32(subnet.AddrBinary, 2);
            double plusz = PrefixKulonbseg(subnet);
            string utolsoCim = Convert.ToString((cim & maszk) + (uint)(plusz) - 2, 2);
            utolsoCim = Padding(utolsoCim, 32);
            return new IPV4(utolsoCim, "");
        }

        // Egy IPv4 cím és a hozzátartozó alhálózati maszk alapján visszadja az adott hálózatot közvetlenül követő hálózat címét
        static public IPV4 GetNextNetworkWithIPV4(IPV4 addr, IPV4 subnet)
        {
            uint cim = Convert.ToUInt32(addr.AddrBinary, 2);
            uint maszk = Convert.ToUInt32(subnet.AddrBinary, 2);
            double plusz = PrefixKulonbseg(subnet);
            string elsoHalozatiCim = Convert.ToString((cim & maszk) + (uint)(plusz), 2);
            elsoHalozatiCim = Padding(elsoHalozatiCim, 32);
            return new IPV4(elsoHalozatiCim, "");
        }

        // Egy IPv4 cím és a hozzátartozó alhálózati maszk alapján visszadja az adott hálózatot közvetlenül megelőző hálózat címét
        static public IPV4 GetPreviousNetworkWithIPV4(IPV4 addr, IPV4 subnet)
        {
            uint cim = Convert.ToUInt32(addr.AddrBinary, 2);
            uint maszk = Convert.ToUInt32(subnet.AddrBinary, 2);
            double minusz = PrefixKulonbseg(subnet);
            string utolsoHalozatiCim = Convert.ToString((cim & maszk) - (uint)(minusz), 2);
            utolsoHalozatiCim = Padding(utolsoHalozatiCim, 32);
            return new IPV4(utolsoHalozatiCim, "");
        }

        //PrefixKulonbseg
        static public double PrefixKulonbseg(IPV4 maszk)
        {
            double plusz = 0;
            for (int i = 0; i < maszk.AddrBinary.Length; i++)
                if (maszk.AddrBinary[i] == '1')
                    plusz++;
            return plusz = Math.Pow(2, (double)(32 - plusz));
        }

        //Padding
        public static string Padding(string resz, int hosszig)
        {
            while (resz.Length < hosszig)
                resz = resz.Insert(0, "0");
            return resz;
        }

    }


}
