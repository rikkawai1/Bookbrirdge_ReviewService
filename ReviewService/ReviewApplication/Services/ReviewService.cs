using AutoMapper;
using Common.Paging;
using Microsoft.EntityFrameworkCore;
using ReviewApplication.Interface;
using ReviewApplication.Models;
using ReviewDomain.Entities;
using ReviewInfrastructure.Repositories;

namespace ReviewApplication.Services
{
    public class ReviewService : IReviewService
    {
        private readonly ReviewRepository _repo;
        private readonly IMapper _mapper;


        public ReviewService(ReviewRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<Review> CreateReviewAsync(ReviewCreateDto dto)
        {
            var entity = _mapper.Map<Review>(dto);
            entity.CreatedAt = DateTime.UtcNow;
            entity.IsActive = true;
            return await _repo.CreateAsync(entity);
        }

        public async Task<PagedResult<Review>> GetReviewByBookId(int BookId, int PageNo, int PageSize)
        {
            var rL = await _repo.GetReviewByBookId(BookId);
            var rLPaging = PagedResult<Review>.Create(rL, PageNo, PageSize);
            return rLPaging;
        }

        // 🔵 Lấy review theo ID
        public async Task<Review?> GetReviewByIdAsync(int reviewId)
        {
            return await _repo.GetReviewById(reviewId);
        }

        public async Task<bool> DeleteReviewAsync(int id)
        {
            var exist = await _repo.GetByIdAsync(id);
            if (exist != null)
            {
                return await _repo.DeleteAsync(id);
            }
            return false;
        }
    }
}
