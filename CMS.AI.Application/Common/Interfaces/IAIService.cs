using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.AI.Application.Common.Interfaces
{
    public interface IAIService
    {
        Task<AIContentAnalysisResult> AnalyzeContentAsync(string content, CancellationToken cancellationToken = default);
        Task<string> GenerateSEOTitleAsync(string content, CancellationToken cancellationToken = default);
        Task<string> GenerateSEODescriptionAsync(string content, CancellationToken cancellationToken = default);
        Task<List<string>> GenerateKeywordsAsync(string content, CancellationToken cancellationToken = default);
        Task<string> ImproveContentAsync(string content, CancellationToken cancellationToken = default);
        Task<string> GenerateImagePromptAsync(string content, CancellationToken cancellationToken = default);
    }

    public class AIContentAnalysisResult
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<string> Keywords { get; set; } = new List<string>();
        public string ImprovedContent { get; set; } = string.Empty;
        public string ImagePrompt { get; set; } = string.Empty;
        public double SentimentScore { get; set; }
        public string Summary { get; set; } = string.Empty;
    }
}
