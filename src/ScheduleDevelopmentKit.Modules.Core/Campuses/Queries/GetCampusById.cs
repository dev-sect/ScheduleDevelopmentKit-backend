using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using ScheduleDevelopmentKit.Common.Exceptions;
using ScheduleDevelopmentKit.DataAccess;

namespace ScheduleDevelopmentKit.Modules.Core.Campuses.Queries
{
    public static class GetCampusById
    {
        [PublicAPI]
        public record Query(Guid Id) : IRequest<Response>;

        [PublicAPI]
        public record Response(Guid Id, string Name, string Address);

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
                var campus = await _dbContext.Campuses.FindAsync(new object[] { request.Id }, cancellationToken);

                if (campus is null)
                    throw new EntityNotFoundException($"Campus with Id {request.Id} not found");

                return new Response(campus.Id, campus.Name.Value, campus.Address.Value);
            }
        }
    }
}