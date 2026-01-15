using Bookify.Domain.Apartments;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bookify.Domain.Bookings
{
    public interface IBookingRepository
    {
        Task<Booking?> GetByIDAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task<bool> IsOverlappingAsync(
        Apartment apartment,
        DateRange duration,
        CancellationToken cancellationToken = default);

        void Add(Booking booking);
    }
}
