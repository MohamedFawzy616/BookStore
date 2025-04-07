using BookStoreTask.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStoreTask.Data
{
    public class BookDbContext : IdentityDbContext //DbContext,
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

            // Configure Book entity
            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired().HasMaxLength(200);
                //entity.Property(e => e.Author).IsRequired().HasMaxLength(100);
                entity.Property(e => e.ISBN).IsRequired().HasMaxLength(13);
                entity.Property(e => e.PublishedDate).IsRequired();
                //entity.Property(e => e.Description).HasMaxLength(2000);
                entity.Property(e => e.UserId).IsRequired();


                entity.HasIndex(b => b.AuthorId);
                //entity.HasIndex(b => b.AuthorId);
                entity.HasIndex(b => b.ISBN)
                .IsUnique();
            });

            //modelBuilder.Entity<Book>()
            //    .HasIndex(b => b.AuthorId);


            //modelBuilder.Entity<Book>()
            //    .HasIndex(b => b.AuthorId);


            //modelBuilder.Entity<Book>()
            //    .HasIndex(b => b.ISBN)
            //    .IsUnique();


            base.OnModelCreating(modelBuilder);
        }
    }
}