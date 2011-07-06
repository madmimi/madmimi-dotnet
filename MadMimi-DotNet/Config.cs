using System;

namespace MadMimi
{
	public class Config
	{
		public bool SkipSslValidation { get; set; }
		public string ApiEndPoint { get; set; }
		public bool UTF8Encode { get; set; }
		public string Username { get; set; }
		public string ApiKey { get; set; }
		
		const string USERNAME = "username";
		const string API_KEY = "api_key";
		
		public Config ()
		{
			SkipSslValidation = false;
			ApiEndPoint = "api.madmimi.com";
			UTF8Encode = true;
		}
		
		public string GetUrl (string path)
		{
			return GetUrl(path, false);
		}
		
		public string GetUrl(string path, bool ssl)
		{
			if (path.StartsWith ("/")) {
				path = path.Substring (1);
			}
			Parameters parameters = new Parameters ();
			parameters.Add (USERNAME, Username);
			parameters.Add (API_KEY, ApiKey);
			return GetUrl(path, ssl, parameters);
		}

		public string GetUrl(string path, bool ssl, Parameters parameters)
		{
			return String.Format ("http{0}://{1}/{2}?{3}", ssl ? "s" : "", ApiEndPoint, path, parameters.ToParameterString ());
		}
	}
}

