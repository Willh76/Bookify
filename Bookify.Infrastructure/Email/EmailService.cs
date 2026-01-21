using Bookify.Application.Abstractions.Email;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bookify.Infrastructure.Email
{
    internal sealed class EmailService : IEmailService
    {
        public Task SendAsync(Domain.Users.Email recipient, string subject, string body)
        {
            return Task.CompletedTask;
        }
    }
}
