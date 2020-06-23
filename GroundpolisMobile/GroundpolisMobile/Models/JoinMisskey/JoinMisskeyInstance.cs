using Newtonsoft.Json;
using System.Collections.Generic;

namespace GroundpolisMobile
{
	public class JoinMisskeyInstance
	{
		[JsonProperty("url")]
		public string Url { get; set; }

		[JsonProperty("langs")]
		public List<string> Langs { get; set; }

		[JsonProperty("notSuspended")]
		public bool NotSuspended { get; set; }

		[JsonProperty("value")]
		public double Value { get; set; }

		[JsonProperty("meta")]
		public Meta Meta { get; set; }

	}
}
