using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using JetBrains.Annotations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ScheduleDevelopmentKit.Common.Exceptions;
using ScheduleDevelopmentKit.DataAccess;
using ScheduleDevelopmentKit.Domain.Entities;
using ScheduleDevelopmentKit.Domain.ValueObjects;

namespace ScheduleDevelopmentKit.Modules.Core.Rooms.Commands
{
    public static class CreateRoom
    {
        [PublicAPI]
        public record Command(Guid CampusId, string Number, bool BelongsToFaculty) : IRequest<Response>;

        [UsedImplicitly]
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(c => c.Number).Length(1,10);
                RuleFor(c => c.Number).Matches(@"^[0-9]{0,9}[0-9a-zA-Z]{0,1}$");
            }
        }

        [PublicAPI]
        public record Response(Guid Id, string Number);

        [UsedImplicitly]
        public class CommandHandler : IRequestHandler<Command, Response>
        {
            private readonly SdkDbContext _dbContext;

            public CommandHandler(SdkDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var campus = await _dbContext.Campuses.Include(c => c.Rooms).FirstOrDefaultAsync(c => c.Id == request.CampusId, cancellationToken);

                if (campus is null)
                    throw new EntityNotFoundException($"Campus with Id {request.CampusId} not found");

                var room = new Room(Guid.NewGuid(), campus, new RoomNumber(request.Number), request.BelongsToFaculty);
                campus.AddRoom(room);

                await _dbContext.AddAsync(room, cancellationToken);
                _dbContext.Update(campus);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return new Response(room.Id, room.Number.Value);
            }
        }
    }
}