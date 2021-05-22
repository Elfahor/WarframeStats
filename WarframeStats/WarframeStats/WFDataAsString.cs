using WarframeStats.Drops;
using WarframeStats.WorldState;

namespace WarframeStats
{
	/// <summary>
	/// Offers conversion of various objects into a human-readable format
	/// </summary>
	public static class WFDataAsString
	{
		/// <summary>
		/// Converts SortieRewards in a human readable format
		/// </summary>
		public static string SortieRewards(SortieRewards sortieRewards)
		{
			string repr = "Sorties can loot:\n";
			foreach (Loot item in sortieRewards.sortieRewards)
			{
				repr += $"	- {item.itemName}: {item.chance}% ({item.rarity})\n";
			}
			return repr;
		}

		/// <summary>
		/// Converts Event[] in a human readable format
		/// </summary>
		public static string Events(Event[] events)
		{
			string repr = $"Event list:\n";
			foreach (Event e in events)
			{
				repr += $"	 - {e.description} on {e.node}\n";
			}
			if (events.Length == 0)
			{
				repr = "There are no events currently";
			}
			return repr;
		}

		/// <summary>
		/// Converts News[] in a human readable format
		/// </summary>
		public static string News(News[] news)
		{
			string repr = $"Here are the news:\n";
			foreach (News n in news)
			{
				repr += $"	 - {n.message}\n";
			}
			if (news.Length == 0)
			{
				repr = "There are no news currently";
			}
			return repr;
		}

		/// <summary>
		/// Converts Sortie in a human readable format
		/// </summary>
		public static string Sortie(Sortie sortie)
		{
			string repr = $"Today's sortie:\n" +
					$"	- Faction: {sortie.faction}\n" +
					$"	- Boss: {sortie.boss}\n" +
					$"	- Time remaining: {sortie.TimeRemaining}\n" +
					$"	- Missions:\n";
			for (int i = 0; i < sortie.missions.Length; i++)
			{
				SortieMission variant = sortie.missions[i];
				repr += $"		- Mission {i + 1}: {variant.missionType}, {variant.modifierDescription}\n";
			}
			return repr;
		}

		/// <summary>
		/// Converts Fissure[] in a human readable format
		/// </summary>
		public static string Fissures(Fissure[] fissures, string tier)
		{
			string repr = $"Current {tier} void fissures\n";
			foreach (Fissure fissure in fissures)
			{
				if (fissure.tier == tier)
				{
					repr += $"	- {fissure.node}:\n" +
					$"		- Type: {fissure.missionKey}\n" +
					$"		- Railjack: {(fissure.isStorm ? "Yes" : "No")}\n" +
					$"		- Faction: {fissure.enemyKey}\n" +
					$"		- Time remaining: {fissure.timeRemaining}\n";
				}
			}
			return repr;
		}

		/// <summary>
		/// Converts VoidTrader in a human readable format
		/// </summary>
		public static string VoidTrader(VoidTrader voidTrader)
		{
			string repr;
			if (voidTrader.active)
			{
				repr = $"{voidTrader.character} is currently at {voidTrader.location}!";
			}
			else
			{
				repr = $"{voidTrader.character} is due in {voidTrader.startString} at {voidTrader.location}";

			}
			return repr;
		}

		/// <summary>
		/// Converts Relic in a human readable format
		/// </summary>
		public static string Relic(Relic relic)
		{
			string repr = $"Relic {relic.tier} {relic.name} contains:\n" +
				$" - Intact:\n" +
				$"{Loots(relic.rewards.Intact)}\n" +
				$" - Exceptionnal:\n" +
				$"{Loots(relic.rewards.Exceptional)}\n" +
				$" - Flawless:\n" +
				$"{Loots(relic.rewards.Flawless)}\n" +
				$" - Radiant:\n" +
				$"{Loots(relic.rewards.Radiant)}\n";
			return repr;
		}

		/// <summary>
		/// Converts Loot in a human readable format
		/// </summary>
		public static string Loot(Loot loot)
		{
			string repr = $"{loot.itemName}: {loot.chance}% ({loot.rarity})";
			return repr;
		}

		/// <summary>
		/// Converts Loot[] in a human readable format
		/// </summary>
		public static string Loots(Loot[] loots)
		{
			string repr = "";
			foreach (Loot loot in loots)
			{
				repr += $"	- {Loot(loot)}\n";
			}
			return repr;
		}

		//public static string ModEnemiesDroppedOn(Mod.EnemyDroppedOn[] enemiesDroppedOn)
		//{
		//	string repr = "The mod drops on:\n";
		//	foreach (Mod.EnemyDroppedOn enemy in enemiesDroppedOn)
		//	{
		//		repr += $"	- {enemy.enemyName}: {enemy.chance}% ({enemy.rarity})\n";
		//	}
		//	return repr;
		//}

		public static string DropLocationsItems(DropLocation[] dropLocations, string searchedName, bool strictMatch = true)
		{
			string repr = $"{searchedName} drops on:\n";
			foreach (DropLocation location in dropLocations)
			{
				repr += $"	- {location.place}: {location.chance}% " + (strictMatch ? "" : $"of {location.item} ") + $"({location.rarity})\n";
			}
			return repr;
		}

		public static string DropLocationsLocations(DropLocation[] dropLocations)
		{
			string repr = $"{dropLocations[0].place} drops:\n";
			foreach (DropLocation location in dropLocations)
			{
				repr += $"	- {location.item}: {location.chance}% ({location.rarity})\n";
			}
			return repr;
		}
	}
}
