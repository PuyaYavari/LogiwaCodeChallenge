using Merchandising.Enums;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace Merchandising.Exceptions
{
	public class MerchandisingException : Exception
	{
		public EnumErrors code { get; set; } = EnumErrors.UNKNOWN;

		public MerchandisingException() : base() { }
		public MerchandisingException(string message) : base(message) { }
		public MerchandisingException(EnumErrors code, string message) : base(message) {
			this.code = code;
		}
	}
}
