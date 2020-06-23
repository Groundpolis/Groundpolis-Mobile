using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GroundpolisMobile
{
	public static class Groundpolis
	{
		public static readonly string[] Permission =
		{
			"read:account",
			"write:account",
			"read:blocks",
			"write:blocks",
			"read:drive",
			"write:drive",
			"read:favorites",
			"write:favorites",
			"read:following",
			"write:following",
			"read:messaging",
			"write:messaging",
			"read:mutes",
			"write:mutes",
			"write:notes",
			"read:notifications",
			"write:notifications",
			"read:reactions",
			"write:reactions",
			"write:votes",
			"read:pages",
			"write:pages",
			"write:page-likes",
			"read:page-likes",
			"read:user-groups",
			"write:user-groups",
		};

		public static string Token { get; private set; }
		public static string Host { get; private set; }

		public static Meta Meta { get; private set; }

		public static bool IsOnline => Meta != null;

		public static void SignOut()
		{
			Token = null;
			Host = null;
			Meta = null;
		}

		public static async Task SignInAsync(string token, string host)
		{
			if (IsOnline) SignOut();
			Token = token;
			Host = host;
			Meta = await MetaAsync();
		}

		public static async Task<Meta> MetaAsync()
		{
			return await PostAsync<Meta>("meta");
		}

		public static async Task<User> UsersShowAsync(string id)
		{
			return await PostAsync<User>("users/show", new Dictionary<string, object>
			{
				{ "userId", id }
			});
		}

		public static async Task<T> PostAsync<T>(string endPoint, Dictionary<string, object> args = null)
		{
			args = args ?? new Dictionary<string, object>();
			if (IsOnline)
			{
				args["i"] = Token;
			}

			var res = await http.PostAsync($"https://{Host}/api/{endPoint}", new StringContent(JsonConvert.SerializeObject(args)));
			var json = await res.Content.ReadAsStringAsync();

			return JsonConvert.DeserializeObject<T>(json);
		}

		private static readonly HttpClient http = new HttpClient();
	}
}