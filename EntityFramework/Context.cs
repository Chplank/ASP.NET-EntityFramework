using EntityFramework.Enities;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework
{
    public class Context : DbContext
    {
        public DbSet<Author> Author { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<BookLoan> BookLoan { get; set; }
        public DbSet<LibraryMember> LibraryMember { get; set; }
        public string DbPath { get; }

        public Context()
        {
            var path = AppContext.BaseDirectory;
            DbPath = System.IO.Path.Join(path, "Library.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .HasIndex(b => b.ISBN)
                .IsUnique(); 

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId); 

            modelBuilder.Entity<Book>()
                .Property(b => b.Title)
                .IsRequired()
                .HasMaxLength(255); 

            modelBuilder.Entity<Book>()
                .Property(b => b.PublicationYear)
                .IsRequired();

            modelBuilder.Entity<Author>()
                .Property(a => a.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Author>()
                .Property(a => a.LastName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<LibraryMember>()
                .HasIndex(lm => lm.Email)
                .IsUnique();

            modelBuilder.Entity<LibraryMember>()
                .Property(lm => lm.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<LibraryMember>()
                .Property(lm => lm.LastName)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<LibraryMember>()
                .Property(lm => lm.Email)
                .IsRequired();

            modelBuilder.Entity<LibraryMember>()
                .Property(lm => lm.PhoneNumber)
                .HasMaxLength(15);

            modelBuilder.Entity<BookLoan>()
                .HasOne(bl => bl.Book)
                .WithMany(b => b.BookLoans)
                .HasForeignKey(bl => bl.BookId);

            modelBuilder.Entity<BookLoan>()
                .HasOne(bl => bl.LibraryMember)
                .WithMany(lm => lm.BookLoans)
                .HasForeignKey(bl => bl.LibraryMemberId);

            modelBuilder.Entity<BookLoan>()
                .Property(bl => bl.LoanDate)
                .IsRequired();

            modelBuilder.Entity<BookLoan>()
                .Property(bl => bl.ReturnDate)
                .IsRequired(false);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }
    }
}
