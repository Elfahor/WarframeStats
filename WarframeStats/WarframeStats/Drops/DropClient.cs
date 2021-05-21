using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace WarframeStats.Drops
{
	/// <summary>
	/// Client for getting drop data
	/// </summary>
	public class DropClient
	{
		private readonly HttpClient http;

		/// <summary>
		/// Reward pool for sorties
		/// </summary>
		public SortieRewards SortieRewards { get; private set; }

		/// <summary>
		/// Reward pool for special missions: Razoback Armadas, Nightmare mode...
		/// </summary>
		public TransientRewards TransientRewards { get; private set; }

		/// <summary>
		/// Get all the data for which enemies mods drop on
		/// </summary>
		public ModLocations ModLocations { get; private set; }

		/// <summary>
		/// Get the whole list of enemies a certain mod drops on
		/// </summary>
		/// <param name="modName">Name of the mod</param>
		/// <returns></returns>
		public async Task<Mod.EnemyDroppedOn[]> GetModDroppersAsync(string modName)
		{
			return await Task.Run(() => GetModDroppers(modName));
		}

		private Mod.EnemyDroppedOn[] GetModDroppers(string modName)
		{
			foreach (Mod item in ModLocations.modLocations)
			{
				if (item.modName == modName)
				{
					return item.enemies;
				}
			}
			throw new ArgumentException("This mod does not exist or does not drop on an enemy");
		}

		/// <summary>
		/// Get data about a relic's tiers and drops
		/// </summary>
		/// <param name="tier">Lith, Meso, Neo, Axi, or Requiem</param>
		/// <param name="name">Names look like O5, K4, Z2</param>
		/// <returns>The relic named "tier" "name"</returns>
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

		/// <summary>
		/// Get the reward pool for a mission
		/// </summary>
		/// <param name="planet">Earth, Ceres, Sedna...</param>
		/// <param name="node">The name of the mission (e.g. Apollo)</param>
		public async Task<MissionNode> GetMissionLootAsync(string planet, string node)
		{
			try
			{
				string response = await http.GetStringAsync($"missionRewards/{planet}/{node}.json");
				try
				{
					return await JsonSerializer.DeserializeAsync<EndlessMission>(response.ToStream());
				}
				catch (JsonException)
				{
					return await JsonSerializer.DeserializeAsync<FiniteMission>(response.ToStream());
				}
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

		/// <summary>
		/// Refreshes static data, like sorties reward pools. This doesn't need to called often as these pools rarely change.
		/// </summary>
		/// <returns></returns>
		public async Task RefreshDataAsync()
		{
			string response;

			response = await http.GetStringAsync("sortieRewards.json");
			SortieRewards = await JsonSerializer.DeserializeAsync<SortieRewards>(response.ToStream());

			response = await http.GetStringAsync("transientRewards.json");
			TransientRewards = await JsonSerializer.DeserializeAsync<TransientRewards>(response.ToStream());

			response = await http.GetStringAsync("modLocations.json");
			ModLocations = await JsonSerializer.DeserializeAsync<ModLocations>(response.ToStream());
		}
	}
}
