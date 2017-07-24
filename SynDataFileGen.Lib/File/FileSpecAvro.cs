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

		public string RecordSetName { get; private set; } = string.Empty;

		#region Constructors

		private FileSpecAvro() { }

		public FileSpecAvro(string recordSetName, List<IFieldSpec> fieldSpecs, int? recordsPerFileMin, int? recordsPerFileMax, string pathSpec, string fieldNameForLoopDateTime)
			: base(recordsPerFileMin, recordsPerFileMax, pathSpec, fieldSpecs, fieldNameForLoopDateTime)
		{
			this.RecordSetName = recordSetName;
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
			var schema = new AvroSchema((string.IsNullOrWhiteSpace(this.RecordSetName) ? nameof(AvroSchema) : this.RecordSetName));

			if (!string.IsNullOrWhiteSpace(this.FieldNameForLoopDateTime))
				schema.Fields.Add(new AvroSchemaTuple(this.FieldNameForLoopDateTime, "string"));

			foreach (var fieldSpec in this.FieldSpecs)
			{
				// We set a value just so we can get the type
				fieldSpec.SetNextValue();

				Type type = fieldSpec.Value.GetType();

				if (TypeUtil.IsNumeric(type))
					schema.Fields.Add(new AvroSchemaTuple(fieldSpec.Name, "double"));
				else if (TypeUtil.IsPrimitive(type))
					schema.Fields.Add(new AvroSchemaTuple(fieldSpec.Name, "string"));
				else
					schema.Fields.Add(new AvroSchemaTuple(fieldSpec.Name, "bytes"));
			}

			return JsonConvert.SerializeObject(schema, Newtonsoft.Json.Formatting.None);
		}

		#endregion
	}

	internal class AvroSchema
	{
		public const string Type = "record";

		public string Name { get; private set; }

		public List<AvroSchemaTuple> Fields { get; private set; } = new List<AvroSchemaTuple>();

		private AvroSchema() { }

		public AvroSchema(string recordSetName)
		{
			this.Name = recordSetName;
		}
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
