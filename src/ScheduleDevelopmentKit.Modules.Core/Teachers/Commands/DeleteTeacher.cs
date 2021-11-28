using System;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ScheduleDevelopmentKit.Common.Exceptions;
using ScheduleDevelopmentKit.DataAccess;

namespace ScheduleDevelopmentKit.Modules.Core.Teachers.Commands
{
    public class DeleteTeacher
    {
        [PublicAPI]
        public record Command(Guid Id) : IRequest;

        [UsedImplicitly]
        public class CommandHandler : IRequestHandler<Command>
        {
            private readonly SdkDbContext _sdkDbContext;

            public CommandHandler(SdkDbContext sdkDbContext)
            {
                _sdkDbContext = sdkDbContext;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var teacher = await _sdkDbContext.Teachers.SingleOrDefaultAsync(
                    t => t.Id == request.Id, cancellationToken);

                if (teacher is null)
                    throw new EntityNotFoundException($"Teacher with Id {request.Id} not found");

                _sdkDbContext.Teachers.Remove(teacher);
                await _sdkDbContext.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}