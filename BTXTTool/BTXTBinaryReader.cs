using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using JpmodLib.IO;
using JpmodLib.Utils;

namespace BTXTTool
{
	// Token: 0x02000009 RID: 9
	public class BTXTBinaryReader : EnhancedBinaryReader
	{
		// Token: 0x06000040 RID: 64 RVA: 0x000034F0 File Offset: 0x000016F0
		public BTXTBinaryReader(Stream input) : base(input)
		{
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000034FC File Offset: 0x000016FC
		public BTXTBinaryReader(Stream input, Encoding encoding) : base(input, encoding)
		{
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00003509 File Offset: 0x00001709
		public BTXTBinaryReader(Stream input, Endian endian) : base(input, endian)
		{
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003516 File Offset: 0x00001716
		public BTXTBinaryReader(Stream input, Encoding encoding, Endian endian) : base(input, encoding, endian)
		{
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003524 File Offset: 0x00001724
		public string ReadStringWithNull()
		{
			List<byte> list = new List<byte>();
			byte b = base.ReadByte();
			while (base.BaseStream.Position < base.BaseStream.Length)
			{
				if (b == 0)
				{
					break;
				}
				list.Add(b);
				b = base.ReadByte();
			}
			return Conv.ByteToString(list.ToArray<byte>(), this.encoding);
		}
	}
}
