using Microsoft.EntityFrameworkCore;

namespace NetCoreStudy3_DTO.Models
{
    public class WebContext : DbContext
    {
        public WebContext(DbContextOptions<WebContext> options) : base(options)
        {
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<MaterialType> MaterialType { get; set; }
        public DbSet<BOM> BOM { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                //entity.Property(e => e.Pro_Id).HasDefaultValueSql("(newid())");
                entity.Property(e => e.Pro_Level).HasMaxLength(2);  //裝備等級16進制顯示
                entity.Property(e => e.Pro_Id).HasMaxLength(4);  //裝備編碼4碼 等級(16進制)+職業類別+部位
            });
        }
    }
}
