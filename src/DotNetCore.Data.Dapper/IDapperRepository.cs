namespace DotNetCore.Data.Dapper
{
    public interface IDapperRepository
    {
        IDbConnection DbConnection { get; }
        IDbTransaction? DbTransaction { get; }
    }
}