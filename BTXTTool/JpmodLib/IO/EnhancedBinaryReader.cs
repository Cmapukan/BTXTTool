using System;
using System.IO;
using System.Text;

namespace JpmodLib.IO
{
	// Token: 0x02000004 RID: 4
	public class EnhancedBinaryReader : BinaryReader
	{
		// Token: 0x06000010 RID: 16 RVA: 0x0000221F File Offset: 0x0000041F
		public EnhancedBinaryReader(Stream input) : base(input)
		{
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002247 File Offset: 0x00000447
		public EnhancedBinaryReader(Stream input, Encoding encoding) : base(input, encoding)
		{
			this.encoding = encoding;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002277 File Offset: 0x00000477
		public EnhancedBinaryReader(Stream input, Endian endian) : base(input)
		{
			this.endian = endian;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022A6 File Offset: 0x000004A6
		public EnhancedBinaryReader(Stream input, Encoding encoding, Endian endian) : base(input, encoding)
		{
			this.encoding = encoding;
			this.endian = endian;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000022E0 File Offset: 0x000004E0
		public override double ReadDouble()
		{
			return BitConverter.ToDouble(this.readBytesByEndian(8), 0);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002300 File Offset: 0x00000500
		public override short ReadInt16()
		{
			return BitConverter.ToInt16(this.readBytesByEndian(2), 0);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002320 File Offset: 0x00000520
		public override int ReadInt32()
		{
			return BitConverter.ToInt32(this.readBytesByEndian(4), 0);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002340 File Offset: 0x00000540
		public override long ReadInt64()
		{
			return BitConverter.ToInt64(this.readBytesByEndian(8), 0);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002360 File Offset: 0x00000560
		public override ushort ReadUInt16()
		{
			return BitConverter.ToUInt16(this.readBytesByEndian(2), 0);
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002380 File Offset: 0x00000580
		public override uint ReadUInt32()
		{
			return BitConverter.ToUInt32(this.readBytesByEndian(4), 0);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000023A0 File Offset: 0x000005A0
		public override ulong ReadUInt64()
		{
			return BitConverter.ToUInt64(this.readBytesByEndian(8), 0);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000023C0 File Offset: 0x000005C0
		private byte[] readBytesByEndian(int count)
		{
			byte[] array = base.ReadBytes(count);
			if (this.endian == Endian.BigEndian)
			{
				Array.Reverse(array);
			}
			return array;
		}

		// Token: 0x04000006 RID: 6
		protected Endian endian = BitConverter.IsLittleEndian ? Endian.LittleEndian : Endian.BigEndian;

		// Token: 0x04000007 RID: 7
		protected Encoding encoding = Encoding.Default;
	}
}
