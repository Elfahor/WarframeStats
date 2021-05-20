namespace WarframeDiscordBot
{
	public class RelicInfo
	{
		public string tier { get; set; }
		public string name { get; set; }
		public RelicRewards rewards { get; set; }

		public override string ToString()
		{
			return $@"The relic {tier} {name} contains the following rewards:
{rewards}";
		}
	}

	public class RelicRewards
	{
		public LootInfo[] Intact { get; set; }
		public LootInfo[] Exceptional { get; set; }
		public LootInfo[] Flawless { get; set; }
		public LootInfo[] Radiant { get; set; }

		public override string ToString()
		{
			return $@"	- Intact:
{LootInfo.ListToString(Intact)}
	- Exceptionnal:
{LootInfo.ListToString(Exceptional)}
	- Flawless:
{LootInfo.ListToString(Flawless)}
	- Radiant:
{LootInfo.ListToString(Radiant)}";
		}
	}
}