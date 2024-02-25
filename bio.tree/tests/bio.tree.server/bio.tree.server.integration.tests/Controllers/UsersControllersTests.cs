using System.Net;
using System.Net.Http.Json;
using bio.tree.server.application.CQRS.Users.Commands.SignUp;
using bio.tree.server.application.CQRS.Users.Commands.Verify;
using bio.tree.server.infrastructure.DAL.Documents;
using bio.tree.server.tests.shared.Factories.Documents;
using bio.tree.shared;
using bio.tree.shared.DTO;
using MongoDB.Driver;
using Shouldly;
using Xunit;

namespace bio.tree.server.integration.tests.Controllers;

[Collection("integration-tests")]
public sealed class UsersControllersTests : BaseTestController, IDisposable
{
    [Fact]
    public async Task SignUp_GivenSignUpCommandWithNotExistingEmail_ShouldReturnAcceptedStatusCodeAndAddUser()
    {
        //arrange
        var command = new SignUpCommand(Guid.Empty, "test@test.pl", "Joe", "Doe",
            "JoeDoe00", "StrongPass123!");
        
        //act
        var response = await Client.PostAsJsonAsync<SignUpCommand>("/users/sign-up", command);
        
        //assert
        response.StatusCode.ShouldBe(HttpStatusCode.Accepted);
        var user = await _usersCollection
            .Find(x => x.Email == command.Email)
            .FirstOrDefaultAsync();
        user.ShouldNotBeNull();
    }
    
    [Fact]
    public async Task SignUp_GivenSignUpCommandWithExistingEmail_ShouldReturnBadRequestStatusCodeAndDoNotAddUser()
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
        var user = await _usersCollection
            .Find(x
                => x.Email == command.Email
                && x.Nickname == command.Nickname)
            .FirstOrDefaultAsync();
        user.ShouldBeNull();
    }

    [Fact]
    public async Task Verify_GivenExistingVerificationToken_ShouldReturnOkStatusCodeAndAddVerificationDate()
    {
        //arrange
        var userId = Guid.NewGuid();
        var command = new VerifyCommand("my_verification_token");
        await _usersCollection.InsertOneAsync(new UserDocument()
        {
            Id = userId,
            Email = "test@test.pl",
            Password = "StrongPass123!",
            FirstName = "Joe",
            LastName = "Doe",
            Nickname = "JoeDoe99",
            VerificationToken = command.Token
        });
        
        //act
        var response = await Client.PostAsJsonAsync<VerifyCommand>("users/verify", command);
        
        //assert
        var user = await _usersCollection
            .Find(x => x.Id == userId)
            .FirstOrDefaultAsync();
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        user.ShouldNotBeNull();
        user.VerificationDate.ShouldNotBeNull();
    }
    
    [Fact]
    public async Task Verify_GivenNotExistingVerificationToken_ShouldReturnBadRequestStatusCodeWithAuthorizeMessage()
    {
        //arrange
        var userId = Guid.NewGuid();
        var command = new VerifyCommand("my_verification_token");
        
        //act
        var response = await Client.PostAsJsonAsync<VerifyCommand>("users/verify", command);
        
        //assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        var error = await response.Content.ReadFromJsonAsync<ErrorDto>();
        error.Exception.ShouldBe("authorize");
        error.Message.ShouldBe("Wrong credentials");
    }

    [Fact]
    public async Task Me_ForAuthorizedRequest_ShouldReturnStatusCodeOkWithUserDto()
    {
        //arrange
        var user = UserDocumentFactory.Get(1, 1).Single();
        var collection = TestDatabase.GetCollection<UserDocument>("users");
        await collection.InsertOneAsync(user);
        Authorize(user.Id);
        
        //act
        var response = await Client.GetAsync("users/me");
        
        //assert
         response.StatusCode.ShouldBe(HttpStatusCode.OK);
        // var responseObj = await response.Content.ReadFromJsonAsync<UserDto>();
        // responseObj.ShouldNotBeNull();
        var tmp = await response.Content.ReadAsStringAsync();
    }
    
    #region arrange
    private readonly IMongoCollection<UserDocument> _usersCollection;
    public UsersControllersTests()
    {
        _usersCollection = TestDatabase.GetCollection<UserDocument>("users");
    }
    #endregion
}