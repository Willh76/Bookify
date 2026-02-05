using Bookify.Domain.Abstractions;
using Bookify.Domain.Users.Events;

namespace Bookify.Domain.Users;

public sealed class User : Entity
{
    private readonly List<Role> _roles = new List<Role>();

    private User()
    {
    }

    public User(Guid id, FirstName firstName, Surname surname, Email email) 
        : base(id)
    {
        FirstName = firstName;
        Surname = surname;
        Email = email;
    }
    public FirstName FirstName { get; private set; }
    public Surname Surname { get; private set; }
    public Email Email { get; private set; }
    public string IdentityId { get; private set; } = string.Empty;
    public IReadOnlyCollection<Role> Roles => _roles.ToList();

    public static User Create(FirstName firstName, Surname surname, Email email)
    {
        User user = new User(Guid.NewGuid(), firstName, surname, email);

        user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));

        user._roles.Add(Role.Registered);

        return user;
    }

    public void SetIdentityId(string identityId)
    {
        IdentityId = identityId;
    }
}
