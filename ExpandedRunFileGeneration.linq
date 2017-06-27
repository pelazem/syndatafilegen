<Query Kind="Program">
  <Reference Relative="..\public\dotnet\lib\pelazem.Common.dll">&lt;MyDocuments&gt;\Code\public\dotnet\lib\pelazem.Common.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Dynamic.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Dynamic.Runtime.dll</Reference>
  <NuGetReference>Newtonsoft.Json</NuGetReference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>pelazem.Common</Namespace>
  <Namespace>System.Dynamic</Namespace>
</Query>

void Main()
{
	dynamic runFile = new ExpandoObject();

	runFile.Config = new ExpandoObject();
//	runFile.Config.IncludeHeaderRow = "true";
//	runFile.Config.Delimiter = ",";
//	runFile.Config.Encloser = "";
//	runFile.Config.PaddingCharacter = " ";  // char
//	runFile.Config.PadAt = "AtStart";   // AtStart, AtEnd
//	runFile.Config.TruncateAt = "AtEnd";    // AtStart, AtEnd
	runFile.Config.OutputFolderRoot = "d:\\test\\";
	runFile.Config.PathSpec = "{yyyy}\\{mm}\\{dd}\\{hh}.txt";
	runFile.Config.EncodingName = "UTF8";   // ASCII, UTF8, UTF16, UTF32
	runFile.Config.RecordsPerFileMin = 100;
	runFile.Config.RecordsPerFileMax = 200;
	runFile.Config.DateStart = "1/1/2017";
	runFile.Config.DateEnd = "3/31/2017";

	runFile.FileSpecs = new List<ExpandoObject>();

	dynamic fileSpecAvro = new ExpandoObject();
	fileSpecAvro.RecordsPerFileMin = 100;
	fileSpecAvro.RecordsPerFileMax = 200;
	runFile.FileSpecs.Add(fileSpecAvro);

	dynamic fileSpecDelimited = new ExpandoObject();
	fileSpecDelimited.IncludeHeaderRow = false;
	fileSpecDelimited.Delimiter = ",";
	fileSpecDelimited.Encloser = string.Empty;
	fileSpecDelimited.EncodingName = "UTF8";
	fileSpecDelimited.RecordsPerFileMin = 100;
	fileSpecDelimited.RecordsPerFileMax = 200;
	runFile.FileSpecs.Add(fileSpecDelimited);

	dynamic fileSpecFixedWidth = new ExpandoObject();
	fileSpecFixedWidth.IncludeHeaderRow = false;
	fileSpecFixedWidth.Delimiter = ",";
	fileSpecFixedWidth.Encloser = string.Empty;
	fileSpecFixedWidth.PaddingCharacter = ' ';
	fileSpecFixedWidth.AddPaddingAt = "AtStart";
	fileSpecFixedWidth.TruncateAt = "AtEnd";
	fileSpecFixedWidth.EncodingName = "UTF8";
	fileSpecFixedWidth.RecordsPerFileMin = 100;
	fileSpecFixedWidth.RecordsPerFileMax = 200;
	runFile.FileSpecs.Add(fileSpecFixedWidth);

	dynamic fileSpecJson = new ExpandoObject();
	fileSpecJson.FileTypeName = "JSON";	// Avro, Delimited, FixedWidth, Json - case-insensitive
	fileSpecJson.EncodingName = "UTF8";
	fileSpecJson.RecordsPerFileMin = 100;
	fileSpecJson.RecordsPerFileMax = 200;
	runFile.FileSpecs.Add(fileSpecJson);
	
	Console.WriteLine(JsonConvert.SerializeObject(runFile, Newtonsoft.Json.Formatting.Indented));
}

