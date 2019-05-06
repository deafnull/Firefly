using Dapper;
using System;
using System.Threading.Tasks;

namespace Firefly.AuthManager.Users
{
    internal class UsersRepository : IUsersRepository
    {
        private readonly IFireflyAuthDatabase database;

        public UsersRepository(IFireflyAuthDatabase database)
        {
            this.database = database ?? throw new ArgumentNullException(nameof(database));
        }

        public async Task Insert(IUser user)
        {
            using (var connection = database.GetConnection())
            {
                var exists = (await Get(user.Username)) != null;

                if (exists)
                    throw new UserAlreadyExistsException(user.Username);

                var insertQuery = @"
                insert into Users(Username, Hash, Salt)
                values (@Username, @Hash, @Salt);";

                await connection.ExecuteAsync(insertQuery, user);
            };
        }

        public async Task<IUser> Get(string userName)
        {
            using (var connection = database.GetConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<StoredUser>(
                    "select top 1 * from Users u (nolock) where u.UserName = @userName", new { userName });
            };
        }
    }
}
