namespace WarframeStats.Drops
{
	public class Mission
	{
		public string gameMode { get; set; }
		public bool isEvent { get; set; }
		public Rewards rewards { get; set; }
		public string planet { get; set; }
		public string location { get; set; }
	}

	public class Rewards
	{
		public Loot[] A { get; set; }
		public Loot[] B { get; set; }
		public Loot[] C { get; set; }
	}

	public class Loot
	{
		public string _id { get; set; }
		public string itemName { get; set; }
		public string rarity { get; set; }
		public float chance { get; set; }
	}
}