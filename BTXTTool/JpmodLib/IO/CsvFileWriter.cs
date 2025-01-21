using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using JpmodLib.Utils;

namespace JpmodLib.IO
{
	// Token: 0x02000006 RID: 6
	public class CsvFileWriter : IDisposable
	{
		// Token: 0x06000022 RID: 34 RVA: 0x0000273D File Offset: 0x0000093D
		public CsvFileWriter(string filepath)
		{
			this.filepath = filepath;
			this.encoding = Encoding.UTF8;
			this.lines = new ArrayList();
			this.regQuate = new Regex("\"");
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002775 File Offset: 0x00000975
		public void Dispose()
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002778 File Offset: 0x00000978
		// (set) Token: 0x06000025 RID: 37 RVA: 0x00002790 File Offset: 0x00000990
		public string[] Header
		{
			get
			{
				return this.header;
			}
			set
			{
				this.header = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000026 RID: 38 RVA: 0x0000279C File Offset: 0x0000099C
		// (set) Token: 0x06000027 RID: 39 RVA: 0x000027B4 File Offset: 0x000009B4
		public Encoding Encoding
		{
			get
			{
				return this.encoding;
			}
			set
			{
				this.encoding = value;
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000027C0 File Offset: 0x000009C0
		public void Save()
		{
			StreamWriter streamWriter;
			if (this.encoding == Encoding.UTF8)
			{
				streamWriter = new StreamWriter(this.filepath, false, new UTF8Encoding(false));
			}
			else
			{
				streamWriter = new StreamWriter(this.filepath, false, this.encoding);
			}
			try
			{
				if (this.header != null)
				{
					this.write(streamWriter, this.header);
				}
				foreach (object obj in this.lines)
				{
					object[] o = (object[])obj;
					this.write(streamWriter, o);
				}
			}
			finally
			{
				streamWriter.Close();
				streamWriter.Dispose();
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000028A4 File Offset: 0x00000AA4
		private void write(StreamWriter sw, object[] o)
		{
			for (int i = 0; i < o.Length; i++)
			{
				if (i != 0)
				{
					sw.Write(",");
				}
				string name = o[i].GetType().Name;
				string name2 = o[i].GetType().Name;
				switch (name2)
				{
				case "String":
					sw.Write(this.getCellData((string)o[i]));
					break;
				case "Byte":
					sw.Write(Conv.ToByteString((byte)o[i]));
					break;
				case "Byte[]":
					sw.Write(Conv.ToByteString((byte[])o[i]));
					break;
				case "Int16":
					sw.Write(Convert.ToString((short)o[i]));
					break;
				case "Int32":
					sw.Write(Convert.ToString((int)o[i]));
					break;
				case "Int64":
					sw.Write(Convert.ToString((long)o[i]));
					break;
				case "Unt16":
					sw.Write(Convert.ToString((ushort)o[i]));
					break;
				case "UInt32":
					sw.Write(Convert.ToString((uint)o[i]));
					break;
				case "UInt64":
					sw.Write(Convert.ToString((ulong)o[i]));
					break;
				}
			}
			sw.WriteLine();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002AA0 File Offset: 0x00000CA0
		private string getCellData(string s)
		{
			string text = s;
			bool flag = false;
			if (this.regQuate.IsMatch(text))
			{
				text = this.regQuate.Replace(text, "\"\"");
				flag = true;
			}
			if (text.Contains('\n') || text.Contains(',') || flag)
			{
				text = '"' + text + '"';
			}
			return text;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002B14 File Offset: 0x00000D14
		private string getCellData(int i)
		{
			return Convert.ToString(i);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002B2C File Offset: 0x00000D2C
		public void AddRow(object[] list)
		{
			this.lines.Add(list);
		}

		// Token: 0x04000010 RID: 16
		private string filepath;

		// Token: 0x04000011 RID: 17
		private ArrayList lines;

		// Token: 0x04000012 RID: 18
		private string[] header;

		// Token: 0x04000013 RID: 19
		private Encoding encoding;

		// Token: 0x04000014 RID: 20
		private Regex regQuate;
	}
}
