using System;
using System.Net;
using System.IO;

namespace MadMimi
{
	public class Result
	{
		HttpStatusCode statusCode = HttpStatusCode.Unused;
		string body = null;
		Exception exception = null;

		public Result (HttpWebResponse response)
		{
			statusCode = response.StatusCode;
			using (StreamReader reader = new StreamReader (response.GetResponseStream ())) {
				body = reader.ReadToEnd ();
			}
		}

		public Result (WebResponse response)
			: this((HttpWebResponse) response)
		{
		}

		public Result (Exception exception)
		{
			this.exception = exception;
		}

		public bool IsSuccess {
			get {
				return !IsError;
			}
		}

		public bool IsError {
			get {
				return (statusCode != HttpStatusCode.OK || exception != null);
			}
		}

		public bool IsException {
			get {
				return exception != null;
			}
		}

		public HttpStatusCode StatusCode {
			get {
				return statusCode;
			}
		}

		public string Body {
			get {
				if (body == null && exception != null) {
					return exception.Message;
				} else {
					return body;
				}
			}
		}

		public Exception Exception {
			get {
				return exception;
			}
		}	
	}
}

