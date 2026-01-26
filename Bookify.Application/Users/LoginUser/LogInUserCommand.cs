using Bookify.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bookify.Application.Users.LoginUser
{
    public sealed record LogInUserCommand(string Email, string Password)
        : ICommand<AccessTokenResponse>;
}
