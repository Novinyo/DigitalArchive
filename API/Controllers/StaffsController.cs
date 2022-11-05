using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Staffs;
using Application.Staffs.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class StaffController: BaseApiController
    {
        [HttpGet("staffs")]
        public async Task<IActionResult> GetAllStaffs(CancellationToken ct)
        {
            var result = await Mediator.Send(new All.Query(), ct);
            return HandleResult(result);
        }

        [HttpGet("staffs/{schoolId}")]
        public async Task<IActionResult> GetStaffs(Guid schoolId, CancellationToken ct)
        {
            var result = await Mediator.Send(new AllBySchool.Query{SchoolId = schoolId}, ct);
            return HandleResult(result);
        }

         [HttpGet("{id}")]
        public async Task<IActionResult> GetStaffById(Guid id)
        {
            var result = await Mediator.Send(new Details.Query { Id = id });

            return HandleResult(result);
        }

        
        [HttpPost]
        public async Task<IActionResult> AddStaff(StaffWDto entity, CancellationToken ct)
        {
            var result = await Mediator.Send(new Create.Command { Staff = entity }, ct);

            return HandleResult(result);
        }

          [HttpPut("{id}")]
        public async Task<IActionResult> EditStaffType(Guid id, StaffWDto staff)
        {
            staff.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command { Staff = staff }));
        }
    }
}