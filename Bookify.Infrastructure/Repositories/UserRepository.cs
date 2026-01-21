using Bookify.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bookify.Infrastructure.Repositories
{
    internal sealed class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) 
            : base(context)
        {
        }
    }
}
