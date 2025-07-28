using Encryption.Exceptions;
using Encryption.Models;

namespace Encryption
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Secrecy secrecy = new Secrecy();

                Console.Write("Üzenet: ");
                string message = Console.ReadLine();
                int length = message.Length;

                secrecy.ValidFormat(message);

                string key = secrecy.keyGenerator(length);
                Console.WriteLine($"Kulcs: {key}");

                string encrypted = secrecy.Encrypting(message, key);
                Console.WriteLine($"Titkosított üzenet: {encrypted}");

                string decodedMessage = secrecy.Decoding(key, encrypted);
                Console.WriteLine($"Eredeti üzenet: {decodedMessage}");
            }
            catch (InvalidFormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
    }
}
