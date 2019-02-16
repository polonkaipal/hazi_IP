using System;
using System.Text;

namespace TesztIP
{
    class IPV4
    {
        //Egy IP számhoz tartozó szöveges megjegyzés maximális karakterhossza
        // Eng: CommentMaxLength Hu: MegjegyzesMaxHossza
        private const int CommentMaxLength = 70;

        //Attribútumok (adattagok) listája

        // Az IP cím hány bájton kerül tárolásra
        // IPV4 esetén =4, IPV6 esezén =16
        // Eng: numOfMembers Hu: cimTagokSzama
        private const int NumOfMembers = 4;

        // Az IP cím bájtjai csökkenő helyiérték szerint
        // indexek: [0]-> 31.-24. / [1]-> 23.-16. / [2]-> 15.-8. / [3]-> 7.-0. bitek
        // Eng: IPaddrMembers Hu: IPcimTagok
        private byte[] IPaddrMembers = new byte[NumOfMembers];
        // Megjegyzés 
        // Eng: Comment Hu: leiroSzoveg
        private string comment = "";

        //Decimális számok ellenőrzése
        private static bool DecSzamokEll(byte szam)
        {
            if (szam > 255 || szam < 0)
                throw new ArgumentException("A szám csak 0 és 255 közötti lehet!!!");
            else
                return true;
        }

        //Konstruktorok
        //Kon#1
        public IPV4(byte d31, byte d23, byte d15, byte d7) //d31 - az adott tag legmagasabb helyiértékű bitjét jelenti a 31
        {
            if (DecSzamokEll(d31) && DecSzamokEll(d23) && DecSzamokEll(d15) && DecSzamokEll(7))
            {
                this.IPaddrMembers[0] = d31;
                this.IPaddrMembers[1] = d23;
                this.IPaddrMembers[2] = d15;
                this.IPaddrMembers[3] = d7;
            }
            else
                throw new ArgumentException("A szám csak 0 és 255 közötti lehet!!!");
        }

        //Kon#2
        public IPV4(byte d31, byte d23, byte d15, byte d7, string leiro)
        {
            if (DecSzamokEll(d31) && DecSzamokEll(d23) && DecSzamokEll(d15) && DecSzamokEll(7))
            {
                this.IPaddrMembers[0] = d31;
                this.IPaddrMembers[1] = d23;
                this.IPaddrMembers[2] = d15;
                this.IPaddrMembers[3] = d7;
                AddComment(leiro);
            }
            else
                throw new ArgumentException("A szám csak 0 és 255 közötti lehet!!!");
        }

        //Kon#3
        //A cím fomátuma a.b.c.d, ahol a,b,c,d decimális, vagy bináris számok
        //
        //A konstruktor működjön a pont nélküli 32 karakteres bináris formátumra és a 35 karakteres pontokkal elválasztottra is
        //A nem megfelelő címre dobjon System.ArgumentException kivételt az alábbiak szerint:
        // - ha pontozott a cím,akkor négy számtag kell legyen
        // - ha a cím bináris formában van és nem 32 és nem 35 hosszú
        // - ha pontozott decimális és ( rövidebb 7-nél, vagy hosszabb 15-nél )
        public IPV4(string ipaddress, string leiro)
        {
            if (ipaddress.Length == 32)
            {
                foreach (char szam in ipaddress)
                    if (szam != '0' && szam != '1')
                        throw new ArgumentException("Csak '0' vagy '1' karaktereket tartalmazhat");
                IPaddrMembers[0] = Convert.ToByte(ipaddress.Substring(0, 8), 2);
                IPaddrMembers[1] = Convert.ToByte(ipaddress.Substring(8, 8), 2);
                IPaddrMembers[2] = Convert.ToByte(ipaddress.Substring(16, 8), 2);
                IPaddrMembers[3] = Convert.ToByte(ipaddress.Substring(24, 8), 2);

            }

            else if (ipaddress.Length == 35)
            {
                string[] cimek = ipaddress.Split('.');

                if (cimek.Length != NumOfMembers)
                    throw new ArgumentException((new StringBuilder("A cím csak {0} tagú lehet!!!", NumOfMembers)).ToString());
                else
                    foreach (string cim in cimek)
                        if (cim.Length < 8 || cim.Length > 16)
                            throw new ArgumentException("A címtagok 8 vagy 16 bit hosszú lehet");

                foreach (string oktett in cimek)
                {
                    foreach (var szam in oktett)
                        if (szam != '0' && szam != '1')
                            throw new ArgumentException("Érvénytelen bináris formátum!");
                }

                IPaddrMembers[0] = Convert.ToByte(cimek[0], 2);
                IPaddrMembers[1] = Convert.ToByte(cimek[1], 2);
                IPaddrMembers[2] = Convert.ToByte(cimek[2], 2);
                IPaddrMembers[3] = Convert.ToByte(cimek[3], 2);
            }

            else if (ipaddress.Split('.').Length == 4)
            {
                string[] cimek = ipaddress.Split('.');

                try
                {
                    IPaddrMembers[0] = Convert.ToByte(cimek[0]);
                    IPaddrMembers[1] = Convert.ToByte(cimek[1]);
                    IPaddrMembers[2] = Convert.ToByte(cimek[2]);
                    IPaddrMembers[3] = Convert.ToByte(cimek[3]);
                }
                catch (OverflowException) { Console.WriteLine("A cím formátuma nem megfelelő!!"); }
            }

            else
                throw new ArgumentException("Az IP cím csak 32 darab 1-est és 0-ást tartalmazhat! Vagy szabályos pontozott decimális címet!");

            //a megjegyzés beállítására ezt használja!
            AddComment(leiro);

        }

