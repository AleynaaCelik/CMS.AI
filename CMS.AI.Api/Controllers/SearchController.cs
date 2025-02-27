using CMS.AI.Application.Common.Interfaces;
using CMS.AI.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS.AI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService<Content> _searchService;
        private readonly IUnitOfWork _unitOfWork;

        public SearchController(ISearchService<Content> searchService, IUnitOfWork unitOfWork)
        {
            _searchService = searchService;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyCollection<Content>>> Search(
            [FromQuery] string query,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            CancellationToken cancellationToken = default)
        {
            var results = await _searchService.SearchAsync(query, page, pageSize, cancellationToken);
            return Ok(results);
        }

        [HttpPost("index")]
        public async Task<IActionResult> IndexAllContents(CancellationToken cancellationToken)
        {
            var contents = await _unitOfWork.Repository<Content>().GetAllAsync();

            foreach (var content in contents)
            {
                await _searchService.IndexDocumentAsync(content, cancellationToken);
            }

            return Ok(new { message = $"Successfully indexed {contents.Count} content items." });
        }

        [HttpPost("index/{id}")]
        public async Task<IActionResult> IndexContent(Guid id, CancellationToken cancellationToken)
        {
            var content = await _unitOfWork.Repository<Content>().GetByIdAsync(id);

            if (content == null)
            {
                return NotFound();
            }

            await _searchService.IndexDocumentAsync(content, cancellationToken);

            return Ok(new { message = "Content indexed successfully." });
        }
    }
}
