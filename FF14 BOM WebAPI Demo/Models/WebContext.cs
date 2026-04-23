using Microsoft.EntityFrameworkCore;

namespace FF14BOM.Models
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
            //裝備 欄位設定
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Pro_Id);  //設定Pro_Id為主鍵

                entity.Property(e => e.Pro_Level).HasMaxLength(2);  //裝備等級16進制顯示
                entity.Property(e => e.Pro_Type).HasMaxLength(1);   //裝備職業別 1巧匠2大地
                entity.Property(e => e.Pro_part).HasMaxLength(1);   //裝備部位  頭1身體2身體3手4褲5鞋6
                entity.Property(e => e.Pro_Name).HasMaxLength(20);  //裝備名稱20字以內
                entity.Property(e => e.Pro_Id).HasMaxLength(4);  //裝備編碼4碼 等級(16進制)+職業類別+部位
            });
            //材料 欄位設定
            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(e => e.Mtr_id);  //設定Mtr_id為主鍵

                entity.Property(e => e.Mtr_id).HasMaxLength(5);
                entity.Property(e => e.Mtr_type).HasMaxLength(2);
                entity.Property(e => e.Mtr_Name).HasMaxLength(20);   
            });
            //BOM欄位設定
            modelBuilder.Entity<BOM>(entity =>
            {
                //設定Pro_Id、Mtr_Id為複合主鍵
                entity.HasKey(e => new { e.Pro_Id,e.Mtr_id});  

                entity.Property(e => e.Pro_Id).HasMaxLength(4);
                entity.Property(e => e.Mtr_id).HasMaxLength(5);
            });
            //材料類別欄位設定
            modelBuilder.Entity<MaterialType>(entity =>
            {
                entity.HasKey(e => e.Mtr_type); //設定Mtr_type為主鍵

                entity.Property(e => e.Mtr_type).HasMaxLength(2);
                entity.Property(e => e.Mtr_Type_name).HasMaxLength(20);
            });

        }
    }
}
