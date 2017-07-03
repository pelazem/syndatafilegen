using System;
using System.Reflection;

namespace SynDataFileGen.Lib
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
