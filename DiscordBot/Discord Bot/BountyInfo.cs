namespace WarframeDiscordBot
{
	public class BountyInfo
	{
		public string _id { get; set; }
		public string bountyLevel { get; set; }
		public BountyRewardInfo rewards { get; set; }

		public override string ToString()
		{
			return $@"{bountyLevel} drops:
{rewards}";
		}
	}
	public class BountyRewardInfo
	{
		public BountyLootInfo[] A { get; set; }
		public BountyLootInfo[] B { get; set; }
		public BountyLootInfo[] C { get; set; }

		public override string ToString()
		{
			return $@"	- Bounty A:
{LootInfo.ListToString(A)}
	- Bounty B:
{LootInfo.ListToString(B)}
	- Bounty C:
{LootInfo.ListToString(C)}";
		}
	}

	public class BountyLootInfo : LootInfo
	{
		public string stage { get; set; }

		public override string ToString()
		{
			return base.ToString() + $" in {stage}";
		}
	}
}