using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using SynDataFileGen.Lib;
using pelazem.util;

namespace CCLF17.Lib
{
	public class CCLFGenerator
	{
		#region Constants

		public const string PATHSPEC_CCLF0 = "P.VATHN.ACO.CCLF0.D{yy}{MM}{dd}.T{hh}0000t.txt";
		public const string PATHSPEC_CCLF1 = "P.VATHN.ACO.CCLF1.D{yy}{MM}{dd}.T{hh}0000t.txt";
		public const string PATHSPEC_CCLF2 = "P.VATHN.ACO.CCLF2.D{yy}{MM}{dd}.T{hh}0000t.txt";
		public const string PATHSPEC_CCLF3 = "P.VATHN.ACO.CCLF3.D{yy}{MM}{dd}.T{hh}0000t.txt";
		public const string PATHSPEC_CCLF4 = "P.VATHN.ACO.CCLF4.D{yy}{MM}{dd}.T{hh}0000t.txt";
		public const string PATHSPEC_CCLF5 = "P.VATHN.ACO.CCLF5.D{yy}{MM}{dd}.T{hh}0000t.txt";
		public const string PATHSPEC_CCLF6 = "P.VATHN.ACO.CCLF6.D{yy}{MM}{dd}.T{hh}0000t.txt";
		public const string PATHSPEC_CCLF7 = "P.VATHN.ACO.CCLF7.D{yy}{MM}{dd}.T{hh}0000t.txt";
		public const string PATHSPEC_CCLF8 = "P.VATHN.ACO.CCLF8.D{yy}{MM}{dd}.T{hh}0000t.txt";
		public const string PATHSPEC_CCLF9 = "P.VATHN.ACO.CCLF9.D{yy}{MM}{dd}.T{hh}0000t.txt";
		public const string PATHSPEC_CCLFA = "P.VATHN.ACO.CCLFA.D{yy}{MM}{dd}.T{hh}0000t.txt";
		public const string PATHSPEC_CCLFB = "P.VATHN.ACO.CCLFB.D{yy}{MM}{dd}.T{hh}0000t.txt";

		#endregion

		#region Properties

		public string OutputFolderRoot = @"c:\test\cclf17\";

		public bool IncludeHeaderRow { get; set; } = false;
		public string Delimiter { get; set; } = string.Empty;
		public string Encloser { get; set; } = string.Empty;
		public char FixedWidthPaddingCharacter { get; set; } = ' ';
		public Util.Location FixedWidthAddPadding { get; set; } = Util.Location.AtStart;
		public Util.Location FixedWidthTruncate { get; set; } = Util.Location.AtEnd;
		public Encoding Encoding { get; set; } = Encoding.UTF8;

		public int RecordsPerFileMin { get; set; } = 100;
		public int RecordsPerFileMax { get; set; } = 200;

		private DateTime DateStart { get; set; } = new DateTime(2017, 4, 1, 0, 0, 0, DateTimeKind.Utc);
		private DateTime DateEnd { get; set; } = new DateTime(2017, 6, 30, 0, 0, 0, DateTimeKind.Utc);

		#endregion

