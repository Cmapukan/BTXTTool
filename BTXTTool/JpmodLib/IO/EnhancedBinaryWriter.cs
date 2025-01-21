using System;
using System.IO;
using System.Text;

namespace JpmodLib.IO
{
	// Token: 0x02000002 RID: 2
	public class EnhancedBinaryWriter : BinaryWriter
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public EnhancedBinaryWriter(Stream input) : base(input)
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002078 File Offset: 0x00000278
		public EnhancedBinaryWriter(Stream input, Encoding encoding) : base(input, encoding)
		{
			this.encoding = encoding;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020A8 File Offset: 0x000002A8
		public EnhancedBinaryWriter(Stream input, Endian endian) : base(input)
		{
			this.endian = endian;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020D7 File Offset: 0x000002D7
		public EnhancedBinaryWriter(Stream input, Encoding encoding, Endian endian) : base(input, encoding)
		{
			this.encoding = encoding;
			this.endian = endian;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002110 File Offset: 0x00000310
		public override void Write(char data)
		{
			this.writeBytesByEndian(this.encoding.GetBytes(new char[]
			{
				data
			}));
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000213C File Offset: 0x0000033C
		public override void Write(char[] data)
		{
			foreach (char ch in data)
			{
				this.Write(ch);
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Write(string data)
		{
			this.Write(data.ToCharArray());
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000217D File Offset: 0x0000037D
		public override void Write(double data)
		{
			this.writeBytesByEndian(BitConverter.GetBytes(data));
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000218D File Offset: 0x0000038D
		public override void Write(short data)
		{
			this.writeBytesByEndian(BitConverter.GetBytes(data));
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000219D File Offset: 0x0000039D
		public override void Write(int data)
		{
			this.writeBytesByEndian(BitConverter.GetBytes(data));
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021AD File Offset: 0x000003AD
		public override void Write(long data)
		{
			this.writeBytesByEndian(BitConverter.GetBytes(data));
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021BD File Offset: 0x000003BD
		public override void Write(ushort data)
		{
			this.writeBytesByEndian(BitConverter.GetBytes(data));
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021CD File Offset: 0x000003CD
		public override void Write(uint data)
		{
			this.writeBytesByEndian(BitConverter.GetBytes(data));
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000021DD File Offset: 0x000003DD
		public override void Write(ulong data)
		{
			this.writeBytesByEndian(BitConverter.GetBytes(data));
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000021F0 File Offset: 0x000003F0
		private void writeBytesByEndian(byte[] data)
		{
			if (this.endian == Endian.BigEndian)
			{
				Array.Reverse(data);
			}
			base.Write(data);
		}

		// Token: 0x04000001 RID: 1
		protected Endian endian = BitConverter.IsLittleEndian ? Endian.LittleEndian : Endian.BigEndian;

		// Token: 0x04000002 RID: 2
		protected Encoding encoding = Encoding.Default;
	}
}
