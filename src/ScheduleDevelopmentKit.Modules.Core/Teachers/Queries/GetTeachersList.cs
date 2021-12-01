using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ScheduleDevelopmentKit.DataAccess;

namespace ScheduleDevelopmentKit.Modules.Core.Teachers.Queries
{
    public static class GetTeachersList
    {
        [PublicAPI]
        public record Query() : IRequest<Response>;

        [PublicAPI]
        public record Response(IReadOnlyCollection<Response.Teacher> Teachers)
        {
            public record Teacher(
                Guid Id,
                string FirstName,
                string LastName,
                string? MiddleName,
                string PhoneNumber,
                string Email);
        }

        [UsedImplicitly]
        public class QueryHandler : IRequestHandler<Query, Response>
        {
            private readonly SdkDbContext _context;

            public QueryHandler(SdkDbContext context)
            {
                _context = context;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var teachers = await _context.Teachers.Select(
                    t => new Response.Teacher(
                        t.Id,
                        t.Name.FirstName,
                        t.Name.LastName,
                        t.Name.MiddleName,
                        t.PhoneNumber.Value,
                        t.Email.Value)).ToListAsync(cancellationToken);

                return new Response(teachers);
            }
        }
    }
}