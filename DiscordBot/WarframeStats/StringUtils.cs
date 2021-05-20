using System.IO;
using System.Text;

namespace WarframeStats
{
	internal static class StringUtils
	{
		public static MemoryStream ToStream(this string s)
		{
			return new MemoryStream(Encoding.UTF8.GetBytes(s ?? ""));
		}
	}
}
