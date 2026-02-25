using INSS.FIP.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace INSS.FIP.Data.CMPDataSource;

public class sourceCMPDbContext : DbContext, IDbContext
{
    private readonly string? _connectionString;
    private readonly IConfiguration _configuration;
    public sourceCMPDbContext()
    { }
    public sourceCMPDbContext(string? connectionString, IConfiguration configuration)
    {
        _connectionString = connectionString;
        _configuration = configuration;
    }
    public sourceCMPDbContext(DbContextOptions<sourceCMPDbContext> options)
        : base(options)
    { }
    public virtual DbSet<VwOdsInssightcmp> viewData { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured && !string.IsNullOrWhiteSpace(_connectionString))
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var viewName = _configuration["CMPDataViewName"];
        string viewSchema = _configuration["CMPDataViewNameSchema"]!;

        modelBuilder.Entity<VwOdsInssightcmp>(entity =>
        {
            entity
                .HasNoKey()
                .ToView(viewName, schema: viewSchema);

            entity.Property(e => e.AddressLine1).HasMaxLength(4000);
            entity.Property(e => e.AddressLine2).HasMaxLength(4000);
            entity.Property(e => e.AddressLine3).HasMaxLength(4000);
            entity.Property(e => e.Country).HasMaxLength(4000);
            entity.Property(e => e.County).HasMaxLength(4000);
            entity.Property(e => e.Name).HasMaxLength(4000);
            entity.Property(e => e.PostCode).HasMaxLength(4000);
            entity.Property(e => e.SourceRef).HasMaxLength(4000);
            entity.Property(e => e.Town).HasMaxLength(4000);
        });

    }

}
