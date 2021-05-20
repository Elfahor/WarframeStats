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
