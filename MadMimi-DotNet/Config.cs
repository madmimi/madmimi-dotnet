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
		
		public string GetUrl (string path, bool ssl)
		{
			if (path.StartsWith ("/")) {
				path = path.Substring(1);
			}
			return String.Format ("http{0}://{1}/{2}?username={3}&api_key={4}", ssl ? "s" : "", ApiEndPoint, path, Username, ApiKey);
		}

		public string GetUrl (string path, bool ssl, string paramString)
		{
			return GetUrl (path, ssl) + "&" + paramString;
		}
	}
}

