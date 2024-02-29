using bio.tree.server.application.CQRS.Abstractions.Queries;
using bio.tree.server.application.CQRS.Users.Queries;
using bio.tree.server.application.DTO;
using bio.tree.server.infrastructure.DAL.Documents;
using bio.tree.server.infrastructure.DAL.Documents.Mappers;
using MongoDB.Driver;

namespace bio.tree.server.infrastructure.DAL.QueryHandlers;

internal sealed class GetUserByIdQueryHandler(IMongoDatabase database) 
    : IQueryHandler<GetUserByIdQuery, UserDto>
{
    private const string CollectionName = "users";
    private readonly IMongoCollection<UserDocument> _collection = database.GetCollection<UserDocument>(CollectionName);

    public async Task<UserDto> HandleAsync(GetUserByIdQuery query, CancellationToken cancellationToken)
        => (await _collection.Find(x => x.Id == query.Id).FirstOrDefaultAsync(cancellationToken)).AsDto();
}