using Bookstore.Domain.Entities.AuthorEntities;
using Bookstore.Domain.Entities.AwardAuthorEntity;
using Bookstore.Domain.Entities.AwardEntities;
using Bookstore.Domain.Entities.BookEntities;
using Bookstore.Domain.Entities.ComicEntities;
using Bookstore.Domain.Entities.PublisherEntities;
using Bookstore.Domain.Entities.ReviewEntities;
using Bookstore.Domain.Entities.UserEntities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.Infrastructure.Persistence
{
    public class AppDbContext : IdentityDbContext<User, Role, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Award> Awards { get; set; }
        public DbSet<AwardAuthor> AwardAuthors { get; set; }
        public DbSet<ComicIssue> ComicIssues { get; set; }
        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = Guid.NewGuid(), Name = "Editor", NormalizedName = "EDITOR" },
                new Role { Id = Guid.NewGuid(), Name = "Librarian", NormalizedName = "LIBRARIAN" }
                );

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

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasOne(r => r.User)
                      .WithMany(u => u.Reviews)
                      .HasForeignKey(r => r.UserId)
                      .IsRequired();
            });

            modelBuilder.Entity<Author>().HasData(
        new Author { Id = 1, FullName = "Jane Austen", Biography = "English novelist", DateOfBirth = DateTime.SpecifyKind(new DateTime(1775, 12, 16), DateTimeKind.Utc) },
        new Author { Id = 2, FullName = "Mark Twain", Biography = "American writer", DateOfBirth = DateTime.SpecifyKind(new DateTime(1835, 11, 30), DateTimeKind.Utc) },
        new Author { Id = 3, FullName = "Virginia Woolf", Biography = "English modernist author", DateOfBirth = DateTime.SpecifyKind(new DateTime(1882, 1, 25), DateTimeKind.Utc) },
        new Author { Id = 4, FullName = "Ernest Hemingway", Biography = "American novelist", DateOfBirth = DateTime.SpecifyKind(new DateTime(1899, 7, 21), DateTimeKind.Utc) },
        new Author { Id = 5, FullName = "Agatha Christie", Biography = "English crime writer", DateOfBirth = DateTime.SpecifyKind(new DateTime(1890, 9, 15), DateTimeKind.Utc) }
    );

            // --- Seed Publishers ---
            modelBuilder.Entity<Publisher>().HasData(
                new Publisher { Id = 1, Name = "Penguin Books", Address = "80 Strand, London", Website = "https://penguin.co.uk" },
                new Publisher { Id = 2, Name = "HarperCollins", Address = "195 Broadway, New York", Website = "https://harpercollins.com" },
                new Publisher { Id = 3, Name = "Random House", Address = "1745 Broadway, New York", Website = "https://randomhouse.com" }
            );

            // --- Seed Books ---
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "Pride and Prejudice", PageCount = 432, PublishedDate = DateTime.SpecifyKind(new DateTime(1813, 1, 28), DateTimeKind.Utc), ISBN = "1111111111111", AuthorId = 1, PublisherId = 1 },
                new Book { Id = 2, Title = "Emma", PageCount = 384, PublishedDate = DateTime.SpecifyKind(new DateTime(1815, 12, 23), DateTimeKind.Utc), ISBN = "1111111111112", AuthorId = 1, PublisherId = 2 },
                new Book { Id = 3, Title = "Adventures of Huckleberry Finn", PageCount = 366, PublishedDate = DateTime.SpecifyKind(new DateTime(1884, 12, 10), DateTimeKind.Utc), ISBN = "2222222222221", AuthorId = 2, PublisherId = 2 },
                new Book { Id = 4, Title = "The Adventures of Tom Sawyer", PageCount = 274, PublishedDate = DateTime.SpecifyKind(new DateTime(1876, 6, 1), DateTimeKind.Utc), ISBN = "2222222222222", AuthorId = 2, PublisherId = 3 },
                new Book { Id = 5, Title = "Mrs Dalloway", PageCount = 296, PublishedDate = DateTime.SpecifyKind(new DateTime(1925, 5, 14), DateTimeKind.Utc), ISBN = "3333333333331", AuthorId = 3, PublisherId = 1 },
                new Book { Id = 6, Title = "To the Lighthouse", PageCount = 320, PublishedDate = DateTime.SpecifyKind(new DateTime(1927, 5, 5), DateTimeKind.Utc), ISBN = "3333333333332", AuthorId = 3, PublisherId = 3 },
                new Book { Id = 7, Title = "The Old Man and the Sea", PageCount = 127, PublishedDate = DateTime.SpecifyKind(new DateTime(1952, 9, 1), DateTimeKind.Utc), ISBN = "4444444444441", AuthorId = 4, PublisherId = 1 },
                new Book { Id = 8, Title = "A Farewell to Arms", PageCount = 355, PublishedDate = DateTime.SpecifyKind(new DateTime(1929, 9, 27), DateTimeKind.Utc), ISBN = "4444444444442", AuthorId = 4, PublisherId = 2 },
                new Book { Id = 9, Title = "Murder on the Orient Express", PageCount = 256, PublishedDate = DateTime.SpecifyKind(new DateTime(1934, 1, 1), DateTimeKind.Utc), ISBN = "5555555555551", AuthorId = 5, PublisherId = 1 },
                new Book { Id = 10, Title = "And Then There Were None", PageCount = 272, PublishedDate = DateTime.SpecifyKind(new DateTime(1939, 11, 6), DateTimeKind.Utc), ISBN = "5555555555552", AuthorId = 5, PublisherId = 3 },
                new Book { Id = 11, Title = "Persuasion", PageCount = 248, PublishedDate = DateTime.SpecifyKind(new DateTime(1817, 12, 20), DateTimeKind.Utc), ISBN = "1111111111113", AuthorId = 1, PublisherId = 3 },
                new Book { Id = 12, Title = "The Call of the Wild", PageCount = 232, PublishedDate = DateTime.SpecifyKind(new DateTime(1903, 4, 1), DateTimeKind.Utc), ISBN = "6666666666661", AuthorId = 2, PublisherId = 1 }
            );

            // --- Seed Awards ---
            modelBuilder.Entity<Award>().HasData(
                new Award { Id = 1, Name = "Best Novel", Description = "Award for best novel", YearBegan = 1950 },
                new Award { Id = 2, Name = "Lifetime Achievement", Description = "Award for lifetime contribution", YearBegan = 1960 },
                new Award { Id = 3, Name = "Debut Author", Description = "Award for first book", YearBegan = 1970 },
                new Award { Id = 4, Name = "Readers Choice", Description = "Voted by readers", YearBegan = 1980 }
            );

            // --- Seed AwardAuthor links using anonymous types ---
            modelBuilder.Entity<AwardAuthor>().HasData(
                new { AwardId = 1, AuthorId = 1, Year = 1951 },
                new { AwardId = 1, AuthorId = 2, Year = 1952 },
                new { AwardId = 1, AuthorId = 3, Year = 1953 },
                new { AwardId = 2, AuthorId = 1, Year = 1965 },
                new { AwardId = 2, AuthorId = 4, Year = 1970 },
                new { AwardId = 2, AuthorId = 5, Year = 1975 },
                new { AwardId = 3, AuthorId = 2, Year = 1971 },
                new { AwardId = 3, AuthorId = 3, Year = 1972 },
                new { AwardId = 3, AuthorId = 5, Year = 1973 },
                new { AwardId = 4, AuthorId = 1, Year = 1981 },
                new { AwardId = 4, AuthorId = 2, Year = 1982 },
                new { AwardId = 4, AuthorId = 3, Year = 1983 },
                new { AwardId = 4, AuthorId = 4, Year = 1984 },
                new { AwardId = 4, AuthorId = 5, Year = 1985 },
                new { AwardId = 1, AuthorId = 5, Year = 1955 }
            );
        }
    }
}
