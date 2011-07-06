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
		
		/// <summary>
		/// Initializes a new instance of the <see cref="MadMimi.Result"/> class.
		/// </summary>
		/// <param name='response'>
		/// An HttpWebResponse which contains the result.
		/// </param>
		public Result (HttpWebResponse response)
		{
			statusCode = response.StatusCode;
			using (StreamReader reader = new StreamReader (response.GetResponseStream ())) {
				body = reader.ReadToEnd ();
			}
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="MadMimi.Result"/> class.
		/// </summary>
		/// <param name='response'>
		/// A WebResponse which contains the result (usually with a 40X status).
		/// </param>
		public Result (WebResponse response)
			: this((HttpWebResponse) response)
		{
		}
		
		/// <summary>
		/// Initializes a new instance of the <see cref="MadMimi.Result"/> class.
		/// </summary>
		/// <param name='exception'>
		/// The Exception which contains the result (usually caused by a missing parameter, macro, or network issue).
		/// </param>
		public Result (Exception exception)
		{
			this.exception = exception;
		}
		
		/// <summary>
		/// Gets a value indicating whether this represents a successful API request.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance represents a successful API request; otherwise, <c>false</c>.
		/// </value>
		public bool IsSuccess {
			get {
				return !IsError;
			}
		}
		
		/// <summary>
		/// Gets a value indicating whether this instance represents a failed API request. 
		/// This may be because of an Exception or an error returned from the API (non-200 response code)
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance represents a failed API request; otherwise, <c>false</c>.
		/// </value>
		public bool IsError {
			get {
				return (statusCode != HttpStatusCode.OK || exception != null);
			}
		}
		
		/// <summary>
		/// Gets a value indicating whether this instance represents a failed API request because of an exception.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance represents a failed API request because of an exception; otherwise, <c>false</c>.
		/// </value>
		public bool IsException {
			get {
				return exception != null;
			}
		}
		
		/// <summary>
		/// Gets the status code. Defaults to HttpStatusCode. Unused if this result represents an Exception.
		/// </summary>
		/// <value>
		/// The status code.
		/// </value>
		public HttpStatusCode StatusCode {
			get {
				return statusCode;
			}
		}
		
		/// <summary>
		/// Gets the body of the response. Defaults to null if this result represent an Exception
		/// </summary>
		/// <value>
		/// The body.
		/// </value>
		public string Body {
			get {
				if (body == null && exception != null) {
					return exception.Message;
				} else {
					return body;
				}
			}
		}
		
		/// <summary>
		/// Gets the exception.
		/// </summary>
		/// <value>
		/// The exception.
		/// </value>
		public Exception Exception {
			get {
				return exception;
			}
		}	
	}
}

