using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Modules.ProductModule;
using Talabat.Core.Specification;

namespace Talabat.Repository
{
	public static class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
	{
		public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,ISpecification<TEntity> specification)
		{
			var query = inputQuery; // _dbContext.set<TEntity>().where(p=>p.Id)
			if(specification.Criteria is not null) // E => E.Id ==1
			{
				query = query.Where(specification.Criteria);
			}
			// _dbContext.set<TEntity>().where(p=>p.Id).Include()
			query = specification.Include.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));
			// query = _dbContext.set<TEntity>().where(p => p.Id).Include(p => p.Brand)
			// query = _dbContext.set<TEntity>().where(p => p.Id).Include(p => p.Brand).Include(p => p.Category)
			return query;
		}

	}
}
