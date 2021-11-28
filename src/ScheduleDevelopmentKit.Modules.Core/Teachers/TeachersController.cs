using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ScheduleDevelopmentKit.Modules.Core.Teachers.Commands;
using ScheduleDevelopmentKit.Modules.Core.Teachers.Queries;

namespace ScheduleDevelopmentKit.Modules.Core.Teachers
{
    [ApiController]
    [Route("api/v1/teachers")]
    public class TeachersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TeachersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<ActionResult<CreateTeacher.Response>> CreateTeacher(CreateTeacher.Command command)
        {
            return await _mediator.Send(command);
        }

        [HttpPost("edit")]
        public async Task<ActionResult<EditTeacher.Response>> CreateTeacherById(EditTeacher.Command command)
        {
            return await _mediator.Send(command);
        }

        [HttpPost("get-by-id")]
        public async Task<ActionResult<GetTeacherById.Response>> GetTeacherById(GetTeacherById.Query query)
        {
            return await _mediator.Send(query);
        }

        [HttpPost("get-list")]
        public async Task<ActionResult<GetTeachersList.Response>> GetTeachersList(GetTeachersList.Query query)
        {
            return await _mediator.Send(query);
        }
    }
}