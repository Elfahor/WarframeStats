using System;
using System.Text.Json.Serialization;

namespace WarframeStats.WorldState
{
	/// <summary>
	/// Nightwave challenges, season...
	/// </summary>
	public class Nightwave
	{
		/// <summary>
		/// The current challenges you can complete to get standing
		/// </summary>
		[JsonPropertyName("activeChallenges")]
		public NWChallenge[] ActiveChallenges { get; internal set; }

		/// <summary>
		/// Date at which the current season will end
		/// </summary>
		[JsonPropertyName("expiry")]
		public DateTime ExpiryDate { get; internal set; }

		/// <summary>
		/// Current Season
		/// </summary>
		[JsonPropertyName("season")]
		public int Season { get; internal set; }

		/// <summary>
		/// Date at which the current season started
		/// </summary>
		[JsonPropertyName("activation")]
		public DateTime StartDate { get; internal set; }

		/// <summary>
		/// This seems to be a legacy name for nightwave
		/// </summary>
		[JsonPropertyName("tag")]
		public string Tag { get; internal set; }

		/// <summary>
		/// Time until ExpiryDate
		/// </summary>
		public string TimeRemaining => (DateTime.UtcNow - ExpiryDate).Negate().ToWFString();

		internal Params _params { get; set; }
		internal bool active { get; set; }
		internal string id { get; set; }
		internal int phase { get; set; }
		internal object[] possibleChallenges { get; set; }
		internal string[] rewardTypes { get; set; }
		internal string startString { get; set; }

		/// <summary>
		/// Challenges that give Nightwave points
		/// </summary>
		public class NWChallenge
		{
			/// <summary>
			/// What the player must do
			/// </summary>
			[JsonPropertyName("desc")]
			public string Description { get; internal set; }

			[JsonPropertyName("expiry")]
			public DateTime ExpiryDate { get; internal set; }

			/// <summary>
			/// The challenge is a daily challenge
			/// </summary>
			[JsonPropertyName("isDaily")]
			public bool IsDaily { get; internal set; }

			/// <summary>
			/// The challenge is an elite weekly challenge
			/// </summary>
			[JsonPropertyName("isElite")]
			public bool IsElite { get; internal set; }

			/// <summary>
			/// Standing gave by the challenge: 
			/// 1000 for daily, 4500 for weekly, 7000 for elite weekly
			/// </summary>
			[JsonPropertyName("reputation")]
			public int StandingGave { get; internal set; }

			/// <summary>
			/// Date at which the challenge started
			/// </summary>
			[JsonPropertyName("activation")]
			public DateTime StartDate { get; internal set; }
			/// <summary>
			/// Name of the challenge
			/// </summary>
			[JsonPropertyName("title")]
			public string Title { get; internal set; }

			/// <summary>
			/// Time until ExpiryDate
			/// </summary>
			public string TimeRemaining => (DateTime.UtcNow - ExpiryDate).Negate().ToWFString();

			internal bool active { get; set; }
			internal string id { get; set; }
			internal string startString { get; set; }
		}
	}
}