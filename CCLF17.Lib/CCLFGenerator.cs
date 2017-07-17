using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SynDataFileGen.Lib;
using pelazem.util;

namespace CCLF17.Lib
{
	public class CCLFGenerator
	{
		#region Constants

		public const string PATHSPEC_CCLF0 = "P.VATHN.ACO.CCLF0.D{yy}{mm}{dd}.T{hh}0000t.txt";
		public const string PATHSPEC_CCLF1 = "P.VATHN.ACO.CCLF1.D{yy}{mm}{dd}.T{hh}0000t.txt";
		public const string PATHSPEC_CCLF2 = "P.VATHN.ACO.CCLF2.D{yy}{mm}{dd}.T{hh}0000t.txt";
		public const string PATHSPEC_CCLF3 = "P.VATHN.ACO.CCLF3.D{yy}{mm}{dd}.T{hh}0000t.txt";
		public const string PATHSPEC_CCLF4 = "P.VATHN.ACO.CCLF4.D{yy}{mm}{dd}.T{hh}0000t.txt";
		public const string PATHSPEC_CCLF5 = "P.VATHN.ACO.CCLF5.D{yy}{mm}{dd}.T{hh}0000t.txt";
		public const string PATHSPEC_CCLF6 = "P.VATHN.ACO.CCLF6.D{yy}{mm}{dd}.T{hh}0000t.txt";
		public const string PATHSPEC_CCLF7 = "P.VATHN.ACO.CCLF7.D{yy}{mm}{dd}.T{hh}0000t.txt";
		public const string PATHSPEC_CCLF8 = "P.VATHN.ACO.CCLF8.D{yy}{mm}{dd}.T{hh}0000t.txt";
		public const string PATHSPEC_CCLF9 = "P.VATHN.ACO.CCLF9.D{yy}{mm}{dd}.T{hh}0000t.txt";
		public const string PATHSPEC_CCLFA = "P.VATHN.ACO.CCLFA.D{yy}{mm}{dd}.T{hh}0000t.txt";
		public const string PATHSPEC_CCLFB = "P.VATHN.ACO.CCLFB.D{yy}{mm}{dd}.T{hh}0000t.txt";

		#endregion

		#region Properties

		public string OutputFolderRoot = @"c:\test\cclf\";

		public bool IncludeHeaderRow { get; set; } = false;
		public string Delimiter { get; set; } = string.Empty;
		public string Encloser { get; set; } = string.Empty;
		public char FixedWidthPaddingCharacter { get; set; } = ' ';
		public Util.Location FixedWidthAddPadding { get; set; } = Util.Location.AtStart;
		public Util.Location FixedWidthTruncate { get; set; } = Util.Location.AtEnd;
		public Encoding Encoding { get; set; } = Encoding.UTF8;

		public int RecordsPerFileMin { get; set; } = 100;
		public int RecordsPerFileMax { get; set; } = 200;

		#endregion

