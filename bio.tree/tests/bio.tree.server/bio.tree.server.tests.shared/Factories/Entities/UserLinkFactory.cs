using bio.tree.server.domain.Models;
using bio.tree.server.domain.ValueObjects;
using bio.tree.server.domain.ValueObjects.UserLink;
using Bogus;

namespace bio.tree.server.tests.shared.Factories.Entities;

internal static class UserLinkFactory
{
    internal static IEnumerable<UserLink> Get(int count = 1)
    {
        var userLinkFaker = new Faker<UserLink>()
            .CustomInstantiator(f 
                => new UserLink(
                    new EntityId(Guid.NewGuid()),
                    new EntityId(Guid.NewGuid()), 
                    new Url(f.Internet.Url())
                    ));
        return userLinkFaker.Generate(count);
    }
}