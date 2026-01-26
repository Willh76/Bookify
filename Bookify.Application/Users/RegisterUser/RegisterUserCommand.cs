using Bookify.Application.Abstractions.Messaging;

namespace Bookify.Application.Users.RegisterUser
{
    public sealed record RegisterUserCommand(
        string Email,
        string FirstName,
        string Surname,
        string Password) : ICommand<Guid>;
}
