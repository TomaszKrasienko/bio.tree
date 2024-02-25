using bio.tree.server.domain.Models;
using bio.tree.server.infrastructure.DAL.Documents;
using Bogus;

namespace bio.tree.server.tests.shared.Factories.Documents;

internal static class UserLinkDocumentFactory
{
    internal static List<UserLinkDocument> Get(int count = 1)
    {
        var userLinkDocumentFaker = new Faker<UserLinkDocument>()
            .RuleFor(x => x.Id, f => Guid.NewGuid())
            .RuleFor(x => x.PlatformId, f => Guid.NewGuid())
            .RuleFor(x => x.Url, f => f.Internet.Url());
        return userLinkDocumentFaker.Generate(count);
    }
}