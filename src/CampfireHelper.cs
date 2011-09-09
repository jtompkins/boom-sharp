using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Net;
using System.IO;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace BoomSharp
{
	public class CampfireHelper
	{
		public static string CAMPFIRE_BASE_URL = "https://{0}.campfirenow.com";
		public static string SPEAK_URL = "/room/{0}/speak.xml";

		public string SiteName { get; private set; }
		public NetworkCredential Credentials { get; private set; }
		public IDictionary<string, int> Rooms { get; private set; }

		public CampfireHelper()
		{
			this.SiteName = BoomSharp.Config["CampfireSite"];
			this.Credentials = new NetworkCredential(BoomSharp.Config["CampfireApiToken"], "X");
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

		private XElement Post(string url, XElement data)
		{
			WebRequest request = HttpWebRequest.Create(this.SiteUrl + url);

			request.ContentType = "application/xml";
			request.Credentials = this.Credentials;
			request.Method = "POST";

			//((HttpWebRequest)request).ServicePoint.ConnectionLimit = 20;

			if (data != null)
			{
				request.ContentLength = Encoding.UTF8.GetByteCount(data.ToString());

				Stream requestStream = request.GetRequestStream();
				requestStream.Write(Encoding.UTF8.GetBytes(data.ToString()), 0, (int)request.ContentLength);
				requestStream.Flush();
			}

			using (Stream responseStream = request.GetResponse().GetResponseStream())
			using (var reader = new StreamReader(responseStream))
			{
				string responseString = reader.ReadToEnd();

				if (string.IsNullOrWhiteSpace(responseString)) return null;

				return XElement.Parse(responseString);
			}
		}

		public bool Speak(string room, string message)
		{
			if (this.Rooms.ContainsKey(room))
			{
				string speakUrl = String.Format(SPEAK_URL, this.Rooms[room].ToString());

				XElement messageXml = new XElement("message", new XElement("type", "TextMessage"), new XElement("body", message));

				XElement result = this.Post(speakUrl, messageXml);

				if (result != null)
					return true;
			}

			return false;
		}
	}
}
