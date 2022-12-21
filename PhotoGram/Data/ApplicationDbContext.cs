using Microsoft.EntityFrameworkCore;
using PhotoGram.Models;

namespace PhotoGram.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
            
        }
        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<Account>()
                .HasMany(a => a.LikedComments)
                .WithMany(c => c.Likes);
            model.Entity<Account>()
                .HasMany(a => a.Followers)
                .WithMany(a => a.Following);
            model.Entity<Account>()
                .HasMany(a => a.LikedPosts)
                .WithMany(p => p.Likes);
            model.Entity<Account>()
                .HasMany( a => a.Posts)
                .WithOne(p => p.account);
            model.Entity<Account>()
                .HasMany(a => a.Comments)
                .WithOne(c => c.account);
            model.Entity<Account>()
                .HasIndex(a => a.ScreenName)
                .IsUnique();
            base.OnModelCreating(model);

        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }
    }
}
