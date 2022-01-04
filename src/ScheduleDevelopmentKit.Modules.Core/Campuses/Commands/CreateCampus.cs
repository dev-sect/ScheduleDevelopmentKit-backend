using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using JetBrains.Annotations;
using MediatR;
using ScheduleDevelopmentKit.DataAccess;
using ScheduleDevelopmentKit.Domain.Entities;
using ScheduleDevelopmentKit.Domain.ValueObjects;

namespace ScheduleDevelopmentKit.Modules.Core.Campuses.Commands
{
    public class CreateCampus
    {
        [PublicAPI]
        public record Command(
            string Name,
            string Address) : IRequest<Response>;

        [UsedImplicitly]
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(c => c.Name).MaximumLength(100);
                RuleFor(c => c.Address).MaximumLength(300);
            }
        }

        [PublicAPI]
        public record Response(Guid Id, string Name);

        [UsedImplicitly]
        public class CommandHandler : IRequestHandler<Command, Response>
        {
            private readonly SdkDbContext _sdkDbContext;

            public CommandHandler(SdkDbContext context)
            {
                _sdkDbContext = context;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var campus = new Campus(
                    Guid.NewGuid(),
                    new CampusName(request.Name),
                    new CampusAddress(request.Address));

                await _sdkDbContext.Campuses.AddAsync(campus, cancellationToken);
                await _sdkDbContext.SaveChangesAsync(cancellationToken);

                return new Response(campus.Id, campus.Name.Value);
            }
        }
    }
}