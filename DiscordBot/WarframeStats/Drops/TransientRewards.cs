namespace WarframeStats.Drops
{
	public class TransientRewards
	{
		public TransientReward[] transientRewards { get; set; }
	}

	public class TransientReward
	{
		public string _id { get; set; }
		public string objectiveName { get; set; }
		public TransientLoot[] rewards { get; set; }
	}

	public class TransientLoot : Loot
	{
		public string rotation { get; set; }
	}
}