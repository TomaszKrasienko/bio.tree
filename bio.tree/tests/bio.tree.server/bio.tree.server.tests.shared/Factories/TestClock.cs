using bio.tree.server.application.Services;

namespace bio.tree.server.tests.shared.Factories;

public class TestClock : IClock
{
    private readonly DateTimeOffset _dateTimeOffset;
    private TestClock()
        => _dateTimeOffset = new DateTimeOffset();

    private TestClock(DateTimeOffset dateTimeOffset)
        => _dateTimeOffset = dateTimeOffset;
    
    public static IClock Get()
        => new TestClock();

    public static IClock Get(DateTimeOffset dateTimeOffset)
        => new TestClock(dateTimeOffset);

    public DateTimeOffset Now()
        => _dateTimeOffset;
}