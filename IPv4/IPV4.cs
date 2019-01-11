using System;

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
        private static bool decSzamokEll(byte szam)
        {
            if (szam > 255 || szam < 0) { throw new ArgumentException("A szám csak 0 és 255 közötti lehet!!!"); }
            else { return true; };
        }

        //Konstruktorok
        //Kon#1
        public IPV4(byte d31, byte d23, byte d15, byte d7) //d31 - az adott tag legmagasabb helyiértékű bitjét jelenti a 31
        {
            try
            {
                if (decSzamokEll(d31) && decSzamokEll(d23) && decSzamokEll(d15) && decSzamokEll(7))
                {
                    this.IPaddrMembers[0] = d31;
                    this.IPaddrMembers[1] = d23;
                    this.IPaddrMembers[2] = d15;
                    this.IPaddrMembers[3] = d7;
                }
                else { throw new ArgumentException("A szám csak 0 és 255 közötti lehet!!!"); }
            }
            catch (ArgumentException ae) { Console.WriteLine(ae); }
        }
        //Kon#2
        public IPV4(byte d31, byte d23, byte d15, byte d7, string leiro)
        {
            try
            {
                if (decSzamokEll(d31) && decSzamokEll(d23) && decSzamokEll(d15) && decSzamokEll(7))
                {
                    this.IPaddrMembers[0] = d31;
                    this.IPaddrMembers[1] = d23;
                    this.IPaddrMembers[2] = d15;
                    this.IPaddrMembers[3] = d7;
                    AddComment(leiro);
                }
                else { throw new ArgumentException("A szám csak 0 és 255 közötti lehet!!!"); }
            }
            catch (ArgumentException ae) { Console.WriteLine(ae); }
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


        // Eng: AddrBinary Hu: CimBinaris
        public String AddrBinary
        {
            get
            {
                return "?";
            }
            set
            {
                //value -ban lesz az érték, amit fel kell dolgozni
                //csak 32 karakter hosszú lehet, egyéb esetben kivételt kell dobni!
            }
        }

        // Eng: AddrBinaryDotted Hu: CimBinarisPontokkal
        public String AddrBinaryDotted
        {
            get
            {
                return "?";
            }
            set
            {
                //value -ban lesz az érték, amit fel kell dolgozni
                //csak 35 karakter hosszú lehet, egyéb esetben kivételt kell dobni!
            }
        }

        // Eng: AddrDecimalDotted Hu: CimDecimalisPontokkal
        public String AddrDecimalDotted
        {
            get
            {
                return "?";
            }
            set
            {
                //value -ban lesz az érték, amit fel kell dolgozni
                //hibás formátum esetén kivételt kell dobni! 
                //A [Teszt - 2.txt] fájlból látható, milyen kivételekre milyen üzenetet kell adni!
                // - Nincs meg a 4 címtag!"
                // - A decimális szám csak 0..255 értéket vehet fel!
                // - Nem decimális a formátum!

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
