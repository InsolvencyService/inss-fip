using INSS.FIP.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    public virtual DbSet<View_Data> viewData { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured && !string.IsNullOrWhiteSpace(_connectionString))
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var viewName = _configuration["CMPDataViewName"] ?? "vw_Eiir";
        string viewSchema = _configuration["CMPDataViewNameSchema"] ?? "ext";
        modelBuilder.Entity<View_Data>()
            .HasNoKey()
            .ToView(viewName, schema: viewSchema);

    }
}
