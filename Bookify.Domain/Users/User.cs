using Bookify.Domain.Abstractions;
using Bookify.Domain.Users.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bookify.Domain.Users
{
    public sealed class User : Entity
    {
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

        public static User Create(FirstName firstName, Surname surname, Email email)
        {
            User user = new User(Guid.NewGuid(), firstName, surname, email);

            user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));

            return user;
        }
    }
}
