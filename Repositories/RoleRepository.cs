using Dapper_Blog.Models;
using Microsoft.Data.SqlClient;

namespace Dapper_Blog.Repositories
{
    public sealed class RoleRepository : Repository<Role>
    {
        private readonly SqlConnection _connection;

        public RoleRepository(SqlConnection connection) : base(connection)
            => _connection = connection;

    }
}