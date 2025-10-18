using Common.Infrastructure.Repositories;
using ReviewDomain.Entities;
using ReviewInfrastructure.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewInfrastructure.Repositories
{
    public class ReviewImageRepository : BaseRepository<ReviewImage, int>
    {
        public ReviewImageRepository(ReviewDBContext context) : base(context) { }

    }
}
