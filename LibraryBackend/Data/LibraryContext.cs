using LibraryBackend.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryBackend.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

        
        // Represent the Book table in the database
        public DbSet<Book> Books { get; set; } = null!;

        // Represent the User table in the database
        public DbSet<User> Users { get; set; } = null!;

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

              // Add unique constraints
              eb.HasIndex(b => b.Title).IsUnique();  // Title must be unique
              eb.HasIndex(b => b.ISBN).IsUnique();  // ISBN must be unique

            });

            // Configure the User entity
            modelBuilder.Entity<User>(eu =>
            {
                // Specify the primary key for the User entity
                eu.HasKey(u => u.Id);

                // Configure the Username and set maximum length
                eu.Property(u => u.Username)
                  .IsRequired()
                  .HasMaxLength(100);

                // Configure the Email and set maximum length
                eu.Property(u => u.Email)
                  .IsRequired()
                  .HasMaxLength(200);

                // Configure the PasswordHash as required
                eu.Property(u => u.PasswordHash)
                  .IsRequired();

                // Configure the FullName and set maximum length
                eu.Property(u => u.FullName)
                  .HasMaxLength(100);

                // Add unique constraints
                eu.HasIndex(u => u.Username).IsUnique(); // Username must be unique
                eu.HasIndex(u => u.Email).IsUnique(); // Email must be unique
            });

            // Call the base implementation
            base.OnModelCreating(modelBuilder);
        }
    }
}
