using CMS.AI.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CMS.AI.Infrastructure.Services
{
    public class OpenAIService : IAIService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _model;

        public OpenAIService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiKey = configuration["OpenAI:ApiKey"];
            _model = configuration["OpenAI:Model"] ?? "gpt-4";

            _httpClient.BaseAddress = new Uri("https://api.openai.com/v1/");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
        }

        public async Task<string> GenerateSEOTitleAsync(string content, CancellationToken cancellationToken = default)
        {
            var prompt = $"Generate an SEO-friendly title (60 characters max) for this content: {content}";
            return await SendCompletionRequestAsync(prompt, cancellationToken);
        }

        // Diğer metodları da benzer şekilde güncelle

        private async Task<string> SendCompletionRequestAsync(string prompt, CancellationToken cancellationToken = default)
        {
            var requestBody = new
            {
                model = _model,
                messages = new[]
                {
                new { role = "system", content = "You are a professional content writer and SEO expert." },
                new { role = "user", content = prompt }
            },
                max_tokens = 500,
                temperature = 0.7
            };

            var response = await _httpClient.PostAsJsonAsync("chat/completions", requestBody, cancellationToken);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync(cancellationToken);
            using var document = JsonDocument.Parse(responseBody);

            var choices = document.RootElement.GetProperty("choices");
            var firstChoice = choices[0];
            var message = firstChoice.GetProperty("message");
            var content = message.GetProperty("content").GetString() ?? string.Empty;

            return content.Trim();
        }
    }
}
