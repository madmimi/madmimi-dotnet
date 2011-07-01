using System;
using System.Net;
using System.IO;
using System.Web;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Net.Mail;

namespace MadMimi {
	public class Api {
		
		public Config Config { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="MadMimi.Api"/> class.
		/// </summary>
		public Api() {
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MadMimi.Api"/> class.
		/// </summary>
		/// <param name='username'>
		/// Username.
		/// </param>
		/// <param name='apiKey'>
		/// API key.
		/// </param>
		public Api(string username,string apiKey) {
			Config = new Config();
			Config.Username = username;
			Config.ApiKey = apiKey;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MadMimi.Api"/> class.
		/// </summary>
		/// <param name='config'>
		/// The <see cref="MadMimi.Config"/> to use.
		/// </param>
		public Api (Config config)
		{
			Config = config;
		}
		
		public Result AddAudienceListMembership (string email, string list)
		{
			string data = String.Format ("email={0}", HttpUtility.UrlEncode (email));
			string path = String.Format ("/audience_lists/{0}/add", HttpUtility.UrlEncode (list));
			return Post (path, data);
		}
		
		public Result SendEmail (MailAddress sender, string subject, MailAddress recipient, string html, string plainText)
		{
			string data = String.Format (
				"from={0}&subject={1}&recipients={2}&promotion_name={1}", 
				HttpUtility.UrlEncode (sender.ToString ()), 
				HttpUtility.UrlEncode (subject), 
				HttpUtility.UrlEncode (recipient.ToString ())
			);
			if (html != null) {
				data += "&raw_html=" + HttpUtility.UrlEncode(html);
			}
			if (plainText != null) {
				data += "&raw_plain_text=" + HttpUtility.UrlEncode (plainText);
			}
			return Post ("/mailer", data, true);
		}
		
		public Result SendPromotion(Promotion promotion) {
			return new Result(new NotImplementedException());
		}
		
		private Result Post (string path, string data)
		{
			return Post (path, data, false);
		}

		private Result Post(string path, string data, bool ssl)
		{
			try {
				if (Config.SkipSslValidation) {
					ServicePointManager.ServerCertificateValidationCallback += delegate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) {	return true; };
				}
				
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create (Config.GetUrl (path, ssl));
				
				request.Method = "POST";
				request.ContentType = "application/x-www-form-urlencoded";
				request.ServicePoint.Expect100Continue = false;
				
				if (data != null) {
					if (Config.UTF8Encode) {
						UTF8Encoding encoding = new UTF8Encoding ();
						byte[] bytes = encoding.GetBytes (data);
						request.ContentLength = bytes.Length;
					
						Stream stream = request.GetRequestStream ();
						stream.Write (bytes, 0, bytes.Length);
						stream.Close ();
					} else {
						request.ContentLength = data.Length;
						StreamWriter writer = new StreamWriter (request.GetRequestStream (), Encoding.ASCII);
						writer.Write (data);
						writer.Close ();
					}
				} else {
					request.ContentLength = 0;
				}
				
				Console.WriteLine (Config.GetUrl (path, ssl));
				Console.WriteLine (data);

				HttpWebResponse response = (HttpWebResponse)(request.GetResponse ());

				return new Result (response);
			} catch (WebException webex) {
				return new Result (webex.Response);
			} catch (Exception ex) {
				Console.WriteLine (ex.Message);
				Console.WriteLine (ex.StackTrace);
				return new Result (ex);
			} finally {
				if (Config.SkipSslValidation) {
					ServicePointManager.ServerCertificateValidationCallback -= delegate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) {	return true; };
				}
			}
		}
		
		private	Result Get (string path, string data)
		{
			try {
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create (Config.GetUrl (path, false, data));
				request.Method = "GET";
				request.ContentType = "application/x-www-form-urlencoded";
				request.ContentLength = 0;
				
				return new Result ((HttpWebResponse)request.GetResponse ());
			} catch (WebException webex) {
				return new Result (webex.Response);
			} catch (Exception ex) {
				return new Result (ex);
			}
		}
	}
}

