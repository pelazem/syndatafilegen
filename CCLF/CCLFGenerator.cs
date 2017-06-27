using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Generator;
using pelazem.Common;

namespace CCLF
{
	public class CCLFGenerator
	{
		#region Properties

		public GeneratorConfig Config { get; private set; }

		#endregion

		#region Constructors

		private CCLFGenerator() { }

		public CCLFGenerator(GeneratorConfig config)
		{
			this.Config = config;
		}

		#endregion

		public void Run()
		{
			// //////////////////////////////////////////////////
			// CCLF8 -> CCLF1, CCLF5, CCLF6, CCLF7, CCLF9
			List<CCLF8> CCLF8S = GenerateCCLF8("P.VATHN.ACO.CCLF8.D{yy}{mm}{dd}.T{hh}0000t.txt");

			List<Category> BENE_HIC_NUM = CCLF8S.Select(c => c.BENE_HIC_NUM).Distinct().Select(d => new Category() { Value = d }).ToList();
			// //////////////////////////////////////////////////


			// //////////////////////////////////////////////////
			// CCLF1 -> CCLF2, CCLF3, CCLF4, CCLFA
			List<CCLF1> CCLF1S = GenerateCCLF1("P.VATHN.ACO.CCLF1.D{yy}{mm}{dd}.T{hh}0000t.txt", BENE_HIC_NUM);

			List<Category> CUR_CLM_UNIQ_ID = CCLF1S.Select(c => c.CUR_CLM_UNIQ_ID).Distinct().Select(d => new Category() { Value = d }).ToList();

			List<Category> BENE_EQTBL_BIC_HICN_NUM = CCLF1S.Select(c => c.BENE_EQTBL_BIC_HICN_NUM).Distinct().Select(d => new Category() { Value = d }).ToList();

			List<Category> PRVDR_OSCAR_NUM = CCLF1S.Select(c => c.PRVDR_OSCAR_NUM).Distinct().Select(d => new Category() { Value = d }).ToList();

			List<Category> PRNCPL_DGNS_CD = CCLF1S.Select(c => c.PRNCPL_DGNS_CD).Distinct().Select(d => new Category() { Value = d }).ToList();
			// //////////////////////////////////////////////////


			// //////////////////////////////////////////////////
			// CCLF2
			List<CCLF2> CCLF2S = GenerateCCLF2("P.VATHN.ACO.CCLF2.D{yy}{mm}{dd}.T{hh}0000t.txt", CUR_CLM_UNIQ_ID, BENE_HIC_NUM, BENE_EQTBL_BIC_HICN_NUM, PRVDR_OSCAR_NUM);
			// //////////////////////////////////////////////////


			// //////////////////////////////////////////////////
			// CCLF3
			List<CCLF3> CCLF3S = GenerateCCLF3("P.VATHN.ACO.CCLF3.D{yy}{mm}{dd}.T{hh}0000t.txt", CUR_CLM_UNIQ_ID, BENE_HIC_NUM, BENE_EQTBL_BIC_HICN_NUM, PRNCPL_DGNS_CD, PRVDR_OSCAR_NUM);
			// //////////////////////////////////////////////////


			// //////////////////////////////////////////////////
			// CCLF4
			List<CCLF4> CCLF4S = GenerateCCLF4("P.VATHN.ACO.CCLF4.D{yy}{mm}{dd}.T{hh}0000t.txt", CUR_CLM_UNIQ_ID, BENE_HIC_NUM, BENE_EQTBL_BIC_HICN_NUM, PRNCPL_DGNS_CD, PRVDR_OSCAR_NUM);
			// //////////////////////////////////////////////////


			// //////////////////////////////////////////////////
			// CCLF5 -> CCLFB
			List<CCLF5> CCLF5S = GenerateCCLF5("P.VATHN.ACO.CCLF5.D{yy}{mm}{dd}.T{hh}0000t.txt", CUR_CLM_UNIQ_ID, BENE_HIC_NUM, BENE_EQTBL_BIC_HICN_NUM, PRNCPL_DGNS_CD);
			// //////////////////////////////////////////////////


			// //////////////////////////////////////////////////
			// CCLF6
			List<CCLF6> CCLF6S = GenerateCCLF6("P.VATHN.ACO.CCLF6.D{yy}{mm}{dd}.T{hh}0000t.txt", CUR_CLM_UNIQ_ID, BENE_HIC_NUM, BENE_EQTBL_BIC_HICN_NUM);
			// //////////////////////////////////////////////////


			// //////////////////////////////////////////////////
			// CCLF7
			List<CCLF7> CCLF7S = GenerateCCLF7("P.VATHN.ACO.CCLF7.D{yy}{mm}{dd}.T{hh}0000t.txt", CUR_CLM_UNIQ_ID, BENE_HIC_NUM);
			// //////////////////////////////////////////////////


			// //////////////////////////////////////////////////
			// CCLF9
			List<CCLF9> CCLF9S = GenerateCCLF9("P.VATHN.ACO.CCLF9.D{yy}{mm}{dd}.T{hh}0000t.txt", BENE_HIC_NUM);
			// //////////////////////////////////////////////////


			// //////////////////////////////////////////////////
			// CCLFA
			List<CCLFA> CCLFAS = GenerateCCLFA("P.VATHN.ACO.CCLFA.D{yy}{mm}{dd}.T{hh}0000t.txt", CUR_CLM_UNIQ_ID, new DateTime(2017, 1, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(2017, 3, 1, 0, 0, 0, DateTimeKind.Utc));
			// //////////////////////////////////////////////////


			// //////////////////////////////////////////////////
			// CCLFB
			List<CCLFB> CCLFBS = GenerateCCLFB("P.VATHN.ACO.CCLFB.D{yy}{mm}{dd}.T{hh}0000t.txt");
			// //////////////////////////////////////////////////


			// //////////////////////////////////////////////////
			// CCLF0
			// Contains summary statistics for the above files
			GenerateCCLF0("P.VATHN.ACO.CCLF0.D{yy}{mm}{dd}.T{hh}0000t.txt", CCLF1S, CCLF2S, CCLF3S, CCLF4S, CCLF5S, CCLF6S, CCLF7S, CCLF8S, CCLF9S, CCLFAS, CCLFBS);
			// //////////////////////////////////////////////////
		}

		private List<CCLF8> GenerateCCLF8(string pathSpec)
		{
			List<IFieldSpec<CCLF8>> fieldSpecs = CCLF8Specs.GetFieldSpecs();

			FileSpecFixedWidth<CCLF8> fileSpec = new FileSpecFixedWidth<CCLF8>(this.Config.IncludeHeaderRow, this.Config.Delimiter, this.Config.Encloser, this.Config.PaddingCharacter, this.Config.PadAt, this.Config.TruncateAt, fieldSpecs, this.Config.Encoding, this.Config.RecordsPerFileMin, this.Config.RecordsPerFileMax);

			Generator<CCLF8> generator = new Generator<CCLF8>
			(
				this.Config.OutputFolderRoot,
				pathSpec,
				fileSpec,
				fieldSpecs,
				null,
				null,
				null
			);

			return generator.Run().ToList();
		}

		private List<CCLF1> GenerateCCLF1(string pathSpec, List<Category> BENE_HIC_NUM)
		{
			List<IFieldSpec<CCLF1>> fieldSpecs = CCLF1Specs.GetFieldSpecs(BENE_HIC_NUM);

			FileSpecFixedWidth<CCLF1> fileSpec = new FileSpecFixedWidth<CCLF1>(this.Config.IncludeHeaderRow, this.Config.Delimiter, this.Config.Encloser, this.Config.PaddingCharacter, this.Config.PadAt, this.Config.TruncateAt, fieldSpecs, this.Config.Encoding, this.Config.RecordsPerFileMin, this.Config.RecordsPerFileMax);

			Generator<CCLF1> generator = new Generator<CCLF1>
			(
				this.Config.OutputFolderRoot,
				pathSpec,
				fileSpec,
				fieldSpecs,
				null,
				null,
				null
			);

			return generator.Run().ToList();
		}

		private List<CCLF2> GenerateCCLF2(string pathSpec, List<Category> CUR_CLM_UNIQ_ID, List<Category> BENE_HIC_NUM, List<Category> BENE_EQTBL_BIC_HICN_NUM, List<Category> PRVDR_OSCAR_NUM)
		{
			List<IFieldSpec<CCLF2>> fieldSpecs = CCLF2Specs.GetFieldSpecs(CUR_CLM_UNIQ_ID, BENE_HIC_NUM, BENE_EQTBL_BIC_HICN_NUM, PRVDR_OSCAR_NUM);

			FileSpecFixedWidth<CCLF2> fileSpec = new FileSpecFixedWidth<CCLF2>(this.Config.IncludeHeaderRow, this.Config.Delimiter, this.Config.Encloser, this.Config.PaddingCharacter, this.Config.PadAt, this.Config.TruncateAt, fieldSpecs, this.Config.Encoding, this.Config.RecordsPerFileMin, this.Config.RecordsPerFileMax);

			Generator<CCLF2> generator = new Generator<CCLF2>
			(
				this.Config.OutputFolderRoot,
				pathSpec,
				fileSpec,
				fieldSpecs,
				null,
				null,
				null
			);

			return generator.Run().ToList();
		}

		private List<CCLF3> GenerateCCLF3(string pathSpec, List<Category> CUR_CLM_UNIQ_ID, List<Category> BENE_HIC_NUM, List<Category> BENE_EQTBL_BIC_HICN_NUM, List<Category> PRNCPL_DGNS_CD, List<Category> PRVDR_OSCAR_NUM)
		{
			List<IFieldSpec<CCLF3>> fieldSpecs = CCLF3Specs.GetFieldSpecs(CUR_CLM_UNIQ_ID, BENE_HIC_NUM, BENE_EQTBL_BIC_HICN_NUM, PRNCPL_DGNS_CD, PRVDR_OSCAR_NUM);

			FileSpecFixedWidth<CCLF3> fileSpec = new FileSpecFixedWidth<CCLF3>(this.Config.IncludeHeaderRow, this.Config.Delimiter, this.Config.Encloser, this.Config.PaddingCharacter, this.Config.PadAt, this.Config.TruncateAt, fieldSpecs, this.Config.Encoding, this.Config.RecordsPerFileMin, this.Config.RecordsPerFileMax);

			Generator<CCLF3> generator = new Generator<CCLF3>
			(
				this.Config.OutputFolderRoot,
				pathSpec,
				fileSpec,
				fieldSpecs,
				null,
				null,
				null
			);

			return generator.Run().ToList();
		}

		private List<CCLF4> GenerateCCLF4(string pathSpec, List<Category> CUR_CLM_UNIQ_ID, List<Category> BENE_HIC_NUM, List<Category> BENE_EQTBL_BIC_HICN_NUM, List<Category> PRNCPL_DGNS_CD, List<Category> PRVDR_OSCAR_NUM)
		{
			List<IFieldSpec<CCLF4>> fieldSpecs = CCLF4Specs.GetFieldSpecs(CUR_CLM_UNIQ_ID, BENE_HIC_NUM, BENE_EQTBL_BIC_HICN_NUM, PRNCPL_DGNS_CD, PRVDR_OSCAR_NUM);

			FileSpecFixedWidth<CCLF4> fileSpec = new FileSpecFixedWidth<CCLF4>(this.Config.IncludeHeaderRow, this.Config.Delimiter, this.Config.Encloser, this.Config.PaddingCharacter, this.Config.PadAt, this.Config.TruncateAt, fieldSpecs, this.Config.Encoding, this.Config.RecordsPerFileMin, this.Config.RecordsPerFileMax);

			Generator<CCLF4> generator = new Generator<CCLF4>
			(
				this.Config.OutputFolderRoot,
				pathSpec,
				fileSpec,
				fieldSpecs,
				null,
				null,
				null
			);

			return generator.Run().ToList();
		}

		private List<CCLF5> GenerateCCLF5(string pathSpec, List<Category> CUR_CLM_UNIQ_ID, List<Category> BENE_HIC_NUM, List<Category> BENE_EQTBL_BIC_HICN_NUM, List<Category> PRNCPL_DGNS_CD)
		{
			List<IFieldSpec<CCLF5>> fieldSpecs = CCLF5Specs.GetFieldSpecs(CUR_CLM_UNIQ_ID, BENE_HIC_NUM, BENE_EQTBL_BIC_HICN_NUM, PRNCPL_DGNS_CD);

			FileSpecFixedWidth<CCLF5> fileSpec = new FileSpecFixedWidth<CCLF5>(this.Config.IncludeHeaderRow, this.Config.Delimiter, this.Config.Encloser, this.Config.PaddingCharacter, this.Config.PadAt, this.Config.TruncateAt, fieldSpecs, this.Config.Encoding, this.Config.RecordsPerFileMin, this.Config.RecordsPerFileMax);

			Generator<CCLF5> generator = new Generator<CCLF5>
			(
				this.Config.OutputFolderRoot,
				pathSpec,
				fileSpec,
				fieldSpecs,
				null,
				null,
				null
			);

			return generator.Run().ToList();
		}

		private List<CCLF6> GenerateCCLF6(string pathSpec, List<Category> CUR_CLM_UNIQ_ID, List<Category> BENE_HIC_NUM, List<Category> BENE_EQTBL_BIC_HICN_NUM)
		{
			List<IFieldSpec<CCLF6>> fieldSpecs = CCLF6Specs.GetFieldSpecs(CUR_CLM_UNIQ_ID, BENE_HIC_NUM, BENE_EQTBL_BIC_HICN_NUM);

			FileSpecFixedWidth<CCLF6> fileSpec = new FileSpecFixedWidth<CCLF6>(this.Config.IncludeHeaderRow, this.Config.Delimiter, this.Config.Encloser, this.Config.PaddingCharacter, this.Config.PadAt, this.Config.TruncateAt, fieldSpecs, this.Config.Encoding, this.Config.RecordsPerFileMin, this.Config.RecordsPerFileMax);

			Generator<CCLF6> generator = new Generator<CCLF6>
			(
				this.Config.OutputFolderRoot,
				pathSpec,
				fileSpec,
				fieldSpecs,
				null,
				null,
				null
			);

			return generator.Run().ToList();
		}

		private List<CCLF7> GenerateCCLF7(string pathSpec, List<Category> CUR_CLM_UNIQ_ID, List<Category> BENE_HIC_NUM)
		{
			List<IFieldSpec<CCLF7>> fieldSpecs = CCLF7Specs.GetFieldSpecs(CUR_CLM_UNIQ_ID, BENE_HIC_NUM);

			FileSpecFixedWidth<CCLF7> fileSpec = new FileSpecFixedWidth<CCLF7>(this.Config.IncludeHeaderRow, this.Config.Delimiter, this.Config.Encloser, this.Config.PaddingCharacter, this.Config.PadAt, this.Config.TruncateAt, fieldSpecs, this.Config.Encoding, this.Config.RecordsPerFileMin, this.Config.RecordsPerFileMax);

			Generator<CCLF7> generator = new Generator<CCLF7>
			(
				this.Config.OutputFolderRoot,
				pathSpec,
				fileSpec,
				fieldSpecs,
				null,
				null,
				null
			);

			return generator.Run().ToList();
		}

		private List<CCLF9> GenerateCCLF9(string pathSpec, List<Category> BENE_HIC_NUM)
		{
			List<IFieldSpec<CCLF9>> fieldSpecs = CCLF9Specs.GetFieldSpecs(BENE_HIC_NUM);

			FileSpecFixedWidth<CCLF9> fileSpec = new FileSpecFixedWidth<CCLF9>(this.Config.IncludeHeaderRow, this.Config.Delimiter, this.Config.Encloser, this.Config.PaddingCharacter, this.Config.PadAt, this.Config.TruncateAt, fieldSpecs, this.Config.Encoding, this.Config.RecordsPerFileMin, this.Config.RecordsPerFileMax);

			Generator<CCLF9> generator = new Generator<CCLF9>
			(
				this.Config.OutputFolderRoot,
				pathSpec,
				fileSpec,
				fieldSpecs,
				null,
				null,
				null
			);

			return generator.Run().ToList();
		}

		private List<CCLFA> GenerateCCLFA(string pathSpec, List<Category> CUR_CLM_UNIQ_ID, DateTime dateStartClaimAdmission, DateTime dateEndClaimAdmission)
		{
			List<IFieldSpec<CCLFA>> fieldSpecs = CCLFASpecs.GetFieldSpecs(CUR_CLM_UNIQ_ID, dateStartClaimAdmission, dateEndClaimAdmission);

			FileSpecFixedWidth<CCLFA> fileSpec = new FileSpecFixedWidth<CCLFA>(this.Config.IncludeHeaderRow, this.Config.Delimiter, this.Config.Encloser, this.Config.PaddingCharacter, this.Config.PadAt, this.Config.TruncateAt, fieldSpecs, this.Config.Encoding, this.Config.RecordsPerFileMin, this.Config.RecordsPerFileMax);

			Generator<CCLFA> generator = new Generator<CCLFA>
			(
				this.Config.OutputFolderRoot,
				pathSpec,
				fileSpec,
				fieldSpecs,
				null,
				null,
				null
			);

			return generator.Run().ToList();
		}

		private List<CCLFB> GenerateCCLFB(string pathSpec)
		{
			List<IFieldSpec<CCLFB>> fieldSpecs = CCLFBSpecs.GetFieldSpecs();

			FileSpecFixedWidth<CCLFB> fileSpec = new FileSpecFixedWidth<CCLFB>(this.Config.IncludeHeaderRow, this.Config.Delimiter, this.Config.Encloser, this.Config.PaddingCharacter, this.Config.PadAt, this.Config.TruncateAt, fieldSpecs, this.Config.Encoding, this.Config.RecordsPerFileMin, this.Config.RecordsPerFileMax);

			Generator<CCLFB> generator = new Generator<CCLFB>
			(
				this.Config.OutputFolderRoot,
				pathSpec,
				fileSpec,
				fieldSpecs,
				null,
				null,
				null
			);

			return generator.Run().ToList();
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
			List<IFieldSpec<CCLF0>> fieldSpecs = CCLF0Specs.GetFieldSpecs();

			FileSpecFixedWidth<CCLF0> fileSpec = new FileSpecFixedWidth<CCLF0>(false, "|", string.Empty, this.Config.PaddingCharacter, Util.Location.AtEnd, Util.Location.AtEnd, fieldSpecs, this.Config.Encoding, null, null);

			List<CCLF0> items = new List<CCLF0>();

			// CCLF1
			items.Add(new CCLF0() { File_Type = nameof(CCLF1), File_Name = "Part A Claims Header File", Number_Of_Records = CCLF1S.Count.ToString(), Length_Of_Record = "1" });

			// CCLF2
			items.Add(new CCLF0() { File_Type = nameof(CCLF2), File_Name = "Part A Claims Revenue Center Detail File", Number_Of_Records = CCLF2S.Count.ToString(), Length_Of_Record = "1" });

			// CCLF3
			items.Add(new CCLF0() { File_Type = nameof(CCLF3), File_Name = "Part A Procedure Code File", Number_Of_Records = CCLF3S.Count.ToString(), Length_Of_Record = "1" });

			// CCLF4
			items.Add(new CCLF0() { File_Type = nameof(CCLF4), File_Name = "Part A Diagnosis Code File", Number_Of_Records = CCLF4S.Count.ToString(), Length_Of_Record = "1" });

			// CCLF5
			items.Add(new CCLF0() { File_Type = nameof(CCLF5), File_Name = "Part B Physicians File", Number_Of_Records = CCLF5S.Count.ToString(), Length_Of_Record = "1" });

			// CCLF6
			items.Add(new CCLF0() { File_Type = nameof(CCLF6), File_Name = "Part B DME File", Number_Of_Records = CCLF6S.Count.ToString(), Length_Of_Record = "1" });

			// CCLF7
			items.Add(new CCLF0() { File_Type = nameof(CCLF7), File_Name = "Part D File", Number_Of_Records = CCLF7S.Count.ToString(), Length_Of_Record = "1" });

			// CCLF8
			items.Add(new CCLF0() { File_Type = nameof(CCLF8), File_Name = "Beneficiary Demographics File", Number_Of_Records = CCLF8S.Count.ToString(), Length_Of_Record = "1" });

			// CCLF9
			items.Add(new CCLF0() { File_Type = nameof(CCLF9), File_Name = "BENE XREF File", Number_Of_Records = CCLF9S.Count.ToString(), Length_Of_Record = "1" });

			// CCLFA
			items.Add(new CCLF0() { File_Type = nameof(CCLFA), File_Name = "Part A BE and Demo Codes File", Number_Of_Records = CCLFAS.Count.ToString(), Length_Of_Record = "1" });

			// CCLFB
			items.Add(new CCLF0() { File_Type = nameof(CCLFB), File_Name = "Part B BE and Demo Codes File", Number_Of_Records = CCLFBS.Count.ToString(), Length_Of_Record = "1" });


			Generator<CCLF0> generator = new Generator<CCLF0>(this.Config.OutputFolderRoot, pathSpec, fileSpec, fieldSpecs);

			generator.Run(items);
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