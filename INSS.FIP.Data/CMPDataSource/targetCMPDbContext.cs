using INSS.FIP.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace INSS.FIP.Data.CMPDataSource;

public class TargetCMPDbContext : DbContext, IDbContext
{
    private readonly string? _connectionString;

    public TargetCMPDbContext()
    { }

    public TargetCMPDbContext(string? connectionString)
    {
        _connectionString = connectionString;
    }

    public TargetCMPDbContext(DbContextOptions<TargetCMPDbContext> options)
        : base(options)
    { }

    public virtual DbSet<BankruptcyCreditorsList> BankruptcyCreditorsLists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured && !string.IsNullOrWhiteSpace(_connectionString))
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BankruptcyCreditorsList>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Id_BankruptcyCreditorsList");

            entity.ToTable("BankruptcyCreditorsList");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.SourceRef).HasMaxLength(450);
        });
    }
}
