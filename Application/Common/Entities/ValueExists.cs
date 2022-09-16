using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Application.Interfaces;
using Persistence;

namespace Application.Common.Entities
{
    public class ValueExists<T> : IEntityExists<T>
    {
         private readonly DataContext _context;
        public ValueExists(DataContext context)
        {
            _context = context;
        }
        

        public Task<bool> checkIfExists(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}