        // Belső használatú metódusok
        // --------------------------
        // Eng: AddComment Hu: HozzadMegjegyzest
        private void AddComment(string szoveg)
        {
            if (szoveg.Length > CommentMaxLength)
                throw new ArgumentException("Túl hosszú a leíró szöveg!");
            else
                this.comment = szoveg;
        }


        // Kivülről elérhető metódusok

        // Eng: AddrMembersArray Hu: CimTagokTombje
        public byte[] AddrMembersArray
        {
            get
            {
                return this.IPaddrMembers;
            }
        }

        //0-kal tölti fel az elejét, ha a hossz nincs meg a 8 bit
        private string Padding(string resz, int hosszig)
        {
            while (resz.Length < hosszig)
                resz = resz.Insert(0, "0");
            return resz;
        }

        // Eng: AddrBinary Hu: CimBinaris
        public String AddrBinary
        {
            get
            {
                StringBuilder cim = new StringBuilder();
                for (int i = 0; i < IPaddrMembers.Length; i++)
                    cim.Append(Padding(Convert.ToString(Convert.ToInt32(IPaddrMembers[i]), 2), 8));
                return cim.ToString();
            }
            set
            {
                //value -ban lesz az érték, amit fel kell dolgozni
                //csak 32 karakter hosszú lehet, egyéb esetben kivételt kell dobni!

                if (value.Length != 32)
                    throw new ArgumentException("A cím nem 32 bites!!!");
                else
                {
                    IPaddrMembers[0] = Convert.ToByte(value.Substring(0, 8), 2);
                    IPaddrMembers[1] = Convert.ToByte(value.Substring(8, 8), 2);
                    IPaddrMembers[2] = Convert.ToByte(value.Substring(16, 8), 2);
                    IPaddrMembers[3] = Convert.ToByte(value.Substring(24, 8), 2);
                }
            }
        }

        // Eng: AddrBinaryDotted Hu: CimBinarisPontokkal
        public String AddrBinaryDotted
        {
            get
            {
                StringBuilder cim = new StringBuilder();
                for (int i = 0; i < IPaddrMembers.Length; i++)
                    cim.Append(Padding(Convert.ToString(Convert.ToInt32(IPaddrMembers[i]), 2), 8) + (i < 3 ? "." : ""));
                return cim.ToString();
            }
            set
            {
                //value -ban lesz az érték, amit fel kell dolgozni
                //csak 35 karakter hosszú lehet, egyéb esetben kivételt kell dobni!
                if (value.Length != 35)
                    throw new ArgumentException("A cím nem pontozott bináris szám!!!");
                else
                {
                    IPaddrMembers[0] = Convert.ToByte(value.Substring(0, 8), 2);
                    IPaddrMembers[1] = Convert.ToByte(value.Substring(9, 8), 2);
                    IPaddrMembers[2] = Convert.ToByte(value.Substring(18, 8), 2);
                    IPaddrMembers[3] = Convert.ToByte(value.Substring(27, 8), 2);
                }

            }
        }

        // Eng: AddrDecimalDotted Hu: CimDecimalisPontokkal
        public String AddrDecimalDotted
        {
            get
            {
                StringBuilder resz = new StringBuilder();
                for (int i = 0; i < IPaddrMembers.Length; i++)
                    resz.Append(IPaddrMembers[i] + (i < 3 ? "." : ""));
                return resz.ToString();
            }
            set
            {
                //value -ban lesz az érték, amit fel kell dolgozni
                //hibás formátum esetén kivételt kell dobni! 
                //A [Teszt - 2.txt] fájlból látható, milyen kivételekre milyen üzenetet kell adni!
                // - Nincs meg a 4 címtag!"
                // - A decimális szám csak 0..255 értéket vehet fel!
                // - Nem decimális a formátum!
                string[] reszek = value.Split('.');
                if (reszek.Length != NumOfMembers)
                    throw new ArgumentException("Nincs meg a 4 címtag!");

                decimal resz;
                for (int i = 0; i < reszek.Length; i++)
                {
                    try { resz = Convert.ToDecimal(reszek[i]); }
                    catch (FormatException) { throw new ArgumentException("Nem decimális a formátum!"); }
                }

                for (int i = 0; i < reszek.Length; i++)
                {
                    resz = Convert.ToInt32(reszek[i]);
                    if (resz < 0 || resz > 255)
                        throw new ArgumentException("A decimális szám csak 0..255 értéket vehet fel!");
                }

                for (int i = 0; i < reszek.Length; i++)
                    IPaddrMembers[i] = Convert.ToByte(reszek[i]);
            }
        }

        // Eng: Comment Hu: Megjegyzes
        public string Comment
        {
            get
            {
                return this.comment;
            }
            set
            {
                this.AddComment(value);
            }
        }

    }

}
