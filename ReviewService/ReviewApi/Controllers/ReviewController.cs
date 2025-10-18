using Microsoft.AspNetCore.Mvc;
using ReviewApplication.Interface;
using ReviewApplication.Models;
using ReviewDomain.Entities;
using Common.Paging;

namespace ReviewApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _service;

        public ReviewController(IReviewService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview([FromBody] ReviewCreateRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _service.CreateReviewAsync(request);
            return CreatedAtAction(nameof(GetReviewById), new { id = created.ReviewId }, created);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllReviews([FromQuery] int pageNo = 1, [FromQuery] int pageSize = 10)
        {
            PagedResult<Review> result = await _service.GetAllReviewsAsync(pageNo, pageSize);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReviewById(int id)
        {
            var review = await _service.GetReviewByIdAsync(id);
            if (review == null)
                return NotFound();

            return Ok(review);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            bool deleted = await _service.DeleteReviewAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
