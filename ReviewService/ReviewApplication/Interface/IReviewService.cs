using Common.Paging;
using ReviewApplication.Models;
using ReviewDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewApplication.Interface
{
    public interface IReviewService
    {
        Task<Review> CreateReviewAsync(ReviewCreateDto dto);

        Task<PagedResult<Review>> GetReviewByBookId(int BookId, int PageNo, int PageSize);

        Task<Review?> GetReviewByIdAsync(int reviewId);

        Task<bool> DeleteReviewAsync(int id);
    }
}
