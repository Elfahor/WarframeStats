using System;
using System.Text.Json.Serialization;

namespace WarframeStats.WorldState
{
	/// <summary>
	/// Full data about the state of the world
	/// </summary>
	public class WorldState
	{
		/// <summary>
		/// When the data have last been updated
		/// </summary>
		public DateTime timestamp { get; set; }
		/// <summary>
		/// News about Prime Access, DevStreams, etc.
		/// </summary>
		public News[] news { get; set; }
		/// <summary>
		/// Current events, e.g. Thermia Fractures
		/// </summary>
		public Event[] events { get; set; }
		/// <summary>
		/// You shouldn't use this as alerts have been removed from the game. This is only here to help with API deserialization.
		/// </summary>
		[Obsolete]
		public object[] alerts { get; set; }
		/// <summary>
		/// Info about the current sortie: boss, faction, modifiers
		/// </summary>
		public Sortie sortie { get; set; }
		/// <summary>
		/// All Syndicate Missions. Not recommended right now.
		/// </summary>
		public SyndicateMissions[] syndicateMissions { get; set; }
		/// <summary>
		/// Void fissures data: tier, node, etc.
		/// </summary>
		public Fissure[] fissures { get; set; }
		/// <summary>
		/// You shouldn't use this. This is only here to help with API deserialization.
		/// </summary>
		[Obsolete]
		public object[] globalUpgrades { get; set; }
		/// <summary>
		/// I don't know what that is but it is in the API
		/// </summary>
		public FlashSale[] flashSales { get; set; }
		/// <summary>
		/// Invasions data: reward, factions, nodes
		/// </summary>
		public Invasion[] invasions { get; set; }
		/// <summary>
		/// You shouldn't use this as dark sectors have been removed from the game. This is only here to help with API deserialization.
		/// </summary>
		[Obsolete]
		public object[] darkSectors { get; set; }
		/// <summary>
		/// When baro
		/// </summary>
		public VoidTrader voidTrader { get; set; }
		/// <summary>
		/// Darvo's deals
		/// </summary>
		public DailyDeal[] dailyDeals { get; set; }
		/// <summary>
		/// Daily Simaris synthesis
		/// </summary>
		public Simaris simaris { get; set; }
		/// <summary>
		/// Nobody plays the CONCLAVE
		/// </summary>
		public ConclaveChallenge[] conclaveChallenges { get; set; }
		/// <summary>
		/// You shouldn't use this as persistent enemies have been removed from the game. This is only here to help with API deserialization.
		/// </summary>
		[Obsolete]
		public object[] persistentEnemies { get; set; }
		/// <summary>
		/// Day/Night cycle on Earth
		/// </summary>
		public EarthCycle earthCycle { get; set; }
		/// <summary>
		/// Day/Night cycle in the PoE
		/// </summary>
		public CetusCycle cetusCycle { get; set; }
		/// <summary>
		/// Wyrm cycle on Deimos
		/// </summary>
		public CambionCycle cambionCycle { get; set; }
		/// <summary>
		/// I don't know what that is but it is probably some removed content
		/// </summary>
		[Obsolete]
		public object[] weeklyChallenges { get; set; }
		/// <summary>
		/// Progress for the construction of Razorback Armadas and Balor Fomorians
		/// </summary>
		public ConstructionProgress constructionProgress { get; set; }
		/// <summary>
		/// Warm/Cold cycle in the Orb Vallis
		/// </summary>
		public VallisCycle vallisCycle { get; set; }
		/// <summary>
		/// Nightwave challenges
		/// </summary>
		public Nightwave nightwave { get; set; }
		/// <summary>
		/// I guess it's kuva siphons/flood, but it always gives empty list.
		/// </summary>
		[Obsolete]
		public object[] kuva { get; set; }
		/// <summary>
		/// The current Arbitration
		/// </summary>
		public Arbitration arbitration { get; set; }
		/// <summary>
		/// Sentient anomaly in the Veil Proxima
		/// </summary>
		public SentientOutposts sentientOutposts { get; set; }
		/// <summary>
		/// The current rotation item and the possible items
		/// </summary>
		public SteelPathDeals steelPath { get; set; }
	}

