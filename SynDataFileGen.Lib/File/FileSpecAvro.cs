using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using Microsoft.Hadoop.Avro;
using Microsoft.Hadoop.Avro.Container;
using Microsoft.Hadoop.Avro.Schema;
using Newtonsoft.Json;
using pelazem.util;

namespace SynDataFileGen.Lib
{
	public class FileSpecAvro : FileSpecBase
	{
		private const int SYNCNUM = 24;

		#region Constructors

		private FileSpecAvro() { }

		public FileSpecAvro(List<IFieldSpec> fieldSpecs, int? recordsPerFileMin, int? recordsPerFileMax, string pathSpec, string fieldNameForLoopDateTime)
			: base(recordsPerFileMin, recordsPerFileMax, pathSpec, fieldSpecs, fieldNameForLoopDateTime)
		{
		}

		#endregion

		public override Stream GetContentStream(List<ExpandoObject> records)
		{
			var result = new MemoryStream();

			// We get a JSON schema for the passed type. Normally, the AvroSerializer expects a type passed that has DataContract/DataMember attributes, but I don't want users of this utility
			// to have to modify their POCOs in any way, hence building a schema here and proceeding with that.
			string schema = GetJsonSchema();

			var serializer = AvroSerializer.CreateGeneric(schema);
			var rootSchema = serializer.WriterSchema as RecordSchema;

			// We'll write the Avro content to a memory stream
			using (var interim = new MemoryStream())
			{
				// Avro serializer with deflate
				using (var avroWriter = AvroContainer.CreateGenericWriter(schema, interim, Codec.Deflate))
				{
					using (var seqWriter = new SequentialWriter<object>(avroWriter, SYNCNUM))
					{
						foreach (var record in records)
						{
							IDictionary<string, object> recordProperties = record as IDictionary<string, object>;
							dynamic avroRecord = new AvroRecord(rootSchema);

							foreach (KeyValuePair<string, object> recordKVP in recordProperties)
								avroRecord[recordKVP.Key] = recordKVP.Value;

							seqWriter.Write(avroRecord);
						}

						seqWriter.Flush();

						interim.Seek(0, SeekOrigin.Begin);

						interim.CopyTo(result);
					}
				}
			}

			return result;
		}

		#region Avro Utility

		/// <summary>
		/// Microsoft.Hadoop.Avro2 serializer has very limited list of supported primitive types. Their aliases are neither .NET nor C#-compliant.
		/// See https://github.com/dougmsft/microsoft-avro/blob/master/Microsoft.Hadoop.Avro/Schema/JsonSchemaBuilder.cs
		/// </summary>
		private string GetJsonSchema()
		{
			var schema = new AvroSchema();

			if (!string.IsNullOrWhiteSpace(this.FieldNameForLoopDateTime))
				schema.fields.Add(new AvroSchemaTuple(this.FieldNameForLoopDateTime, "string"));

			foreach (var fieldSpec in this.FieldSpecs)
			{
				Type type = fieldSpec.Value.GetType();

				if (TypeUtil.IsNumeric(type))
					schema.fields.Add(new AvroSchemaTuple(this.FieldNameForLoopDateTime, "double"));
				else if (TypeUtil.IsPrimitive(type))
					schema.fields.Add(new AvroSchemaTuple(this.FieldNameForLoopDateTime, "string"));
				else
					schema.fields.Add(new AvroSchemaTuple(this.FieldNameForLoopDateTime, "bytes"));
			}

			return JsonConvert.SerializeObject(schema, Newtonsoft.Json.Formatting.None);
		}

		//private string GetJsonSchema<T>(List<PropertyInfo> props)
		//{
		//	var schema = new AvroSchema();

		//	foreach (PropertyInfo prop in props)
		//	{
		//		string typeAlias = GetAvroPrimitiveTypeAlias(prop.PropertyType);

		//		if (string.IsNullOrWhiteSpace(typeAlias))
		//			throw new Exception("The Microsoft.Hadoop.Avro2 serializer does not support this type: " + prop.PropertyType.Name);

		//		schema.fields.Add(new AvroSchemaTuple(prop.Name, typeAlias));
		//	}

		//	return JsonConvert.SerializeObject(schema, Newtonsoft.Json.Formatting.None);
		//}

		/// <summary>
		/// Microsoft.Hadoop.Avro2 serializer has very limited list of supported primitive types. Their aliases are neither .NET nor C#-compliant.
		/// See https://github.com/dougmsft/microsoft-avro/blob/master/Microsoft.Hadoop.Avro/Schema/JsonSchemaBuilder.cs
		/// </summary>
		//private static readonly Dictionary<Type, string> AvroPrimitiveTypeAliases = new Dictionary<Type, string>
		//{
		//	{ TypeUtil.TypeBool, "boolean" },
		//	{ TypeUtil.TypeInt32, "int" },
		//	{ TypeUtil.TypeInt64, "long" },
		//	{ TypeUtil.TypeSingle, "float" },
		//	{ TypeUtil.TypeDouble, "double" },
		//	{ TypeUtil.TypeString, "string" },
		//	{ typeof(Byte[]), "bytes" }
		//};

		//private string GetAvroPrimitiveTypeAlias(Type type)
		//{
		//	if (type == null)
		//		return string.Empty;
		//	else
		//		return AvroPrimitiveTypeAliases.ContainsKey(type) ? AvroPrimitiveTypeAliases[type] : string.Empty;
		//}

		#endregion
	}

	internal class AvroSchema
	{
		public string Type { get; set; } = "record";
		public string Name { get; set; } = nameof(AvroSchema);
		public List<AvroSchemaTuple> fields = new List<AvroSchemaTuple>();
	}

	internal class AvroSchemaTuple
	{
		public string Name { get; set; }
		public string Type { get; set; }

		public AvroSchemaTuple() { }
		public AvroSchemaTuple(string name, string type)
		{
			this.Name = name;
			this.Type = type;
		}
	}
}
