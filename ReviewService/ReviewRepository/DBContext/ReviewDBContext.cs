using Microsoft.EntityFrameworkCore;
using ReviewDomain.Entities;

namespace ReviewInfrastructure.DBContext
{
    public class ReviewDBContext : DbContext
    {
        public ReviewDBContext(DbContextOptions<ReviewDBContext> options)
            : base(options)
        {
        }

        public DbSet<Review> Reviews { get; set; } = default!;
        public DbSet<ReviewImage> ReviewImages { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // -----------------------------
            // 🔗 Cấu hình quan hệ 1 - Nhiều
            // -----------------------------
            modelBuilder.Entity<Review>()
                .HasMany(r => r.ReviewImages)
                .WithOne(ri => ri.Review)
                .HasForeignKey(ri => ri.ReviewId)
                .OnDelete(DeleteBehavior.Cascade);

            // -----------------------------
            // 🕒 Cấu hình thời gian tạo mặc định (UTC)
            // -----------------------------
            modelBuilder.Entity<Review>()
                .Property(r => r.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // -----------------------------
            // 🚫 Soft Delete Filter (chỉ lấy review chưa xóa)
            // -----------------------------
            modelBuilder.Entity<Review>()
                .HasQueryFilter(r => !r.IsActive);

            // -----------------------------
            // 🧱 Đặt tên bảng (nếu muốn)
            // -----------------------------
            modelBuilder.Entity<Review>().ToTable("Reviews");
            modelBuilder.Entity<ReviewImage>().ToTable("ReviewImages");
        }

        // -----------------------------
        // ⚙️ Gợi ý override SaveChangesAsync (nếu muốn xử lý soft delete logic)
        // -----------------------------
        public override int SaveChanges()
        {
            HandleSoftDelete();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            HandleSoftDelete();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void HandleSoftDelete()
        {
            foreach (var entry in ChangeTracker.Entries<Review>())
            {
                if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    entry.Entity.IsActive = true;
                }
            }
        }
    }
}
