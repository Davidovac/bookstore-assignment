using BookstoreApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Award> Awards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AwardAuthor>()
                .HasKey(aa => new { aa.AwardId, aa.AuthorId });

            modelBuilder.Entity<AwardAuthor>()
                .HasOne(aa => aa.Award)
                .WithMany(a => a.AwardAuthors)
                .HasForeignKey(aa => aa.AwardId);

            modelBuilder.Entity<AwardAuthor>()
                .HasOne(aa => aa.Author)
                .WithMany(a => a.AwardAuthors)
                .HasForeignKey(aa => aa.AuthorId);
        }
    }
}
