using Dapper;
using Microsoft.Data.SqlClient;

namespace TicketMonster.Admin.Repository;

public class BaseRepo
{
    protected readonly string _TicketMonsterConnection;

    protected BaseRepo(IConfiguration configuration) => _TicketMonsterConnection = configuration.GetConnectionString("TicketMonsterConnection");

    // Query
    protected async Task<IEnumerable<T>> QueryAsync<T>(string query, object parameters = null) where T : class
    {
        return await ExecuteSql(async conn => await conn.QueryAsync<T>(query, parameters));
    }

    // INSERT UPDATE DELETE
    protected Task<int> ExecuteAsync<T>(string query, object parameters = null)
    {
        return ExecuteSql(async conn => await conn.ExecuteAsync(query, parameters));
    }

    protected async Task<TResult> ExecuteSql<TResult>(Func<SqlConnection, Task<TResult>> sqlQuery)
    {
        try
        {
            using var conn = new SqlConnection(_TicketMonsterConnection);
            await conn.OpenAsync();
            var result = await sqlQuery(conn);
            await conn.CloseAsync();
            return result;
        }
        catch (Exception) { throw; }
    }

    #region example
    // SQL Injection => SQL 參數法
    // Create
    public async Task<int> CreateXXX(xxxDTO input)
    {
        string sql = "insert into XXXTable (ooo) values (@ooo)";
        var parameters = new { ooo = input.ooo };
        int affectedRows = await ExecuteAsync<xxxDTO>(sql, parameters);
        return affectedRows;
    }

    // Delete
    public async Task<int> DeleteXXX(int xxxId)
    {
        string sql = "delete from XXXTable where Id = @Id";
        var parameters = new { xxxId = xxxId };
        int affectedRows = await ExecuteAsync<xxxDTO>(sql, parameters);
        return affectedRows;
    }

    // Update
    public async Task<int> UpdateXXX(xxxDTO input)
    {
        string sql = "update xxxTable SET ooo = @ooo WHERE Id = @Id";
        var parameters = new { ooo = input.ooo, Id = input.Id };
        int affectedRows = await ExecuteAsync<xxxDTO>(sql, parameters);
        return affectedRows;
    }

    public class xxxDTO
    {
        public int Id { get; set; }
        public string ooo { get; set; }
    }
    #endregion

    #region origin
    //protected async Task<IEnumerable<T>> ExecuteQuery<T>(string sql, object parameters = null)
    //{
    //    try
    //    {
    //        using var conn = new SqlConnection(_TicketMonsterConnection);
    //        await conn.OpenAsync();
    //        var result = await conn.QueryAsync<T>(sql, parameters);
    //        await conn.CloseAsync();
    //        return result;
    //    }
    //    catch (Exception) { throw; }
    //}

    //// INSERT UPDATE DELETE
    //protected async Task<int> ExecuteNonQuery(string sql, object parameters = null)
    //{
    //    try
    //    {
    //        using var conn = new SqlConnection(_TicketMonsterConnection);
    //        await conn.OpenAsync();
    //        var affectedRows = await conn.ExecuteAsync(sql, parameters);
    //        await conn.CloseAsync();
    //        return affectedRows;
    //    }
    //    catch (Exception) { throw; }
    //}
    #endregion
}