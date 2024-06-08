using Microsoft.EntityFrameworkCore;
using Practical_work.Models;

namespace Practical_work;

public partial class EnglishstoreContext : DbContext
{
    public EnglishstoreContext()
    {
    }

    public EnglishstoreContext(DbContextOptions<EnglishstoreContext> options)
        : base(options)
    { }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=englishstore;Username=postgres;Password=qwe123qwe123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_manufacturer");

            entity.ToTable("manufacturers");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Startworkdate).HasColumnName("startworkdate");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_product");

            entity.ToTable("products");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Image)
                .HasMaxLength(255)
                .HasColumnName("image");
            entity.Property(e => e.Isactive).HasColumnName("isactive");
            entity.Property(e => e.Manufacturerid).HasColumnName("manufacturerid");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Price).HasColumnName("price");

            entity.HasOne(d => d.Manufacturer).WithMany(p => p.Products)
                .HasForeignKey(d => d.Manufacturerid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_product_manufacturer");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_sale");

            entity.ToTable("sales");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Count).HasColumnName("count");
            entity.Property(e => e.Productid).HasColumnName("productid");
            entity.Property(e => e.Saletime)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("saletime");

            entity.HasOne(d => d.Product).WithMany(p => p.Sales)
                .HasForeignKey(d => d.Productid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_sale_product");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pk_user");

            entity.ToTable("users");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Login)
                .HasMaxLength(255)
                .HasColumnName("login");
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .HasColumnName("password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
