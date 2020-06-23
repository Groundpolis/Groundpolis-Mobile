using Newtonsoft.Json;

namespace GroundpolisMobile
{
	public class MiAuthStatus
	{ 
		[JsonProperty("ok")]
		public bool Ok { get; set; }

		[JsonProperty("token")]
		public string Token { get; set; }

		[JsonProperty("user")]
		public User User { get; set; }
	}
}
