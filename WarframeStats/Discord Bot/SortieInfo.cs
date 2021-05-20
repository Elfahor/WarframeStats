namespace WarframeDiscordBot
{
	public class SortieInfo
	{
		public LootInfo[] sortieRewards { get; set; }

		public override string ToString()
		{
			return $@"The sorties can yield:
{LootInfo.ListToString(sortieRewards)}";
		}
	}
}