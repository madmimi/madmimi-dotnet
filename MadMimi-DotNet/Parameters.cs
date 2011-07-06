using System;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Text;

namespace MadMimi
{
	public class Parameters : StringDictionary
	{
		public const string ON = "on";
		public const string OFF = "off";
		public const string EMAIL = "email";
		
		public virtual string ToParameterString()
		{
			StringBuilder parameterString = new StringBuilder ();
			bool first = true;
			foreach (DictionaryEntry parameter in this) {
				if (!first) {
					parameterString.Append ("&");
				}
				parameterString.Append (parameter.Key);
				parameterString.Append ("=");
				parameterString.Append (HttpUtility.UrlEncode ((string) parameter.Value));
			}
			return parameterString.ToString();
		}
	}
}

