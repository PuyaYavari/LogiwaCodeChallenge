using Merchandising.Entities;
using Merchandising.Repositories.Contracts;
using Merchandising.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Merchandising.Repositories
{
	public class RepositoryProduct : RepositoryBase<Product>, IRepository<Product>
	{
		public RepositoryProduct(MerchandisingContext context): base(context) { }

	}
}
