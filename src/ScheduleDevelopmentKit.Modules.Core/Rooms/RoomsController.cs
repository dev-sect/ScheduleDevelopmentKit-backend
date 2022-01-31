using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ScheduleDevelopmentKit.Modules.Core.Campuses.Commands;
using ScheduleDevelopmentKit.Modules.Core.Rooms.Commands;
using ScheduleDevelopmentKit.Modules.Core.Rooms.Queries;

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

        [HttpPost("get-by-id")]
        public async Task<ActionResult<GetRoomById.Response>> GetRoomById(GetRoomById.Query query)
        {
            return await _mediator.Send(query);
        }

        [HttpPost("get-by-campus-id")]
        public async Task<ActionResult<GetRoomsByCampusId.Response>> GetRoomsByCampusId(GetRoomsByCampusId.Query query)
        {
            return await _mediator.Send(query);
        }
    }
}