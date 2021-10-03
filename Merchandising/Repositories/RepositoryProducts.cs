using Merchandising.Entities;
using Merchandising.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Merchandising.Repositories
{
	public class RepositoryProducts : RepositoryBase<Product>
	{
		public RepositoryProducts(MerchandisingContext context): base(context) { }

	}
}
