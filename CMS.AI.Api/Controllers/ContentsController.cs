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
    public class ContentsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContentsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Content>>> GetAll()
        {
            var contents = await _unitOfWork.Repository<Content>().GetAllAsync();
            return Ok(contents);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Content>> GetById(Guid id)
        {
            var content = await _unitOfWork.Repository<Content>().GetByIdAsync(id);
            if (content == null)
            {
                return NotFound();
            }
            return Ok(content);
        }

        [HttpPost]
        public async Task<ActionResult<Content>> Create(CreateContentRequest request)
        {
            var content = new Content(request.Title, request.Body, "admin");

            await _unitOfWork.Repository<Content>().AddAsync(content);
            await _unitOfWork.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = content.Id }, content);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateContentRequest request)
        {
            var content = await _unitOfWork.Repository<Content>().GetByIdAsync(id);
            if (content == null)
            {
                return NotFound();
            }

            content.Update(request.Title, request.Body, "admin");

            await _unitOfWork.Repository<Content>().UpdateAsync(content);
            await _unitOfWork.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var content = await _unitOfWork.Repository<Content>().GetByIdAsync(id);
            if (content == null)
            {
                return NotFound();
            }

            await _unitOfWork.Repository<Content>().DeleteAsync(content);
            await _unitOfWork.SaveChangesAsync();

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
}


