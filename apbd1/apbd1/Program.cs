using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace apbd1
{
    class Program
    {
        public static bool isValidUrl = false;
        public async static Task Main(string[] args)
        {
            //validations
            if (args.Length == 0) throw new ArgumentNullException();
            var websiteUrl = args[0];
            CheckIfValidUrl(websiteUrl);

            if (isValidUrl == false) throw new ArgumentException();

            var httpClient = new HttpClient();

            var response = await httpClient.GetAsync(websiteUrl);

            try
            {
                var content = await response.Content.ReadAsStringAsync();
                var regex = new Regex(@"[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+");

                var matchCollectionString = regex.Matches(content).Cast<Match>()
                .Select(m => m.Value)
                .ToArray();

                string[] matchCollectionStringUnique = matchCollectionString.Distinct().ToArray();

                if (matchCollectionStringUnique.Length > 0)
                {
                    foreach (string item in matchCollectionStringUnique)
                    {
                        Console.WriteLine(item);
                    }
                }
                else {
                    Console.WriteLine("Nie znaleziono adresów email");
                }
            }
            catch(HttpRequestException) {
                Console.WriteLine("Błąd w czasie pobierania strony");
            }

            // freeing http client resources
            httpClient.Dispose();
            response.Dispose();
        }
        static void CheckIfValidUrl(string url) {
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "HEAD";
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                response.Close();
                isValidUrl = true;
            }
            catch
            {
                isValidUrl =  false;
            }
        }
    }
}
