namespace WarframeStats.Drops
{
	public class BlueprintLocations
	{
		public Blueprint[] blueprintLocations { get; set; }
	}

	public class Blueprint
	{
		public string _id { get; set; }
		public string itemName { get; set; }
		public string blueprintName { get; set; }
		public EnemyBlueprintDropsOn[] enemies { get; set; }
	}

	public class EnemyBlueprintDropsOn
	{
		public string _id { get; set; }
		public string enemyName { get; set; }
		public float enemyItemDropChance { get; set; }
		public float enemyBlueprintDropChance { get; set; }
		public string rarity { get; set; }
		public float chance { get; set; }
	}
}