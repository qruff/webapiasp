using Microsoft.EntityFrameworkCore;

namespace webapiasp.Models
{
    public partial class TravelContext : DbContext
    {
        public TravelContext()
        {
        }

        public TravelContext(DbContextOptions<TravelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cart> Carts { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Exploiter> Exploiters { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<Tour> Tours { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=travel;User Id=postgres;Password=root;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("cart_pkey");

                entity.ToTable("cart");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.ExploiterId).HasColumnName("exploiter_id");
                entity.Property(e => e.Quantity).HasColumnName("quantity");
                entity.Property(e => e.TourId).HasColumnName("tour_id");

                entity.HasOne(d => d.Exploiter).WithMany(p => p.Carts)
                    .HasForeignKey(d => d.ExploiterId)
                    .HasConstraintName("fk7dfk9l6vaa0hfodl7kgy65ld7");

                entity.HasOne(d => d.Tour).WithMany(p => p.Carts)
                    .HasForeignKey(d => d.TourId)
                    .HasConstraintName("fkqab46hbr6hs5m6yh9i8yde74h");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("category_pkey");

                entity.ToTable("category");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<Exploiter>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("exploiter_pkey");

                entity.ToTable("exploiter");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .HasColumnName("email");
                entity.Property(e => e.Firstname)
                    .HasMaxLength(255)
                    .HasColumnName("firstname");
                entity.Property(e => e.Lastname)
                    .HasMaxLength(255)
                    .HasColumnName("lastname");
                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .HasColumnName("password");
                entity.Property(e => e.Phone)
                    .HasMaxLength(255)
                    .HasColumnName("phone");
                entity.Property(e => e.Role)
                    .HasMaxLength(255)
                    .HasColumnName("role");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("orders_pkey");

                entity.ToTable("orders");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.ExploiterId).HasColumnName("exploiter_id");
                entity.Property(e => e.GuideAssigned)
                    .HasMaxLength(255)
                    .HasColumnName("guide_assigned");
                entity.Property(e => e.GuideDate)
                    .HasMaxLength(255)
                    .HasColumnName("guide_date");
                entity.Property(e => e.GuidePersonId).HasColumnName("guide_person_id");
                entity.Property(e => e.GuideStatus)
                    .HasMaxLength(255)
                    .HasColumnName("guide_status");
                entity.Property(e => e.GuideTime)
                    .HasMaxLength(255)
                    .HasColumnName("guide_time");
                entity.Property(e => e.OrderDate)
                    .HasMaxLength(255)
                    .HasColumnName("order_date");
                entity.Property(e => e.OrderId)
                    .HasMaxLength(255)
                    .HasColumnName("order_id");
                entity.Property(e => e.Quantity).HasColumnName("quantity");
                entity.Property(e => e.TourId).HasColumnName("tour_id");

                entity.HasOne(d => d.Exploiter).WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ExploiterId)
                    .HasConstraintName("fks2exl8i37og258lnlo9g59jg3");

                entity.HasOne(d => d.Tour).WithMany(p => p.Orders)
                    .HasForeignKey(d => d.TourId)
                    .HasConstraintName("fkm8pv8kwjvs1ggdpv9uq8lrnmm");
            });

            modelBuilder.Entity<Tour>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("tour_pkey");

                entity.ToTable("tour");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.CategoryId).HasColumnName("category_id");
                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");
                entity.Property(e => e.ImageName)
                    .HasMaxLength(255)
                    .HasColumnName("image_name");
                entity.Property(e => e.Price)
                    .HasPrecision(38, 2)
                    .HasColumnName("price");
                entity.Property(e => e.Quantity).HasColumnName("quantity");
                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .HasColumnName("title");

                entity.HasOne(d => d.Category).WithMany(p => p.Tours)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("fkfi3if9cr2rk7llxvky6boui8h");
            });

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

}