	/// <summary>
	/// Info about the current sortie: boss, faction, modifiers
	/// </summary>
	public class Sortie
	{
		internal string id { get; set; }
		[JsonPropertyName("activation")]
		public DateTime StartDate { get; set; }
		internal string startString { get; set; }
		[JsonPropertyName("expiry")]
		public DateTime ExpiryDate { get; set; }
		internal bool active { get; set; }
		internal string rewardPool { get; set; }
		/// <summary>
		/// The different missions with their modifiers
		/// </summary>
		[JsonPropertyName("variants")]
		public SortieMission[] Missions { get; set; }
		[JsonPropertyName("boss")]
		public string Boss { get; set; }
		[JsonPropertyName("faction")]
		public string Faction { get; set; }
		internal bool expired { get; set; }
		internal string eta { get; set; }

		public string TimeRemaining => (DateTime.Now.ToUniversalTime() - ExpiryDate.ToUniversalTime()).Negate().ToWFString();

		public class SortieMission
		{
			public string boss { get; set; }
			public string planet { get; set; }
			public string missionType { get; set; }
			public string modifier { get; set; }
			public string modifierDescription { get; set; }
			public string node { get; set; }
		}
	}


	public class VoidTrader
	{
		internal string id { get; set; }
		[JsonPropertyName("activation")]
		public DateTime StartDate { get; set; }
		internal string startString { get; set; }
		[JsonPropertyName("expiry")]
		public DateTime ExpiryDate { get; set; }
		[JsonPropertyName("active")]
		public bool Active { get; set; }
		[JsonPropertyName("character")]
		public string TraderName { get; set; }
		[JsonPropertyName("location")]
		public string Location { get; set; }
		[JsonPropertyName("inventory"), Obsolete]
		public object[] Inventory { get; set; }
		internal string psId { get; set; }
		internal string endString { get; set; }

		public string TimeRemaining => (DateTime.Now.ToUniversalTime() - ExpiryDate.ToUniversalTime()).Negate().ToWFString();
	}

	public class Simaris
	{
		public string target { get; set; }
		public bool isTargetActive { get; set; }
		public string asString { get; set; }
	}

	public class EarthCycle
	{
		public string id { get; set; }
		public DateTime expiry { get; set; }
		public DateTime activation { get; set; }
		public bool isDay { get; set; }
		public string state { get; set; }
		public string timeLeft { get; set; }
	}

	public class CetusCycle
	{
		public string id { get; set; }
		public DateTime expiry { get; set; }
		public DateTime activation { get; set; }
		public bool isDay { get; set; }
		public string state { get; set; }
		public string timeLeft { get; set; }
		public bool isCetus { get; set; }
		public string shortString { get; set; }
	}

	public class CambionCycle
	{
		public string id { get; set; }
		public DateTime activation { get; set; }
		public DateTime expiry { get; set; }
		public string active { get; set; }
	}

	public class ConstructionProgress
	{
		public string id { get; set; }
		public string fomorianProgress { get; set; }
		public string razorbackProgress { get; set; }
		public string unknownProgress { get; set; }
	}

	public class VallisCycle
	{
		public string id { get; set; }
		public DateTime expiry { get; set; }
		public bool isWarm { get; set; }
		public string state { get; set; }
		public DateTime activation { get; set; }
		public string timeLeft { get; set; }
		public string shortString { get; set; }
	}

	public class Nightwave
	{
		public string id { get; set; }
		public DateTime activation { get; set; }
		public string startString { get; set; }
		public DateTime expiry { get; set; }
		public bool active { get; set; }
		public int season { get; set; }
		public string tag { get; set; }
		public int phase { get; set; }
		public Params _params { get; set; }
		public object[] possibleChallenges { get; set; }
		public NWChallenge[] activeChallenges { get; set; }
		public string[] rewardTypes { get; set; }
	}

	public class Params
	{
	}

