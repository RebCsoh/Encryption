namespace Encryption.Models
{
    public class Overlap : Secrecy
    {
        string abc = "abcdefghijklmnopqrstuvwxyz ";

        //kiszámolja a kulcsot az első mondatra
        public string getKey(string encrypted, string message)
        {
            string key = string.Empty;
            for (int i = 0; i < encrypted.Length; i++)
            {
                int encryptedCode = CharToNumber(encrypted[i]);
                int messageCode = CharToNumber(message[i]);
                key += abc[(encryptedCode - messageCode + 27) % 27];
            }
            return key;
        }

        //megnézi a második szöveget és közös kulcsot keres
        public string[] FindKey(string encrypted1, string encrypted2)
        {
            List<string> finalKeys = new List<string>();

            string[] words = File.ReadAllLines("words.txt");

            foreach (string word in words)
            {
                if (word.Length > encrypted1.Length)
                    continue;

                //első üzenet elejére lesz illesztve
                string keyparts = getKey(encrypted1.Substring(0, word.Length), word);

                //bővitjük ha rövid első üzenet
                string fullkey1 = keyparts;
                while (fullkey1.Length < encrypted1.Length)
                    fullkey1 += fullkey1;
                fullkey1 = fullkey1.Substring(0, encrypted1.Length);


                //bővités második üzenet
                string fullkey2 = fullkey1;
                while (fullkey2.Length < encrypted2.Length)
                    fullkey2 += fullkey2;
                fullkey2 = fullkey2.Substring(0, encrypted2.Length);

                string decoded2 = Decoding(fullkey2, encrypted2);

                //szavak ellenőrzése
                string[] parts = decoded2.Split(' ');
                bool allGood = true;

                foreach (string part in parts)
                {
                    if (!words.Contains(part))
                    {
                        allGood = false;
                        break;
                    }

                }

                if (allGood && !finalKeys.Contains(fullkey1))
                    finalKeys.Add(fullkey1);

            }
            return finalKeys.ToArray();
        }
    }
}
