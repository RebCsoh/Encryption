namespace Encryption.Models
{
    public class Overlap : Secrecy
    {
        string abc = "abcdefghijklmnopqrstuvwxyz ";

        public string[] FindKey(string encrypted1, string encrypted2, string[] words)
        {
            List<string> keys = new List<string>();
            List<string> finalKeys = new List<string>();

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
                    if(!keys.Contains(tmpKey))
                        keys.Add(tmpKey);
                }
            }

            //visszafejtjük a 2. üzenetre, ha értelmes lesz a kulccsal akkor érvényes a kulcs
            for (int i = 0; i < keys.Count; i++)
            {
                string tmpMessage = Decoding(keys[i], encrypted2);

                if (words.Contains(tmpMessage))
                    finalKeys.Add(keys[i]);
            }
            return finalKeys.ToArray();
        }
    }
}
