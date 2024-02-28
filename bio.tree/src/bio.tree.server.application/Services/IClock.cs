namespace bio.tree.server.application.Services;

public interface IClock
{
    DateTimeOffset Now();
}