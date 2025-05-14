using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DAL.models
{
    public partial class FindYourWayContext : DbContext
    {
        public FindYourWayContext()
        {
        }

        public FindYourWayContext(DbContextOptions<FindYourWayContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PlacesTable> PlacesTables { get; set; }
        public virtual DbSet<ProviderTable> ProviderTables { get; set; }
        public virtual DbSet<ShopsTable> ShopsTables { get; set; }
        public virtual DbSet<SizeContainTable> SizeContainTables { get; set; }
        public virtual DbSet<StationsInShopTable> StationsInShopTables { get; set; }
        public virtual DbSet<TrucksTable> TrucksTables { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=408-08;dataBase=FindYourWay;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlacesTable>(entity =>
            {
                entity.HasKey(e => e.PlaceId)
                    .HasName("PK__Places_t__E1216A36EDAC7A12");

                entity.ToTable("Places_table");

                entity.Property(e => e.PlaceId).HasColumnName("placeId");

                entity.Property(e => e.IdMaps)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("idMaps");

                entity.Property(e => e.Lang)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("lang");

                entity.Property(e => e.Lat)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("lat");

                entity.Property(e => e.PlaceAddress)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("placeAddress");
            });

            modelBuilder.Entity<ProviderTable>(entity =>
            {
                entity.HasKey(e => e.ProviderId)
                    .HasName("PK__Provider__107017F352B0093F");

                entity.ToTable("Provider_table");

                entity.Property(e => e.ProviderId)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("providerId")
                    .IsFixedLength(true);

                entity.Property(e => e.CrateVolume).HasColumnName("crateVolume");

                entity.Property(e => e.CrateWeight).HasColumnName("crateWeight");

                entity.Property(e => e.FactoryName)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("factoryName");

                entity.Property(e => e.LeavingTime).HasColumnName("leavingTime");

                entity.Property(e => e.MaxGoodsInCrate).HasColumnName("maxGoodsInCrate");

                entity.Property(e => e.PasswordProvider)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("passwordProvider");

                entity.Property(e => e.PlaceId).HasColumnName("placeId");

                entity.Property(e => e.ProviderName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("providerName");

                entity.HasOne(d => d.Place)
                    .WithMany(p => p.ProviderTables)
                    .HasForeignKey(d => d.PlaceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("place_id_fk");
            });

            modelBuilder.Entity<ShopsTable>(entity =>
            {
                entity.HasKey(e => e.ShopId)
                    .HasName("PK__Shops_ta__E5C424DC0659B551");

                entity.ToTable("Shops_table");

                entity.Property(e => e.ShopId).HasColumnName("shopId");

                entity.Property(e => e.GoodsRequired).HasColumnName("goodsRequired");

                entity.Property(e => e.HourDayFinish).HasColumnName("hourDayFinish");

                entity.Property(e => e.HourDayStart).HasColumnName("hourDayStart");

                entity.Property(e => e.PlaceId).HasColumnName("placeId");

                entity.Property(e => e.ProviderId)
                    .IsRequired()
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("providerId")
                    .IsFixedLength(true);

                entity.Property(e => e.ShopName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("shopName");

                entity.HasOne(d => d.Place)
                    .WithMany(p => p.ShopsTables)
                    .HasForeignKey(d => d.PlaceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Shops_table_Places_table");

                entity.HasOne(d => d.Provider)
                    .WithMany(p => p.ShopsTables)
                    .HasForeignKey(d => d.ProviderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Shops_table_Provider_table");
            });

            modelBuilder.Entity<SizeContainTable>(entity =>
            {
                entity.HasKey(e => e.SizeContainId)
                    .HasName("PK__SizeCont__C86215C95B047086");

                entity.ToTable("SizeContain_table");

                entity.Property(e => e.SizeContainId).HasColumnName("sizeContainId");

                entity.Property(e => e.VolumeSqmr).HasColumnName("volumeSqmr");

                entity.Property(e => e.WeightSize).HasColumnName("weightSize");
            });

            modelBuilder.Entity<StationsInShopTable>(entity =>
            {
                entity.ToTable("StationsInShop_table");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.LicensePlateTruck)
                    .IsRequired()
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.ProviderId)
                    .IsRequired()
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("providerId")
                    .IsFixedLength(true);

                entity.Property(e => e.ShopId).HasColumnName("shopId");

                entity.Property(e => e.StationSerialNumber).HasColumnName("stationSerialNumber");

                entity.Property(e => e.TimeFinish).HasColumnName("timeFinish");

                entity.Property(e => e.TimeStart).HasColumnName("timeStart");

                entity.HasOne(d => d.LicensePlateTruckNavigation)
                    .WithMany(p => p.StationsInShopTables)
                    .HasForeignKey(d => d.LicensePlateTruck)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StationsInShop_table_Trucks_table");

                entity.HasOne(d => d.Provider)
                    .WithMany(p => p.StationsInShopTables)
                    .HasForeignKey(d => d.ProviderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StationsInShop_table_Provider_table");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.StationsInShopTables)
                    .HasForeignKey(d => d.ShopId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StationsInShop_table_Shops_table");
            });

            modelBuilder.Entity<TrucksTable>(entity =>
            {
                entity.HasKey(e => e.LicensePlate)
                    .HasName("PK__Trucks_t__026BC15D61FA7C47");

                entity.ToTable("Trucks_table");

                entity.Property(e => e.LicensePlate)
                    .HasMaxLength(8)
                    .IsUnicode(false);

                entity.Property(e => e.IdOfDriver)
                    .IsRequired()
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("idOfDriver")
                    .IsFixedLength(true);

                entity.Property(e => e.NameOfDriver)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("nameOfDriver");

                entity.Property(e => e.ProviderId)
                    .IsRequired()
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("providerId")
                    .IsFixedLength(true);

                entity.Property(e => e.SizeId).HasColumnName("sizeId");

                entity.HasOne(d => d.Provider)
                    .WithMany(p => p.TrucksTables)
                    .HasForeignKey(d => d.ProviderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("providerId2_fk");

                entity.HasOne(d => d.Size)
                    .WithMany(p => p.TrucksTables)
                    .HasForeignKey(d => d.SizeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("size2_id_fk");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
