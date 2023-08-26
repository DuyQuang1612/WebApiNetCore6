using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MyApiNetCore6.Data
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions options) : base(options)
        {
        }
        #region
        public DbSet<NguoiDung> nguoiDungs { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<HangHoa> HangHoas { get; set; }
        public DbSet<Loai> Loais { get; set; }
        public DbSet<DonHang> DonHangs { get; set; }
        public DbSet<DonHangChiTiet> DonHangChiTiets { get; set; }
        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //DonHang
            modelBuilder.Entity<DonHang>(e =>
            {
                e.ToTable("DonHang");
                e.HasKey(dh => dh.MaDh);
                e.Property(dh => dh.NgayDat).HasDefaultValueSql("getutcdate()");
                e.Property(dh => dh.NguoiNhan).IsRequired().HasMaxLength(100);  
            });

            //DonHangChiTiet
            modelBuilder.Entity<DonHangChiTiet>(entity =>
            {
                entity.ToTable("DonHangChiTiet");
                entity.HasKey(e => new {e.MaDh,e.MaHh});
                entity.HasOne(e => e.DonHang)
                      .WithMany(e => e.DonHangChiTiets)
                      .HasForeignKey(e => e.MaDh)
                      .HasConstraintName("FK_DonHangCT_DonHang");

                entity.HasOne(e => e.HangHoa)
                      .WithMany(e => e.DonHangChiTiets)
                      .HasForeignKey(e => e.MaHh)
                      .HasConstraintName("FK_DonHangCT_HangHoa");
            });

            //NguoiDUNG
            modelBuilder.Entity<NguoiDung>(entity =>
            {
                entity.HasIndex(e => e.UserName).IsUnique();
                entity.Property(e => e.HoTen).IsRequired().HasMaxLength(150);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(150);
            });
        }
    }
   
}
