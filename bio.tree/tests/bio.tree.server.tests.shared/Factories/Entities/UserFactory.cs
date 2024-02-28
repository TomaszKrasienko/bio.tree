using bio.tree.server.domain.Models;
using bio.tree.server.domain.ValueObjects;
using bio.tree.server.domain.ValueObjects.User;
using Bogus;

namespace bio.tree.server.tests.shared.Factories.Entities;

internal static class UserFactory
{
    internal static List<User> Get(int count = 1, int userLinkCount = 1, bool isVerified = true, 
        bool afterResetPass = false)
    {
        var userFaker = new Faker<User>()
            .CustomInstantiator(f
                => new User(
                    new EntityId(Guid.NewGuid()),
                    new FullName(f.Name.FirstName(), f.Name.LastName()),
                    new Email(f.Internet.Email()),
                    new Nickname(f.Lorem.Word()),
                    new Password(f.Lorem.Word()),
                    isVerified ? new VerificationToken(f.Lorem.Word(), f.Date.PastOffset()) : null,
                    afterResetPass ? new ResetToken(f.Lorem.Word(), f.Date.FutureOffset()) : null,
                    userLinkCount == 0 ? null : UserLinkFactory.Get(userLinkCount) 
                ));
        return userFaker.Generate(count);
    }
}