		public void Run()
		{
			// //////////////////////////////////////////////////
			// CCLF8 -> CCLF1, CCLF5, CCLF6, CCLF7, CCLF9
			List<CCLF8> CCLF8S = GenerateList<CCLF8>(CCLF8Specs.GetFieldSpecs(), PATHSPEC_CCLF8);

			List<Category> BENE_HIC_NUM = CCLF8S.Select(c => c.BENE_HIC_NUM).Distinct().Select(d => new Category() { Value = d }).ToList();
			// //////////////////////////////////////////////////


			// //////////////////////////////////////////////////
			// CCLF1 -> CCLF2, CCLF3, CCLF4, CCLFA
			List<CCLF1> CCLF1S = GenerateCCLF1(PATHSPEC_CCLF1, BENE_HIC_NUM);

			List<Category> CUR_CLM_UNIQ_ID = CCLF1S.Select(c => c.CUR_CLM_UNIQ_ID).Distinct().Select(d => new Category() { Value = d }).ToList();

			List<Category> BENE_EQTBL_BIC_HICN_NUM = CCLF1S.Select(c => c.BENE_EQTBL_BIC_HICN_NUM).Distinct().Select(d => new Category() { Value = d }).ToList();

			List<Category> PRVDR_OSCAR_NUM = CCLF1S.Select(c => c.PRVDR_OSCAR_NUM).Distinct().Select(d => new Category() { Value = d }).ToList();

			List<Category> PRNCPL_DGNS_CD = CCLF1S.Select(c => c.PRNCPL_DGNS_CD).Distinct().Select(d => new Category() { Value = d }).ToList();
			// //////////////////////////////////////////////////


			// //////////////////////////////////////////////////
			// CCLF2
			List<CCLF2> CCLF2S = GenerateCCLF2(PATHSPEC_CCLF2, CUR_CLM_UNIQ_ID, BENE_HIC_NUM, BENE_EQTBL_BIC_HICN_NUM, PRVDR_OSCAR_NUM);
			// //////////////////////////////////////////////////


			// //////////////////////////////////////////////////
			// CCLF3
			List<CCLF3> CCLF3S = GenerateCCLF3(PATHSPEC_CCLF3, CUR_CLM_UNIQ_ID, BENE_HIC_NUM, BENE_EQTBL_BIC_HICN_NUM, PRNCPL_DGNS_CD, PRVDR_OSCAR_NUM);
			// //////////////////////////////////////////////////


			// //////////////////////////////////////////////////
			// CCLF4
			List<CCLF4> CCLF4S = GenerateCCLF4(PATHSPEC_CCLF4, CUR_CLM_UNIQ_ID, BENE_HIC_NUM, BENE_EQTBL_BIC_HICN_NUM, PRNCPL_DGNS_CD, PRVDR_OSCAR_NUM);
			// //////////////////////////////////////////////////


			// //////////////////////////////////////////////////
			// CCLF5 -> CCLFB
			List<CCLF5> CCLF5S = GenerateCCLF5(PATHSPEC_CCLF5, CUR_CLM_UNIQ_ID, BENE_HIC_NUM, BENE_EQTBL_BIC_HICN_NUM, PRNCPL_DGNS_CD);
			// //////////////////////////////////////////////////


			// //////////////////////////////////////////////////
			// CCLF6
			List<CCLF6> CCLF6S = GenerateCCLF6(PATHSPEC_CCLF6, CUR_CLM_UNIQ_ID, BENE_HIC_NUM, BENE_EQTBL_BIC_HICN_NUM);
			// //////////////////////////////////////////////////


			// //////////////////////////////////////////////////
			// CCLF7
			List<CCLF7> CCLF7S = GenerateCCLF7(PATHSPEC_CCLF7, CUR_CLM_UNIQ_ID, BENE_HIC_NUM);
			// //////////////////////////////////////////////////


			// //////////////////////////////////////////////////
			// CCLF9
			List<CCLF9> CCLF9S = GenerateCCLF9(PATHSPEC_CCLF9, BENE_HIC_NUM);
			// //////////////////////////////////////////////////


			// //////////////////////////////////////////////////
			// CCLFA
			List<CCLFA> CCLFAS = GenerateCCLFA(PATHSPEC_CCLFA, CUR_CLM_UNIQ_ID, new DateTime(2017, 1, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(2017, 3, 1, 0, 0, 0, DateTimeKind.Utc));
			// //////////////////////////////////////////////////


			// //////////////////////////////////////////////////
			// CCLFB
			List<CCLFB> CCLFBS = GenerateCCLFB(PATHSPEC_CCLFB);
			// //////////////////////////////////////////////////


			// //////////////////////////////////////////////////
			// CCLF0
			// Contains summary statistics for the above files
			GenerateCCLF0(PATHSPEC_CCLF0, CCLF1S, CCLF2S, CCLF3S, CCLF4S, CCLF5S, CCLF6S, CCLF7S, CCLF8S, CCLF9S, CCLFAS, CCLFBS);
			// //////////////////////////////////////////////////
		}

		private List<CCLF8> GenerateCCLF8(string pathSpec)
		{
			List<IFieldSpec> fieldSpecs = CCLF8Specs.GetFieldSpecs();

			FileSpecFixedWidth fileSpec = new FileSpecFixedWidth(this.IncludeHeaderRow, this.Delimiter, this.Encloser, this.FixedWidthPaddingCharacter, this.FixedWidthAddPadding, this.FixedWidthTruncate, fieldSpecs, this.Encoding, this.RecordsPerFileMin, this.RecordsPerFileMax, pathSpec);

			return new Generator(this.OutputFolderRoot, fileSpec).Run<CCLF8>();
		}

		private List<CCLF1> GenerateCCLF1(string pathSpec, List<Category> BENE_HIC_NUM)
		{
			List<IFieldSpec> fieldSpecs = CCLF1Specs.GetFieldSpecs(BENE_HIC_NUM);

			FileSpecFixedWidth fileSpec = new FileSpecFixedWidth(this.IncludeHeaderRow, this.Delimiter, this.Encloser, this.FixedWidthPaddingCharacter, this.FixedWidthAddPadding, this.FixedWidthTruncate, fieldSpecs, this.Encoding, this.RecordsPerFileMin, this.RecordsPerFileMax, pathSpec);

			return new Generator(this.OutputFolderRoot, fileSpec).Run<CCLF1>();
		}

		private List<CCLF2> GenerateCCLF2(string pathSpec, List<Category> CUR_CLM_UNIQ_ID, List<Category> BENE_HIC_NUM, List<Category> BENE_EQTBL_BIC_HICN_NUM, List<Category> PRVDR_OSCAR_NUM)
		{
			List<IFieldSpec> fieldSpecs = CCLF2Specs.GetFieldSpecs(CUR_CLM_UNIQ_ID, BENE_HIC_NUM, BENE_EQTBL_BIC_HICN_NUM, PRVDR_OSCAR_NUM);

			FileSpecFixedWidth fileSpec = new FileSpecFixedWidth(this.IncludeHeaderRow, this.Delimiter, this.Encloser, this.FixedWidthPaddingCharacter, this.FixedWidthAddPadding, this.FixedWidthTruncate, fieldSpecs, this.Encoding, this.RecordsPerFileMin, this.RecordsPerFileMax, pathSpec);

			return new Generator(this.OutputFolderRoot, fileSpec).Run<CCLF2>();
		}

		private List<CCLF3> GenerateCCLF3(string pathSpec, List<Category> CUR_CLM_UNIQ_ID, List<Category> BENE_HIC_NUM, List<Category> BENE_EQTBL_BIC_HICN_NUM, List<Category> PRNCPL_DGNS_CD, List<Category> PRVDR_OSCAR_NUM)
		{
			List<IFieldSpec> fieldSpecs = CCLF3Specs.GetFieldSpecs(CUR_CLM_UNIQ_ID, BENE_HIC_NUM, BENE_EQTBL_BIC_HICN_NUM, PRNCPL_DGNS_CD, PRVDR_OSCAR_NUM);

			FileSpecFixedWidth fileSpec = new FileSpecFixedWidth(this.IncludeHeaderRow, this.Delimiter, this.Encloser, this.FixedWidthPaddingCharacter, this.FixedWidthAddPadding, this.FixedWidthTruncate, fieldSpecs, this.Encoding, this.RecordsPerFileMin, this.RecordsPerFileMax, pathSpec);

			return new Generator(this.OutputFolderRoot, fileSpec).Run<CCLF3>();
		}

		private List<CCLF4> GenerateCCLF4(string pathSpec, List<Category> CUR_CLM_UNIQ_ID, List<Category> BENE_HIC_NUM, List<Category> BENE_EQTBL_BIC_HICN_NUM, List<Category> PRNCPL_DGNS_CD, List<Category> PRVDR_OSCAR_NUM)
		{
			List<IFieldSpec> fieldSpecs = CCLF4Specs.GetFieldSpecs(CUR_CLM_UNIQ_ID, BENE_HIC_NUM, BENE_EQTBL_BIC_HICN_NUM, PRNCPL_DGNS_CD, PRVDR_OSCAR_NUM);

			FileSpecFixedWidth fileSpec = new FileSpecFixedWidth(this.IncludeHeaderRow, this.Delimiter, this.Encloser, this.FixedWidthPaddingCharacter, this.FixedWidthAddPadding, this.FixedWidthTruncate, fieldSpecs, this.Encoding, this.RecordsPerFileMin, this.RecordsPerFileMax, pathSpec);

			return new Generator(this.OutputFolderRoot, fileSpec).Run<CCLF4>();
		}

		private List<CCLF5> GenerateCCLF5(string pathSpec, List<Category> CUR_CLM_UNIQ_ID, List<Category> BENE_HIC_NUM, List<Category> BENE_EQTBL_BIC_HICN_NUM, List<Category> PRNCPL_DGNS_CD)
		{
			List<IFieldSpec> fieldSpecs = CCLF5Specs.GetFieldSpecs(CUR_CLM_UNIQ_ID, BENE_HIC_NUM, BENE_EQTBL_BIC_HICN_NUM, PRNCPL_DGNS_CD);

			FileSpecFixedWidth fileSpec = new FileSpecFixedWidth(this.IncludeHeaderRow, this.Delimiter, this.Encloser, this.FixedWidthPaddingCharacter, this.FixedWidthAddPadding, this.FixedWidthTruncate, fieldSpecs, this.Encoding, this.RecordsPerFileMin, this.RecordsPerFileMax, pathSpec);

			return new Generator(this.OutputFolderRoot, fileSpec).Run<CCLF5>();
		}

		private List<CCLF6> GenerateCCLF6(string pathSpec, List<Category> CUR_CLM_UNIQ_ID, List<Category> BENE_HIC_NUM, List<Category> BENE_EQTBL_BIC_HICN_NUM)
		{
			List<IFieldSpec> fieldSpecs = CCLF6Specs.GetFieldSpecs(CUR_CLM_UNIQ_ID, BENE_HIC_NUM, BENE_EQTBL_BIC_HICN_NUM);

			FileSpecFixedWidth fileSpec = new FileSpecFixedWidth(this.IncludeHeaderRow, this.Delimiter, this.Encloser, this.FixedWidthPaddingCharacter, this.FixedWidthAddPadding, this.FixedWidthTruncate, fieldSpecs, this.Encoding, this.RecordsPerFileMin, this.RecordsPerFileMax, pathSpec);

			return new Generator(this.OutputFolderRoot, fileSpec).Run<CCLF6>();
		}

		private List<CCLF7> GenerateCCLF7(string pathSpec, List<Category> CUR_CLM_UNIQ_ID, List<Category> BENE_HIC_NUM)
		{
			List<IFieldSpec> fieldSpecs = CCLF7Specs.GetFieldSpecs(CUR_CLM_UNIQ_ID, BENE_HIC_NUM);

			FileSpecFixedWidth fileSpec = new FileSpecFixedWidth(this.IncludeHeaderRow, this.Delimiter, this.Encloser, this.FixedWidthPaddingCharacter, this.FixedWidthAddPadding, this.FixedWidthTruncate, fieldSpecs, this.Encoding, this.RecordsPerFileMin, this.RecordsPerFileMax, pathSpec);

			return new Generator(this.OutputFolderRoot, fileSpec).Run<CCLF7>();
		}

		private List<CCLF9> GenerateCCLF9(string pathSpec, List<Category> BENE_HIC_NUM)
		{
			List<IFieldSpec> fieldSpecs = CCLF9Specs.GetFieldSpecs(BENE_HIC_NUM);

			FileSpecFixedWidth fileSpec = new FileSpecFixedWidth(this.IncludeHeaderRow, this.Delimiter, this.Encloser, this.FixedWidthPaddingCharacter, this.FixedWidthAddPadding, this.FixedWidthTruncate, fieldSpecs, this.Encoding, this.RecordsPerFileMin, this.RecordsPerFileMax, pathSpec);

			return new Generator(this.OutputFolderRoot, fileSpec).Run<CCLF9>();
		}

		private List<CCLFA> GenerateCCLFA(string pathSpec, List<Category> CUR_CLM_UNIQ_ID, DateTime dateStartClaimAdmission, DateTime dateEndClaimAdmission)
		{
			List<IFieldSpec> fieldSpecs = CCLFASpecs.GetFieldSpecs(CUR_CLM_UNIQ_ID, dateStartClaimAdmission, dateEndClaimAdmission);

			FileSpecFixedWidth fileSpec = new FileSpecFixedWidth(this.IncludeHeaderRow, this.Delimiter, this.Encloser, this.FixedWidthPaddingCharacter, this.FixedWidthAddPadding, this.FixedWidthTruncate, fieldSpecs, this.Encoding, this.RecordsPerFileMin, this.RecordsPerFileMax, pathSpec);

			return new Generator(this.OutputFolderRoot, fileSpec).Run<CCLFA>();
		}

		private List<CCLFB> GenerateCCLFB(string pathSpec)
		{
			List<IFieldSpec> fieldSpecs = CCLFBSpecs.GetFieldSpecs();

			FileSpecFixedWidth fileSpec = new FileSpecFixedWidth(this.IncludeHeaderRow, this.Delimiter, this.Encloser, this.FixedWidthPaddingCharacter, this.FixedWidthAddPadding, this.FixedWidthTruncate, fieldSpecs, this.Encoding, this.RecordsPerFileMin, this.RecordsPerFileMax, pathSpec);

			return new Generator(this.OutputFolderRoot, fileSpec).Run<CCLFB>();
		}

		private void GenerateCCLF0
		(
			string pathSpec,
			List<CCLF1> CCLF1S,
			List<CCLF2> CCLF2S,
			List<CCLF3> CCLF3S,
			List<CCLF4> CCLF4S,
			List<CCLF5> CCLF5S,
			List<CCLF6> CCLF6S,
			List<CCLF7> CCLF7S,
			List<CCLF8> CCLF8S,
			List<CCLF9> CCLF9S,
			List<CCLFA> CCLFAS,
			List<CCLFB> CCLFBS
		)
		{
			List<IFieldSpec> fieldSpecs = CCLF0Specs.GetFieldSpecs();

			FileSpecFixedWidth fileSpec = new FileSpecFixedWidth(false, "|", string.Empty, this.FixedWidthPaddingCharacter, Util.Location.AtEnd, Util.Location.AtEnd, fieldSpecs, this.Encoding, null, null, pathSpec);

			List<CCLF0> items = new List<CCLF0>
			{
				// CCLF1
				new CCLF0() { File_Type = nameof(CCLF1), File_Name = "Part A Claims Header File", Number_Of_Records = CCLF1S.Count.ToString(), Length_Of_Record = "1" },

				// CCLF2
				new CCLF0() { File_Type = nameof(CCLF2), File_Name = "Part A Claims Revenue Center Detail File", Number_Of_Records = CCLF2S.Count.ToString(), Length_Of_Record = "1" },

				// CCLF3
				new CCLF0() { File_Type = nameof(CCLF3), File_Name = "Part A Procedure Code File", Number_Of_Records = CCLF3S.Count.ToString(), Length_Of_Record = "1" },

				// CCLF4
				new CCLF0() { File_Type = nameof(CCLF4), File_Name = "Part A Diagnosis Code File", Number_Of_Records = CCLF4S.Count.ToString(), Length_Of_Record = "1" },

				// CCLF5
				new CCLF0() { File_Type = nameof(CCLF5), File_Name = "Part B Physicians File", Number_Of_Records = CCLF5S.Count.ToString(), Length_Of_Record = "1" },

				// CCLF6
				new CCLF0() { File_Type = nameof(CCLF6), File_Name = "Part B DME File", Number_Of_Records = CCLF6S.Count.ToString(), Length_Of_Record = "1" },

				// CCLF7
				new CCLF0() { File_Type = nameof(CCLF7), File_Name = "Part D File", Number_Of_Records = CCLF7S.Count.ToString(), Length_Of_Record = "1" },

				// CCLF8
				new CCLF0() { File_Type = nameof(CCLF8), File_Name = "Beneficiary Demographics File", Number_Of_Records = CCLF8S.Count.ToString(), Length_Of_Record = "1" },

				// CCLF9
				new CCLF0() { File_Type = nameof(CCLF9), File_Name = "BENE XREF File", Number_Of_Records = CCLF9S.Count.ToString(), Length_Of_Record = "1" },

				// CCLFA
				new CCLF0() { File_Type = nameof(CCLFA), File_Name = "Part A BE and Demo Codes File", Number_Of_Records = CCLFAS.Count.ToString(), Length_Of_Record = "1" },

				// CCLFB
				new CCLF0() { File_Type = nameof(CCLFB), File_Name = "Part B BE and Demo Codes File", Number_Of_Records = CCLFBS.Count.ToString(), Length_Of_Record = "1" }
			};

			Generator generator = new Generator(this.OutputFolderRoot, fileSpec);

			generator.Run(items);
		}


		private List<T> GenerateList<T>(List<IFieldSpec> fieldSpecs, string pathSpec)
			where T : new()
		{
			FileSpecFixedWidth fileSpec = new FileSpecFixedWidth(this.IncludeHeaderRow, this.Delimiter, this.Encloser, this.FixedWidthPaddingCharacter, this.FixedWidthAddPadding, this.FixedWidthTruncate, fieldSpecs, this.Encoding, this.RecordsPerFileMin, this.RecordsPerFileMax, pathSpec);

			return new Generator(this.OutputFolderRoot, fileSpec).Run<T>();
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
