using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using WebAppBlazor.Models;

namespace API_NoiThat.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

        public DbSet<Account> Account { get; set; }
        public DbSet<Bill> Bill { get; set; }
        public DbSet<BillDetail> BillDetail { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<CategoryType> CategoryType { get; set; }
        public DbSet<Favourite> Favourite { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Voucher> Voucher { get; set; }
        public DbSet<ProductImage> ProductImage { get; set; }
        public DbSet<Reviews> Reviews { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Shipping> Shipping { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>().HasKey(h => h.ID);
            modelBuilder.Entity<Bill>().HasKey(h => h.ID);
            modelBuilder.Entity<BillDetail>().HasKey(h => h.ID);
            modelBuilder.Entity<Category>().HasKey(h => h.ID);
            modelBuilder.Entity<CategoryType>().HasKey(h => h.ID);
            modelBuilder.Entity<Favourite>().HasKey(h => h.ID);
            modelBuilder.Entity<Payment>().HasKey(h => h.ID);
            modelBuilder.Entity<Product>().HasKey(h => h.ID);
            modelBuilder.Entity<ProductImage>().HasKey(h => h.ID);
            modelBuilder.Entity<Reviews>().HasKey(h => h.ID);
            modelBuilder.Entity<Role>().HasKey(h => h.ID);
            modelBuilder.Entity<Shipping>().HasKey(h => h.ID);
            modelBuilder.Entity<Voucher>().HasKey(h => h.ID);

            // Quan he 1 - n Role - Account
            modelBuilder.Entity<Account>()
                .HasOne<Role>(h => h.Role)
                .WithMany(i => i.Account)
                .HasForeignKey(e => e.IDRole);

            // Quan he 1 - n Account - Bill
            modelBuilder.Entity<Bill>()
                .HasOne<Account>(h => h.Account)
                .WithMany(i => i.Bill)
                .HasForeignKey(e => e.IDNguoiDung);

            // Quan he 1 - n Account - Favourite
            modelBuilder.Entity<Favourite>()
                .HasOne<Account>(h => h.Account)
                .WithMany(i => i.Favourite)
                .HasForeignKey(e => e.IDNguoiDung);

            // Quan he 1 - n Account - Reviews
            modelBuilder.Entity<Reviews>()
                .HasOne<Account>(h => h.Account)
                .WithMany(i => i.Reviews)
                .HasForeignKey(e => e.IDTaiKhoan);

            // Quan he 1 - n Payment - Bill
            modelBuilder.Entity<Bill>()
                .HasOne<Payment>(h => h.Payment)
                .WithMany(i => i.Bill)
                .HasForeignKey(e => e.IDThanhToan);

            // Quan he 1 - n Shipping - Bill
            modelBuilder.Entity<Bill>()
                .HasOne<Shipping>(h => h.Shipping)
                .WithMany(i => i.Bill)
                .HasForeignKey(e => e.IDVanChuyen);

            // Quan he 1 - n Bill - BillDetail
            modelBuilder.Entity<BillDetail>()
                .HasOne<Bill>(h => h.Bill)
                .WithMany(i => i.BillDetail)
                .HasForeignKey(e => e.IDHoaDon);

            // Quan he 1 - n Product - BillDetail
            modelBuilder.Entity<BillDetail>()
                .HasOne<Product>(h => h.Product)
                .WithMany(i => i.BillDetail)
                .HasForeignKey(e => e.IDSanPham);

            // Quan he 1 - n Product - ProductImage
            modelBuilder.Entity<ProductImage>()
                .HasOne<Product>(h => h.Product)
                .WithMany(i => i.ProductImage)
                .HasForeignKey(e => e.IDSanPham);

            // Quan he 1 - n Product - Reviews
            modelBuilder.Entity<Reviews>()
                .HasOne<Product>(h => h.Product)
                .WithMany(i => i.Reviews)
                .HasForeignKey(e => e.IDSanPham);

            // Quan he 1 - n Product - Favourite
            modelBuilder.Entity<Favourite>()
                .HasOne<Product>(h => h.Product)
                .WithMany(i => i.Favourite)
                .HasForeignKey(e => e.IDSanPham);

            // Quan he 1 - n Product - Voucher
            modelBuilder.Entity<Voucher>()
                .HasOne<Product>(h => h.Product)
                .WithMany(i => i.Voucher)
                .HasForeignKey(e => e.IDSanPham)
                .OnDelete(DeleteBehavior.Restrict);

            // Quan he 1 - n CagoryType - Product
            modelBuilder.Entity<Product>()
                .HasOne<CategoryType>(h => h.CategoryType)
                .WithMany(i => i.Product)
                .HasForeignKey(e => e.IDLoaiDM);

            // Quan he 1 - n Category - CagoryType
            modelBuilder.Entity<CategoryType>()
                .HasOne<Category>(h => h.Category)
                .WithMany(i => i.CategoryType)
                .HasForeignKey(e => e.IDDanhMuc);

            // Quan he 1 - n Category - Voucher
            modelBuilder.Entity<Voucher>()
                .HasOne<Category>(h => h.Category)
                .WithMany(i => i.Voucher)
                .HasForeignKey(e => e.IDDanhMuc);

        }
    }
}
