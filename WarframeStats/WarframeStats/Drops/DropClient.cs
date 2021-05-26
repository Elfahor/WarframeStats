using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace WarframeStats.Drops
{
	/// <summary>
	/// Client for getting drop data
	/// </summary>
	public class DropClient : IDisposable
	{
		private readonly HttpClient http;

		public DropClient()
		{
			http = new HttpClient
			{
				BaseAddress = new Uri("https://drops.warframestat.us/data/")
			};
		}

		/// <summary>
		/// Reward pool for sorties
		/// </summary>
		public SortieRewards SortieRewards { get; private set; }

		/// <summary>
		/// Reward pool for special missions: Razoback Armadas, Nightmare mode...
		/// </summary>
		public TransientRewards TransientRewards { get; private set; }

		public void Dispose()
		{
			http.Dispose();
		}

		/// <summary>
		/// Refreshes static data, like sorties reward pools. This doesn't need to called often as these pools rarely change.
		/// </summary>
		/// <returns></returns>
		public async Task RefreshDataAsync()
		{
			string responseSortieRewards, responseTransientRewards;
			Task<string> sortieRewardsTasks = http.GetStringAsync("sortieRewards.json");
			Task<string> transientRewardsTask = http.GetStringAsync("transientRewards.json");

			responseSortieRewards = await sortieRewardsTasks;
			responseTransientRewards = await transientRewardsTask;

			Task<SortieRewards> sortieRewardObj = Task.Run(() => JsonSerializer.Deserialize<SortieRewards>(responseSortieRewards));
			Task<TransientRewards> transientRewardsObj = Task.Run(() => JsonSerializer.Deserialize<TransientRewards>(responseTransientRewards));

			SortieRewards = await sortieRewardObj;
			TransientRewards = await transientRewardsObj;
		}

		/// <summary>
		/// Get all locations and items whose name match <paramref name="itemOrLocation"/>
		/// </summary>
		/// <param name="itemOrLocation">what to search</param>
		/// <returns>A list of all the locations/item matching the <paramref name="itemOrLocation"/></returns>
		public async Task<DropLocation[]> SearchForItemAndLocationAsync(string itemOrLocation)
		{
			string requestUri = "http://api.warframestat.us/drops/search/" + itemOrLocation.Replace("/", "%2F");
			string response = await http.GetStringAsync(requestUri);
			return JsonSerializer.Deserialize<DropLocation[]>(response);
		}
		/// <summary>
		/// Get all items whose name match <paramref name="itemOrLocation"/>, either strictly or not
		/// </summary>
		/// <param name="item">what to search</param>
		/// <param name="strictMatch">should the search result match strictly <paramref name="item"/></param>
		/// <returns>A list of all the items matching the <paramref name="itemOrLocation"/></returns>
		public async Task<DropLocation[]> SearchForItemAsync(string item, bool strictMatch = true)
		{
			DropLocation[] dropLocations = await SearchForItemAndLocationAsync(item);
			List<DropLocation> validated = new List<DropLocation>(dropLocations.Length);
			string searchedItemUPPER = item.ToUpper();
			foreach (DropLocation location in dropLocations)
			{
				string itemNameUPPER = location.item.ToUpper();
				if ((strictMatch && searchedItemUPPER == itemNameUPPER) || (!strictMatch && searchedItemUPPER.Contains(itemNameUPPER)))
				{
					validated.Add(location);
				}
			}
			validated.TrimExcess();
			return validated.ToArray();
		}
		/// <summary>
		/// Get all items whose name match <paramref name="location"/>, either strictly or not
		/// </summary>
		/// <param name="location">what to search</param>
		/// <param name="strictMatch">should the search result match strictly <paramref name="location"/></param>
		/// <returns>A list of all the items matching the <paramref name="location"/></returns>
		public async Task<DropLocation[]> SearchForLocationAsync(string location, bool strictMatch = false)
		{
			DropLocation[] dropLocations = await SearchForItemAndLocationAsync(location);
			List<DropLocation> validated = new List<DropLocation>(dropLocations.Length);
			string searchedLocUPPER = location.ToUpper();
			foreach (DropLocation loc in dropLocations)
			{
				string locNameUPPER = loc.place.ToUpper();
				if ((strictMatch && searchedLocUPPER == locNameUPPER) || (!strictMatch && (searchedLocUPPER.Contains(locNameUPPER) || locNameUPPER.Contains(searchedLocUPPER))))
				{
					validated.Add(loc);
				}
			}
			validated.TrimExcess();
			return validated.ToArray();
		}

		/// <summary>
		/// Get the reward pool for a mission
		/// </summary>
		/// <param name="planet">Earth, Ceres, Sedna...</param>
		/// <param name="node">The name of the mission (e.g. Apollo)</param>
		internal async Task<MissionNode> GetMissionLootAsync(string planet, string node)
		{
			try
			{
				string response = await http.GetStringAsync($"missionRewards/{planet}/{node}.json");
				try
				{
					return JsonSerializer.Deserialize<EndlessMission>(response);
				}
				catch (JsonException)
				{
					return JsonSerializer.Deserialize<FiniteMission>(response);
				}
			}
			catch (HttpRequestException)
			{
				throw new ArgumentException($"There is no mission called {node} on {planet}");
			}
		}

		/// <summary>
		/// Get data about a relic's tiers and drops
		/// </summary>
		/// <param name="tier">Lith, Meso, Neo, Axi, or Requiem</param>
		/// <param name="name">Names look like O5, K4, Z2</param>
		/// <returns>The relic named "tier" "name"</returns>
		internal async Task<Relic> GetRelicLootAsync(string tier, string name)
		{
			try
			{
				string response = await http.GetStringAsync($"relics/{tier}/{name}.json");
				return JsonSerializer.Deserialize<Relic>(response);
			}
			catch (HttpRequestException)
			{
				throw new ArgumentException($"There is no relic {tier} {name}");
			}
		}
	}
}
