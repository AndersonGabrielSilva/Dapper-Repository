using System.Collections.Generic;
using System.Linq;
using Dapper_Blog.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Dapper_Blog.Repositories
{
    public sealed class UserRepository : Repository<User>
    {
        private readonly SqlConnection _connection;

        public UserRepository(SqlConnection connection) : base(connection)
            => _connection = connection;

        public List<User> ReadWithRole()
        {
            var query = @"
                SELECT
                    [User].*,
                    [Role].*
                FROM
                    [User]
                    LEFT JOIN [UserRole] ON [UserRole].[UserId] = [User].[Id]
                    LEFT JOIN [Role] ON [UserRole].[RoleId] = [Role].[Id]";

            var users = new List<User>();
            var items = _connection.Query<User, Role, User>(
                query,
                (user, role) =>
                {
                    var usr = users.FirstOrDefault(x => x.Id == user.Id);
                    if (usr == null)
                    {
                        usr = user;

                        usr.AddRoule(role);

                        users.Add(usr);
                    }
                    else
                        usr.AddRoule(role);

                    return user;
                }, splitOn: "Id");

            return users;
        }
    }
}