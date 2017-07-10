using System;
using System.Reflection;
using pelazem.util;

namespace SynDataFileGen.Lib
{
	public class FieldSpecDynamic : FieldSpecBase
	{
		#region Properties

		public Func<object> FuncToGenerateValue { get; private set; }

		#endregion

		#region Constructors

		private FieldSpecDynamic() { }

		public FieldSpecDynamic(string name, Func<object> funcToGenerateValue, bool enforceUniqueValues, string formatString)
			: base(name, enforceUniqueValues, formatString)
		{
			this.FuncToGenerateValue = funcToGenerateValue;
		}

		public FieldSpecDynamic(string name, Func<object> funcToGenerateValue, bool enforceUniqueValues, string formatString, int? fixedWidthLength, Util.Location? fixedWidthAddPadding = Util.Location.AtStart, Util.Location? fixedWidthTruncate = Util.Location.AtEnd, char? fixedWidthPaddingChar = null)
			: base(name, enforceUniqueValues, formatString, fixedWidthLength, fixedWidthPaddingChar, fixedWidthAddPadding, fixedWidthTruncate)
		{
			this.FuncToGenerateValue = funcToGenerateValue;
		}

		#endregion

		#region FieldSpecBase implementation

		protected override object GetValue()
		{
			if (this.FuncToGenerateValue == null)
				return string.Empty;

			object result = this.FuncToGenerateValue();

			if (this.EnforceUniqueValues)
			{
				while (this.UniqueValues.ContainsKey(result))
					result = this.FuncToGenerateValue();

				this.UniqueValues.Add(result, false);
			}

			return result;
		}

		#endregion
	}
}
