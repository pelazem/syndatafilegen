using System;
using System.Reflection;

namespace SynDataFileGen.Lib
{
	public interface IFieldSpec
	{
		string Name { get; }

		bool EnforceUniqueValues { get; }

		string FormatString { get; }


		int? FixedWidthLength { get; }

		char? FixedWidthPaddingChar { get; }
	
		Util.Location? FixedWidthAddPadding { get; }

		Util.Location? FixedWidthTruncate { get; }


		object Value { get; }
	}
}
