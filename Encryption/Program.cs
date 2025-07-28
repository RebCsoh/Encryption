using Encryption.Exceptions;
using Encryption.Models;
using Microsoft.Win32.SafeHandles;

namespace Encryption
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Menü: \n" +
                "1. Üzenet titkosítás. \n" +
                "2. Kulcsok megtalálása. \n" +
                "Kérem válasszon a fenti opciók közül (1 vagy 2)");

            int choice = int.Parse(Console.ReadLine());
            Console.Clear();

            Secrecy secrecy = new Secrecy();
            Overlap overlap = new Overlap();

            try
            {
                switch(choice)
                {
                    case 1:
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
                        break;

                    case 2:
                        Console.Write("Első titkosított üzenet: ");
                        string encrypted1 = Console.ReadLine();

                        Console.Write("Második titkosított üzenet: ");
                        string encrypted2 = Console.ReadLine();

                        secrecy.ValidFormat(encrypted1);
                        secrecy.ValidFormat(encrypted2);

                        string[] keys = overlap.FindKey(encrypted1, encrypted2);

                        if (keys.Length == 0)
                            Console.WriteLine("Nincs érvényes kulcs.");
                        else
                        {
                            Console.WriteLine("Lehetséges kulcsok:");
                            foreach (string k in keys)
                            {
                                string msg = secrecy.Decoding(k, encrypted2);
                                Console.WriteLine($"Kulcs: {k}, Üzenet: {msg}");
                            }
                        }
                        break;
                }               
            }
            catch (InvalidFormatException ex)
            {
                Console.WriteLine(ex.Message);
            }   
        }
    }
}
