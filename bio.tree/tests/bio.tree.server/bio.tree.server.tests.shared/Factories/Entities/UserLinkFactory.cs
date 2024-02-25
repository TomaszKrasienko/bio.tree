using bio.tree.server.domain.Models;
using bio.tree.server.domain.ValueObjects;
using bio.tree.server.domain.ValueObjects.UserLink;
using Bogus;

namespace bio.tree.server.tests.shared.Factories.Entities;

public static class UserLinkFactory
{
    public static List<UserLink> Get(int count = 1)
    {
        var userLinkFaker = new Faker<UserLink>()
            .RuleFor(p => p.PlatformId, f => new EntityId(Guid.NewGuid()))
            .RuleFor(p => p.Id, f => new EntityId(Guid.NewGuid()))
            .RuleFor(p => p.Url, f => new Url(f.PickRandom<string>()));
        return userLinkFaker.Generate(count);
    }
}