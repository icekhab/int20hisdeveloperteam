using Newtonsoft.Json;

namespace Int20h.Models
{
	public class Image
	{
		[JsonProperty("preview")]
		public string PreviewUrl { get; set; }
	}
}