﻿using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ScheduleDevelopmentKit.Modules.Core.Campuses.Commands;
using ScheduleDevelopmentKit.Modules.Core.Campuses.Queries;

namespace ScheduleDevelopmentKit.Modules.Core.Campuses
{
    [ApiController]
    [Route("api/v1/campuses")]
    public class CampusesController
    {
        private readonly IMediator _mediator;

        public CampusesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create")]
        public async Task<ActionResult<CreateCampus.Response>> CreateTeacher(CreateCampus.Command command)
        {
            return await _mediator.Send(command);
        }

        [HttpPost("get-list")]
        public async Task<ActionResult<GetCampusesList.Response>> GetCampuses(GetCampusesList.Query query)
        {
            return await _mediator.Send(query);
        }
    }
}