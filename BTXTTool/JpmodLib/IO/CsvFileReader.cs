using System;
using System.Collections;
using System.IO;
using System.Text;

namespace JpmodLib.IO
{
	// Token: 0x02000007 RID: 7
	public class CsvFileReader : IDisposable
	{
		// Token: 0x0600002D RID: 45 RVA: 0x00002B3C File Offset: 0x00000D3C
		public CsvFileReader(string filepath)
		{
			this.filepath = filepath;
			this.HasHeader = false;
			this.header = null;
			this.lines = new ArrayList();
			this.encoding = Encoding.UTF8;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002B73 File Offset: 0x00000D73
		public void Dispose()
		{
			this.Close();
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002B80 File Offset: 0x00000D80
		public string[] Header
		{
			get
			{
				return this.header;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002B98 File Offset: 0x00000D98
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00002BB0 File Offset: 0x00000DB0
		public bool HasHeader
		{
			get
			{
				return this.bHeader;
			}
			set
			{
				this.bHeader = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002BBC File Offset: 0x00000DBC
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00002BD4 File Offset: 0x00000DD4
		public Encoding TextEncoding
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

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002BE0 File Offset: 0x00000DE0
		public ArrayList Rows
		{
			get
			{
				return this.lines;
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002BF8 File Offset: 0x00000DF8
		public bool Open()
		{
			bool result;
			if (this.sr != null)
			{
				result = true;
			}
			else if (!File.Exists(this.filepath))
			{
				result = false;
			}
			else
			{
				this.sr = new StreamReader(this.filepath, this.encoding);
				if (this.sr == null)
				{
					result = false;
				}
				else
				{
					this.pos = 0;
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002C64 File Offset: 0x00000E64
		public bool Open(string path)
		{
			this.filepath = path;
			return this.Open();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002C84 File Offset: 0x00000E84
		public void Close()
		{
			if (this.sr != null)
			{
				this.sr.Close();
				this.sr = null;
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002CB4 File Offset: 0x00000EB4
		public bool Load()
		{
			bool result;
			if (!this.Open())
			{
				result = false;
			}
			else
			{
				if (this.HasHeader)
				{
					this.header = this.read();
				}
				if (this.sr.EndOfStream)
				{
					result = false;
				}
				else
				{
					while (!this.sr.EndOfStream)
					{
						this.lines.Add(this.read());
					}
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002D2C File Offset: 0x00000F2C
		public string[] GetNext()
		{
			string[] result;
			if (this.pos >= this.lines.Count)
			{
				result = null;
			}
			else
			{
				result = (string[])this.lines[this.pos++];
			}
			return result;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002D7C File Offset: 0x00000F7C
		protected virtual string[] read()
		{
			ArrayList arrayList = new ArrayList();
			string text = this.readLine();
			int num = 0;
			bool flag = false;
			string text2 = null;
			while (text.Length > num)
			{
				char c = text[num];
				if (c <= '\r')
				{
					if (c != '\n' && c != '\r')
					{
						goto IL_1AF;
					}
					if (flag)
					{
						text2 += text[num];
						if (num == text.Length - 1)
						{
							text += this.readLine();
						}
					}
					if (text2 == null)
					{
						text2 = "";
					}
					num++;
				}
				else if (c != '"')
				{
					if (c != '\'')
					{
						if (c != ',')
						{
							goto IL_1AF;
						}
						if (flag)
						{
							text2 += text[num];
						}
						else
						{
							if (text2 == null)
							{
								text2 = "";
							}
							arrayList.Add(text2);
							text2 = null;
						}
						num++;
					}
					else if (num != 0)
					{
						text2 += text[num++];
					}
				}
				else
				{
					if (!flag)
					{
						if (text2 != null)
						{
							throw new Exception();
						}
						text2 = "";
						flag = true;
					}
					else if (num + 1 < text.Length && text[num + 1] == '"')
					{
						text2 += text[num++];
					}
					else
					{
						flag = false;
					}
					num++;
				}
				continue;
				IL_1AF:
				text2 += text[num++];
			}
			if (text2 != null)
			{
				arrayList.Add(text2);
			}
			return (string[])arrayList.ToArray(typeof(string));
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002F98 File Offset: 0x00001198
		private string readLine()
		{
			string text = "";
			while (!this.sr.EndOfStream)
			{
				text += Convert.ToChar(this.sr.Read());
				if (text[text.Length - 1] == '\n')
				{
					break;
				}
			}
			return text;
		}

		// Token: 0x04000015 RID: 21
		private string filepath;

		// Token: 0x04000016 RID: 22
		private string[] header;

		// Token: 0x04000017 RID: 23
		private bool bHeader;

		// Token: 0x04000018 RID: 24
		private StreamReader sr;

		// Token: 0x04000019 RID: 25
		private ArrayList lines;

		// Token: 0x0400001A RID: 26
		private int pos;

		// Token: 0x0400001B RID: 27
		private Encoding encoding;
	}
}
