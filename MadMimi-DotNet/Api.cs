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
		const string FORM_URL_ENCODED = "application/x-www-form-urlencoded";
		
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
		
		public Result AddAudienceListMembership(string email, string list)
		{
			Parameters parameters = new Parameters ();
			parameters.Add(Parameters.EMAIL, email);
			string path = String.Format ("/audience_lists/{0}/add", HttpUtility.UrlEncode (list));
			return Post (path, parameters);
		}
		
		public Result SendEmail(MailAddress fromAddress, string subject, MailAddress recipientAddress, string rawHtml, string rawPlainText)
		{
			TransactionalMailingParameters parameters = new TransactionalMailingParameters (fromAddress, subject, recipientAddress, rawHtml, rawPlainText);
			return Post ("/mailer", parameters, true);
		}
		
		public Result SendEmail(Parameters parameters) {
			return Post ("/mailer", parameters, true);
		}
		
		private Result Post (string path, Parameters parameters)
		{
			return Post (path, parameters, false);
		}

		private Result Post(string path, Parameters parameters, bool ssl)
		{
			try {
				if (Config.SkipSslValidation) {
					ServicePointManager.ServerCertificateValidationCallback += delegate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) {	return true; };
				}
				
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create (Config.GetUrl (path, ssl));
				
				request.Method = "POST";
				request.ContentType = FORM_URL_ENCODED;
				request.ServicePoint.Expect100Continue = false;
				
				if (parameters != null) {
					string data = parameters.ToParameterString ();
					if (data.Length > 0) {
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
					}
				} else {
					request.ContentLength = 0;
				}
				
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
		
		private
			Result Get(string path, Parameters parameters)
		{
			try {
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create (Config.GetUrl (path, false, parameters));
			request.Method = "GET";
			request.ContentType = FORM_URL_ENCODED;
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

