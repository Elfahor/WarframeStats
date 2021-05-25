using Discord;
using Discord.WebSocket;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using WarframeStats;
using WarframeStats.Drops;
using WarframeStats.WorldState;

namespace WarframeDiscordBot
{
	internal class Program
	{
		private static readonly string botToken = File.ReadAllText("bot_token.txt");

		private static readonly DiscordSocketConfig discordConfig = new DiscordSocketConfig { MessageCacheSize = 1 };
		private static readonly DiscordSocketClient discordClient = new DiscordSocketClient(discordConfig);
		private static readonly HttpClient httpClient = new HttpClient();
		private static readonly WarframeClient warframeClient = new WarframeClient();

		private class BotActivity : IActivity
		{
			public string Name => "api.warframestat.us";

			public ActivityType Type => ActivityType.Listening;

			public ActivityProperties Flags => ActivityProperties.None;

			public string Details => "Made by Elfahor";
		}

		private static async Task Main() => await MainAsync();

		private static async Task MainAsync()
		{
			discordClient.Log += Log;
			discordClient.MessageReceived += MessageReceived;

			httpClient.BaseAddress = new Uri("https://drops.warframestat.us/data/");

			await warframeClient.RefreshDataAsync("pc");
			await warframeClient.RefreshDataAsync("ps4");
			await warframeClient.RefreshDataAsync("xb1");

			await discordClient.LoginAsync(TokenType.Bot, botToken);
			await discordClient.StartAsync();

			await discordClient.SetActivityAsync(new BotActivity());

			await Task.Delay(-1);
		}

		private static async Task Log(LogMessage message)
		{
			await Task.Run(() => Console.WriteLine(message.ToString()));
		}

		private static async Task MessageReceived(SocketMessage message)
		{
			string[] words = message.Content.Split(" ");

			if (message.Content.StartsWith("what at location "))
			{
				try
				{
					try
					{
						EndlessMission missionInfo = (EndlessMission)await warframeClient.GetMissionLootAsync(words[3], words[4]);
						await message.Channel.SendMessageAsync(missionInfo.ToString());
					}
					catch (InvalidCastException)
					{
						FiniteMission missionInfo = (FiniteMission)await warframeClient.GetMissionLootAsync(words[3], words[4]);
						await message.Channel.SendMessageAsync(missionInfo.ToString());
					}
				}
				catch (ArgumentException e)
				{
					await message.Channel.SendMessageAsync(e.Message);
				}
			}

			else if (message.Content.StartsWith("what in relic "))
			{
				try
				{
					Relic relicInfo = await warframeClient.GetRelicLootAsync(words[3], words[4]);
					await message.Channel.SendMessageAsync(WFDataAsString.Relic(relicInfo));
				}
				catch (ArgumentException e)
				{
					await message.Channel.SendMessageAsync(e.Message);
				}
			}

			else if (message.Content.StartsWith("what in sorties"))
			{
				SortieRewards sortieRewards = warframeClient.SortieRewards;
				await message.Channel.SendMessageAsync(WFDataAsString.SortieRewards(sortieRewards));
			}

			else if (message.Content.StartsWith("are there events"))
			{
				Event[] events = warframeClient.WorldStatePC.events;
				await message.Channel.SendMessageAsync(WFDataAsString.Events(events));
			}

			else if (message.Content.StartsWith("give me the news"))
			{
				News[] news = warframeClient.WorldStatePC.news;
				await message.Channel.SendMessageAsync(WFDataAsString.News(news));
			}

			else if (message.Content.StartsWith("what is the sortie"))
			{
				Sortie sortie = warframeClient.WorldStatePC.sortie;
				await message.Channel.SendMessageAsync(WFDataAsString.Sortie(sortie));
			}

			else if (message.Content.StartsWith("what are the void fissures"))
			{
				Fissure[] fissures = warframeClient.WorldStatePC.fissures;
				await message.Channel.SendMessageAsync(WFDataAsString.Fissures(fissures, words[5]));
			}

			else if (message.Content.StartsWith("when baro"))
			{
				VoidTrader voidTrader = warframeClient.WorldStatePC.voidTrader;
				await message.Channel.SendMessageAsync(WFDataAsString.VoidTrader(voidTrader));
			}

			else if (message.Content.StartsWith("where "))
			{
				string itemName = "";
				for (int i = 1; i < words.Length; i++)
				{
					itemName += words[i] + " ";
				}
				itemName = itemName.Trim();
				DropLocation[] locations = await warframeClient.dropClient.SearchForItemAsync(itemName, strictMatch: true);
				await message.Channel.SendMessageAsync(WFDataAsString.DropLocationsItems(locations, itemName));
			}

			else if (message.Content.StartsWith("what "))
			{
				string placeName = "";
				for (int i = 1; i < words.Length; i++)
				{
					placeName += words[i] + " ";
				}
				placeName = placeName.Trim();
				DropLocation[] locations = await warframeClient.dropClient.SearchForLocationAsync(placeName);
				await message.Channel.SendMessageAsync(WFDataAsString.DropLocationsLocations(locations));
			}
		}
	}
}
