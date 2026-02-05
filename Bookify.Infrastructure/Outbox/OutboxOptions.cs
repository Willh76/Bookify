using System;
using System.Collections.Generic;
using System.Text;

namespace Bookify.Infrastructure.Outbox;

internal class OutboxOptions
{
    public int IntervalInSeconds { get; init; }
    public int BatchSize { get; init; }
}
