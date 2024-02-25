using bio.tree.server.application.CQRS.Abstractions.Queries;
using bio.tree.server.application.CQRS.Users.Queries;
using bio.tree.server.infrastructure.DAL.Documents;
using bio.tree.shared.DTO;
using MongoDB.Driver;

namespace bio.tree.server.infrastructure.DAL.QueryHandlers;

internal sealed class GetUserByIdQueryHandler(IMongoDatabase database) 
    : IQueryHandler<GetUserByIdQuery, UserDto>
{
    private const string CollectionName = "users";
    private readonly IMongoCollection<UserDocument> _collection = database.GetCollection<UserDocument>(CollectionName);

    public Task<UserDto> HandleAsync(GetUserByIdQuery query, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}