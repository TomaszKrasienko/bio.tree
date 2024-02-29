using bio.tree.server.application.CQRS.Abstractions.Queries;
using bio.tree.server.application.DTO;

namespace bio.tree.server.application.CQRS.Users.Queries;

public sealed record GetUserByIdQuery(Guid Id) : IQuery<UserDto>;