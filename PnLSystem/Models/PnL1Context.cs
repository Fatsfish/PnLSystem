using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PnLSystem.Models;

public partial class PnL1Context : DbContext
{
    public PnL1Context()
    {
    }

    public PnL1Context(DbContextOptions<PnL1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<BrandGroup> BrandGroups { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Contract> Contracts { get; set; }

    public virtual DbSet<HelpRequest> HelpRequests { get; set; }

    public virtual DbSet<InputSheet> InputSheets { get; set; }

    public virtual DbSet<InputSheetExpense> InputSheetExpenses { get; set; }

    public virtual DbSet<InputSheetRevenue> InputSheetRevenues { get; set; }

    public virtual DbSet<PnLreport> PnLreports { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Ratio> Ratios { get; set; }

    public virtual DbSet<ReportExpense> ReportExpenses { get; set; }

    public virtual DbSet<ReportRevenue> ReportRevenues { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Stock> Stocks { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<StoreGroup> StoreGroups { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Brand__3214EC272BFC0560");

            entity.ToTable("Brand");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(2000);
            entity.Property(e => e.IsDelete).HasColumnName("isDelete");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
        });

        modelBuilder.Entity<BrandGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__BrandGro__3214EC27B9911F3B");

            entity.ToTable("BrandGroup");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IsAdmin).HasColumnName("isAdmin");

            entity.HasOne(d => d.Group).WithMany(p => p.BrandGroups)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("FK__BrandGrou__Group__2F10007B");

            entity.HasOne(d => d.User).WithMany(p => p.BrandGroups)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__BrandGrou__UserI__2E1BDC42");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3214EC27111157F4");

            entity.ToTable("Category");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(2000);
            entity.Property(e => e.IsDelete).HasColumnName("isDelete");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasOne(d => d.Brand).WithMany(p => p.Categories)
                .HasForeignKey(d => d.BrandId)
                .HasConstraintName("FK__Category__BrandI__3D5E1FD2");
        });

        modelBuilder.Entity<Contract>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Contract__3214EC27921770A1");

