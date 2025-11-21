using INSS.FIP.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace INSS.FIP.Data.CMPDataSource;

public class targetCMPDbContext : DbContext, IDbContext
{
    private readonly string? _connectionString;
    public targetCMPDbContext()
    { }
    public targetCMPDbContext(string? connectionString)
    {
        _connectionString = connectionString;
    }
    public targetCMPDbContext(DbContextOptions<targetCMPDbContext> options)
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
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id).ValueGeneratedOnAdd();
            entity.ToTable("BankruptcyCreditorsList");
        });
    }
}
