using Discord;
using Discord.WebSocket;
using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WarframeStats;
using WarframeStats.WorldState;
using WarframeStats.Drops;

namespace WarframeDiscordBot
{
	internal class Program
	{
		private static readonly string botToken = File.ReadAllText("bot_token.txt");

		private static readonly DiscordSocketClient discordClient = new DiscordSocketClient();
		private static readonly HttpClientHandler handler = new HttpClientHandler();
		private static readonly HttpClient httpClient = new HttpClient(handler);
		private static readonly WarframeClient warframeClient = new WarframeClient();

		private class BotActivity : IActivity
		{
			public string Name => "ws.warframestat.us";

			public ActivityType Type => ActivityType.Listening;

			public ActivityProperties Flags => ActivityProperties.None;

			public string Details => "ws.warframestat.us";
		}

		private static void Main() => MainAsync().GetAwaiter().GetResult();

		private static async Task MainAsync()
		{
			discordClient.Log += Log;
			discordClient.MessageReceived += MessageReceived;

			//handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
			httpClient.BaseAddress = new Uri("https://drops.warframestat.us/data/");

			await warframeClient.RefreshDataAsync("pc");
			await warframeClient.RefreshDataAsync("ps4");
			await warframeClient.RefreshDataAsync("xb1");

			await discordClient.LoginAsync(TokenType.Bot, botToken);
			await discordClient.StartAsync();

			await discordClient.SetActivityAsync(new BotActivity());

			await Task.Delay(-1);
		}

		private static async Task<Task> Log(LogMessage message)
		{
			await Task.Run(() => Console.WriteLine(message.ToString()));
			return Task.CompletedTask;
		}

		private static async Task<Task> MessageReceived(SocketMessage message)
		{
			string[] words = message.Content.Split(" ");

			//if (message.Content.StartsWith("what at location "))
			//{
			//	try
			//	{
			//		string response = await httpClient.GetStringAsync($"missionRewards/{words[3]}/{words[4]}.json");
			//		try
			//		{
			//			EndlessMissionInfo missionInfo = JsonSerializer.Deserialize<EndlessMissionInfo>(response);
			//			await message.Channel.SendMessageAsync(missionInfo.ToString());
			//		}
			//		catch (JsonException)
			//		{
			//			FiniteMissionInfo missionInfo = JsonSerializer.Deserialize<FiniteMissionInfo>(response);
			//			await message.Channel.SendMessageAsync(missionInfo.ToString());
			//		}
			//
			//	}
			//	catch (HttpRequestException)
			//	{
			//		await message.Channel.SendMessageAsync($"There is no mission node called {words[4]} on {words[3]} !");
			//	}
			//}
			//
			//if (message.Content.StartsWith("what in relic "))
			//{
			//	try
			//	{
			//		string response = await httpClient.GetStringAsync($"relics/{words[3]}/{words[4]}.json");
			//
			//		RelicInfo relicInfo = JsonSerializer.Deserialize<RelicInfo>(response);
			//
			//		await message.Channel.SendMessageAsync(relicInfo.ToString());
			//	}
			//	catch (HttpRequestException)
			//	{
			//		await message.Channel.SendMessageAsync($"There is no relic {words[3]} {words[4]} !");
			//	}
			//}

			if (message.Content.StartsWith("what in sorties"))
			{
				string repr = "Sorties can loot:\n";
				SortieRewards sortieRewards = warframeClient.SortieRewards;
				foreach (Loot item in sortieRewards.sortieRewards)
				{
					repr += $"	- {item.itemName}: {item.chance}% ({item.rarity})\n";
				}

				await message.Channel.SendMessageAsync(repr);
			}

			if (message.Content.StartsWith("what in transient"))
			{

			}

			if (message.Content.StartsWith("are there events"))
			{
				Event[] events = warframeClient.WorldStatePC.events;

				string repr = $"Event list:\n";
				foreach (Event e in events)
				{
					repr += $"	 - {e.description} on {e.node}\n";
				}
				if (events.Length == 0)
				{
					repr = "There are no events currently";
				}

				await message.Channel.SendMessageAsync(repr);
			}

			if (message.Content.StartsWith("give me the news"))
			{
				News[] news = warframeClient.WorldStatePC.news;
				string repr = $"Here are the news:\n";
				foreach (News n in news)
				{
					repr += $"	 - {n.message}\n";
				}
				if (news.Length == 0)
				{
					repr = "There are no news currently";
				}
				await message.Channel.SendMessageAsync(repr);
			}

			if (message.Content.StartsWith("what is the sortie"))
			{
				Sortie sortie = warframeClient.WorldStatePC.sortie;
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
				await message.Channel.SendMessageAsync(repr);
			}

			if (message.Content.StartsWith("what are the void fissures"))
			{
				Fissure[] fissures = warframeClient.WorldStatePC.fissures;

				if (words.Length == 5)
				{
					await message.Channel.SendMessageAsync($"Current void fissures:\n");
					string tmprepr;
					foreach (Fissure fissure in fissures)
					{
						tmprepr = $"	- {fissure.node}:\n" +
							$"		- Type: {fissure.missionKey}\n" +
							$"		- Railjack: {(fissure.isStorm ? "Yes" : "No")}\n" +
							$"		- Tier: {fissure.tier}\n" +
							$"		- Faction: {fissure.enemyKey}\n" +
							$"		- Time remaining: {fissure.timeRemaining}";
						await message.Channel.SendMessageAsync(tmprepr);
					}
				}
				else
				{
					string repr = $"Current {words[5]} void fissures\n";
					foreach (Fissure fissure in fissures)
					{
						if (fissure.tier == words[5])
						{
							repr += $"	- {fissure.node}:\n" +
							$"		- Type: {fissure.missionKey}\n" +
							$"		- Railjack: {(fissure.isStorm ? "Yes" : "No")}\n" +
							$"		- Faction: {fissure.enemyKey}\n" +
							$"		- Time remaining: {fissure.timeRemaining}\n";
						}
					}
					await message.Channel.SendMessageAsync(repr);
				}

			}

			if (message.Content.StartsWith("when baro"))
			{
				VoidTrader voidTrader = warframeClient.WorldStatePC.voidTrader;
				string repr;
				if (voidTrader.active)
				{
					repr = $"{voidTrader.character} is currently at {voidTrader.location}!";
				}
				else
				{
					repr = $"{voidTrader.character} is due in {voidTrader.startString} at {voidTrader.location}";

				}
				await message.Channel.SendMessageAsync(repr);
			}

			return Task.CompletedTask;
		}
	}
}
