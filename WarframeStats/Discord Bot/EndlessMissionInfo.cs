namespace WarframeDiscordBot
{
	public class EndlessMissionInfo : MissionInfo
	{
		public EndlessMissionRewards rewards { get; set; }

		public override string ToString()
		{
			return $@"{location} on {planet} ({gameMode}) has the following rewards:
{rewards}";
		}

		public class EndlessMissionRewards
		{
			public LootInfo[] A { get; set; }
			public LootInfo[] B { get; set; }
			public LootInfo[] C { get; set; }

			public override string ToString()
			{
				return $@"	- Rotation A:
{LootInfo.ListToString(A)}
	- Rotation B:
{LootInfo.ListToString(B)}
	- Rotation C:
{LootInfo.ListToString(C)}";
			}
		}
	}
}


