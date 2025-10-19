using Microsoft.EntityFrameworkCore;
using ReviewDomain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace ReviewInfrastructure.DBContext
{
    public class ReviewDBContext : DbContext
    {
        public ReviewDBContext(DbContextOptions<ReviewDBContext> options)
            : base(options)
        {
        }

        public DbSet<Review> Reviews { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Table name
            modelBuilder.Entity<Review>().ToTable("Reviews");

            // Default value for CreatedAt (UTC)
            modelBuilder.Entity<Review>()
                .Property(r => r.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // Soft delete filter: chỉ lấy review đang hoạt động
            modelBuilder.Entity<Review>()
                .HasQueryFilter(r => r.IsActive);

            // Các ràng buộc cơ bản
            modelBuilder.Entity<Review>(entity =>
            {
                entity.Property(r => r.Comment)
                      .IsRequired()
                      .HasMaxLength(1000);

                entity.Property(r => r.Rating)
                      .IsRequired()
                      .HasDefaultValue(1);

                entity.Property(r => r.UserId)
                      .IsRequired();

                entity.Property(r => r.BookId)
                      .IsRequired();

                entity.Property(r => r.StoreId)
                      .IsRequired();

                entity.Property(r => r.OrderId)
                      .IsRequired();

                entity.Property(r => r.IsActive)
                      .HasDefaultValue(false);
            });
        }

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
                    // Soft delete: set IsActive = false
                    entry.State = EntityState.Modified;
                    entry.Entity.IsActive = false;
                }
            }
        }
    }
}
