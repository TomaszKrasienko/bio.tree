using bio.tree.server.application.Services;

namespace bio.tree.server.infrastructure.Time;

internal sealed class Clock : IClock
{
    public DateTimeOffset Now()
        => DateTimeOffset.Now;
}