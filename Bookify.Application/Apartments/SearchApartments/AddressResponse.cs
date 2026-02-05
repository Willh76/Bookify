using System;
using System.Collections.Generic;
using System.Text;

namespace Bookify.Application.Apartments.SearchApartments;

public sealed class AddressResponse
{
    public string Country { get; init; }
    public string State { get; init; }
    public string PostCode { get; init; }
    public string City { get; init; }
    public string Street { get; init; }
}
