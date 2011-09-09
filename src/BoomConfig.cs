using System;
using System.IO;
using System.Collections.Generic;

using Newtonsoft.Json;

namespace BoomSharp
{
	public class BoomConfig
	{
		public static string ConfigPath
		{
			get
			{
				string configPath = (Environment.OSVersion.Platform == PlatformID.Unix || 
                   					Environment.OSVersion.Platform == PlatformID.MacOSX)
				    			? Environment.GetEnvironmentVariable("HOME")
				    			: Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");
			
				configPath += Path.DirectorySeparatorChar + ".boom.conf";
				
				return configPath;
			}
		}
				
		private IDictionary<string, object> Configuration { get; set; }
		
		public BoomConfig()
		{
			if (this.Configuration == null)
				this.LoadConfiguration();
			
			if (this.Configuration == null)
				this.Configuration = new Dictionary<string, object>();
		}
		
		private void LoadConfiguration()
		{
			if ((!String.IsNullOrEmpty(BoomConfig.ConfigPath)) && (File.Exists(BoomConfig.ConfigPath)))
			{
				using (StreamReader f = new StreamReader(BoomConfig.ConfigPath))
				{
					string configJson = f.ReadToEnd();
					
					this.Configuration = JsonConvert.DeserializeObject<Dictionary<string, object>>(configJson);
				}
			}
		}
		
		private void SaveConfiguration()
		{
			if (!String.IsNullOrEmpty(BoomConfig.ConfigPath))
			{
				using (StreamWriter w = File.CreateText(BoomConfig.ConfigPath))
				{
					string configJson = JsonConvert.SerializeObject(this.Configuration);
					
					w.Write(configJson);
				}
			}
		}
		
		public string this[string key]
		{
			get
			{
				if (this.Configuration.ContainsKey(key))
					return this.Configuration[key].ToString();
				
				return String.Empty;
			}
			
			set
			{
				if (!this.Configuration.ContainsKey(key))
					this.Configuration.Add(key, value);
				else
					this.Configuration[key] = value;
				
				this.SaveConfiguration();
			}
		}
	}
}