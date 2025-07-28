namespace Encryption.Models
{
    public class Overlap : Secrecy
    {
        string abc = "abcdefghijklmnopqrstuvwxyz ";

        public string[] FindKey(string encrypted1, string encrypted2)
        {
            List<string> keys = new List<string>();
            List<string> finalKeys = new List<string>();

            string[] words = File.ReadAllLines("words.txt");

            //titkosított üzenet <= üzenet, keressük ezeket a szvakat a listában
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length >= encrypted1.Length)
                {
                    string tmpKey = string.Empty;

                    //karakterenként visszafejtjük, hogy mi lehetett a kulcs
                    //kulcs = titkosítottkód - üzenetkód + átalakítás
                    for (int j = 0; j < encrypted1.Length; j++)
                    {
                        int messageCode = CharToNumber(words[i][j]);
                        int encryptedCode = CharToNumber(encrypted1[j]);
                        char keyCode = abc[(encryptedCode - messageCode + 27) % 27];

                        tmpKey += keyCode;
                    }
                    if (!keys.Contains(tmpKey))
                        keys.Add(tmpKey);
                }
            }

            //visszafejtjük a 2. üzenetre, ha értelmes lesz a szó akkor érvényes a kulcs
            //szavakra kell bontanunk
            for (int i = 0; i < keys.Count; i++)
            {
                //nem lehet a msásodik üzenet hosszabb, mert indexelési hiba lesz
                string extendedKey = keys[i];

                while (extendedKey.Length < encrypted2.Length)
                    extendedKey += extendedKey;

                extendedKey = extendedKey.Substring(0, encrypted2.Length);


                string tmpMessage = Decoding(extendedKey, encrypted2);
                string[] parts = tmpMessage.Split(' ');
                bool allGood = true;

                foreach (string part in parts)
                {
                    if (!words.Contains(part))
                    {
                        allGood = false;
                        break;
                    }
                    
                }

                if (allGood)
                    finalKeys.Add(keys[i]);

            }
            return finalKeys.ToArray();
        }
    }
}
