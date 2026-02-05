using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bookify.Domain.Abstractions;

public interface IDomainEvent : INotification
{
}
