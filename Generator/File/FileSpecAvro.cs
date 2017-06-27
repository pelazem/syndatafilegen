using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Hadoop.Avro;
using Microsoft.Hadoop.Avro.Container;
using Microsoft.Hadoop.Avro.Schema;
using Newtonsoft.Json;
using pelazem.Common;

namespace Generator
{
	public class FileSpecAvro<T> : IFileSpec<T>
	{
		private const int SYNCNUM = 24;

		#region Constructors

		private FileSpecAvro() { }

		public FileSpecAvro(int? recordsPerFileMin = null, int? recordsPerFileMax = null)
		{
			this.RecordsPerFileMin = recordsPerFileMin;
			this.RecordsPerFileMax = recordsPerFileMax;
		}

		#endregion

		#region IFileSpec implementation

		public int? RecordsPerFileMin { get; private set; }

		public int? RecordsPerFileMax { get; private set; }

		public Stream GetFileContent(List<T> items)
		{
			if (items == null || items.Count == 0)
				return null;

			var result = new MemoryStream();

			var props = TypeHelper.GetPrimitiveProps(typeof(T));

			// We get a JSON schema for the passed type. Normally, the AvroSerializer expects a type passed that has DataContract/DataMember attributes, but I don't want users of this utility
			// to have to modify their POCOs in any way, hence building a schema here and proceeding with that.
			string schema = GetJsonSchema(props);

			var serializer = AvroSerializer.CreateGeneric(schema);
			var rootSchema = serializer.WriterSchema as RecordSchema;

			// We'll write the Avro content to a memory stream
			using (var outputStream = new MemoryStream())
			{
				// Avro serializer with deflate
				using (var avroWriter = AvroContainer.CreateGenericWriter(schema, outputStream, Codec.Deflate))
				{
					using (var seqWriter = new SequentialWriter<object>(avroWriter, SYNCNUM))
					{
						foreach (T item in items)
						{
							dynamic avroRecord = new AvroRecord(rootSchema);

							foreach (PropertyInfo prop in props)
								avroRecord[prop.Name] = prop.GetValueEx(item);

							seqWriter.Write(avroRecord);
						}
					}
				}

				outputStream.Seek(0, SeekOrigin.Begin);

				outputStream.CopyTo(result);
			}

			return result;
		}

		#endregion

		#region Utility

		private string GetJsonSchema(List<PropertyInfo> props)
		{
			var schema = new AvroSchema();
			schema.name = typeof(T).Name;

			foreach (PropertyInfo prop in props)
			{
				string typeAlias = Util.GetAvroPrimitiveTypeAlias(prop.PropertyType);

				if (string.IsNullOrWhiteSpace(typeAlias))
					throw new Exception("FileSpecAvro.GetJsonSchema: The Microsoft.Hadoop.Avro2 serializer does not support this type: " + prop.PropertyType.Name);

				schema.fields.Add(new AvroSchemaTuple(prop.Name, typeAlias));
			}

			return JsonConvert.SerializeObject(schema, Newtonsoft.Json.Formatting.None);
		}

		#endregion
	}

	internal class AvroSchema
	{
		public string type { get; set; } = "record";
		public string name { get; set; }
		public List<AvroSchemaTuple> fields = new List<AvroSchemaTuple>();
	}

	internal class AvroSchemaTuple
	{
		public string name { get; set; }
		public string type { get; set; }

		public AvroSchemaTuple() { }
		public AvroSchemaTuple(string name, string type)
		{
			this.name = name;
			this.type = type;
		}
	}
}
