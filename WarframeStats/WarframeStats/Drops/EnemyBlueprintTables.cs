namespace WarframeStats.Drops
{
	public class EnemyBlueprintTables
	{
		public EnemyBlueprint[] enemyBlueprintTables { get; set; }
	}

	public class EnemyBlueprint
	{
		public string _id { get; set; }
		public string enemyName { get; set; }
		public string enemyItemDropChance { get; set; }
		public string blueprintDropChance { get; set; }
		public Item[] items { get; set; }
		public EnemyDroppedMod[] mods { get; set; }
	}

	public class Item
	{
		public string _id { get; set; }
		public string itemName { get; set; }
		public string rarity { get; set; }
		public float chance { get; set; }
	}
}