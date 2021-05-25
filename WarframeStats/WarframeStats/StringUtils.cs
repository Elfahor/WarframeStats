using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WarframeStats
{
	internal static class StringUtils
	{
		public static MemoryStream ToStream(this string s) => new MemoryStream(Encoding.UTF8.GetBytes(s ?? ""));

		public static string ToWFString(this TimeSpan timeSpan) => $"{(timeSpan.Days > 0 ? $"{timeSpan.Days}d " : "")}{timeSpan.Hours}h {timeSpan.Minutes}m {timeSpan.Seconds}s";

		public static Task<T> JsonDeserializeAsync<T>(string response)
		{
			return Task.Run(() => JsonSerializer.Deserialize<T>(response));
		}
	}
}
