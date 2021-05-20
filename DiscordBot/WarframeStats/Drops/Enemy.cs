namespace WarframeStats.Drops
{
	public class EnemyModTable
	{
		public Enemy[] enemyModTables { get; set; }
	}

	public class Enemy
	{
		public string _id { get; set; }
		public string enemyName { get; set; }
		public string ememyModDropChance { get; set; }
		public string enemyModDropChance { get; set; }
		public EnemyDroppedMod[] mods { get; set; }
	}

	public class EnemyDroppedMod
	{
		public string _id { get; set; }
		public string modName { get; set; }
		public string rarity { get; set; }
		public float? chance { get; set; }
	}
}