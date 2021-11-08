using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using ScheduleDevelopmentKit.DataAccess;
using ScheduleDevelopmentKit.Domain.Entities;
using ScheduleDevelopmentKit.Domain.ValueObjects;

namespace ScheduleDevelopmentKit.Application.Teachers.Commands
{
    public static class CreateTeacher
    {
        [PublicAPI]
        public record Command(
            string FirstName,
            string LastName,
            string? MiddleName,
            string PhoneNumber,
            string Email) : IRequest<Response>;

        [PublicAPI]
        public record Response(
            Guid Id,
            string FullName);
        
        [UsedImplicitly]
        public class CommandHandler : IRequestHandler<Command, Response>
        {
            private readonly SdkDbContext _sdkDbContext;

            public CommandHandler(SdkDbContext sdkDbContext)
            {
                _sdkDbContext = sdkDbContext;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var teacher = new Teacher(
                    Guid.NewGuid(),
                    new PersonName(
                        request.FirstName,
                        request.LastName,
                        request.MiddleName),
                    new Email(request.Email),
                    new PhoneNumber(request.PhoneNumber));

                await _sdkDbContext.Teachers.AddAsync(teacher, cancellationToken);

                await _sdkDbContext.SaveChangesAsync(cancellationToken);

                return new Response(teacher.Id, teacher.Name.FullName);
            }
        }
    }
}