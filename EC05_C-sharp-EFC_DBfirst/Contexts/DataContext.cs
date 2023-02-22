using System;
using System.Collections.Generic;
using EC05_C_sharp_EFC_DBfirst.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EC05_C_sharp_EFC_DBfirst.Contexts;

public partial class DataContext : DbContext
{
    public DataContext()
    {
    }

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Elias\\Downloads\\EC-utbildning-webbutvecklare-NET\\05-Datalagring\\EC05-Databases\\EC05_C-sharp-EFC_DBfirst\\EC05_C-sharp-EFC_DBfirst\\EC05_C-sharp-EFC_DBfirst\\Contexts\\sql_dbfirst.mdf;Integrated Security=True;Connect Timeout=30");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Addresse__3214EC074F45585C");

            entity.Property(e => e.PostalCode).IsFixedLength();
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC0766F85C50");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.PhoneNumber).IsFixedLength();

            entity.HasOne(d => d.Address).WithMany(p => p.Customers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Customers__Addre__3B75D760");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
