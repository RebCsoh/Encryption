namespace Encryption
{
    public class Secrery
    {
        // kulcs generálás
        string abc = "abcdefghijklmnopqrstuvwxyz ";
        public string keyGenerator()
        {
            string GeneratedKey = string.Empty;
            Random random = new Random();
            int lenght = random.Next(1,100);

            for (int i = 0; i < lenght; i++)
            {
                int number = random.Next(0,27);
                int lettre = abc[number];
                GeneratedKey += lettre;
            }
            return GeneratedKey;
        }

        //titkosított üzenet
        public string Encrypting(string message, string key)
        {
            string encrypted = string.Empty;
            int tmp = 0;

            for (int i = 0; i < message.Length;i++)
            {
                tmp = CharToNumber(message[i]) + CharToNumber(key[i]);
                encrypted += abc[tmp];
            }
            return encrypted;
        }

        public int CharToNumber(char lettre)
        {
            return abc.IndexOf(lettre);
        }

        //dekodolás
        public string Decoding(string key, string encrypted)
        {
            string message = string.Empty;
            int tmp = 0;

            for (int i = 0;i < encrypted.Length;i++)
            {
                tmp = CharToNumber(encrypted[i]) - CharToNumber(key[i]);
                if (tmp < 0)
                {
                    tmp += 27;
                }
                message += abc[tmp];
            }
            return message;
        }
    }
}
