using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace WarframeStats.WorldState
{
	/// <summary>
	/// Client for getting the world state from ws.warframestat.us
	/// </summary>
	public class WorldStateClient : IDisposable
	{
		private readonly HttpClient http;

		/// <summary>
		/// The current world state from the warframe API on PC
		/// </summary>
		public WorldState WorldStatePC { get; private set; } = null;
		/// <summary>
		/// The current world state from the warframe API on PS4
		/// </summary>
		public WorldState WorldStatePS4 { get; private set; } = null;
		/// <summary>
		/// The current world state from the warframe API on XB1
		/// </summary>
		public WorldState WorldStateXB1 { get; private set; } = null;

		public WorldStateClient()
		{
			http = new HttpClient
			{
				BaseAddress = new Uri("https://ws.warframestat.us/")
			};
		}

		/// <summary>
		/// Updates the data from the warframe API.
		/// </summary>
		/// <exception cref="JsonException">If there is a problem with the data</exception>
		/// <exception cref="HttpRequestException">If there is a problem with the platform</exception>
		/// <param name="platform">Either "pc", "ps4", "xb1"</param>
		/// <returns></returns>
		public async Task RefreshDataAsync(string platform)
		{
			HashSet<string> validPlatforms = new HashSet<string> { "pc", "xb1", "ps4" };
			if (!validPlatforms.Contains(platform)) throw new ArgumentException($"{platform} is not a valid platform: must be pc, ps4 or xb1");

			string response = await http.GetStringAsync(platform);
			WorldState data = JsonSerializer.Deserialize<WorldState>(response);
			switch (platform)
			{
				case "pc":
					WorldStatePC = data;
					break;
				case "ps4":
					WorldStatePS4 = data;
					break;
				case "xb1":
					WorldStateXB1 = data;
					break;
			}
		}

		public void Dispose()
		{
			http.Dispose();
		}
	}
}
