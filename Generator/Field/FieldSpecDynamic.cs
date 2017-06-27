﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using pelazem.Common;

namespace Generator
{
	public class FieldSpecDynamic<T> : FieldSpecBase<T>
		where T : new()
	{
		#region FieldSpecBase implementation

		public override void SetValue(T item)
		{
			if (this.Prop.PropertyType.Equals(TypeHelper.TypeString) && !string.IsNullOrWhiteSpace(this.FormatString))
				this.Prop.SetValueEx(item, string.Format(this.FormatString, GetValue()));
			else
				this.Prop.SetValueEx(item, GetValue());
		}

		#endregion

		#region Properties

		public Func<object> FuncToGenerateValue { get; private set; }

		#endregion

		#region Constructors

		private FieldSpecDynamic() { }

		public FieldSpecDynamic(PropertyInfo prop, Func<object> funcToGenerateValue, bool enforceUniqueValues, string formatString)
			: base(prop, enforceUniqueValues, formatString)
		{
			this.FuncToGenerateValue = funcToGenerateValue;
		}

		public FieldSpecDynamic(PropertyInfo prop, Func<object> funcToGenerateValue, bool enforceUniqueValues, string formatString, int? fixedWidthLength, Util.Location? fixedWidthAddPadding = Util.Location.AtStart, Util.Location? fixedWidthTruncate = Util.Location.AtEnd, char? fixedWidthPaddingChar = null)
			: base(prop, enforceUniqueValues, formatString, fixedWidthLength, fixedWidthPaddingChar, fixedWidthAddPadding, fixedWidthTruncate)
		{
			this.FuncToGenerateValue = funcToGenerateValue;
		}

		#endregion

		private object GetValue()
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
	}
}