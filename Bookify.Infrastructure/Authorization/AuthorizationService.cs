using Bookify.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Infrastructure.Authorization
{
    internal sealed class AuthorizationService
    {
        private readonly ApplicationDbContext _context;

        public AuthorizationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserRolesResponse> GetRolesForUserAsync(string identityId)
        {
            UserRolesResponse roles = await _context.Set<User>()
                .Where(user => user.IdentityId == identityId)
                .Select(user => new UserRolesResponse
                {
                    Id = user.Id,
                    Roles = user.Roles.ToList()
                })
                .FirstAsync();

            return roles;
        }

        internal async Task<HashSet<string>> GetPermissionsForUserAsync(string identityId)
        {
            ICollection<Permission> permissions = await _context.Set<User>()
                .Where(user => user.IdentityId == identityId)
                .SelectMany(user => user.Roles.Select(role => role.Permissions))
                .FirstAsync();

            HashSet<string> permissionsSet = permissions.Select(permission => permission.Name).ToHashSet();

            return permissionsSet;
        }
    }
}
