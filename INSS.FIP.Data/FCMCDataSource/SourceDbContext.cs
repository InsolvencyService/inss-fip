using Microsoft.EntityFrameworkCore;

namespace INSS.FIP.Data.FCMCDataSource
{
    public partial class SourceDbContext : DbContext
    {
        private readonly string? _connectionString;

        public SourceDbContext()
        { }

        public SourceDbContext(string? connectionString)
        {
            _connectionString = connectionString;
        }

        public SourceDbContext(DbContextOptions<iirwebdbContext> options)
            : base(options)
        { }

        public virtual DbSet<vw_FindIp> vw_FindIps { get; set; } = null!;

        public virtual DbSet<vw_findipauthbody> vw_findipauthbodies { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured && !string.IsNullOrWhiteSpace(_connectionString))
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
          

            modelBuilder.Entity<vw_FindIp>(entity =>
            {
                entity.HasNoKey();
                entity.ToView("vw_findip");


                entity.Property(e => e.IpNo)
                    .HasColumnName("IpNo")
                    .IsRequired();

                entity.Property(e => e.Forenames)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("forenames");

                entity.Property(e => e.Surname)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("surname");

                entity.Property(e => e.RegisteredFirmName)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("RegisteredFirmName");

                entity.Property(e => e.RegisteredAddressLine3)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("RegisteredAddressLine3");

                entity.Property(e => e.RegisteredAddressLine4)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("RegisteredAddressLine4");

                entity.Property(e => e.RegisteredAddressLine5)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("RegisteredAddressLine5");

                entity.Property(e => e.RegisteredPostCode)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("RegisteredPostCode");

                entity.Property(e => e.RegisteredPhone)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("RegisteredPhone");

                entity.Property(e => e.IpEmailAddress)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("IpEmailAddress");

                entity.Property(e => e.IncludeOnInternet)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("IncludeOnInternet")
                    .HasDefaultValueSql("(' ')");

                entity.Property(e => e.LicensingBody)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("LicensingBody");
            });

            modelBuilder.Entity<vw_findipauthbody>(entity =>
            {
                entity.HasKey(e => e.AuthBodyCode);

                entity.ToView("vw_findipauthbody"); 

                entity.Property(e => e.AuthBodyCode)
                    .HasMaxLength(5)
                    .HasColumnName("AuthBodyCode")
                    .IsRequired();

                entity.Property(e => e.AuthBodyName)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("AuthBodyName");

                entity.Property(e => e.AuthBodyAddressLine1)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("AuthBodyAddressLine1");

                entity.Property(e => e.AuthBodyAddressLine2)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("AuthBodyAddressLine2");

                entity.Property(e => e.AuthBodyAddressLine3)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("AuthBodyAddressLine3");

                entity.Property(e => e.AuthBodyAddressLine4)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("AuthBodyAddressLine4");

                entity.Property(e => e.AuthBodyAddressLine5)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("AuthBodyAddressLine5");

                entity.Property(e => e.AuthBodyPostcode)
                    .HasMaxLength(8000)
                    .IsUnicode(false)
                    .HasColumnName("AuthBodyPostcode");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
