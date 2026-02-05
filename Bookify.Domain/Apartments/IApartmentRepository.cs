using Bookify.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bookify.Domain.Apartments;

public interface IApartmentRepository
{
    Task<Apartment?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
