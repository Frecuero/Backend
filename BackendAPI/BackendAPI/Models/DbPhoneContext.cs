using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BackendAPI.Models;

public partial class DbPhoneContext : DbContext
{
    public DbPhoneContext()
    {
    }

    public DbPhoneContext(DbContextOptions<DbPhoneContext> options)
        : base(options)
    {
    }

    public virtual DbSet<PhoneBook> PhoneBooks { get; set; }

    public virtual DbSet<TypeContact> TypeContacts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PhoneBook>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("phone_book_pkey");

            entity.ToTable("phone_book");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Comments).HasColumnName("comments");
            entity.Property(e => e.ContactTypeId).HasColumnName("contact_type_id");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.Gender)
                .HasMaxLength(10)
                .HasColumnName("gender");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("phone_number");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasOne(d => d.ContactType).WithMany(p => p.PhoneBooks)
                .HasForeignKey(d => d.ContactTypeId)
                .HasConstraintName("phone_book_contact_type_id_fkey");
        });

        modelBuilder.Entity<TypeContact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("type_contact_pkey");

            entity.ToTable("type_contact");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
