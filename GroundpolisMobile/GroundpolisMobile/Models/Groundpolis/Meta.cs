using Newtonsoft.Json;

namespace GroundpolisMobile
{
	public class Meta
	{
		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("maintainerName")]
		public string MaintainerName { get; set; }

		[JsonProperty("maintainerEmail")]
		public string MaintainerEmail { get; set; }

		[JsonProperty("bannerUrl")]
		public string BannerUrl { get; set; }

		[JsonProperty("version")]
		public string Version { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("maxNoteTextLength")]
		public int MaxNoteTextLength { get; set; }

		[JsonProperty("disableRegistration")]
		public bool DisableRegistration { get; set; }

		[JsonProperty("features")]
		public Feature Features { get; set; }

		public bool IsGroundpolis => Version?.Contains("-gp-") ?? false;
	}

	public class Feature
	{
		public bool miauth { get; set; }
		public bool localTimeline { get; set; }
		public bool globalTimeline { get; set; }
		public bool catTimeline { get; set; }
		public bool featured { get; set; }
	}
}
