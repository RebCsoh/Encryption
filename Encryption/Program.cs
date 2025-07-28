using Encryption.Exceptions;
using Encryption.Models;
using Microsoft.Win32.SafeHandles;

namespace Encryption
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Secrecy secrecy = new Secrecy();
            Overlap overlap = new Overlap();
            string[] words = ReadFile("words.txt");

            try
            {
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

            Console.Write("Első titkosított üzenet: ");
            string encrypted1 = Console.ReadLine();

            Console.Write("Második titkosított üzenet: ");
            string encrypted2 = Console.ReadLine();

            secrecy.ValidFormat(encrypted1);
            secrecy.ValidFormat(encrypted2);

            string[] keys = overlap.FindKey(encrypted1, encrypted2, words);

            if (keys.Length == 0)
                Console.WriteLine("Nincs érvényes kulcs.");
            else
            {
                Console.WriteLine("Lehetséges kulcsok:");
                foreach (string key in keys)
                {
                    string message = secrecy.Decoding(key, encrypted2);
                    Console.WriteLine($"Kulcs: {key}, Üzenet: {message}");
                }
            }
        }

        static string[] ReadFile(string path)
        {
            return File.ReadAllLines(path);
        }
    }
}
