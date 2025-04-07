using Azure.Core;
using EgyptCurrencyRates.Filter;
using EgyptCurrencyRates.Models;
using EgyptCurrencyRates.ViewModels;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Hosting;
using System.Globalization;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography;
using System.Text;

namespace EgyptCurrencyRates.Help
{
    [ExceptionLog]
    public class MediaManager
    {
        IWebHostEnvironment host;
        public MediaManager(IWebHostEnvironment _hosting)
        {
            host = _hosting;
        }

        int headerId = 1; int bodyId = 1; int footerId = 1;
        public async Task PostToTelegram()
        {
            FileModels fileModels = new FileModels(host);

            CounterViewModel counter = fileModels.LoadCounter();
            headerId = int.Parse(counter.headerId);
            bodyId = int.Parse(counter.bodyId);
            footerId = int.Parse(counter.footerId);

            PostViewModel header = fileModels.Header().First(a => a.Id == headerId);
            PostViewModel body = fileModels.Body().First(a => a.Id == bodyId);
            PostViewModel footer = fileModels.Footer().First(a => a.Id == footerId);



            List<GoldPriceModel> GoldPrices = fileModels.GoldPrices().Where(g => g.UnitTypeId == 1).ToList();

            CultureInfo culture = new CultureInfo("ar-EG");

            string formattedDate = Analytics.Day() + " - " + GoldPrices.FirstOrDefault().Date.Value.ToString("dd MMMM yyyy - h.mm tt", culture);

            string k24 = GoldPrices.FirstOrDefault(g => g.GoldUnitId == 2).EgyptianPound;
            string k22 = GoldPrices.FirstOrDefault(g => g.GoldUnitId == 3).EgyptianPound;
            string k21 = GoldPrices.FirstOrDefault(g => g.GoldUnitId == 4).EgyptianPound;
            string k18 = GoldPrices.FirstOrDefault(g => g.GoldUnitId == 5).EgyptianPound;
            string k14 = GoldPrices.FirstOrDefault(g => g.GoldUnitId == 6).EgyptianPound;

            body.Value = body.Value.Replace("@date", formattedDate).Replace("@k24", k24).Replace("@k21", k21).Replace("@k18", k18).Replace("@k14", k14);

            string post = header.Value + Environment.NewLine + Environment.NewLine +
                body.Value + Environment.NewLine + Environment.NewLine +
                footer.Value + Environment.NewLine;

            headerId++; bodyId++; footerId++;

            if (headerId >= 32) { headerId = 1; }
            if (bodyId >= 3) { bodyId = 1; }
            if (footerId >= 20) { footerId = 1; }


            var creator = new FileCreator(host);

            creator.SaveCounter(new CounterViewModel { headerId = headerId.ToString(), bodyId = bodyId.ToString(), footerId = footerId.ToString() });


            #region Telegram
            string botToken = "7548174039:AAGp9nuyLv8ygR52g7oILNKBAGripyz4Arc";
            string chatId = "-1002481245413";
            string channelId = "-1002305568159";
            string message = post;

            string group_url = $"https://api.telegram.org/bot{botToken}/sendMessage?chat_id={chatId}&text={Uri.EscapeDataString(message)}";
            string channedl_url = $"https://api.telegram.org/bot{botToken}/sendMessage?chat_id={channelId}&text={Uri.EscapeDataString(message)}";


            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage group_response = await client.GetAsync(group_url);
                await group_response.Content.ReadAsStringAsync();

                HttpResponseMessage channel_response = await client.GetAsync(channedl_url);
                await channel_response.Content.ReadAsStringAsync();
            }
            #endregion
        }


        public string GenerateGoldTwitt()
        {
            FileModels fileModels = new FileModels(host);
            List<GoldPriceModel> GoldPrices = fileModels.GoldPrices().Where(g => g.UnitTypeId == 1).ToList();

            CultureInfo culture = new CultureInfo("ar-EG");

            string formattedDate = Analytics.Day() + " - " + GoldPrices.FirstOrDefault().Date.Value.ToString("dd MMMM yyyy - h.mm tt", culture);

            string k24 = GoldPrices.FirstOrDefault(g => g.GoldUnitId == 2).EgyptianPound;
            string k22 = GoldPrices.FirstOrDefault(g => g.GoldUnitId == 3).EgyptianPound;
            string k21 = GoldPrices.FirstOrDefault(g => g.GoldUnitId == 4).EgyptianPound;
            string k18 = GoldPrices.FirstOrDefault(g => g.GoldUnitId == 5).EgyptianPound;
            string k14 = GoldPrices.FirstOrDefault(g => g.GoldUnitId == 6).EgyptianPound;

            StringBuilder twitt = new StringBuilder();

            twitt.AppendLine("✨🔥💰 أسعار الذهب اليوم في مصر! 💰🔥✨");
            twitt.AppendLine();
            twitt.AppendLine(formattedDate);
            twitt.AppendLine();
            twitt.AppendLine(string.Format("🏅 عيار 24: {0} جنيه", k24));
            twitt.AppendLine(string.Format("🏅 عيار 21: {0} جنيه", k21));
            twitt.AppendLine(string.Format("🏅 عيار 18: {0} جنيه", k18));
            twitt.AppendLine();
            twitt.AppendLine("🚀 عايز تعرف آخر التحديثات؟ اضغط هنا");
            twitt.AppendLine("https://egyptcurrencyrates.com/Gold/Index");
            twitt.AppendLine();
            twitt.AppendLine("#الذهب #اسعار_الذهب #مصر #الاستثمار #الاقتصاد");

            return twitt.ToString();
        }

