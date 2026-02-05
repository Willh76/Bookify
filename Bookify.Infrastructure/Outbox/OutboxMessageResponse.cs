namespace Bookify.Infrastructure.Outbox;

internal sealed partial class ProcessOutboxMessagesJob
{
    internal sealed record OutboxMessageResponse(Guid Id, string Content);
}
