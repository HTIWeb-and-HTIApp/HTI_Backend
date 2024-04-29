using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HTI.Core.RepositoriesContract
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<List<T>> FindByCondition(Expression<Func<T, bool>> expression = null,
                                                  Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
                                                  Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);


    }
}
