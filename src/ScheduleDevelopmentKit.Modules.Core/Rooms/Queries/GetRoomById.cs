using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ScheduleDevelopmentKit.Common.Exceptions;
using ScheduleDevelopmentKit.DataAccess;

namespace ScheduleDevelopmentKit.Modules.Core.Rooms.Queries
{
    public static class GetRoomById
    {
        [PublicAPI]
        public record Query(Guid Id) : IRequest<Response>;

        [PublicAPI]
        public record Response(Guid Id, Guid CampusId, string Number, bool BelongsToFaculty);

        [UsedImplicitly]
        public class QueryHandler : IRequestHandler<Query, Response>
        {
            private readonly SdkDbContext _dbContext;

            public QueryHandler(SdkDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var room = await _dbContext.Rooms
                                     .Include(r => r.Campus)
                                     .FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

                if (room is null)
                    throw new EntityNotFoundException($"Room with Id {request.Id} not found");

                return new Response(room.Id, room.Campus.Id, room.Number.Value, room.BelongsToFaculty);
            }
        }
    }
}