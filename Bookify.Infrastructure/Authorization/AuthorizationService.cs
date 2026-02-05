using Bookify.Application.Abstractions.Caching;
using Bookify.Domain.Users;
using Bookify.Infrastructure.Caching;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Infrastructure.Authorization;

internal sealed class AuthorizationService
{
    private readonly ApplicationDbContext _context;
    private readonly ICacheService _cacheService;

    public AuthorizationService(
        ApplicationDbContext context,
        ICacheService cacheService)
    {
        _context = context;
        _cacheService = cacheService;
    }

    public async Task<UserRolesResponse> GetRolesForUserAsync(string identityId)
    {
        string cacheKey = $"auth:roles-{identityId}";

        UserRolesResponse? cachedRoles = await _cacheService.GetAsync<UserRolesResponse>(cacheKey);

        if (cachedRoles is  not null) 
            return cachedRoles;

        UserRolesResponse roles = await _context.Set<User>()
            .Where(user => user.IdentityId == identityId)
            .Select(user => new UserRolesResponse
            {
                Id = user.Id,
                Roles = user.Roles.ToList()
            })
            .FirstAsync();

        await _cacheService.SetAsync(cacheKey, roles);

        return roles;
    }

    internal async Task<HashSet<string>> GetPermissionsForUserAsync(string identityId)
    {
        string cacheKey = $"auth:permissions-{identityId}";

        HashSet<string>? cachedPermissions = await _cacheService.GetAsync<HashSet<string>>(cacheKey);

        if (cachedPermissions is not null)
            return cachedPermissions;

        ICollection<Permission> permissions = await _context.Set<User>()
            .Where(user => user.IdentityId == identityId)
            .SelectMany(user => user.Roles.Select(role => role.Permissions))
            .FirstAsync();

        HashSet<string> permissionsSet = permissions.Select(permission => permission.Name).ToHashSet();

        await _cacheService.SetAsync(cacheKey, permissionsSet);

        return permissionsSet;
    }
}
