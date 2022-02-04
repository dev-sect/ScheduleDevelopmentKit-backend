using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ScheduleDevelopmentKit.DataAccess;

namespace ScheduleDevelopmentKit.Modules.Core.Campuses.Queries
{
    public static class GetCampusesList
    {
        [PublicAPI]
        public record Query() : IRequest<Response>;

        [PublicAPI]
        public record Response(IReadOnlyCollection<Response.Campus> Campuses)
        {
            public record Campus(
                Guid Id,
                string Name,
                string Address);
        };

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
                var campuses = await _dbContext.Campuses.Select(
                    c => new Response.Campus(
                        c.Id,
                        c.Name.Value,
                        c.Address.Value)).ToListAsync(cancellationToken: cancellationToken);

                return new Response(campuses);
            }
        }
    }
}