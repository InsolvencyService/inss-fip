

namespace INSS.FIP.Data.CMPDataSource.Interfaces
{
    ///<summary>Implementation of Repository pattern for for unit tests as mocking EF Core is a b@t%h.</summary>
    public interface IBankruptcyCreditorsRepository
    {
        Task AddBankruptcyCreditorsListAsync(List<BankruptcyCreditorsList> records);

        Task DeleteAllBankruptcyCreditorsListAsync();

        Task SaveBankruptcyCreditorsListChangesAsync();

        Task BeginTransactionAsync();

        Task CommitTransactionAsync();

        Task RollbackTransactionAsync();
    }
}