		public void Run()
		{
			// //////////////////////////////////////////////////
			// CCLF8 -> CCLF1, CCLF5, CCLF6, CCLF7, CCLF9
			List<ExpandoObject> CCLF8S = GenerateCCLF8();

			List<Category> BENE_HIC_NUM = CCLF8S.Select(c => (c as IDictionary<string, object>)[CCLFData.BENE_HIC_NUM]).Distinct().Select(d => new Category() { Value = d }).ToList();
			// //////////////////////////////////////////////////


			// //////////////////////////////////////////////////
			// CCLF1 -> CCLF2, CCLF3, CCLF4, CCLFA
			List<ExpandoObject> CCLF1S = GenerateCCLF1(BENE_HIC_NUM);

			List<Category> CUR_CLM_UNIQ_ID = CCLF1S.Select(c => (c as IDictionary<string, object>)[CCLFData.CUR_CLM_UNIQ_ID]).Distinct().Select(d => new Category() { Value = d }).ToList();

			List<Category> BENE_EQTBL_BIC_HICN_NUM = CCLF1S.Select(c => (c as IDictionary<string, object>)[CCLFData.BENE_EQTBL_BIC_HICN_NUM]).Distinct().Select(d => new Category() { Value = d }).ToList();

			List<Category> PRVDR_OSCAR_NUM = CCLF1S.Select(c => (c as IDictionary<string, object>)[CCLFData.PRVDR_OSCAR_NUM]).Distinct().Select(d => new Category() { Value = d }).ToList();

			List<Category> PRNCPL_DGNS_CD = CCLF1S.Select(c => (c as IDictionary<string, object>)[CCLFData.PRNCPL_DGNS_CD]).Distinct().Select(d => new Category() { Value = d }).ToList();
			// //////////////////////////////////////////////////


			// //////////////////////////////////////////////////
			// CCLF2
			List<ExpandoObject> CCLF2S = GenerateCCLF2(CUR_CLM_UNIQ_ID, BENE_HIC_NUM, BENE_EQTBL_BIC_HICN_NUM, PRVDR_OSCAR_NUM);
			// //////////////////////////////////////////////////


			// //////////////////////////////////////////////////
			// CCLF3
			List<ExpandoObject> CCLF3S = GenerateCCLF3(CUR_CLM_UNIQ_ID, BENE_HIC_NUM, BENE_EQTBL_BIC_HICN_NUM, PRNCPL_DGNS_CD, PRVDR_OSCAR_NUM);
			// //////////////////////////////////////////////////


			// //////////////////////////////////////////////////
			// CCLF4
			List<ExpandoObject> CCLF4S = GenerateCCLF4(CUR_CLM_UNIQ_ID, BENE_HIC_NUM, BENE_EQTBL_BIC_HICN_NUM, PRNCPL_DGNS_CD, PRVDR_OSCAR_NUM);
			// //////////////////////////////////////////////////


			// //////////////////////////////////////////////////
			// CCLF5 -> CCLFB
			List<ExpandoObject> CCLF5S = GenerateCCLF5(CUR_CLM_UNIQ_ID, BENE_HIC_NUM, BENE_EQTBL_BIC_HICN_NUM, PRNCPL_DGNS_CD);
			// //////////////////////////////////////////////////


			// //////////////////////////////////////////////////
			// CCLF6
			List<ExpandoObject> CCLF6S = GenerateCCLF6(CUR_CLM_UNIQ_ID, BENE_HIC_NUM, BENE_EQTBL_BIC_HICN_NUM);
			// //////////////////////////////////////////////////


			// //////////////////////////////////////////////////
			// CCLF7
			List<ExpandoObject> CCLF7S = GenerateCCLF7(CUR_CLM_UNIQ_ID, BENE_HIC_NUM);
			// //////////////////////////////////////////////////


			// //////////////////////////////////////////////////
			// CCLF9
			List<ExpandoObject> CCLF9S = GenerateCCLF9(BENE_HIC_NUM);
			// //////////////////////////////////////////////////


			// //////////////////////////////////////////////////
			// CCLFA
			List<ExpandoObject> CCLFAS = GenerateCCLFA(CUR_CLM_UNIQ_ID, DateStart, DateEnd);
			// //////////////////////////////////////////////////


			// //////////////////////////////////////////////////
			// CCLFB
			List<ExpandoObject> CCLFBS = GenerateCCLFB();
			// //////////////////////////////////////////////////


			// //////////////////////////////////////////////////
			// CCLF0
			// Contains summary statistics for the above files
			GenerateCCLF0(CCLF1S, CCLF2S, CCLF3S, CCLF4S, CCLF5S, CCLF6S, CCLF7S, CCLF8S, CCLF9S, CCLFAS, CCLFBS);
			// //////////////////////////////////////////////////
		}

		private FileSpecFixedWidth GetFileSpec(List<IFieldSpec> fieldSpecs, string pathSpec)
		{
			return new FileSpecFixedWidth(this.IncludeHeaderRow, this.Delimiter, this.Encloser, this.FixedWidthPaddingCharacter, this.FixedWidthAddPadding, this.FixedWidthTruncate, fieldSpecs, this.Encoding, this.RecordsPerFileMin, this.RecordsPerFileMax, pathSpec, null);
		}

