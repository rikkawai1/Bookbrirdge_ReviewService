using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReviewDomain.Entities
{
    public class ReviewImage
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReviewImageId { get; set; }

        [Required]
        [ForeignKey(nameof(Review))]
        public int ReviewId { get; set; }

        [Required]
        public string ImageUrl { get; set; } = default!;

        // Navigation property
        public Review Review { get; set; } = default!;
    }
}
