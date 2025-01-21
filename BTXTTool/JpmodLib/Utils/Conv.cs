using System;
using System.Text;

namespace JpmodLib.Utils
{
	// Token: 0x0200000A RID: 10
	public class Conv
	{
		// Token: 0x06000045 RID: 69 RVA: 0x00003590 File Offset: 0x00001790
		public static int CountAsBytes(string s, Encoding e)
		{
			return e.GetByteCount(s);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000035AC File Offset: 0x000017AC
		public static byte IntToByte(int i)
		{
			return BitConverter.GetBytes(i)[0];
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000035C8 File Offset: 0x000017C8
		public static string ByteToString(byte[] buff, Encoding encoding)
		{
			string result;
			if (buff == null || buff.Length == 0)
			{
				result = "";
			}
			else
			{
				int count;
				if (buff[buff.Length - 1] == 0)
				{
					count = buff.Length - 1;
				}
				else
				{
					count = buff.Length;
				}
				result = encoding.GetString(buff, 0, count);
			}
			return result;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0000361C File Offset: 0x0000181C
		public static string ToByteString(byte b)
		{
			return BitConverter.ToString(new byte[]
			{
				b
			});
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003640 File Offset: 0x00001840
		public static string ToByteString(byte[] b)
		{
			Array.Reverse(b);
			return BitConverter.ToString(b).Replace("-", "");
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003670 File Offset: 0x00001870
		public static string ToByteString(long l)
		{
			return Conv.ToByteString(BitConverter.GetBytes(l));
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003690 File Offset: 0x00001890
		public static string ToByteString(int i)
		{
			return Conv.ToByteString(BitConverter.GetBytes(i));
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000036B0 File Offset: 0x000018B0
		public static string ToByteString(short s)
		{
			return Conv.ToByteString(BitConverter.GetBytes(s));
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000036D0 File Offset: 0x000018D0
		public static long ByteStringToLong(string s)
		{
			return Convert.ToInt64(s, 16);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000036EC File Offset: 0x000018EC
		public static int ByteStringToInt32(string s)
		{
			return Convert.ToInt32(s, 16);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003708 File Offset: 0x00001908
		public static short ByteStringToInt16(string s)
		{
			return Convert.ToInt16(s, 16);
		}
	}
}
