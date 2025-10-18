using ReviewDomain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewApplication.Models
{
    public class ReviewDTO
    {
        public int ReviewId { get; set; }

        [Required]
        public string UserId { get; set; } = default!;

        [Range(1, 5)]
        public int Rating { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Comment { get; set; } = default!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<ReviewImage> Images { get; set; }
    }
    public class ReviewCreateRequest
    {
        public string UserId { get; set; } = default!;

        public int Rating { get; set; }

        public string Comment { get; set; } = default!;
        public List<ImageCreateRequest> Images { get; set; }
    }
    public class ReviewImageDTO
    {
        public int ReviewImageId { get; set; }

        public int ReviewId { get; set; }

        public string ImageUrl { get; set; } = default!;

    }
    public class ImageCreateRequest
    {
        public string ImageUrl { get; set; } = default!;

    }

}
