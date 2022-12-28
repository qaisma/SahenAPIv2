using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SahenDBv2.DB
{
    public partial class SahenDBContext : DbContext
    {
        public SahenDBContext()
        {
        }

        public SahenDBContext(DbContextOptions<SahenDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<EmployeeType> EmployeeTypes { get; set; } = null!;
        public virtual DbSet<Manager> Managers { get; set; } = null!;
        public virtual DbSet<Menu> Menus { get; set; } = null!;
        public virtual DbSet<MenuItem> MenuItems { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrdersMenuItem> OrdersMenuItems { get; set; } = null!;
        public virtual DbSet<OrdersStatus> OrdersStatuses { get; set; } = null!;
        public virtual DbSet<Restaurant> Restaurants { get; set; } = null!;
        public virtual DbSet<RestaurantTable> RestaurantTables { get; set; } = null!;
        public virtual DbSet<Visit> Visits { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=QTHINKPAD;Initial Catalog=SahenDB;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.IsAdmin).HasColumnName("isAdmin");

                entity.Property(e => e.Name).HasMaxLength(150);

                entity.Property(e => e.Password).HasMaxLength(150);

                entity.Property(e => e.RestaurantId).HasColumnName("RestaurantID");

                entity.Property(e => e.Username).HasMaxLength(150);

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.RestaurantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employees_Restaurants");

                entity.HasOne(d => d.UserTypeNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.UserType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employees_EmployeeTypes");
            });

            modelBuilder.Entity<EmployeeType>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Manager>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.Property(e => e.Password).HasMaxLength(350);

                entity.Property(e => e.Username).HasMaxLength(150);
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.IsFeatured).HasColumnName("isFeatured");

                entity.Property(e => e.Name).HasMaxLength(150);

                entity.Property(e => e.RestaurantId).HasColumnName("RestaurantID");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.Menus)
                    .HasForeignKey(d => d.RestaurantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Menus_Restaurants");
            });

            modelBuilder.Entity<MenuItem>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.IsEnabled)
                    .IsRequired()
                    .HasColumnName("isEnabled")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.MenuId).HasColumnName("MenuID");

                entity.Property(e => e.Name).HasMaxLength(150);

                entity.Property(e => e.Price).HasColumnType("smallmoney");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.MenuItems)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MenuItems_Menus");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.VisitId).HasColumnName("VisitID");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.Status)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_OrdersStatuses");

                entity.HasOne(d => d.Visit)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.VisitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_Visits");
            });

            modelBuilder.Entity<OrdersMenuItem>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.MenuItemId });

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.MenuItemId).HasColumnName("MenuItemID");

                entity.Property(e => e.Notes).HasMaxLength(500);

                entity.HasOne(d => d.MenuItem)
                    .WithMany(p => p.OrdersMenuItems)
                    .HasForeignKey(d => d.MenuItemId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrdersMenuItems_MenuItems");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrdersMenuItems)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrdersMenuItems_Orders");
            });

            modelBuilder.Entity<OrdersStatus>(entity =>
            {
                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("ID");

                entity.Property(e => e.StatusName).HasMaxLength(50);
            });

            modelBuilder.Entity<Restaurant>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CurrencyCode).HasMaxLength(50);

                entity.Property(e => e.ManagerId).HasColumnName("ManagerID");

                entity.Property(e => e.Name).HasMaxLength(150);

                entity.Property(e => e.Tax).HasColumnType("smallmoney");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.Restaurants)
                    .HasForeignKey(d => d.ManagerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Restaurants_Managers");
            });

            modelBuilder.Entity<RestaurantTable>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.IsReserved).HasColumnName("isReserved");

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.Property(e => e.QrCode).HasColumnName("QR_Code");

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.RestaurantTables)
                    .HasForeignKey(d => d.RestaurantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RestaurantTables_Restaurants");
            });

            modelBuilder.Entity<Visit>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CustomerName).HasMaxLength(150);

                entity.Property(e => e.CustomerPhone).HasMaxLength(150);

                entity.Property(e => e.EndedAt).HasColumnType("datetime");

                entity.Property(e => e.TableId).HasColumnName("TableID");

                entity.HasOne(d => d.Table)
                    .WithMany(p => p.Visits)
                    .HasForeignKey(d => d.TableId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Visits_RestaurantTables");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
