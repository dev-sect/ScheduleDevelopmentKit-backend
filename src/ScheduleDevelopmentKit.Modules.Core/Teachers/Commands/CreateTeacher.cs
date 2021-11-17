using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using JetBrains.Annotations;
using MediatR;
using ScheduleDevelopmentKit.DataAccess;
using ScheduleDevelopmentKit.Domain.Entities;
using ScheduleDevelopmentKit.Domain.ValueObjects;

namespace ScheduleDevelopmentKit.Modules.Core.Teachers.Commands
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

        [UsedImplicitly]
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.FirstName).MaximumLength(20);
                RuleFor(x => x.LastName).MaximumLength(20);
                RuleFor(x => x.MiddleName).MaximumLength(20);
                RuleFor(x => x.PhoneNumber).Length(12).Must(x => x.StartsWith("+7"));
                RuleFor(x => x.Email).MaximumLength(50).Must(x => x.Contains("@"));
            }
        }

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