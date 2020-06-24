using Newtonsoft.Json;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

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

		public static ReactiveProperty<Session> CurrentSessionState { get; } = new ReactiveProperty<Session>();

		public static Session CurrentSession => CurrentSessionState.Value;

		public static string Token => CurrentSession?.Token;
		public static string Host => CurrentSession?.Host;

		public static Meta Meta { get; private set; }

		public static bool IsOnline => CurrentSession != null;

		public static List<Session> Sessions { get; private set; } = new List<Session>();

		public static string UserDataPath => Path.Combine(FileSystem.AppDataDirectory, "user.json");


		public static async Task InitializeAsync()
		{
			if (File.Exists(UserDataPath))
			{
				var file = File.ReadAllText(UserDataPath);
				Sessions = JsonConvert.DeserializeObject<List<Session>>(file);

				await SwitchAsync(0);
			}
		}

		public static async Task SignOutAsync()
		{
			if (CurrentSession == null) return;
			Sessions.Remove(CurrentSession);
			if (Sessions.Count > 0)
			{
				await SwitchAsync(0);
			}
			else
			{
				CurrentSessionState.Value = null;
			}
		}

		public static async Task SignInAsync(string token, string host)
		{
			var session = new Session
			{
				Host = host,
				Token = token
			};
			Sessions.Insert(0, session);

			CurrentSessionState.Value = session;
			await UpdateIAndMetaAsync();
			SaveUserData();
		}

		public static async Task SwitchAsync(int index)
		{
			var s = Sessions[index];
			// 手前に持ってくる
			if (index > 0)
			{
				Sessions.Remove(s);
				Sessions.Insert(0, s);
			}
			CurrentSessionState.Value = s;
			await UpdateIAndMetaAsync();
			SaveUserData();
		}

		public static async Task UpdateIAndMetaAsync()
		{
			Meta = await MetaAsync();
			CurrentSession.User = await IAsync();
		}

		public static void SaveUserData()
		{
			var json = JsonConvert.SerializeObject(Sessions);
			File.WriteAllText(UserDataPath, json);
		}

		public static async Task<Meta> MetaAsync()
		{
			return await PostAsync<Meta>("meta");
		}

		public static async Task<User> IAsync()
		{
			return await PostAsync<User>("i");
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

	public class Session
	{ 
		public string Token { get; set; }
		public string Host { get; set; }
		public User User { get; set; }
	}
}