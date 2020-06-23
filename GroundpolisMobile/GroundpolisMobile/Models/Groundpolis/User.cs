using Newtonsoft.Json;
using System.Collections.Generic;

namespace GroundpolisMobile
{
	public class User
	{
		[JsonProperty("host")]
		public string Host { get; set; }

		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("notesCount")]
		public long NotesCount { get; set; }

		[JsonProperty("username")]
		public string Username { get; set; }
	}

}
