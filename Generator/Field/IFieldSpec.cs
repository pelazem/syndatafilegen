using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
	public interface IFieldSpec<T>
		where T : new()
	{
		PropertyInfo Prop { get; }

		bool EnforceUniqueValues { get; }

		string FormatString { get; }


		int? FixedWidthLength { get; }

		char? FixedWidthPaddingChar { get; }
	
		Util.Location? FixedWidthAddPadding { get; }

		Util.Location? FixedWidthTruncate { get; }


		void SetValue(T item);
	}
}
