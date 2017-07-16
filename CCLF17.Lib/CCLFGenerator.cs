using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SynDataFileGen.Lib;

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



		private List<CCLF8> GenerateCCLF8()
		{
			List<IFieldSpec> fieldSpecs = CCLF8Specs.GetFieldSpecs();

			FileSpecFixedWidth fileSpec = new FileSpecFixedWidth(this.IncludeHeaderRow, this.Delimiter, this.Encloser, this.FixedWidthPaddingCharacter, this.FixedWidthAddPadding, this.FixedWidthTruncate, fieldSpecs, this.Encoding, this.RecordsPerFileMin, this.RecordsPerFileMax, PATHSPEC_CCLF8);

			Generator generator = new Generator(this.OutputFolderRoot, fileSpec);

			generator.Run();

			List<CCLF8> result = generator.GetResults<CCLF8>();

			return result;
		}

		private List<CCLF1> GenerateCCLF1(List<Category> BENE_HIC_NUM)
		{
			List<IFieldSpec> fieldSpecs = CCLF1Specs.GetFieldSpecs(BENE_HIC_NUM);

			FileSpecFixedWidth fileSpec = new FileSpecFixedWidth(this.IncludeHeaderRow, this.Delimiter, this.Encloser, this.FixedWidthPaddingCharacter, this.FixedWidthAddPadding, this.FixedWidthTruncate, fieldSpecs, this.Encoding, this.RecordsPerFileMin, this.RecordsPerFileMax, PATHSPEC_CCLF1);

			Generator generator = new Generator(this.OutputFolderRoot, fileSpec);

			generator.Run();

			return generator.Run().ToList();
		}

		private List<CCLF2> GenerateCCLF2(List<Category> CUR_CLM_UNIQ_ID, List<Category> BENE_HIC_NUM, List<Category> BENE_EQTBL_BIC_HICN_NUM, List<Category> PRVDR_OSCAR_NUM)
		{
			List<IFieldSpec> fieldSpecs = CCLF2Specs.GetFieldSpecs(CUR_CLM_UNIQ_ID, BENE_HIC_NUM, BENE_EQTBL_BIC_HICN_NUM, PRVDR_OSCAR_NUM);

			FileSpecFixedWidth fileSpec = new FileSpecFixedWidth(this.IncludeHeaderRow, this.Delimiter, this.Encloser, this.FixedWidthPaddingCharacter, this.FixedWidthAddPadding, this.FixedWidthTruncate, fieldSpecs, this.Encoding, this.RecordsPerFileMin, this.RecordsPerFileMax, PATHSPEC_CCLF2);

			Generator generator = new Generator(this.OutputFolderRoot, fileSpec);

			return generator.Run().ToList();
		}

		private List<CCLF3> GenerateCCLF3(List<Category> CUR_CLM_UNIQ_ID, List<Category> BENE_HIC_NUM, List<Category> BENE_EQTBL_BIC_HICN_NUM, List<Category> PRNCPL_DGNS_CD, List<Category> PRVDR_OSCAR_NUM)
		{
			List<IFieldSpec> fieldSpecs = CCLF3Specs.GetFieldSpecs(CUR_CLM_UNIQ_ID, BENE_HIC_NUM, BENE_EQTBL_BIC_HICN_NUM, PRNCPL_DGNS_CD, PRVDR_OSCAR_NUM);

			FileSpecFixedWidth fileSpec = new FileSpecFixedWidth(this.IncludeHeaderRow, this.Delimiter, this.Encloser, this.FixedWidthPaddingCharacter, this.FixedWidthAddPadding, this.FixedWidthTruncate, fieldSpecs, this.Encoding, this.RecordsPerFileMin, this.RecordsPerFileMax, PATHSPEC_CCLF3);

			Generator generator = new Generator(this.OutputFolderRoot, fileSpec);

			return generator.Run().ToList();
		}

		private List<CCLF4> GenerateCCLF4(List<Category> CUR_CLM_UNIQ_ID, List<Category> BENE_HIC_NUM, List<Category> BENE_EQTBL_BIC_HICN_NUM, List<Category> PRNCPL_DGNS_CD, List<Category> PRVDR_OSCAR_NUM)
		{
			List<IFieldSpec> fieldSpecs = CCLF4Specs.GetFieldSpecs(CUR_CLM_UNIQ_ID, BENE_HIC_NUM, BENE_EQTBL_BIC_HICN_NUM, PRNCPL_DGNS_CD, PRVDR_OSCAR_NUM);

			FileSpecFixedWidth fileSpec = new FileSpecFixedWidth(this.IncludeHeaderRow, this.Delimiter, this.Encloser, this.FixedWidthPaddingCharacter, this.FixedWidthAddPadding, this.FixedWidthTruncate, fieldSpecs, this.Encoding, this.RecordsPerFileMin, this.RecordsPerFileMax, PATHSPEC_CCLF4);

			Generator generator = new Generator(this.OutputFolderRoot, fileSpec);

			return generator.Run().ToList();
		}

		private List<CCLF5> GenerateCCLF5(List<Category> CUR_CLM_UNIQ_ID, List<Category> BENE_HIC_NUM, List<Category> BENE_EQTBL_BIC_HICN_NUM, List<Category> PRNCPL_DGNS_CD)
		{
			List<IFieldSpec> fieldSpecs = CCLF5Specs.GetFieldSpecs(CUR_CLM_UNIQ_ID, BENE_HIC_NUM, BENE_EQTBL_BIC_HICN_NUM, PRNCPL_DGNS_CD);

			FileSpecFixedWidth fileSpec = new FileSpecFixedWidth(this.IncludeHeaderRow, this.Delimiter, this.Encloser, this.FixedWidthPaddingCharacter, this.FixedWidthAddPadding, this.FixedWidthTruncate, fieldSpecs, this.Encoding, this.RecordsPerFileMin, this.RecordsPerFileMax, PATHSPEC_CCLF5);

			Generator generator = new Generator(this.OutputFolderRoot, fileSpec);

			return generator.Run().ToList();
		}

		private List<CCLF6> GenerateCCLF6(List<Category> CUR_CLM_UNIQ_ID, List<Category> BENE_HIC_NUM, List<Category> BENE_EQTBL_BIC_HICN_NUM)
		{
			List<IFieldSpec> fieldSpecs = CCLF6Specs.GetFieldSpecs(CUR_CLM_UNIQ_ID, BENE_HIC_NUM, BENE_EQTBL_BIC_HICN_NUM);

			FileSpecFixedWidth fileSpec = new FileSpecFixedWidth(this.IncludeHeaderRow, this.Delimiter, this.Encloser, this.FixedWidthPaddingCharacter, this.FixedWidthAddPadding, this.FixedWidthTruncate, fieldSpecs, this.Encoding, this.RecordsPerFileMin, this.RecordsPerFileMax, PATHSPEC_CCLF6);

			Generator generator = new Generator(this.OutputFolderRoot, fileSpec);

			return generator.Run().ToList();
		}

		private List<CCLF7> GenerateCCLF7(List<Category> CUR_CLM_UNIQ_ID, List<Category> BENE_HIC_NUM)
		{
			List<IFieldSpec> fieldSpecs = CCLF7Specs.GetFieldSpecs(CUR_CLM_UNIQ_ID, BENE_HIC_NUM);

			FileSpecFixedWidth fileSpec = new FileSpecFixedWidth(this.IncludeHeaderRow, this.Delimiter, this.Encloser, this.FixedWidthPaddingCharacter, this.FixedWidthAddPadding, this.FixedWidthTruncate, fieldSpecs, this.Encoding, this.RecordsPerFileMin, this.RecordsPerFileMax, PATHSPEC_CCLF7);

			Generator generator = new Generator(this.OutputFolderRoot, fileSpec);

			return generator.Run().ToList();
		}

		private List<CCLF9> GenerateCCLF9(List<Category> BENE_HIC_NUM)
		{
			List<IFieldSpec> fieldSpecs = CCLF9Specs.GetFieldSpecs(BENE_HIC_NUM);

			FileSpecFixedWidth fileSpec = new FileSpecFixedWidth(this.IncludeHeaderRow, this.Delimiter, this.Encloser, this.FixedWidthPaddingCharacter, this.FixedWidthAddPadding, this.FixedWidthTruncate, fieldSpecs, this.Encoding, this.RecordsPerFileMin, this.RecordsPerFileMax, PATHSPEC_CCLF9);

			Generator generator = new Generator(this.OutputFolderRoot, fileSpec);

			return generator.Run().ToList();
		}

		private List<CCLFA> GenerateCCLFA(List<Category> CUR_CLM_UNIQ_ID, DateTime dateStartClaimAdmission, DateTime dateEndClaimAdmission)
		{
			List<IFieldSpec> fieldSpecs = CCLFASpecs.GetFieldSpecs(CUR_CLM_UNIQ_ID, dateStartClaimAdmission, dateEndClaimAdmission);

			FileSpecFixedWidth fileSpec = new FileSpecFixedWidth(this.IncludeHeaderRow, this.Delimiter, this.Encloser, this.FixedWidthPaddingCharacter, this.FixedWidthAddPadding, this.FixedWidthTruncate, fieldSpecs, this.Encoding, this.RecordsPerFileMin, this.RecordsPerFileMax, PATHSPEC_CCLFA);

			Generator generator = new Generator(this.OutputFolderRoot, fileSpec);

			return generator.Run().ToList();
		}

		private List<CCLFB> GenerateCCLFB(string pathSpec)
		{
			List<IFieldSpec> fieldSpecs = CCLFBSpecs.GetFieldSpecs();

			FileSpecFixedWidth fileSpec = new FileSpecFixedWidth(this.IncludeHeaderRow, this.Delimiter, this.Encloser, this.FixedWidthPaddingCharacter, this.FixedWidthAddPadding, this.FixedWidthTruncate, fieldSpecs, this.Encoding, this.RecordsPerFileMin, this.RecordsPerFileMax, PATHSPEC_CCLFB);

			Generator generator = new Generator(this.OutputFolderRoot, fileSpec);

			return generator.Run().ToList();
		}

	}
}
