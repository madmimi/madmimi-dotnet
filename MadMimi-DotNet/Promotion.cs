using System;
using System.Net.Mail;

namespace MadMimi
{
	public class Promotion
	{
		public int Id { get; set; }
		
		public string Name { get; set; }
		public string Html { get; set; }
		public string PlainText { get; set; }
		public string Subject { get; set; }
		
		public MailAddress From { get; set; } 
		public MailAddress Recipient { get; set; }
		public MailAddress Bcc { get; set; }

		public bool PreventResend { get; set; }
		public bool TrackLinks { get; set; }
		public bool CheckSuppressed { get; set; }
		public bool Hidden { get; set; }
		public bool SkipPlaceholders { get; set; }
		
		public Promotion ()
		{
			// Defaults
			PreventResend = false;
			TrackLinks = true;
			CheckSuppressed = false;
			Hidden = false;
			SkipPlaceholders = false;
		}
	}
}

