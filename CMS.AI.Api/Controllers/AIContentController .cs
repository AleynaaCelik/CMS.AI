// CMS.AI.Api/Controllers/AIContentController.cs
using CMS.AI.Application.Common.Interfaces;
using CMS.AI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CMS.AI.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AIContentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAIService _aiService;

        public AIContentController(IUnitOfWork unitOfWork, IAIService aiService)
        {
            _unitOfWork = unitOfWork;
            _aiService = aiService;
        }

        [HttpPost("generate")]
        public async Task<ActionResult<Content>> GenerateContent(GenerateContentRequest request, CancellationToken cancellationToken)
        {
            // AI servisi ile içerik oluştur
            var improvedContent = await _aiService.ImproveContentAsync(request.Prompt, cancellationToken);
            var seoTitle = await _aiService.GenerateSEOTitleAsync(improvedContent, cancellationToken);
            var keywords = await _aiService.GenerateKeywordsAsync(improvedContent, cancellationToken);

            // İçeriği kaydet
            var content = new Content(seoTitle, improvedContent, "admin");

            // Metadata ekle
            foreach (var keyword in keywords)
            {
                content.AddMetaData("en", "keyword", keyword, "admin");
            }

            // Veritabanına kaydet
            await _unitOfWork.Repository<Content>().AddAsync(content);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return CreatedAtRoute(nameof(GetGeneratedContent), new { id = content.Id }, content);
        }

        [HttpGet("{id}", Name = "GetGeneratedContent")]
        public async Task<ActionResult<Content>> GetGeneratedContent(Guid id, CancellationToken cancellationToken)
        {
            var content = await _unitOfWork.Repository<Content>().GetByIdAsync(id);

            if (content == null)
            {
                return NotFound();
            }

            return Ok(content);
        }

        [HttpPost("{id}/analyze")]
        public async Task<ActionResult<AIContentAnalysisResult>> AnalyzeContent(Guid id, CancellationToken cancellationToken)
        {
            var content = await _unitOfWork.Repository<Content>().GetByIdAsync(id);

            if (content == null)
            {
                return NotFound();
            }

            var analysisResult = await _aiService.AnalyzeContentAsync(content.Body, cancellationToken);

            return Ok(analysisResult);
        }

        [HttpPost("{id}/enhance")]
        public async Task<ActionResult<Content>> EnhanceContent(Guid id, CancellationToken cancellationToken)
        {
            var content = await _unitOfWork.Repository<Content>().GetByIdAsync(id);

            if (content == null)
            {
                return NotFound();
            }

            // İçeriği geliştir
            var improvedContent = await _aiService.ImproveContentAsync(content.Body, cancellationToken);
            var seoTitle = await _aiService.GenerateSEOTitleAsync(improvedContent, cancellationToken);
            var keywords = await _aiService.GenerateKeywordsAsync(improvedContent, cancellationToken);

            // İçeriği güncelle
            content.Update(seoTitle, improvedContent, "admin");

            // Metadata ekle
            foreach (var keyword in keywords)
            {
                content.AddMetaData("en", "keyword", keyword, "admin");
            }

            // Veritabanına kaydet
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Ok(content);
        }
    }

    public class GenerateContentRequest
    {
        public string Prompt { get; set; } = string.Empty;
    }
}