	public class NWChallenge
	{
		public string id { get; set; }
		public DateTime activation { get; set; }
		public string startString { get; set; }
		public DateTime expiry { get; set; }
		public bool active { get; set; }
		public bool isDaily { get; set; }
		public bool isElite { get; set; }
		public string desc { get; set; }
		public string title { get; set; }
		public int reputation { get; set; }
	}

	public class Arbitration
	{
		public DateTime activation { get; set; }
		public DateTime expiry { get; set; }
		public string enemy { get; set; }
		public string type { get; set; }
		public bool archwing { get; set; }
		/// <summary>
		/// Dunno
		/// </summary>
		public bool sharkwing { get; set; }
		public string node { get; set; }
	}

	public class SentientOutposts
	{
		public Mission mission { get; set; }
		public DateTime activation { get; set; }
		public DateTime expiry { get; set; }
		public bool active { get; set; }
		public string id { get; set; }
	}

	public class Mission
	{
		public string node { get; set; }
		public string faction { get; set; }
		public string type { get; set; }
	}

	public class SteelPathDeals
	{
		public SteelPathReward currentReward { get; set; }
		public DateTime activation { get; set; }
		public DateTime expiry { get; set; }
		public string remaining { get; set; }
		public SteelPathReward[] rotation { get; set; }
	}

	public class SteelPathReward
	{
		public string name { get; set; }
		public int cost { get; set; }
	}

	public class News
	{
		public string id { get; set; }
		public string message { get; set; }
		public string link { get; set; }
		public string imageLink { get; set; }
		public bool priority { get; set; }
		public DateTime date { get; set; }
		public string eta { get; set; }
		public bool update { get; set; }
		public bool primeAccess { get; set; }
		public bool stream { get; set; }
		public Translations translations { get; set; }
		public string asString { get; set; }
	}

	public class Translations
	{
		public string en { get; set; }
		public string fr { get; set; }
		public string it { get; set; }
		public string de { get; set; }
		public string es { get; set; }
		public string pt { get; set; }
		public string ru { get; set; }
		public string pl { get; set; }
		public string tr { get; set; }
		public string ja { get; set; }
		public string zh { get; set; }
		public string ko { get; set; }
		public string tc { get; set; }
	}

	public class Event
	{
		public string id { get; set; }
		public DateTime activation { get; set; }
		public string startString { get; set; }
		public DateTime expiry { get; set; }
		public bool active { get; set; }
		public int maximumScore { get; set; }
		public int currentScore { get; set; }
		public object smallInterval { get; set; }
		public object largeInterval { get; set; }
		public string description { get; set; }
		public string tooltip { get; set; }
		public string node { get; set; }
		public object[] concurrentNodes { get; set; }
		public string scoreLocTag { get; set; }
		public Reward[] rewards { get; set; }
		public bool expired { get; set; }
		public float health { get; set; }
		public Interimstep[] interimSteps { get; set; }
		public object[] progressSteps { get; set; }
		public bool isPersonal { get; set; }
		public bool isCommunity { get; set; }
		public object[] regionDrops { get; set; }
		public object[] archwingDrops { get; set; }
		public string asString { get; set; }
		public Metadata metadata { get; set; }
		public object[] completionBonuses { get; set; }
		public string scoreVar { get; set; }
		public DateTime altExpiry { get; set; }
		public DateTime altActivation { get; set; }
		public Nextalt nextAlt { get; set; }
	}

	public class Metadata
	{
	}

	public class Nextalt
	{
		public DateTime expiry { get; set; }
		public DateTime activation { get; set; }
	}

	public class Reward
	{
		public string[] items { get; set; }
		public object[] countedItems { get; set; }
		public int credits { get; set; }
		public string asString { get; set; }
		public string itemString { get; set; }
		public string thumbnail { get; set; }
		public int color { get; set; }
	}

	public class Interimstep
	{
		public int goal { get; set; }
		public Reward reward { get; set; }
		public Message message { get; set; }
	}

	public class Message
	{
	}

	public class SyndicateMissions
	{
		public string id { get; set; }
		public DateTime activation { get; set; }
		public string startString { get; set; }
		public DateTime expiry { get; set; }
		public bool active { get; set; }
		public string syndicate { get; set; }
		public string[] nodes { get; set; }
		public Job[] jobs { get; set; }
		public string eta { get; set; }
	}