		private List<ExpandoObject> GenerateCCLF8()
		{
			List<IFieldSpec> fieldSpecs = CCLF8Specs.GetFieldSpecs();

			FileSpecFixedWidth fileSpec = GetFileSpec(fieldSpecs, PATHSPEC_CCLF8);

			Generator g = new Generator(this.OutputFolderRoot, this.DateStart, this.DateEnd, fileSpec, new WriterLocalFile());
			g.Run(true);

			return g.Results;
		}

		private List<ExpandoObject> GenerateCCLF1(List<Category> BENE_HIC_NUM)
		{
			List<IFieldSpec> fieldSpecs = CCLF1Specs.GetFieldSpecs(BENE_HIC_NUM);

			FileSpecFixedWidth fileSpec = GetFileSpec(fieldSpecs, PATHSPEC_CCLF1);

			Generator g =  new Generator(this.OutputFolderRoot, this.DateStart, this.DateEnd, fileSpec, new WriterLocalFile());
			g.Run(true);

			return g.Results;
		}

		private List<ExpandoObject> GenerateCCLF2(List<Category> CUR_CLM_UNIQ_ID, List<Category> BENE_HIC_NUM, List<Category> BENE_EQTBL_BIC_HICN_NUM, List<Category> PRVDR_OSCAR_NUM)
		{
			List<IFieldSpec> fieldSpecs = CCLF2Specs.GetFieldSpecs(CUR_CLM_UNIQ_ID, BENE_HIC_NUM, BENE_EQTBL_BIC_HICN_NUM, PRVDR_OSCAR_NUM);

			FileSpecFixedWidth fileSpec = GetFileSpec(fieldSpecs, PATHSPEC_CCLF2);

			Generator g =  new Generator(this.OutputFolderRoot, this.DateStart, this.DateEnd, fileSpec, new WriterLocalFile());
			g.Run(true);

			return g.Results;
		}

		private List<ExpandoObject> GenerateCCLF3(List<Category> CUR_CLM_UNIQ_ID, List<Category> BENE_HIC_NUM, List<Category> BENE_EQTBL_BIC_HICN_NUM, List<Category> PRNCPL_DGNS_CD, List<Category> PRVDR_OSCAR_NUM)
		{
			List<IFieldSpec> fieldSpecs = CCLF3Specs.GetFieldSpecs(CUR_CLM_UNIQ_ID, BENE_HIC_NUM, BENE_EQTBL_BIC_HICN_NUM, PRNCPL_DGNS_CD, PRVDR_OSCAR_NUM);

			FileSpecFixedWidth fileSpec = GetFileSpec(fieldSpecs, PATHSPEC_CCLF3);

			Generator g =  new Generator(this.OutputFolderRoot, this.DateStart, this.DateEnd, fileSpec, new WriterLocalFile());
			g.Run(true);

			return g.Results;
		}

		private List<ExpandoObject> GenerateCCLF4(List<Category> CUR_CLM_UNIQ_ID, List<Category> BENE_HIC_NUM, List<Category> BENE_EQTBL_BIC_HICN_NUM, List<Category> PRNCPL_DGNS_CD, List<Category> PRVDR_OSCAR_NUM)
		{
			List<IFieldSpec> fieldSpecs = CCLF4Specs.GetFieldSpecs(CUR_CLM_UNIQ_ID, BENE_HIC_NUM, BENE_EQTBL_BIC_HICN_NUM, PRNCPL_DGNS_CD, PRVDR_OSCAR_NUM);

			FileSpecFixedWidth fileSpec = GetFileSpec(fieldSpecs, PATHSPEC_CCLF4);

			Generator g =  new Generator(this.OutputFolderRoot, this.DateStart, this.DateEnd, fileSpec, new WriterLocalFile());
			g.Run(true);

			return g.Results;
		}

