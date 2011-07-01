using System;
using NDesk.Options;
using MadMimi;
using System.Net.Mail;

namespace MadMimiConsole
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			string username = "", apiKey = "", action = "", email = "", list = "", html = "", plainText = "", sender = "", subject = "";
			bool help = false;
			Result result = null;
			
			OptionSet p = new OptionSet ()
				.Add ("u|username=", delegate (string v) { username = v; })
				.Add ("p|apikey|api_key=", delegate (string v) { apiKey = v; })
				.Add ("?|help", delegate (string v) { help = v != null; })
				.Add ("a|action=", delegate (string v) { action = v.ToLower (); })
				.Add ("e|email|recipient:", delegate (string v) { email = v; })
				.Add ("l|list|listname:", delegate (string v) { list = v; })
				.Add ("h|html:", delegate (string v) { html = v; })
				.Add ("t|text|plaintext:", delegate (string v) { plainText = v; })
				.Add ("f|from|sender:", delegate (string v) { sender = v; })
				.Add ("s|subject:", delegate (string v) { subject = v; })
					;
			p.Parse (args);
			
			if (help) {
				showHelp ();
				return;
			}
			
			Config config = new Config ();
			config.Username = username;
			config.ApiKey = apiKey;
			//config.ApiEndPoint = "app1.madmimi.managedmachine.com";
			config.UTF8Encode = true;
			Api api = new Api (config);
			
			switch (action) {
			case "addlistmembership":
			case "alm":
				if (email == null || list == null) {
					Console.WriteLine ("Add List Membership requires email and list arguments.");
					return;
				}
				result = api.AddAudienceListMembership (email, list);
				break;
			case "sendemail":
			case "se":
				if (email == null || sender == null || (html == null && plainText == null) || subject == null) {
					Console.WriteLine ("Send Email requires sender, subject, recipient and html or plain text arguments.");
					return;
				}
				new MailAddress (sender);
				new MailAddress (email);
				
				result = api.SendEmail (new MailAddress (sender), subject, new MailAddress (email), html, plainText);
				break;
			}
			
			if (result.IsSuccess) {
				Console.WriteLine ("Success!");
				Console.WriteLine (result.Body);
			} else if (result.IsException) {
				Console.WriteLine ("Exception! " + result.Body);
				Console.WriteLine (result.Exception.StackTrace);
			} else if (result.IsError) {
				Console.WriteLine ("Error! " + result.StatusCode);
				Console.WriteLine (result.Body);
			} else {
				Console.WriteLine ("WTF!");
				Console.WriteLine (result.Body);
			}
		}
		
		private static void showHelp() {
			Console.WriteLine ("Help!");
		}
	}
}
