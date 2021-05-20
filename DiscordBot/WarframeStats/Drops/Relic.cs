namespace WarframeStats.Drops
{
	public class Relic
	{
		public string tier { get; set; }
		public string name { get; set; }
		public RelicReward rewards { get; set; }
	}

	public class RelicReward
	{
		public Loot[] Intact { get; set; }
		public Loot[] Exceptional { get; set; }
		public Loot[] Flawless { get; set; }
		public Loot[] Radiant { get; set; }
	}
}