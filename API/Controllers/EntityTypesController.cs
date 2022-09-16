using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Application.EntityTypes;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class EntityTypesController: BaseApiController
    {
        [HttpGet("entityTypes")]
        public async Task<IActionResult> GetEntityTypes(CancellationToken ct)
        {
            var result = await Mediator.Send(new All.Query(), ct);

            return HandleResult(result);
        }

        [HttpGet("activeEntityTypes")]
        public async Task<IActionResult> GetActiveEntityTypes(CancellationToken ct)
        {
            var result = await Mediator.Send(new List.Query(), ct);

            return HandleResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddEntityType(CommonDto entityType, CancellationToken ct)
        {
            var result = await Mediator.Send(new Create.Command { EntityTypes = entityType }, ct);

            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEntityTypeById(Guid id)
        {
            var result = await Mediator.Send(new Details.Query { Id = id });

            return HandleResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditEntityType(Guid id, CommonDto entityType)
        {
            entityType.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command { EntityType = entityType }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntityType(Guid id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
        }
    }
}