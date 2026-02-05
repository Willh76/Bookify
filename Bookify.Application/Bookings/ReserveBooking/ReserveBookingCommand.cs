using Bookify.Application.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bookify.Application.Bookings.ReserveBooking;

public record ReserveBookingCommand(Guid ApartmentId,
    Guid UserId,
    DateOnly StartDate,
    DateOnly EndDate) : ICommand<Guid>;
