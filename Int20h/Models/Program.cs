using System;
using Newtonsoft.Json;

namespace Int20h.Models
{
	public class Program
	{
		[JsonProperty("image")]
		public Image Image { get; set; }
		[JsonProperty("realtime_begin")]
		public long BeginTime { get; set; }
		[JsonProperty("realtime_end")]
		public long EndTime { get; set; }
		[JsonProperty("title")]
		public string Title { get; set; }
		[JsonProperty("subtitle")]
		public string Subtitle { get; set; }

        public string Time => $"{DateTimeOffset.FromUnixTimeSeconds(BeginTime).ToString(@"t")} - {DateTimeOffset.FromUnixTimeSeconds(EndTime).ToString(@"t")}";
    }
}