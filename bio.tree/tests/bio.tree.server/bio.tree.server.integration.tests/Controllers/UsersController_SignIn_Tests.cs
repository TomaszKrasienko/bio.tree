using System.Net;
using System.Net.Http.Json;
using bio.tree.server.application.CQRS.Users.Commands.SignIn;
using bio.tree.server.application.Services;
using bio.tree.server.infrastructure.DAL.Documents;
using bio.tree.server.integration.tests._Helpers;
using bio.tree.shared;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using Xunit;

namespace bio.tree.server.integration.tests.Controllers;

[Collection("integration-tests")]
public sealed class UsersController_SignIn_Tests : BaseTestController
{
    [Fact]
    public async Task SignIn_ForExistingUser_ShouldReturnOkStatusCode()
    {
        //arrange
        var command = new SignInCommand("test@test.pl", "pass123");
        var collection = TestDatabase.GetCollection<UserDocument>("users");
        await collection.InsertOneAsync(new UserDocument()
        {
            Id = Guid.NewGuid(),
            Email = command.Email,
            Password = command.Password,
            FirstName = "Joe",
            LastName = "Doe",
            Nickname = "JoeDoe99",
            VerificationToken = "Token",
            VerificationDate = DateTimeOffset.Now
        });
        
        //act
        var response = await Client.PostAsJsonAsync<SignInCommand>("/users/sign-in", command);
        
        //assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task SignIn_ForNotExistingUser_ShouldReturnOkStatusCode()
    {
        //arrange
        var command = new SignInCommand("test@test.pl", "pass123");
        
        //act
        var response = await Client.PostAsJsonAsync<SignInCommand>("/users/sign-in", command);
        
        //assert
        var error = await response.Content.ReadFromJsonAsync<ErrorDto>();
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        error.Exception.ShouldBe("authorize");
        error.Message.ShouldBe("Wrong credentials");
    }
    
    #region arrange
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<IPasswordManager, TestsPasswordManager>();
    }
    #endregion
}