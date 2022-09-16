using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IEntityExists<TEntity>
    {
        Task<bool> checkIfExists(Expression<Func<TEntity, bool>> expression);
    }
}