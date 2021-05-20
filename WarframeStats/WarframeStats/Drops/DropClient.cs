using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace WarframeStats.Drops
{
	public class DropClient
	{
		private readonly HttpClient http;

		/// <summary>
		/// Reward pool for sorties
		/// </summary>
		public SortieRewards SortieRewards {get; private set;}
		/// <summary>
		/// Reward pool for special missions: Razoback Armadas, Nightmare mode...
		/// </summary>
		public TransientRewards TransientRewards { get; private set; }

		public async Task<Relic> GetRelicLootAsync(string tier, string name)
		{
			try
			{
				string response = await http.GetStringAsync($"relics/{tier}/{name}.json");
				return await JsonSerializer.DeserializeAsync<Relic>(response.ToStream());
			}
			catch (HttpRequestException)
			{
				throw new ArgumentException($"There is no relic {tier} {name}");
			}
		}

		public async Task<Mission> GetMissionLootAsync(string planet, string node)
		{
			try
			{
				string response = await http.GetStringAsync($"missionRewards/{planet}/{node}.json");
				return await JsonSerializer.DeserializeAsync<Mission>(response.ToStream());
			}
			catch (HttpRequestException)
			{
				throw new ArgumentException($"There is no mission called {node} on {planet}");
			}
		}

		public DropClient()
		{
			http = new HttpClient
			{
				BaseAddress = new Uri("https://drops.warframestat.us/data/")
			};
		}

		public async Task RefreshDataAsync()
		{
			string response;

			response = await http.GetStringAsync("sortieRewards.json");
			SortieRewards = await JsonSerializer.DeserializeAsync<SortieRewards>(response.ToStream());

			response = await http.GetStringAsync("transientRewards.json");
			TransientRewards = await JsonSerializer.DeserializeAsync<TransientRewards>(response.ToStream());
		}
	}
}
