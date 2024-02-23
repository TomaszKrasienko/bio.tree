using System.Net;
using System.Net.Http.Json;
using bio.tree.server.application.CQRS.Users.Commands.SignUp;
using bio.tree.server.infrastructure.DAL.Documents;
using bio.tree.server.integration.tests._Helpers;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace bio.tree.server.integration.tests.Controllers;

[Collection("integration-tests")]
public sealed class UsersControllersTests : BaseTestController, IDisposable
{
    [Fact]
    public async Task SignUp_GivenSignUpCommandWithNotExistingEmail_ShouldReturnAcceptedStatusCode()
    {
        //arrange
        var command = new SignUpCommand(Guid.Empty, "test@test.pl", "Joe", "Doe",
            "JoeDoe00", "StrongPass123!");
        
        //act
        var response = await Client.PostAsJsonAsync<SignUpCommand>("/users/sign-up", command);
        
        //assert
        response.StatusCode.ShouldBe(HttpStatusCode.Accepted);
    }
    
    [Fact]
    public async Task SignUp_GivenSignUpCommandWithExistingEmail_ShouldReturnBadRequestStatusCode()
    {
        //arrange
        var command = new SignUpCommand(Guid.Empty, "test@test.pl", "Joe", "Doe",
            "JoeDoe00", "StrongPass123!");
        var collection = TestDatabase.GetCollection<UserDocument>("users");
        await collection.InsertOneAsync(new UserDocument()
        {
            Email = command.Email,
            Id = Guid.NewGuid(),
            FirstName = "test_first_name",
            LastName = "test_last_name",
            Nickname = "test_nickname_name",
            Password = "test_pass"
        });
        
        //act
        var response = await Client.PostAsJsonAsync<SignUpCommand>("/users/sign-up", command);
        
        //assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    public void Dispose()
    {
        TestDatabase.Dispose();
    }
}