		private List<ExpandoObject> GenerateCCLF5(List<Category> CUR_CLM_UNIQ_ID, List<Category> BENE_HIC_NUM, List<Category> BENE_EQTBL_BIC_HICN_NUM, List<Category> PRNCPL_DGNS_CD)
		{
			List<IFieldSpec> fieldSpecs = CCLF5Specs.GetFieldSpecs(CUR_CLM_UNIQ_ID, BENE_HIC_NUM, BENE_EQTBL_BIC_HICN_NUM, PRNCPL_DGNS_CD);

			FileSpecFixedWidth fileSpec = GetFileSpec(fieldSpecs, PATHSPEC_CCLF5);

			Generator g =  new Generator(this.OutputFolderRoot, this.DateStart, this.DateEnd, fileSpec, new WriterLocalFile());
			g.Run(true);

			return g.Results;
		}

		private List<ExpandoObject> GenerateCCLF6(List<Category> CUR_CLM_UNIQ_ID, List<Category> BENE_HIC_NUM, List<Category> BENE_EQTBL_BIC_HICN_NUM)
		{
			List<IFieldSpec> fieldSpecs = CCLF6Specs.GetFieldSpecs(CUR_CLM_UNIQ_ID, BENE_HIC_NUM, BENE_EQTBL_BIC_HICN_NUM);

			FileSpecFixedWidth fileSpec = GetFileSpec(fieldSpecs, PATHSPEC_CCLF6);

			Generator g =  new Generator(this.OutputFolderRoot, this.DateStart, this.DateEnd, fileSpec, new WriterLocalFile());
			g.Run(true);

			return g.Results;
		}

		private List<ExpandoObject> GenerateCCLF7(List<Category> CUR_CLM_UNIQ_ID, List<Category> BENE_HIC_NUM)
		{
			List<IFieldSpec> fieldSpecs = CCLF7Specs.GetFieldSpecs(CUR_CLM_UNIQ_ID, BENE_HIC_NUM);

			FileSpecFixedWidth fileSpec = GetFileSpec(fieldSpecs, PATHSPEC_CCLF7);

			Generator g = new Generator(this.OutputFolderRoot, this.DateStart, this.DateEnd, fileSpec, new WriterLocalFile());
			g.Run(true);

			return g.Results;
		}

		private List<ExpandoObject> GenerateCCLF9(List<Category> BENE_HIC_NUM)
		{
			List<IFieldSpec> fieldSpecs = CCLF9Specs.GetFieldSpecs(BENE_HIC_NUM);

			FileSpecFixedWidth fileSpec = GetFileSpec(fieldSpecs, PATHSPEC_CCLF9);

			Generator g =  new Generator(this.OutputFolderRoot, this.DateStart, this.DateEnd, fileSpec, new WriterLocalFile());
			g.Run(true);

			return g.Results;
		}

		private List<ExpandoObject> GenerateCCLFA(List<Category> CUR_CLM_UNIQ_ID, DateTime dateStartClaimAdmission, DateTime dateEndClaimAdmission)
		{
			List<IFieldSpec> fieldSpecs = CCLFASpecs.GetFieldSpecs(CUR_CLM_UNIQ_ID, dateStartClaimAdmission, dateEndClaimAdmission);

			FileSpecFixedWidth fileSpec = GetFileSpec(fieldSpecs, PATHSPEC_CCLFA);

			Generator g =  new Generator(this.OutputFolderRoot, this.DateStart, this.DateEnd, fileSpec, new WriterLocalFile());
			g.Run(true);

			return g.Results;
		}

		private List<ExpandoObject> GenerateCCLFB()
		{
			List<IFieldSpec> fieldSpecs = CCLFBSpecs.GetFieldSpecs();

			FileSpecFixedWidth fileSpec = GetFileSpec(fieldSpecs, PATHSPEC_CCLFB);

			Generator g =  new Generator(this.OutputFolderRoot, this.DateStart, this.DateEnd, fileSpec, new WriterLocalFile());
			g.Run(true);

			return g.Results;
		}

