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

            //Award and Author link
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

            //Some new configurations and constraints
            modelBuilder.Entity<AwardAuthor>(entity =>
            {
                entity.ToTable("AuthorAwardBridge");
            });

            modelBuilder.Entity<Author>(entity =>
            {
                entity.Property(a => a.DateOfBirth)
                    .HasColumnName("Birthday");
            });


            //Rucno podesavanje kaskadnog brisanja, ali nije potrebno jer je podrazumevano
            /*modelBuilder.Entity<Author>()
                .HasMany(a => a.AwardAuthors)
                .WithOne(aa => aa.Author)
                .HasForeignKey(aa => aa.AuthorId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Award>()
                .HasMany(a => a.AwardAuthors)
                .WithOne(aa => aa.Award)
                .HasForeignKey(aa => aa.AwardId)
                .OnDelete(DeleteBehavior.Cascade);*/

            modelBuilder.Entity<Publisher>()
                .HasMany(p => p.Books)
                .WithOne(b => b.Publisher)
                .HasForeignKey(b => b.PublisherId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
