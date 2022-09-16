using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Schools;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class SchoolsController : BaseApiController
    {
        [HttpGet("schools")]
        public async Task<IActionResult> GetSchools(CancellationToken ct)
        {
            var result = await Mediator.Send(new All.Query(), ct);

            return HandleResult(result);
        }

        [HttpGet("activeschools")]
        public async Task<IActionResult> GetActiveSchools(CancellationToken ct)
        {
            var result = await Mediator.Send(new List.Query(), ct);

            return HandleResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddSchool(AddSchoolDto school, CancellationToken ct)
        {
            var result = await Mediator.Send(new Create.Command { School = school }, ct);

            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSchoolById(Guid id, CancellationToken ct)
        {
            var result = await Mediator.Send(new Details.Query { Id = id }, ct);

            return HandleResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditSchool(Guid id, AddSchoolDto school, CancellationToken ct)
        {
            school.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command { School = school }, ct));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSchool(Guid id, CancellationToken ct)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { Id = id }, ct));
        }
    }
}