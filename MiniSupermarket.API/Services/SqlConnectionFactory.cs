using Microsoft.Data.SqlClient;
using System.Data;

namespace MiniSupermarket.API.Services;

public sealed class SqlConnectionFactory
{
    private readonly string _connectionString;
    public SqlConnectionFactory(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Thiếu ConnectionStrings:DefaultConnection.");
    }
    public SqlConnection Create() => new(_connectionString);

    public static SqlParameter Money(SqlCommand command, string name, decimal value)
    {
        var parameter = command.Parameters.Add(name, SqlDbType.Decimal);
        parameter.Precision = 18;
        parameter.Scale = 2;
        parameter.Value = value;
        return parameter;
    }
}
