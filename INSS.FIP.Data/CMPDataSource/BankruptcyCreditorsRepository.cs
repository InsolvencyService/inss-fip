using INSS.FIP.Data.CMPDataSource.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace INSS.FIP.Data.CMPDataSource
{
    public class BankruptcyCreditorsRepository : IBankruptcyCreditorsRepository
    {
        private readonly TargetCMPDbContext _context;
        private IDbContextTransaction? _transaction;

        public BankruptcyCreditorsRepository(TargetCMPDbContext context)
        {
            _context = context;
        }

        public async Task AddBankruptcyCreditorsListAsync(List<BankruptcyCreditorsList> records)
        {
            await _context.BankruptcyCreditorsLists.AddRangeAsync(records);
        }

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction == null)
            {
                throw new CMPSyncDatabaseException("No transaction in progress to commit.");
            }
            await _transaction.CommitAsync(); ;
        }

        public async Task DeleteAllBankruptcyCreditorsListAsync()
        {
            await _context.BankruptcyCreditorsLists.ExecuteDeleteAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction == null)
            {
                throw new CMPSyncDatabaseException("No transaction in progress to rollback.");
            }
            await _transaction.RollbackAsync();
        }

        public async Task SaveBankruptcyCreditorsListChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
