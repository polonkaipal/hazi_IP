using System;

namespace TesztIP
{
    class IPV4Utils
    {
        // Előállítja az alhálózati maszk IPv4 címét a hálózati rész bithosszának ismeretében
        // Csak a 8..30 értékeket fogadja el, egyébként kivétel!
        static public IPV4 GetSubnetMask(int netLength)
        {
            return new IPV4(1, 1, 1, 1, "Még nincs implementálva: GetSubnetMask");
        }

        // Egy IPv4 cím és a hozzátartozó alhálózati maszk alapján visszadja a hálózati címet
        // Hibás alhűlózati maszk esetén kivétel! -> throw new System.ArgumentException("Hibás az alhálózati maszk!");
        static public IPV4 GetNetworkFromIPV4(IPV4 addr, IPV4 subnet)
        {
            return new IPV4(1, 1, 1, 1, "Még nincs implementálva: GetNetworkFromIPV4");
        }

        // Egy IPv4 cím és a hozzátartozó alhálózati maszk alapján visszadja az adott hálózatban kiosztható legkisebbb (legelső) hálózati címet
        static public IPV4 GetFirstHostWithIPV4(IPV4 addr, IPV4 subnet)
        {
            return new IPV4(1, 1, 1, 1, "Még nincs implementálva: GetFirstHostWithIPV4");
        }

        // Egy IPv4 cím és a hozzátartozó alhálózati maszk alapján visszadja az adott hálózatban kiosztható legutolsó hálózati címet
        static public IPV4 GetLastHostWithIPV4(IPV4 addr, IPV4 subnet)
        {
            return new IPV4(1, 1, 1, 1, "Még nincs implementálva: GetLastHostWithIPV4");
        }

        // Egy IPv4 cím és a hozzátartozó alhálózati maszk alapján visszadja az adott hálózatot közvetlenül követő hálózat címét
        static public IPV4 GetNextNetworkWithIPV4(IPV4 addr, IPV4 subnet)
        {
            return new IPV4(1, 1, 1, 1, "Még nincs implementálva: GetNextNetworkWithIPV4");
        }

        // Egy IPv4 cím és a hozzátartozó alhálózati maszk alapján visszadja az adott hálózatot közvetlenül megelőző hálózat címét
        static public IPV4 GetPreviousNetworkWithIPV4(IPV4 addr, IPV4 subnet)
        {
            return new IPV4(1, 1, 1, 1, "Még nincs implementálva: GetPreviousNetworkWithIPV4");
        }


    }


}
