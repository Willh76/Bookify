using Bookify.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bookify.Application.Abstractions.Authentication;

public interface IJwtService
{
    Task<Result<string>> GetAccessTokenAsync(
        string email,
        string password,
        CancellationToken cancellationToken = default);
}