		private void GenerateCCLF0
		(
			List<ExpandoObject> CCLF1S,
			List<ExpandoObject> CCLF2S,
			List<ExpandoObject> CCLF3S,
			List<ExpandoObject> CCLF4S,
			List<ExpandoObject> CCLF5S,
			List<ExpandoObject> CCLF6S,
			List<ExpandoObject> CCLF7S,
			List<ExpandoObject> CCLF8S,
			List<ExpandoObject> CCLF9S,
			List<ExpandoObject> CCLFAS,
			List<ExpandoObject> CCLFBS
		)
		{
			List<IFieldSpec> fieldSpecs = CCLF0Specs.GetFieldSpecs();

			FileSpecFixedWidth fileSpec = new FileSpecFixedWidth(false, "|", string.Empty, this.FixedWidthPaddingCharacter, Util.Location.AtEnd, Util.Location.AtEnd, fieldSpecs, this.Encoding, null, null, PATHSPEC_CCLF0, null);

			var foo = new ExpandoObject();
			var foo2 = foo as IDictionary<string, object>;
			foo2["bar"] = "gbaz";

			List<ExpandoObject> items = new List<ExpandoObject>
			{
				GetCCLF0Item(CCLFData.CCLF1, "Part A Claims Header File", CCLF1S.Count.ToString()),
				GetCCLF0Item(CCLFData.CCLF2, "Part A Claims Revenue Center Detail File", CCLF2S.Count.ToString()),
				GetCCLF0Item(CCLFData.CCLF3, "Part A Procedure Code File", CCLF3S.Count.ToString()),
				GetCCLF0Item(CCLFData.CCLF4, "Part A Diagnosis Code File", CCLF4S.Count.ToString()),
				GetCCLF0Item(CCLFData.CCLF5, "Part B Physicians File", CCLF5S.Count.ToString()),
				GetCCLF0Item(CCLFData.CCLF6, "Part B DME File", CCLF6S.Count.ToString()),
				GetCCLF0Item(CCLFData.CCLF7, "Part D File", CCLF7S.Count.ToString()),
				GetCCLF0Item(CCLFData.CCLF8, "Beneficiary Demographics File", CCLF8S.Count.ToString()),
				GetCCLF0Item(CCLFData.CCLF9, "BENE XREF File", CCLF9S.Count.ToString()),
				GetCCLF0Item(CCLFData.CCLFA, "Part A BE and Demo Codes File", CCLFAS.Count.ToString()),
				GetCCLF0Item(CCLFData.CCLFB, "Part B BE and Demo Codes File", CCLFBS.Count.ToString()),

			};

			Generator generator = new Generator(this.OutputFolderRoot, this.DateStart, this.DateEnd, fileSpec, new WriterLocalFile());

			generator.Run(items);
		}

		private ExpandoObject GetCCLF0Item(string fileType, string fileName, string numOfRecords)
		{
			ExpandoObject result = new ExpandoObject();

			IDictionary<string, object> resultKVP = result as IDictionary<string, object>;
			resultKVP[CCLFData.File_Type] = fileType;
			resultKVP[CCLFData.File_Name] = fileName;
			resultKVP[CCLFData.Number_Of_Records] = numOfRecords;
			resultKVP[CCLFData.Length_Of_Record] = "1";

			return result;
		}

		#region Utility

		/// <summary>
		/// Generate a simulated ICD-10 code.
		/// </summary>
		/// <returns></returns>
		public static string GetICD10Code()
		{
			return
				((char)(Converter.GetInt32(RNG.GetUniform(65, 90)))).ToString() +
				Converter.GetInt32(RNG.GetUniform(0, 10)).ToString() +
				Converter.GetInt32(RNG.GetUniform(0, 10)).ToString() +
				"." +
				Converter.GetInt32(RNG.GetUniform(0, 10)).ToString() +
				Converter.GetInt32(RNG.GetUniform(0, 10)).ToString() +
				(RNG.GetBoolean() ? CCLFData.LIST_ALPHACAPS_NUMERICS.ElementAt(Converter.GetInt32(RNG.GetUniform(0, CCLFData.LIST_ALPHACAPS_NUMERICS.Count - 1))).ToString() : string.Empty)
			;
		}

		#endregion
	}
}
