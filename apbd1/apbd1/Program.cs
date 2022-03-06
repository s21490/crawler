using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace apbd1
{
    class Program
    {
        public async static Task Main(string[] args)
        {
            var websiteUrl = args[0];
            // Console.WriteLine(websiteUrl);

            var httpClient = new HttpClient();
            //var httpClient = new HttpClient();

            var response = await httpClient.GetAsync(websiteUrl);
            //Console.WriteLine(response);

            var content = await response.Content.ReadAsStringAsync();
            //Console.WriteLine(content);

            var regex = new Regex(@" .*@pja.* ");

            var matchCollection = regex.Matches(content);

            foreach (Match item in matchCollection) {
                Console.WriteLine(item.Value);
            }
        }
    }
}
