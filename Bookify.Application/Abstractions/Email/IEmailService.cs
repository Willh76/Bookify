using System;
using System.Collections.Generic;
using System.Text;

namespace Bookify.Application.Abstractions.Email;

public interface IEmailService
{
    Task SendAsync(Domain.Users.Email recipient, string subject, string body);
}
