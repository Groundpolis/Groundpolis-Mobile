using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GroundpolisMobile
{
	public class JoinMisskeyInstances
	{
		[JsonProperty("timestamp")]
		public DateTime Timestamp { get; set; }

		[JsonProperty("instances")]
		public List<JoinMisskeyInstance> Instances { get; set; }
	}
}
