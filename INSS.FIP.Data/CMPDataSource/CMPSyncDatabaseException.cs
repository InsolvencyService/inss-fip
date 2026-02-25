namespace INSS.FIP.Data.CMPDataSource
{
    public class CMPSyncDatabaseException : SystemException
    {
        public CMPSyncDatabaseException() : base() { }
        public CMPSyncDatabaseException(string message) : base(message) { }
        public CMPSyncDatabaseException(string message, Exception innerException) : base(message, innerException) { }
    }
}
