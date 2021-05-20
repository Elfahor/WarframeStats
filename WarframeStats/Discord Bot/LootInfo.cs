namespace WarframeDiscordBot
{
	public class LootInfo
	{
		public string _id { get; set; }
		public string itemName { get; set; }
		public string rarity { get; set; }
		public float chance { get; set; }

		public override string ToString()
		{
			return $"{itemName}: {chance}% ({rarity})";
		}

		public static string ListToString(LootInfo[] loots)
		{
			string ret = "";
			foreach (LootInfo loot in loots)
			{
				ret = $"{ret}		- {loot}\n";
			}
			return ret;
		}
	}
}