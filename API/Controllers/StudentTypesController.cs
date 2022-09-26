using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Application.StudentTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class StudentTypeController:BaseApiController
    {
         [HttpGet("studentTypes")]
        public async Task<IActionResult> GetStudentTypes(CancellationToken ct)
        {
            var result = await Mediator.Send(new All.Query(), ct);

            return HandleResult(result);
        }

        [HttpGet("activeStudentTypes")]
        public async Task<IActionResult> GetActiveStudentTypes(CancellationToken ct)
        {
            var result = await Mediator.Send(new List.Query(), ct);

            return HandleResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddStudentType(EntityTypeAddDto entityType, CancellationToken ct)
        {
            var result = await Mediator.Send(new Create.Command { StudentType = entityType }, ct);

            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentTypeById(Guid id)
        {
            var result = await Mediator.Send(new Details.Query { Id = id });

            return HandleResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditStudentType(Guid id, EntityTypeAddDto entityType)
        {
            entityType.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command { StudentType = entityType }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentType(Guid id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
        }
        [HttpGet("exists")]
        public async Task<IActionResult> StudentTypeExists([FromQuery] DuplicateVm query)
        {
            var result = await Mediator.Send(new DuplicateStudentType.Query { ToCheck  = query });

            return HandleResult(result);
        }
    }
}