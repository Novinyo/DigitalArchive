using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using Application.Documents.Type;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class DocumentTypesController:BaseApiController
    {
        [HttpGet("documentTypes")]
        public async Task<IActionResult> GetDocumentTypes(CancellationToken ct)
        {
            var result = await Mediator.Send(new All.Query(), ct);

            return HandleResult(result);
        }

        [HttpGet("activeDocumentTypes")]
        public async Task<IActionResult> GetActiveDocumentTypes(CancellationToken ct)
        {
            var result = await Mediator.Send(new List.Query(), ct);

            return HandleResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddDocumentType(CommonDto documentType, CancellationToken ct)
        {
            var result = await Mediator.Send(new Create.Command { DocumentType = documentType }, ct);

            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocumentTypeById(Guid id)
        {
            var result = await Mediator.Send(new Details.Query { Id = id });

            return HandleResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditDocumentType(Guid id, CommonDto documentType)
        {
            documentType.Id = id;
            return HandleResult(await Mediator.Send(new Edit.Command { DocumentType = documentType }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocumentType(Guid id)
        {
            return HandleResult(await Mediator.Send(new Delete.Command { Id = id }));
        }
    }
}