using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Application.Students;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class StudentsController:BaseApiController
    {
         [HttpGet("students")]
        public async Task<IActionResult> GetStudents(CancellationToken ct)
        {
            var result = await Mediator.Send(new All.Query(), ct);

            return HandleResult(result);
        }

        [HttpGet("activeStudents")]
        public async Task<IActionResult> GetActiveStudents(CancellationToken ct)
        {
            var result = await Mediator.Send(new List.Query(), ct);

            return HandleResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(EntityTypeAddDto entityType, CancellationToken ct)
        {
            var result = await Mediator.Send(new Create.Command { StudentType = entityType }, ct);

            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudentById(Guid id)
        {
            var result = await Mediator.Send(new Details.Query { Id = id });

            return HandleResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditStudent(Guid id, EntityTypeAddDto entityType)
        {
            entityType.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command { StudentType = entityType }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
        }
        [HttpGet("relationships")]
        public IActionResult getRelationships()
        {
            var result = Enum.GetValues(typeof(Relationships)).Cast<Relationships>().Select(r => r.ToString());

            return Ok(result);
        }
    }
}