using System;
using System.Threading.Tasks;
using PPKSharp;

namespace ConsoleTest
{
    class Program
    {
        // the Request class represents a simple HttpClient wrapper that let's us GET and POST to a pre-defined URL,
        // where Request.Get(string url) appends url to the end of our pre-defined URL
        private static Request api;
        private static PPK ppk;
        static async Task Main(string[] args)
        {
            api = new Request();
            string PublicKey = await api.Get("public.key");
            Console.WriteLine(PublicKey); // Output is a public key retrieved from "{url}/public.key"

            ppk = new PPK(PublicKey);

            string EncryptedTextFromServer = await api.Get("encrypted-text.php");
            
            string DecryptedByClient = pem.Decrypt(EncryptedTextFromServer);
            Console.WriteLine(DecryptedByClient); // Output: Hello Client

            string EncryptedByClient = pem.Encrypt("Hello Server");

            string DecryptedByServer = await api.Post("decrypt-post-data.php", EncryptedByClient);
            Console.WriteLine(DecryptedByServer); // Output: Hello Server
        }
    }
}
