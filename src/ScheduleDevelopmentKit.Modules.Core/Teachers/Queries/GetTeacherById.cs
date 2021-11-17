using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ScheduleDevelopmentKit.DataAccess;

namespace ScheduleDevelopmentKit.Modules.Core.Teachers.Queries
{
    public static class GetTeacherById
    {
        [PublicAPI]
        public record Query(Guid Id) : IRequest<Response>;

        [PublicAPI]
        public record Response(
            Guid Id,
            string FirstName,
            string LastName,
            string? MiddleName,
            string Email,
            string PhoneNumber);

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
                var teacher = await _context.Teachers.FirstAsync(t => t.Id == request.Id, cancellationToken);
                return new Response(
                    teacher.Id,
                    teacher.Name.FirstName,
                    teacher.Name.LastName,
                    teacher.Name.MiddleName,
                    teacher.Email.Value,
                    teacher.PhoneNumber.Value);
            }
        }
    }
}