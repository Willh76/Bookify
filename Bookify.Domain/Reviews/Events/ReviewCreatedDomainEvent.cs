using Bookify.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bookify.Domain.Reviews.Events;

public sealed record ReviewCreatedDomainEvent(Guid ReviewId) : IDomainEvent;