	public class Job
	{
		public string id { get; set; }
		public string[] rewardPool { get; set; }
		public string type { get; set; }
		public int[] enemyLevels { get; set; }
		public int[] standingStages { get; set; }
		public int minMR { get; set; }
		public bool isVault { get; set; }
		public string locationTag { get; set; }
	}

	public class Fissure
	{
		public string id { get; set; }
		public DateTime activation { get; set; }
		public string startString { get; set; }
		public DateTime expiry { get; set; }
		public bool active { get; set; }
		public string node { get; set; }
		public string missionType { get; set; }
		public string missionKey { get; set; }
		public string enemy { get; set; }
		public string enemyKey { get; set; }
		public string nodeKey { get; set; }
		public string tier { get; set; }
		public int tierNum { get; set; }
		public bool expired { get; set; }
		[JsonPropertyName("eta")]
		public string timeRemaining { get; set; }
		public bool isStorm { get; set; }
	}

	public class FlashSale
	{
		public string item { get; set; }
		public DateTime expiry { get; set; }
		public DateTime activation { get; set; }
		public int discount { get; set; }
		public int regularOverride { get; set; }
		public int premiumOverride { get; set; }
		public bool isShownInMarket { get; set; }
		public bool isFeatured { get; set; }
		public bool isPopular { get; set; }
		public string id { get; set; }
		public bool expired { get; set; }
		public string eta { get; set; }
	}

	public class Invasion
	{
		public string id { get; set; }
		public DateTime activation { get; set; }
		public string startString { get; set; }
		public string node { get; set; }
		public string nodeKey { get; set; }
		public string desc { get; set; }
		public Attackerreward attackerReward { get; set; }
		public string attackingFaction { get; set; }
		public Attacker attacker { get; set; }
		public DefenderReward defenderReward { get; set; }
		public string defendingFaction { get; set; }
		public Defender defender { get; set; }
		public bool vsInfestation { get; set; }
		public int count { get; set; }
		public int requiredRuns { get; set; }
		public float completion { get; set; }
		public bool completed { get; set; }
		public string eta { get; set; }
		public string[] rewardTypes { get; set; }
	}

	public class Attackerreward
	{
		public object[] items { get; set; }
		public CountedItem[] countedItems { get; set; }
		public int credits { get; set; }
		public string asString { get; set; }
		public string itemString { get; set; }
		public string thumbnail { get; set; }
		public int color { get; set; }
	}

	public class CountedItem
	{
		public int count { get; set; }
		public string type { get; set; }
		public string key { get; set; }
	}

	public class Attacker
	{
		public Reward reward { get; set; }
		public string faction { get; set; }
		public string factionKey { get; set; }
	}

	public class DefenderReward
	{
		public object[] items { get; set; }
		public CountedItem[] countedItems { get; set; }
		public int credits { get; set; }
		public string asString { get; set; }
		public string itemString { get; set; }
		public string thumbnail { get; set; }
		public int color { get; set; }
	}

	public class Defender
	{
		public Reward reward { get; set; }
		public string faction { get; set; }
		public string factionKey { get; set; }
	}
	public class DailyDeal
	{
		public string item { get; set; }
		public DateTime expiry { get; set; }
		public DateTime activation { get; set; }
		public int originalPrice { get; set; }
		public int salePrice { get; set; }
		public int total { get; set; }
		public int sold { get; set; }
		public string id { get; set; }
		public string eta { get; set; }
		public int discount { get; set; }
	}

	public class ConclaveChallenge
	{
		public string id { get; set; }
		public DateTime expiry { get; set; }
		public DateTime activation { get; set; }
		public int amount { get; set; }
		public string mode { get; set; }
		public string category { get; set; }
		public string eta { get; set; }
		public bool expired { get; set; }
		public bool daily { get; set; }
		public bool rootChallenge { get; set; }
		public string endString { get; set; }
		public string description { get; set; }
		public string title { get; set; }
		public int standing { get; set; }
		public string asString { get; set; }
	}
}