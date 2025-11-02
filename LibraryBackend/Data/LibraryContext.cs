using LibraryBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryBackend.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

        
        // Represent the Book table in the database
        public DbSet<Book> Books { get; set; } = null!;

        // Configure the EF Core model and entity relationships
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // configure the Book entity
            modelBuilder.Entity<Book>(eb =>
            {
                // specify the primary key for the Book entity
                eb.HasKey(b => b.Id);

                // Configure the Title and set maximum length
                eb.Property(b => b.Title)
                  .IsRequired()
                  .HasMaxLength(200);

                // Configure the Author and set maximum length
                eb.Property(b => b.Author)
                  .IsRequired()
                  .HasMaxLength(200);

                // Configure the ISBN and set maximum length 
                eb.Property(b => b.ISBN)
                  .HasMaxLength(50);
            });

            // Call the base implementation
            base.OnModelCreating(modelBuilder);
        }
    }
}
