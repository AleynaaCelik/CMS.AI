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
    // CMS.AI.Infrastructure/Services/OpenAIService.cs
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

        public async Task<AIContentAnalysisResult> AnalyzeContentAsync(string content, CancellationToken cancellationToken = default)
        {
            // Analiz sonuçlarını topluca almak için mevcut metotları çağıralım
            var title = await GenerateSEOTitleAsync(content, cancellationToken);
            var description = await GenerateSEODescriptionAsync(content, cancellationToken);
            var keywords = await GenerateKeywordsAsync(content, cancellationToken);
            var improvedContent = await ImproveContentAsync(content, cancellationToken);
            var imagePrompt = await GenerateImagePromptAsync(content, cancellationToken);

            // Daha sonra bir özet oluşturalım
            string prompt = $"Summarize this content in 2-3 sentences: {content}";
            var summary = await SendCompletionRequestAsync(prompt, cancellationToken);

            // Sentiment analizi (örnek bir değer, gerçek bir OpenAI çağrısıyla değiştirilebilir)
            double sentimentScore = 0.7;

            return new AIContentAnalysisResult
            {
                Title = title,
                Description = description,
                Keywords = keywords,
                ImprovedContent = improvedContent,
                ImagePrompt = imagePrompt,
                SentimentScore = sentimentScore,
                Summary = summary
            };
        }

        public async Task<string> GenerateSEOTitleAsync(string content, CancellationToken cancellationToken = default)
        {
            string prompt = $"Generate an SEO-friendly title (60 characters max) for this content: {content}";
            return await SendCompletionRequestAsync(prompt, cancellationToken);
        }

        public async Task<string> GenerateSEODescriptionAsync(string content, CancellationToken cancellationToken = default)
        {
            string prompt = $"Generate an SEO-friendly meta description (160 characters max) for this content: {content}";
            return await SendCompletionRequestAsync(prompt, cancellationToken);
        }

        public async Task<List<string>> GenerateKeywordsAsync(string content, CancellationToken cancellationToken = default)
        {
            string prompt = $"Generate 5-10 relevant keywords or keyword phrases for this content. Return only the keywords separated by commas: {content}";
            string response = await SendCompletionRequestAsync(prompt, cancellationToken);

            // Virgülle ayrılmış kelimeleri listeye dönüştür
            return response.Split(',')
                .Select(k => k.Trim())
                .Where(k => !string.IsNullOrWhiteSpace(k))
                .ToList();
        }

        public async Task<string> ImproveContentAsync(string content, CancellationToken cancellationToken = default)
        {
            string prompt = $"Improve this content by enhancing readability, fixing grammar issues, and making it more engaging: {content}";
            return await SendCompletionRequestAsync(prompt, cancellationToken);
        }

        public async Task<string> GenerateImagePromptAsync(string content, CancellationToken cancellationToken = default)
        {
            string prompt = $"Create a detailed image prompt for DALL-E or Midjourney that represents the main theme of this content: {content}";
            return await SendCompletionRequestAsync(prompt, cancellationToken);
        }

        private async Task<string> SendCompletionRequestAsync(string prompt, CancellationToken cancellationToken = default)
        {
            try
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

                // Yanıt içindeki asıl içeriği çıkar
                var choices = document.RootElement.GetProperty("choices");
                var firstChoice = choices[0];
                var message = firstChoice.GetProperty("message");
                var content = message.GetProperty("content").GetString() ?? string.Empty;

                return content.Trim();
            }
            catch (Exception ex)
            {
                // Gerçek bir implementasyonda uygun hata yönetimi yapmalısınız
                Console.WriteLine($"Error calling OpenAI API: {ex.Message}");
                return "An error occurred while generating content.";
            }
        }
    }
}
