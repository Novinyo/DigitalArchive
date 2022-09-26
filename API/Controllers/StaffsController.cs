using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Staffs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class StaffController: BaseApiController
    {
        [HttpGet("staffs")]
        public async Task<IActionResult> GetStaffs(CancellationToken ct)
        {
            var result = await Mediator.Send(new All.Query(), ct);
            return HandleResult(result);
        }
         [HttpGet("{id}")]
        public async Task<IActionResult> GetStaffById(Guid id)
        {
            var result = await Mediator.Send(new Details.Query { Id = id });

            return HandleResult(result);
        }

        
        // [HttpPost]
        // public async Task<IActionResult> AddStaff(EntityTypeAddDto entityType, CancellationToken ct)
        // {
        //     var result = await Mediator.Send(new Create.Command { StaffType = entityType }, ct);

        //     return HandleResult(result);
        // }
    }
}