using System;
using System.Collections.Generic;
using System.Text;

namespace Bookify.Domain.Apartments
{
    public record Address(
        string Country,
        string State,
        string PostCode,
        string City,
        string Street);
}
