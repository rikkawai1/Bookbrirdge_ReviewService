﻿using Common.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using ReviewDomain.Entities;
using ReviewInfrastructure.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewInfrastructure.Repositories
{
    public class ReviewRepository : BaseRepository<Review, int>
    {
        public ReviewRepository(ReviewDBContext context) : base(context) { }
        public async Task<List<Review>> GetReviewByBookId(int bookId)
        {
            return await _dbSet.Where(r => r.BookId == bookId && r.IsActive).ToListAsync();
        }
        public async Task<Review> GetReviewById(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(r => r.ReviewId == id);

        }
        public async Task<bool> DeleteAsync(int id)
        {
            var exist = await _dbSet.FirstOrDefaultAsync(r => r.ReviewId == id);
            if (exist != null)
            {
                exist.IsActive = false;
                _dbSet.Update(exist);
                await _context.SaveChangesAsync();
            }
            return !exist.IsActive;
        }
    }
}
