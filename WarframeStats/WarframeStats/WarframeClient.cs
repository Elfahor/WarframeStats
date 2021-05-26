using System;
using System.Threading.Tasks;

namespace WarframeStats
{
	/// <summary>
	/// Full client for all services: world state, player stats, drop data
	/// </summary>
	public class WarframeClient : IDisposable
	{
		public WarframeClient()
		{
			worldStateClient = new WorldState.WorldStateClient();
			dropClient = new Drops.DropClient();
		}

		/// <summary>
		/// Client for getting drop data
		/// </summary>
		public Drops.DropClient dropClient { get; }

		/// <summary>
		/// Reward pool for sorties
		/// </summary>
		public Drops.SortieRewards SortieRewards => dropClient.SortieRewards;

		/// <summary>
		/// Reward pool for special missions: Razoback Armadas, Nightmare mode...
		/// </summary>
		public Drops.TransientRewards TransientRewards => dropClient.TransientRewards;

		/// <summary>
		/// Client for getting the world's state
		/// </summary>
		public WorldState.WorldStateClient worldStateClient { get; }
		/// <summary>
		/// The current world state from the warframe API on PC
		/// </summary>
		public WorldState.WorldState WorldStatePC { get => worldStateClient.WorldStatePC; }
		/// <summary>
		/// The current world state from the warframe API on PS4
		/// </summary>
		public WorldState.WorldState WorldStatePS4 { get => worldStateClient.WorldStatePS4; }
		/// <summary>
		/// The current world state from the warframe API on XB1
		/// </summary>
		public WorldState.WorldState WorldStateXB1 { get => worldStateClient.WorldStateXB1; }
		public void Dispose()
		{
			dropClient.Dispose();
			worldStateClient.Dispose();
		}

		/// <summary>
		/// Get the reward pool for a mission
		/// </summary>
		/// <param name="planet">Earth, Ceres, Sedna...</param>
		/// <param name="node">The name of the mission (e.g. Apollo)</param>
		public async Task<Drops.MissionNode> GetMissionLootAsync(string planet, string node) => await dropClient.GetMissionLootAsync(planet, node);

		/// <summary>
		/// Get data about a relic's tiers and drops
		/// </summary>
		/// <param name="tier">Lith, Meso, Neo, Axi, or Requiem</param>
		/// <param name="name">Names look like O5, K4, Z2</param>
		/// <returns>The relic named "tier" "name"</returns>
		public async Task<Drops.Relic> GetRelicLootAsync(string tier, string name) => await dropClient.GetRelicLootAsync(tier, name);

		/// <summary>
		/// Refreshes all the data for all services
		/// </summary>
		/// <param name="platform">Either "pc", "ps4" or "xb1"</param>
		/// <returns>void</returns>
		public async Task RefreshDataAsync(string platform)
		{
			Task worldStateTask = worldStateClient.RefreshDataAsync(platform);
			Task dropTask = dropClient.RefreshDataAsync();
			await worldStateTask;
			await dropTask;
		}

		/// <summary>
		/// Get all locations and items whose name match <paramref name="itemOrLocation"/>
		/// </summary>
		/// <param name="itemOrLocation">what to search</param>
		/// <returns>A list of all the locations/item matching the <paramref name="itemOrLocation"/></returns>
		public async Task<Drops.DropLocation[]> SearchForItemAndLocationAsync(string itemOrLocation) => await dropClient.SearchForItemAndLocationAsync(itemOrLocation);
		/// <summary>
		/// Get all items whose name match <paramref name="itemOrLocation"/>, either strictly or not
		/// </summary>
		/// <param name="item">what to search</param>
		/// <param name="strictMatch">should the search result match strictly <paramref name="item"/></param>
		/// <returns>A list of all the items matching the <paramref name="itemOrLocation"/></returns>
		public async Task<Drops.DropLocation[]> SearchForItemAsync(string item, bool strictMatch = true) => await dropClient.SearchForItemAsync(item, strictMatch);
		/// <summary>
		/// Get all items whose name match <paramref name="location"/>, either strictly or not
		/// </summary>
		/// <param name="location">what to search</param>
		/// <param name="strictMatch">should the search result match strictly <paramref name="location"/></param>
		/// <returns>A list of all the items matching the <paramref name="location"/></returns>
		public async Task<Drops.DropLocation[]> SearchForLocationAsync(string location, bool strictMatch = false) => await dropClient.SearchForLocationAsync(location, strictMatch);
	}
}
