using Bookify.Application.Abstractions.Authentication;
using Bookify.Application.Abstractions.Messaging;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bookify.Application.Users.LoginUser
{
    internal sealed class LoginUserCommandHandler : ICommandHandler<LogInUserCommand, AccessTokenResponse>
    {
        private readonly IJwtService _jwtService;

        public LoginUserCommandHandler(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        public async Task<Result<AccessTokenResponse>> Handle(
            LogInUserCommand request,
            CancellationToken cancellationToken)
        {
            Result<string> result = await _jwtService.GetAccessTokenAsync(
                request.Email,
                request.Password,
                cancellationToken);

            if (result.IsFailure)
                return Result.Failure<AccessTokenResponse>(UserErrors.InvalidCredentials);

            return new AccessTokenResponse(result.Value);
        }
    }
}
