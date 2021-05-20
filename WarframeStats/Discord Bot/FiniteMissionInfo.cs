namespace WarframeDiscordBot
{
	public class FiniteMissionInfo : MissionInfo
	{
		public LootInfo[] rewards { get; set; }

		public override string ToString()
		{
			return $@"{location} on {planet} ({gameMode}) has the following rewards:
{LootInfo.ListToString(rewards)}";
		}
	}
}