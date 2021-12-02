using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ScheduleDevelopmentKit.Common.Exceptions;
using ScheduleDevelopmentKit.DataAccess;
using ScheduleDevelopmentKit.Domain.ValueObjects;

namespace ScheduleDevelopmentKit.Modules.Core.Teachers.Commands
{
    public static class EditTeacher
    {
        [PublicAPI]
        public record Command(
            Guid Id,
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
                RuleFor(x => x.FirstName).NotEmpty().MaximumLength(30);
                RuleFor(x => x.LastName).NotEmpty().MaximumLength(30);
                RuleFor(x => x.MiddleName).MaximumLength(30);
                RuleFor(x => x.PhoneNumber).Matches(@"^\+7[0-9]{10}$");
                RuleFor(x => x.Email).NotEmpty().MaximumLength(60).Must(x => x.Contains("@"));
            }
        }

        [PublicAPI]
        public record Response(
            Guid Id,
            string LastNameAndInitials);

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
                var teacher = await _sdkDbContext.Teachers.SingleOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

                if (teacher is null)
                    throw new EntityNotFoundException($"Teacher with Id {request.Id} not found");

                teacher.Email = new Email(request.Email);
                teacher.Name = new PersonName(request.FirstName, request.LastName, request.LastName);
                teacher.PhoneNumber = new PhoneNumber(request.PhoneNumber);

                _sdkDbContext.Teachers.Update(teacher);

                await _sdkDbContext.SaveChangesAsync(cancellationToken);

                return new Response(teacher.Id, teacher.Name.LastNameAndInitials);
            }
        }
    }
}