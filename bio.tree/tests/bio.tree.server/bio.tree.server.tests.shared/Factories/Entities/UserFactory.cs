using bio.tree.server.domain.Models;
using bio.tree.server.domain.ValueObjects;
using bio.tree.server.domain.ValueObjects.User;
using Bogus;

namespace bio.tree.server.tests.shared.Factories.Entities;

public static class UserFactory
{
    public static List<User> Get(int count = 1, int userLinkCount = 1, bool isVerified = true, 
        bool afterResetPass = false, bool withLinks = false)
    {
        var userFaker = new Faker<User>()
            .RuleFor(p => p.Id, new EntityId(Guid.NewGuid()))
            .RuleFor(p => p.Email, f => new Email(f.Internet.Email()))
            .RuleFor(p => p.FullName, f => new FullName(f.Name.FirstName(), f.Name.LastName()))
            .RuleFor(p => p.Nickname, f => new Nickname(f.PickRandom<string>()))
            .RuleFor(p => p.Password, f => new Password(f.PickRandom<string>()));
        if (isVerified)
        {
            userFaker.RuleFor(p
                => p.VerificationToken, f => new VerificationToken(f.PickRandom<string>(), f.Date.PastOffset()));
        }
        else
        {
            userFaker.RuleFor(p
                => p.VerificationToken, f => new VerificationToken());
        }

        if (afterResetPass)
        {
            userFaker.RuleFor(p
                => p.ResetToken, f => new ResetToken(f.PickRandom<string>(), f.Date.FutureOffset()));
        }

        if (withLinks)
        {
            userFaker.RuleFor(p
                => p.UserLinks, f => UserLinkFactory.Get(userLinkCount));
        }

        return userFaker.Generate(count);
    }
}