using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Modules.ProductModule;

namespace Talabat.Core.Specification
{
	public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
	{
		public Expression<Func<T, bool>>? Criteria { get; set; } = null;
		public List<Expression<Func<T, object>>> Include { get ; set ; } = new List<Expression<Func<T, object>>>();

		// Item الهيجبلي كل Queryالهيتم استخدامه علشان ابني ال specific object الهيتم استخدامه بناء ال
		public BaseSpecification()
        {
			
        }

		//specific object علشان تجبلي  Function ال هبعته لل specification objectهيتم استخدامه في بناء ال
		public BaseSpecification(Expression<Func<T, bool>> criteria)
		{
			Criteria = criteria;
		}
	}
}
