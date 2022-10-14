using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common;
using MediatR;

namespace Application.Documents.Type
{
    public class LoadCategories
    {
         public class Query : IRequest<IEnumerable<string>> { }

        public class Handler : IRequestHandler<Query, IEnumerable<string>>
        {
            public Handler() { }

           public Task<IEnumerable<string>> Handle(Query request, CancellationToken cancellationToken)
            {
                var categories = Enum.GetValues(typeof(Categories)).Cast<Categories>().Select(r => r.ToString());

                return (Task<IEnumerable<string>>)categories;
            }
        }
    }
}