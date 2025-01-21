using System;
using System.IO;

namespace JpmodLib.IO
{
	// Token: 0x0200000B RID: 11
	public class FileUtils
	{
		// Token: 0x06000051 RID: 81 RVA: 0x0000372A File Offset: 0x0000192A
		public static void Backup(string filepath)
		{
			FileUtils.Backup(filepath, "bak");
		}

		// Token: 0x06000052 RID: 82 RVA: 0x0000373C File Offset: 0x0000193C
		public static void Backup(string filepath, string backupExt)
		{
			if (File.Exists(filepath))
			{
				string text = filepath + "." + backupExt;
				if (File.Exists(text))
				{
					File.Delete(text);
				}
				File.Move(filepath, text);
			}
		}
	}
}
