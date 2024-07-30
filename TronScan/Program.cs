using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TronScan
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            await CheckRisk("853793d552635f533aa982b92b35b00e63a1c1add062c099da2450a15119bcb2");
        }

        static async Task CheckRisk(string transactionHash)
        {
            string url = $"https://apilist.tronscanapi.com/api/transaction-info?hash={transactionHash}";

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    JObject data = JObject.Parse(jsonResponse);

                    bool isRiskTransaction = (bool)data["riskTransaction"];

                    Console.WriteLine($"Risk Transaction: {isRiskTransaction}");
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode}");
                }
            }
        }
    }
}
