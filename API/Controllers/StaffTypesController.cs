using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Application.StaffTypes;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class StaffTypeController: BaseApiController
    {
        [HttpGet("staffTypes")]
        public async Task<IActionResult> GetStaffTypes(CancellationToken ct)
        {
            var result = await Mediator.Send(new All.Query(), ct);

            return HandleResult(result);
        }

        [HttpGet("activeStaffTypes")]
        public async Task<IActionResult> GetActiveStaffTypes(CancellationToken ct)
        {
            var result = await Mediator.Send(new List.Query(), ct);

            return HandleResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddStaffType(EntityTypeAddDto entityType, CancellationToken ct)
        {
            var result = await Mediator.Send(new Create.Command { StaffType = entityType }, ct);

            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStaffTypeById(Guid id)
        {
            var result = await Mediator.Send(new Details.Query { Id = id });

            return HandleResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditStaffType(Guid id, EntityTypeAddDto entityType)
        {
            entityType.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command { StaffType = entityType }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaffType(Guid id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
        }
        [HttpGet("exists")]
        public async Task<IActionResult> StaffTypeExists([FromQuery] DuplicateVm query)
        {
            var result = await Mediator.Send(new DuplicateStaffType.Query { ToCheck  = query });

            return HandleResult(result);
        }
    }
}