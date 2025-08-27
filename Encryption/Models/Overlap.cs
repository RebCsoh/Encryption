using System.Diagnostics.Tracing;

namespace Encryption.Models
{
    public class Overlap : Secrecy
    {
        string abc = "abcdefghijklmnopqrstuvwxyz ";

        //kiszámolja a kulcsot az első mondatra
        public string GetKey(string encrypted, string message)
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

        private bool WordsInList(string message, string[] words)
        {
            string[] parts = message.Split(' ');
            foreach (string part in parts)
            {
                if ( string.IsNullOrEmpty(part))
                    continue;

                if (!words.Contains(part))
                    return false;
            }
            return true;
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
                string keyparts = GetKey(encrypted1.Substring(0, word.Length), word);

                //szavak ellenőrzése (1)
                string decoded1 = Decoding(keyparts, encrypted1.Substring(0, word.Length));

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

                //szavak ellenőrzése (2)
                string[] parts2 = decoded2.Split(' ');
                string[] parts1 = decoded1.Split(' ');
                bool allGood = true;

                foreach (string part in parts1.Concat(parts2))
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

        public string[] ValidKeys(string[] finalKeys, string encrypted1, string encrypted2)
        {
            List<string> result = new List<string>();
            string[] words = File.ReadAllLines("words.txt");

            foreach (string key in finalKeys)
            {
                string decodedKey1 = Decoding(key, encrypted1);
                string decodedKey2 = Decoding(key, encrypted2);

                if(WordsInList(decodedKey1,words) && WordsInList(decodedKey2,words))
                    result.Add(key);
            }

            return result.ToArray();
        }
    }
}
