using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Jpmod.Lib;
using JpmodLib.IO;

namespace BTXTTool
{
	// Token: 0x02000008 RID: 8
	internal class Program
	{
		// Token: 0x0600003C RID: 60 RVA: 0x00003000 File Offset: 0x00001200
		private static void Main(string[] args)
		{
			GetOpt getOpt = new GetOpt(args, "uph");
			bool flag = false;
			bool flag2 = false;
			char c;
			while ((c = getOpt.Parse()) != '-')
			{
				char c2 = c;
				if (c2 == 'h')
				{
					Program.help();
					return;
				}
				if (c2 != 'p')
				{
					if (c2 == 'u')
					{
						flag = true;
					}
				}
				else
				{
					flag2 = true;
				}
			}
			string[] arguments = getOpt.GetArguments();
			if (arguments == null || arguments.Length < 1)
			{
				Program.help();
				return;
			}
			string text = arguments[0];
			string text2 = "";
			if (arguments.Length >= 2)
			{
				text2 = arguments[1];
			}
			else
			{
				text2 = ((Path.GetDirectoryName(text).Length > 0) ? (Path.GetDirectoryName(text) + "\\") : "") + Path.GetFileNameWithoutExtension(text);
				if (flag)
				{
					text2 += ".csv";
				}
				else
				{
					text2 += ".btxt";
				}
			}
			if (!File.Exists(text))
			{
				Program.error(string.Format("Ошибка: \"{0}\" не найден.", args[1]));
				return;
			}
			if (File.Exists(text2))
			{
				FileUtils.Backup(text2);
			}
			if (flag2)
			{
				using (CsvFileReader csvFileReader = new CsvFileReader(text))
				{
					csvFileReader.HasHeader = false;
					csvFileReader.Open();
					csvFileReader.Load();
					using (EnhancedBinaryWriter enhancedBinaryWriter = new EnhancedBinaryWriter(new FileStream(text2, FileMode.Create, FileAccess.Write), Encoding.UTF8))
					{
						enhancedBinaryWriter.Write(1111775316);
						enhancedBinaryWriter.Write(1);
						enhancedBinaryWriter.Write(0);
						enhancedBinaryWriter.Write(csvFileReader.Rows.Count);
						foreach (object obj in csvFileReader.Rows)
						{
							string[] array = (string[])obj;
							enhancedBinaryWriter.Write(uint.Parse(array[0]));
						}
						foreach (object obj2 in csvFileReader.Rows)
						{
							string[] array = (string[])obj2;
							enhancedBinaryWriter.Write(array[1]);
							enhancedBinaryWriter.Write(0);
						}
					}
				}
				return;
			}
			if (flag)
			{
				List<uint> list = new List<uint>();
				List<string> list2 = new List<string>();
				using (BTXTBinaryReader btxtbinaryReader = new BTXTBinaryReader(new FileStream(text, FileMode.Open, FileAccess.Read), Encoding.UTF8))
				{
					uint num = btxtbinaryReader.ReadUInt32();
					if (num != 1111775316U)
					{
						throw new NotSupportedException();
					}
					int num2 = btxtbinaryReader.ReadInt32();
					int num3 = btxtbinaryReader.ReadInt32();
					int num4 = btxtbinaryReader.ReadInt32();
					for (int i = 0; i < num4; i++)
					{
						list.Add(btxtbinaryReader.ReadUInt32());
					}
					for (int i = 0; i < num4; i++)
					{
						list2.Add(btxtbinaryReader.ReadStringWithNull());
					}
				}
				using (CsvFileWriter csvFileWriter = new CsvFileWriter(text2))
				{
					for (int i = 0; i < list.Count; i++)
					{
						csvFileWriter.AddRow(new object[]
						{
							list[i],
							list2[i]
						});
					}
					csvFileWriter.Save();
				}
				return;
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00003498 File Offset: 0x00001698
		private static void error(string message)
		{
			Console.WriteLine(message);
			Console.WriteLine();
			Program.help();
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000034AE File Offset: 0x000016AE
		private static void help()
		{
			Console.WriteLine("Использование: ");
			Console.WriteLine("- pack");
			Console.WriteLine(" BTXTTool.exe -p <archive file> [<извлеченный csv>]");
			Console.WriteLine("- unpack");
			Console.WriteLine(" BTXTTool.exe -u <extracted csv> [<архивный файл>]");
		}
	}
}