            entity.ToTable("Contract");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.ImageLink).HasMaxLength(300);
            entity.Property(e => e.IsActive).HasColumnName("isActive");

            entity.HasOne(d => d.Brand).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.BrandId)
                .HasConstraintName("FK__Contract__BrandI__398D8EEE");

            entity.HasOne(d => d.Store).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK__Contract__StoreI__3A81B327");

            entity.HasOne(d => d.User).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Contract__UserId__38996AB5");
        });

        modelBuilder.Entity<HelpRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__HelpRequ__3214EC27B75382B8");

            entity.ToTable("HelpRequest");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.CreationUserId).HasColumnName("CreationUserID");
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(2000);
            entity.Property(e => e.IsDelete).HasColumnName("isDelete");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(70);
            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasOne(d => d.CreationUser).WithMany(p => p.HelpRequests)
                .HasForeignKey(d => d.CreationUserId)
                .HasConstraintName("FK__HelpReque__Creat__49C3F6B7");
        });

        modelBuilder.Entity<InputSheet>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__InputShe__3214EC27702684A3");

            entity.ToTable("InputSheet");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.Brand).WithMany(p => p.InputSheets)
                .HasForeignKey(d => d.BrandId)
                .HasConstraintName("FK__InputShee__Brand__4D94879B");

            entity.HasOne(d => d.Store).WithMany(p => p.InputSheets)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK__InputShee__Store__4CA06362");
        });

        modelBuilder.Entity<InputSheetExpense>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__InputShe__3214EC27181EE7C8");

            entity.ToTable("InputSheetExpense");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(300);
            entity.Property(e => e.ImageLink).HasMaxLength(300);
            entity.Property(e => e.IsFinished).HasColumnName("isFinished");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasOne(d => d.Sheet).WithMany(p => p.InputSheetExpenses)
                .HasForeignKey(d => d.SheetId)
                .HasConstraintName("FK__InputShee__Sheet__5441852A");

            entity.HasOne(d => d.Stock).WithMany(p => p.InputSheetExpenses)
                .HasForeignKey(d => d.StockId)
                .HasConstraintName("FK__InputShee__Stock__5535A963");
        });

        modelBuilder.Entity<InputSheetRevenue>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__InputShe__3214EC275D7A5C7A");

            entity.ToTable("InputSheetRevenue");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(300);
            entity.Property(e => e.ImageLink).HasMaxLength(300);
            entity.Property(e => e.IsFinished).HasColumnName("isFinished");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasOne(d => d.Product).WithMany(p => p.InputSheetRevenues)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__InputShee__Produ__5165187F");

            entity.HasOne(d => d.Sheet).WithMany(p => p.InputSheetRevenues)
                .HasForeignKey(d => d.SheetId)
                .HasConstraintName("FK__InputShee__Sheet__5070F446");
        });

        modelBuilder.Entity<PnLreport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PnLRepor__3214EC27026F0C78");

            entity.ToTable("PnLReport");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");

            entity.HasOne(d => d.Brand).WithMany(p => p.PnLreports)
                .HasForeignKey(d => d.BrandId)
                .HasConstraintName("FK__PnLReport__Brand__59063A47");

            entity.HasOne(d => d.Store).WithMany(p => p.PnLreports)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK__PnLReport__Store__5812160E");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Product__3214EC274D7B678E");

            entity.ToTable("Product");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(2000);
            entity.Property(e => e.ImageLink).HasMaxLength(300);
            entity.Property(e => e.IsDelete).HasColumnName("isDelete");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Product__Categor__403A8C7D");
        });

        modelBuilder.Entity<Ratio>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ratio__3214EC27C09AFBDD");

            entity.ToTable("Ratio");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasColumnName("isActive");

            entity.HasOne(d => d.Product).WithMany(p => p.Ratios)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__Ratio__ProductId__45F365D3");

            entity.HasOne(d => d.Stock).WithMany(p => p.Ratios)
                .HasForeignKey(d => d.StockId)
                .HasConstraintName("FK__Ratio__StockId__46E78A0C");
        });

        modelBuilder.Entity<ReportExpense>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ReportEx__3214EC27B633683B");

            entity.ToTable("ReportExpense");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(300);
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasOne(d => d.Sheet).WithMany(p => p.ReportExpenses)
                .HasForeignKey(d => d.SheetId)
                .HasConstraintName("FK__ReportExp__Sheet__5EBF139D");
        });

        modelBuilder.Entity<ReportRevenue>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ReportRe__3214EC2719CBF305");

            entity.ToTable("ReportRevenue");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(300);
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasOne(d => d.Sheet).WithMany(p => p.ReportRevenues)
                .HasForeignKey(d => d.SheetId)
                .HasConstraintName("FK__ReportRev__Sheet__5BE2A6F2");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Role__3214EC279A29FDEB");

            entity.ToTable("Role");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(300);
            entity.Property(e => e.IsDelete).HasColumnName("isDelete");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Stock__3214EC2766424107");

            entity.ToTable("Stock");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(2000);
            entity.Property(e => e.ImageLink).HasMaxLength(300);
            entity.Property(e => e.IsDelete).HasColumnName("isDelete");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasOne(d => d.Store).WithMany(p => p.Stocks)
                .HasForeignKey(d => d.StoreId)
                .HasConstraintName("FK__Stock__StoreId__4316F928");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Store__3214EC2763D42669");

            entity.ToTable("Store");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreationDate).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(2000);
            entity.Property(e => e.IsDelete).HasColumnName("isDelete");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(70);
            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(100);

            entity.HasOne(d => d.Brand).WithMany(p => p.Stores)
                .HasForeignKey(d => d.BrandId)
                .HasConstraintName("FK__Store__BrandId__31EC6D26");
        });

        modelBuilder.Entity<StoreGroup>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__StoreGro__3214EC270C157142");

            entity.ToTable("StoreGroup");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IsAdmin).HasColumnName("isAdmin");

            entity.HasOne(d => d.Group).WithMany(p => p.StoreGroups)
                .HasForeignKey(d => d.GroupId)
                .HasConstraintName("FK__StoreGrou__Group__35BCFE0A");

            entity.HasOne(d => d.User).WithMany(p => p.StoreGroups)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__StoreGrou__UserI__34C8D9D1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC274D7C9F38");

            entity.ToTable("User");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Bio)
                .IsRequired()
                .HasMaxLength(300);
            entity.Property(e => e.DisplayName)
                .IsRequired()
                .HasMaxLength(140);
            entity.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(140);
            entity.Property(e => e.IsActive).HasColumnName("isActive");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserRole__3214EC2710DE5A00");

            entity.ToTable("UserRole");

            entity.Property(e => e.Id).HasColumnName("ID");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__UserRole__RoleId__29572725");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserRole__UserId__286302EC");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
