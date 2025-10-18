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

            // Default value for CreatedAt (UTC)
            modelBuilder.Entity<Review>()
                .Property(r => r.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            // Soft delete filter
            modelBuilder.Entity<Review>()
                .HasQueryFilter(r => !r.IsActive);

            // Table name
            modelBuilder.Entity<Review>().ToTable("Reviews");
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
                    entry.State = EntityState.Modified;
                    entry.Entity.IsActive = true;
                }
            }
        }
    }
}
