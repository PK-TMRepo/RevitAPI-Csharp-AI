using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WallLayerAnalyzer
{
    public class OpenAIService
    {
        private readonly HttpClient _client;
        private readonly string _apiKey = "YOUR OPEN AI key";

        public OpenAIService()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://api.openai.com/v1/");
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
        }

        public async Task<string> GetMaterialDescriptionAsync(string materialName, string wallTypeName)
        {
            string prompt = "Describe construction material '" + materialName +
                          "' (used in wall type '" + wallTypeName + "') in Polish.\n" +
                          "Include:\n1. Application\n2. Properties\n3. Manufacturers\n4. Standards";
            return await SendChatRequest(prompt) ?? "No description available.";
        }

        public async Task<Tuple<double?, string>> GetUValueAnalysisAsync(string materialName, double thicknessMm)
        {
            string prompt = "For material '" + materialName + "' with thickness " + thicknessMm + " mm:\n" +
                          "1. Calculate U-value (just the number, e.g., 0.25).\n" +
                          "2. Assess compliance with Polish WT 2024 standard (second line).";

            string response = await SendChatRequest(prompt);
            return ParseUValueResponse(response);
        }

        private async Task<string> SendChatRequest(string prompt)
        {
            try
            {
                var requestBody = new
                {
                    model = "gpt-3.5-turbo",
                    messages = new[] { new { role = "user", content = prompt } },
                    temperature = 0.3
                };

                var response = await _client.PostAsync(
                    "chat/completions",
                    new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json"));

                response.EnsureSuccessStatusCode();

                using var doc = await JsonDocument.ParseAsync(await response.Content.ReadAsStreamAsync());
                return doc.RootElement.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }

        private Tuple<double?, string> ParseUValueResponse(string response)
        {
            if (string.IsNullOrEmpty(response))
                return Tuple.Create<double?, string>(null, "Analysis error");

            string[] lines = response.Split('\n');
            if (lines.Length >= 1 && double.TryParse(lines[0].Trim(), out double uValue))
            {
                string comment = lines.Length > 1 ? lines[1] : "No comment";
                return Tuple.Create<double?, string>(uValue, comment);
            }
            return Tuple.Create<double?, string>(null, response);
        }
    }
}