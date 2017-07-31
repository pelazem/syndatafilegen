using System;
using System.Collections.Generic;
using System.Linq;
using pelazem.util;

namespace SynDataFileGen.Lib
{
	public class FieldSpecCategorical : FieldSpecBase
	{
		#region Properties

		/// <summary>
		/// List of category values.
		/// </summary>
		public List<Category> Categories { get; private set; } = null;

		private List<object> Values { get; set; } = new List<object>();

		#endregion

		#region Constructors

		private FieldSpecCategorical() { }

		public FieldSpecCategorical(string name, List<Category> categories, bool enforceUniqueValues, string formatString)
			: base(name, enforceUniqueValues, formatString)
		{
			PrepareValues(categories);

			this.Categories = categories;
		}

		public FieldSpecCategorical(string name, List<Category> categories, bool enforceUniqueValues, string formatString, int? fixedWidthLength, Util.Location? fixedWidthAddPadding = Util.Location.AtStart, Util.Location? fixedWidthTruncate = Util.Location.AtEnd, char? fixedWidthPaddingChar = null)
			: base(name, enforceUniqueValues, formatString, fixedWidthLength, fixedWidthPaddingChar, fixedWidthAddPadding, fixedWidthTruncate)
		{
			PrepareValues(categories);

			this.Categories = categories;
		}

		#endregion

		#region FieldSpecBase implementation

		public override void SetNextValue()
		{
			// If unique values was set but all of them have been used, turn off unique values so we can continue generating with repeated categorical values.
			if (this.EnforceUniqueValues && this.Categories.Count == this.UniqueValues.Count)
				this.EnforceUniqueValues = false;

			// Get a random number somewhere in the interval of possible indexes of the weighted values list
			int random = Converter.GetInt32(RNG.GetUniform(0, this.Values.Count - 1));

			object result = this.Values[random];

			if (this.EnforceUniqueValues)
			{
				while (this.UniqueValues.ContainsKey(result))
				{
					random = Converter.GetInt32(RNG.GetUniform(-1, this.Values.Count));

					result = this.Values[random];
				}

				this.UniqueValues.Add(result, false);
			}

			_value = result;
		}

		#endregion

		private void PrepareValues(List<Category> categories)
		{
			if (categories.Count == 0)
				return;


			// Sum up all weights to determine if we have even weighting
			double sum;

			sum = categories.Aggregate(0.0, (output, next) => output + next.Weight);


			// No further action needed if all elements equally weighted at zero
			if (sum == 0)
			{
				this.Values.AddRange(categories.Select(c => c.Value));

				return;
			}

			// Sort the categories by weight
			categories.Sort((a, b) => { return a.Weight.CompareTo(b.Weight); });

			// If they're all equal non-zero weights, no further action needed
			if (categories.First().Weight == categories.Last().Weight)
			{
				this.Values.AddRange(categories.Select(c => c.Value));

				return;
			}


			// Eliminate zero weights - hey, you pass me stuff with zero weights as well as non-zero weights, I'll assume you want the zeroed stuff not counted :)
			int index = categories.IndexOf(categories.First(c => c.Weight != 0));

			categories.RemoveRange(0, index);


			// Get the minimum non-zero weight; we'll use this to proportionalize values. We'll boost it by 10 to exaggerate small weight differences.
			double normalizerWeight = categories.First().Weight / 10.0;


			// Dividing by the normalizer weight will make each normalized weight at least 1. The 10x boost will exaggerate minimal but non-zero differences.
			categories.ForEach(c => c.ValueCount = Converter.GetInt32(Math.Abs(c.Weight / normalizerWeight)));


			// Now generate number of values records for each category's normalized weight
			// We do not run this in parallel as List<T> is not thread-safe. Perhaps substitute a thread-safe collection here in the future that also supports indexed retrieval at O(1).
			categories.ForEach(c => this.Values.AddRange(Enumerable.Repeat(c.Value, c.ValueCount)));
		}
	}
}
