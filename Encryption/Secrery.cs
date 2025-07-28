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
    }
}
