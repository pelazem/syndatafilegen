using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
	public interface IFieldSpec<T>
	{
		PropertyInfo Prop { get; }

		bool EnforceUniqueValues { get; }

		string FormatString { get; }

		char? PaddingCharIfFixedWidth { get; }

		int? LengthIfFixedWidth { get; }

		Util.Location? AddPaddingAtIfFixedWidth { get; }

		Util.Location? TruncateTooLongAtIfFixedWidth { get; }

		void SetValue(T item);
	}
}
