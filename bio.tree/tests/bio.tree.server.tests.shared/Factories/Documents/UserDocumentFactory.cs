using bio.tree.server.infrastructure.DAL.Documents;
using bio.tree.server.tests.shared.Factories.Entities;
using Bogus;

namespace bio.tree.server.tests.shared.Factories.Documents;

internal static class UserDocumentFactory
{
    internal static IEnumerable<UserDocument> Get(int count = 1, int userLinkCount = 1)
    {
        var userDocumentFaker = new Faker<UserDocument>()
            .RuleFor(p => p.Id, f => Guid.NewGuid())
            .RuleFor(p => p.FirstName, f => f.Name.FirstName())
            .RuleFor(p => p.LastName, f => f.Name.LastName())
            .RuleFor(p => p.Email, f => f.Internet.Email())
            .RuleFor(p => p.Nickname, f => f.Name.FullName())
            .RuleFor(p => p.Password, f => f.Lorem.Word())
            .RuleFor(p => p.VerificationToken, f => f.Lorem.Word())
            .RuleFor(p => p.VerificationDate, f => f.Date.PastOffset())
            .RuleFor(p => p.ResetToken, f => f.Lorem.Word())
            .RuleFor(p => p.ResetTokenExpire, f => f.Date.FutureOffset())
            .RuleFor(p => p.UserLinks, f => userLinkCount == 0 ? null : UserLinkDocumentFactory.Get(userLinkCount));

        return userDocumentFaker.Generate(count);
    }
}