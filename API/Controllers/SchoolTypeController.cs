using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Application.Schools.Type;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class SchoolTypeController:BaseApiController
    {
        [HttpGet("schoolTypes")]
        public async Task<IActionResult> GetSchoolTypes(CancellationToken ct)
        {
            var result = await Mediator.Send(new All.Query(), ct);

            return HandleResult(result);
        }

        [HttpGet("activeschoolTypes")]
        public async Task<IActionResult> GetActiveSchoolTypes(CancellationToken ct)
        {
            var result = await Mediator.Send(new List.Query(), ct);

            return HandleResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddType(CommonDto schoolType, CancellationToken ct)
        {
            var result = await Mediator.Send(new Create.Command { SchoolType = schoolType }, ct);

            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSchoolTypeById(Guid id)
        {
            var result = await Mediator.Send(new Details.Query { Id = id });

            return HandleResult(result);
        }

         [HttpGet("exists")]
        public async Task<IActionResult> SchoolTypeByExists([FromQuery] DuplicateVm query)
        {
            var result = await Mediator.Send(new CheckDuplicate.Query { ToCheck  = query });

            return HandleResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditSchoolType(Guid id, CommonDto schoolType)
        {
            schoolType.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command { SchoolType = schoolType }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchoolType(Guid id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
        }
    }
}