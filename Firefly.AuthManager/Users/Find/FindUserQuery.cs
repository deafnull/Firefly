using MediatR;
using System;

namespace Firefly.AuthManager.Users.Find
{
    public class FindUserQuery : IRequest<IFindUserResponse>
    {
        public FindUserQuery(string userName)
        {
            Username = userName ?? throw new ArgumentNullException();
        }

        public string Username { get; }
    }
}
