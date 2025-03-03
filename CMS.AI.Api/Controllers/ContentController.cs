using CMS.AI.Application.Common.Interfaces;
using CMS.AI.Application.Contents.Commands.CreateContent;
using CMS.AI.Application.Contents.DTOs;
using CMS.AI.Application.Contents.Queries.GetContents;
using CMS.AI.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS.AI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICacheService _cacheService;

        public ContentController(IUnitOfWork unitOfWork, ICacheService cacheService)
        {
            _unitOfWork = unitOfWork;
            _cacheService = cacheService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Content>>> GetAll(CancellationToken cancellationToken)
        {
            var stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();

            // Önce cache'ten verileri almayı deniyoruz
            var contents = await _cacheService.GetOrCreateAsync(
                "all_contents",
                async () => {
                    // Cache miss durumunda veritabanından verileri getiriyoruz
                    Console.WriteLine("Cache miss - getting data from database");
                    return await _unitOfWork.Repository<Content>().GetAllAsync();
                },
                TimeSpan.FromMinutes(10),
                cancellationToken
            );

            stopwatch.Stop();
            Console.WriteLine($"GetAll operation completed in {stopwatch.ElapsedMilliseconds}ms");

            return Ok(contents);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Content>> GetById(Guid id, CancellationToken cancellationToken)
        {
            // Redis cache kullanarak içeriği getir
            var content = await _cacheService.GetOrCreateAsync(
                $"content_{id}",
                async () => await _unitOfWork.Repository<Content>().GetByIdAsync(id),
                TimeSpan.FromMinutes(10),
                cancellationToken);

            if (content == null)
            {
                return NotFound();
            }

            return Ok(content);
        }

        [HttpPost]
        public async Task<ActionResult<Content>> Create(CreateContentRequest request, CancellationToken cancellationToken)
        {
            var content = new Content(request.Title, request.Body, "admin");

            await _unitOfWork.Repository<Content>().AddAsync(content);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // Cache'i temizle
            await _cacheService.RemoveAsync("all_contents", cancellationToken);

            return CreatedAtAction(nameof(GetById), new { id = content.Id }, content);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateContentRequest request, CancellationToken cancellationToken)
        {
            var content = await _unitOfWork.Repository<Content>().GetByIdAsync(id);
            if (content == null)
            {
                return NotFound();
            }

            content.Update(request.Title, request.Body, "admin");

            await _unitOfWork.Repository<Content>().UpdateAsync(content);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // İlgili cache'leri temizle
            await _cacheService.RemoveAsync($"content_{id}", cancellationToken);
            await _cacheService.RemoveAsync("all_contents", cancellationToken);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var content = await _unitOfWork.Repository<Content>().GetByIdAsync(id);
            if (content == null)
            {
                return NotFound();
            }

            await _unitOfWork.Repository<Content>().DeleteAsync(content);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // İlgili cache'leri temizle
            await _cacheService.RemoveAsync($"content_{id}", cancellationToken);
            await _cacheService.RemoveAsync("all_contents", cancellationToken);

            return NoContent();
        }

        [HttpPost("{id}/metadata")]
        public async Task<IActionResult> AddMetadata(Guid id, AddMetadataRequest request, CancellationToken cancellationToken)
        {
            var content = await _unitOfWork.Repository<Content>().GetByIdAsync(id);
            if (content == null)
            {
                return NotFound();
            }

            content.AddMetaData(request.Language, request.Key, request.Value, "admin");

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            // İlgili cache'leri temizle
            await _cacheService.RemoveAsync($"content_{id}", cancellationToken);
            await _cacheService.RemoveAsync("all_contents", cancellationToken);

            return NoContent();
        }
    }

    public class CreateContentRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
    }

    public class UpdateContentRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
    }

    public class AddMetadataRequest
    {
        public string Language { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }
}


