using System.Threading.Tasks;

namespace WarframeStats
{
	/// <summary>
	/// Full client for all services: world state, player stats, drop data
	/// </summary>
	public class WarframeClient
	{
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

		/// <summary>
		/// Client for getting drop data
		/// </summary>
		public Drops.DropClient dropClient { get; }
		/// <summary>
		/// Reward pool for sorties
		/// </summary>
		public Drops.SortieRewards SortieRewards { get => dropClient.SortieRewards; }
		/// <summary>
		/// Reward pool for special missions: Razoback Armadas, Nightmare mode...
		/// </summary>
		public Drops.TransientRewards TransientRewards { get => dropClient.TransientRewards; }
		/// <summary>
		/// Get data about a relic's tiers and drops
		/// </summary>
		/// <param name="tier">Lith, Meso, Neo, Axi, or Requiem</param>
		/// <param name="name">Names look like O5, K4, Z2</param>
		/// <returns>The relic named "tier" "name"</returns>
		public async Task<Drops.Relic> GetRelicLootAsync(string tier, string name) => await dropClient.GetRelicLootAsync(tier, name);
		/// <summary>
		/// Get the reward pool for a mission
		/// </summary>
		/// <param name="planet">Earth, Ceres, Sedna...</param>
		/// <param name="node">The name of the mission (e.g. Apollo)</param>
		public async Task<Drops.MissionNode> GetMissionLootAsync(string planet, string node) => await dropClient.GetMissionLootAsync(planet, node);

		public WarframeClient()
		{
			worldStateClient = new WorldState.WorldStateClient();
			dropClient = new Drops.DropClient();
		}

		/// <summary>
		/// Refreshes all the data for all services
		/// </summary>
		/// <param name="platform">Either "pc", "ps4" or "xb1"</param>
		/// <returns></returns>
		public async Task RefreshDataAsync(string platform)
		{
			await worldStateClient.RefreshDataAsync(platform);
			await dropClient.RefreshDataAsync();
		}

	}
}
