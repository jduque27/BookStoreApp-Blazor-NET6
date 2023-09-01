using System;
using System.Collections.Generic;
using System.Security.Policy;
using BookStoreApp.API.Models.Author;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStoreApp.API.Data;

public partial class BookStoreDbContext : IdentityDbContext<ApiUser>
{
    public BookStoreDbContext()
    {
    }

    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Authors__3214EC073228AA53");

            entity.Property(e => e.Bio).HasMaxLength(250);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
        });


        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Books__3214EC0763E984D8");

            entity.HasIndex(e => e.Isbn, "UQ__Books__447D36EA2A27901C").IsUnique();

            entity.Property(e => e.Image).HasMaxLength(50);
            entity.Property(e => e.Isbn)
                .HasMaxLength(50)
                .HasColumnName("ISBN");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Summary).HasMaxLength(250);
            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.Author).WithMany(p => p.Books)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK_Books_ToTable");
        });

        modelBuilder.Entity<IdentityRole>().HasData(

            new IdentityRole
            {
                Name = "User",
                NormalizedName = "USER",
                Id = "8343074e-8623-4e1a-b0c1-84fb8678c8f3"
            },
            new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "ADMIN",
                Id = "c7ac6cfe-1f10-4baf-b604-cde350db9554"
            }
        );

        var hasher = new PasswordHasher<ApiUser>();

        modelBuilder.Entity<ApiUser>().HasData(
               new ApiUser
               {
                   Id = "8e448afa-f008-446e-a52f-13c449803c2e",
                   Email = "admin@bookstore.com",
                   NormalizedEmail = "ADMIN@BOOKSTORE.COM",
                   UserName = "admin@bookstore.com",
                   NormalizedUserName = "ADMIN@BOOKSTORE.COM",
                   FirstName = "System",
                   LastName = "Admin",
                   PasswordHash = hasher.HashPassword(null, "P@ssword1")
               },
               new ApiUser
               {
                   Id = "30a24107-d279-4e37-96fd-01af5b38cb27",
                   Email = "user@bookstore.com",
                   NormalizedEmail = "USER@BOOKSTORE.COM",
                   UserName = "user@bookstore.com",
                   NormalizedUserName = "USER@BOOKSTORE.COM",
                   FirstName = "System",
                   LastName = "User",
                   PasswordHash = hasher.HashPassword(null, "P@ssword1")
               }
           );

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = "8343074e-8623-4e1a-b0c1-84fb8678c8f3",
                UserId = "30a24107-d279-4e37-96fd-01af5b38cb27"
            },
            new IdentityUserRole<string>
            {
                RoleId = "c7ac6cfe-1f10-4baf-b604-cde350db9554",
                UserId = "8e448afa-f008-446e-a52f-13c449803c2e"
            }
        );

        OnModelCreatingPartial(modelBuilder);
    }

    internal T Map<T>(AuthorCreateDto authorDto)
    {
        throw new NotImplementedException();
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
