﻿using System;
using System.Collections.Generic;
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

		public FileSpecAvro(int? recordsPerFileMin, int? recordsPerFileMax, string pathSpec)
			: base(recordsPerFileMin, recordsPerFileMax, pathSpec)
		{
		}

		public FileSpecAvro(int? recordsPerFileMin, int? recordsPerFileMax, string pathSpec, string fieldNameForLoopDateTime, DateTime? dateStart, DateTime? dateEnd)
			: base(recordsPerFileMin, recordsPerFileMax, pathSpec, fieldNameForLoopDateTime, dateStart, dateEnd)
		{
		}

		#endregion

		#region IFileSpec implementation

		public override Stream GetFileContent(DateTime? dateLoop = null)
		{
			int numOfItems = Converter.GetInt32(RNG.GetUniform(this.RecordsPerFileMin ?? 0, this.RecordsPerFileMax ?? 0));

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
						for (int i = 1; i <= numOfItems; i++)
						{
							dynamic avroRecord = new AvroRecord(rootSchema);

							if (!string.IsNullOrWhiteSpace(this.FieldNameForLoopDateTime) && dateLoop != null)
								avroRecord[this.FieldNameForLoopDateTime] = string.Format(pelazem.util.Constants.FORMAT_DATETIME_UNIVERSAL, dateLoop);

							foreach (var fieldSpec in this.FieldSpecs)
								avroRecord[fieldSpec.Name] = fieldSpec.Value;

							seqWriter.Write(avroRecord);
						}

						seqWriter.Flush();
					}
				}

				interim.Seek(0, SeekOrigin.Begin);

				interim.CopyTo(result);
			}

			return result;
		}

		#endregion

		#region Utility

		/// <summary>
		/// Microsoft.Hadoop.Avro2 serializer has very limited list of supported primitive types. Their aliases are neither .NET nor C#-compliant.
		/// See https://github.com/dougmsft/microsoft-avro/blob/master/Microsoft.Hadoop.Avro/Schema/JsonSchemaBuilder.cs
		/// </summary>
		private string GetJsonSchema()
		{
			var schema = new AvroSchema();
			schema.Name = nameof(AvroSchema);

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

		#endregion
	}

	internal class AvroSchema
	{
		public string Type { get; set; } = "record";
		public string Name { get; set; }
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
