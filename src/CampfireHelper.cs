using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;
using System.Xml.Linq;
using Newtonsoft.Json;
using CampfireClient.API.Support;
using CampfireClient.API.Requests;
using CampfireClient.API.DataTypes;

namespace BoomSharp
{
	public class CampfireHelper
	{
		public static string CAMPFIRE_BASE_URL = "https://{0}.campfirenow.com";

		public string SiteName { get; private set; }

		public string Username { get; private set; }
		public string Password { get; private set; }

		public IDictionary<string, int> Rooms { get; private set; }

		public CampfireHelper()
		{
			this.SiteName = BoomSharp.Config["CampfireSite"];

			this.Username = BoomSharp.Config["CampfireUsername"];
			this.Password = BoomSharp.Config["CampfirePassword"];

			this.Rooms = new Dictionary<string, int>();

			var roomsDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(BoomSharp.Config["CampfireRooms"]);

			foreach (KeyValuePair<string, string> kvp in roomsDict)
				this.Rooms.Add(kvp.Key, Convert.ToInt32(kvp.Value));
		}

		public string SiteUrl
		{
			get
			{
				return String.Format(CAMPFIRE_BASE_URL, this.SiteName);
			}
		}

		public bool Speak(string room, string message)
		{
			if (this.Rooms.ContainsKey(room))
			{
				CampfireContext.Login(this.Username, this.Password, this.SiteUrl);

				SpeakRequest s = new SpeakRequest(this.Rooms[room], message);

				CampfireObject msg = s.MakeRequest();

				if (msg != null)
					return true;
			}

			return false;
		}
	}
}
