using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ScheduleDevelopmentKit.Modules.Core.Campuses.Commands;
using ScheduleDevelopmentKit.Modules.Core.Rooms.Commands;

namespace ScheduleDevelopmentKit.Modules.Core.Rooms
{
    [ApiController]
    [Route("/api/v1/rooms/")]
    public class RoomsController
    {
        private readonly IMediator _mediator;

        public RoomsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<ActionResult<CreateRoom.Response>> CreateRoom(CreateRoom.Command command)
        {
            return await _mediator.Send(command);
        }
    }
}