        public async Task PostToTwitterGold()
        {
            string clientId = "Y0h5Z3k3X2NZdWJNMFEtcVpNQzI6MTpjaQ";
            string ClientSecret = "KIDr6Q8H3xxWViasMWxHJfl2Aaajytr-q2zO_z9NMuWuUVhVjF";
            string ApiKey = "RHFdvcG3l729xypnxWjN4iPR0";
            string ApiSecret = "Tm5uluwOcckv447RiZjBkNLOQcWE0BHlOoWMG4LOqENlBYffdR";
            string AccessToken = "1474118034080022528-kt7hSsFSRNuGOcXdYSBPQMgdMtPMEq";
            string AccessTokenSecret = "xOzg1n3SAcJQEKgXxXtatUe5d2m9MMCYVXuuiDas3CvFf";
            string BearerToken = "AAAAAAAAAAAAAAAAAAAAABK0zwEAAAAAgNMjIJl%2FcNLIhBjiAMsYaQRZtzI%3DMKXznK1xwTqtu1VpytD0fmDQFdspv3cpVchbtxGJpU6Ud2obiO"; // For OAuth 2.0


            string url = "https://api.twitter.com/2/tweets";

            string tweetText = GenerateGoldTwitt();

            try
            {
                string tweetId = await PostTweetAsync(tweetText, ApiKey, ApiSecret, AccessToken, AccessTokenSecret);
                Console.WriteLine($"Tweet posted successfully! Tweet ID: {tweetId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error posting tweet: {ex.Message}");
            }
        }

        public async Task POstToTwitterRates()
        {

        }

        private static async Task<string> PostTweetAsync(string text, string ApiKey, string ApiSecret, string AccessToken, string AccessTokenSecret)
        {
            using (HttpClient client = new HttpClient())
            {
                // Set the base URL for the X API
                string url = "https://api.twitter.com/2/tweets";

                // Create the tweet payload
                var tweetData = new
                {
                    text = text
                };

                string jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(tweetData);
                HttpContent content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

                // Generate OAuth 1.0a headers
                string oauthHeaders = GenerateOAuthHeaders(url, "POST", ApiKey, ApiSecret, AccessToken, AccessTokenSecret);
                client.DefaultRequestHeaders.Add("Authorization", oauthHeaders);

                // Make the POST request to the tweets endpoint
                HttpResponseMessage response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseJson = await response.Content.ReadAsStringAsync();
                    dynamic responseData = Newtonsoft.Json.JsonConvert.DeserializeObject(responseJson);
                    return responseData.data.id;
                }
                else
                {
                    string errorResponse = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Failed to post tweet. Status: {response.StatusCode}, Response: {errorResponse}");
                }
            }
        }

        private static string GenerateOAuthHeaders(string url, string method, string apiKey, string apiSecret, string accessToken, string accessTokenSecret)
        {
            var oauthNonce = Guid.NewGuid().ToString("N");
            var oauthTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
            var oauthSignatureMethod = "HMAC-SHA1";
            var oauthVersion = "1.0";

            // Create the parameter string
            var parameters = new SortedDictionary<string, string>
        {
            { "oauth_consumer_key", apiKey },
            { "oauth_nonce", oauthNonce },
            { "oauth_signature_method", oauthSignatureMethod },
            { "oauth_timestamp", oauthTimestamp },
            { "oauth_token", accessToken },
            { "oauth_version", oauthVersion }
        };

            // Create the signature base string
            var parameterString = string.Join("&", parameters.Select(p => $"{Uri.EscapeDataString(p.Key)}={Uri.EscapeDataString(p.Value)}"));
            var signatureBaseString = $"{method}&{Uri.EscapeDataString(url)}&{Uri.EscapeDataString(parameterString)}";

            // Create the signing key
            var signingKey = $"{Uri.EscapeDataString(apiSecret)}&{Uri.EscapeDataString(accessTokenSecret)}";

            // Generate the signature
            using (var hmac = new HMACSHA1(Encoding.UTF8.GetBytes(signingKey)))
            {
                var signatureBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(signatureBaseString));
                var oauthSignature = Convert.ToBase64String(signatureBytes);

                // Add the signature to the parameters
                parameters.Add("oauth_signature", oauthSignature);

                // Build the OAuth header
                var oauthHeader = "OAuth " + string.Join(", ", parameters.Select(p => $"{Uri.EscapeDataString(p.Key)}=\"{Uri.EscapeDataString(p.Value)}\""));
                return oauthHeader;
            }
        }
    }
}