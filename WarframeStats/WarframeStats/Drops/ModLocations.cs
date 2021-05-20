namespace WarframeStats.Drops
{
	public class ModLocations
	{
		public Mod[] modLocations { get; set; }
	}

	public class Mod
	{
		public string _id { get; set; }
		public string modName { get; set; }
		public EnemyModDropsOn[] enemies { get; set; }
	}

	public class EnemyModDropsOn
	{
		public string _id { get; set; }
		public string enemyName { get; set; }
		public float enemyModDropChance { get; set; }
		public string rarity { get; set; }
		public float? chance { get; set; }
	}
}