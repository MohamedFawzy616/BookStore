using BookStoreTask.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStoreTask.Data
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
        {

        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=DESKTOP-MDNALHH\\SQLEXPRESS01;Initial Catalog=BookStoreDb;Integrated Security=True;TrustServerCertificate=true;Encrypt=True;");

        //    base.OnConfiguring(optionsBuilder);
        //}


        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Author> Authors { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .HasMany(a => a.Books)
                .WithOne(b => b.Author)
                .HasForeignKey(b => b.AuthorId);


            modelBuilder.Entity<Book>()
                .HasIndex(b => b.AuthorId);


            modelBuilder.Entity<Book>()
                .HasIndex(b => b.AuthorId);


            modelBuilder.Entity<Book>()
                .HasIndex(b => b.ISBN)
                .IsUnique();


            base.OnModelCreating(modelBuilder);
        }
    }
}