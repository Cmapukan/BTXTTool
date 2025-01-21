using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Jpmod.Lib
{
	// Token: 0x02000005 RID: 5
	public class GetOpt
	{
		// Token: 0x0600001C RID: 28 RVA: 0x000023F4 File Offset: 0x000005F4
		public GetOpt(string[] argv, string options)
		{
			this.__argv = argv;
			this.__opt = options;
			this.__pos = 0;
			this.__hasValue = false;
			this.__optTmp = new List<char>();
			this.regexOptFormat = new Regex("^(?:-|/)(?<opt>[a-zA-Z]+)$");
			this.parseOpt();
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002448 File Offset: 0x00000648
		public char Parse()
		{
			char c = '-';
			this.__value = null;
			char result;
			if (this.__argv.Length == 0)
			{
				result = c;
			}
			else
			{
				if (this.__optTmp.Count == 0)
				{
					Match match = this.regexOptFormat.Match(this.__argv[this.__pos]);
					if (match.Success)
					{
						this.__optTmp.AddRange(match.Groups["opt"].Value.ToArray<char>());
					}
				}
				if (this.__optTmp.Count > 0)
				{
					c = this.__optTmp[0];
					this.__optTmp.RemoveAt(0);
					if (!this.__opts.ContainsKey(c))
					{
						c = '?';
					}
					else if (this.__opts[c] && this.__optTmp.Count == 0)
					{
						this.__value = this.__argv[++this.__pos];
					}
				}
				else
				{
					c = '-';
				}
				if (this.__optTmp.Count == 0 && c != '-')
				{
					this.__pos++;
				}
				result = c;
			}
			return result;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000025A4 File Offset: 0x000007A4
		public bool HasValue()
		{
			return this.__value != null;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000025C4 File Offset: 0x000007C4
		public string GetValue()
		{
			string result;
			if (this.HasValue())
			{
				result = this.__value;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000025F0 File Offset: 0x000007F0
		public string[] GetArguments()
		{
			return this.__argv.Skip(this.__pos).Take(this.__argv.Count<string>() - this.__pos).ToArray<string>();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002630 File Offset: 0x00000830
		private void parseOpt()
		{
			this.__opts = new Dictionary<char, bool>();
			for (int i = 0; i < this.__opt.Length; i++)
			{
				if (('A' <= this.__opt[i] && 'Z' >= this.__opt[i]) || ('a' <= this.__opt[i] && 'z' >= this.__opt[i]))
				{
					if (!this.__opts.ContainsKey(this.__opt[i]))
					{
						this.__opts.Add(this.__opt[i], false);
						if (i < this.__opt.Length - 1 && this.__opt[i + 1] == ':')
						{
							this.__opts[this.__opt[i++]] = true;
						}
					}
				}
			}
		}

		// Token: 0x04000008 RID: 8
		private Regex regexOptFormat;

		// Token: 0x04000009 RID: 9
		private string[] __argv;

		// Token: 0x0400000A RID: 10
		private string __opt;

		// Token: 0x0400000B RID: 11
		private int __pos;

		// Token: 0x0400000C RID: 12
		private bool __hasValue;

		// Token: 0x0400000D RID: 13
		private string __value;

		// Token: 0x0400000E RID: 14
		private List<char> __optTmp;

		// Token: 0x0400000F RID: 15
		private Dictionary<char, bool> __opts;
	}
}
