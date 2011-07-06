using System;
using System.Net.Mail;

namespace MadMimi
{
	public class TransactionalMailingParameters : Parameters
	{
		const string PROMOTION_NAME = "promotion_name"; // Required
		const string SUBJECT = "subject"; // Defaults to promotion_name
		const string FROM = "from"; // Defaults to account username
		const string BCC = "bcc";
		const string RECIPIENTS = "recipients"; // Required
		const string RAW_HTML = "raw_html";
		const string RAW_PLAIN_TEXT = "raw_plain_text";
		const string CHECK_SUPPRESSED = "check_suppressed";
		const string TRACK_LINKS = "track_links";
		const string HIDDEN = "hidden";
		const string BODY = "body";
		const string SKIP_PLACEHOLDERS = "skip_placeholders";
		const string REMOVE_UNSUBSCRIBE = "remove_unsubscribe";
		const string LIST_NAMES = "list_names";
		
		public TransactionalMailingParameters ()
		{
			
		}
		
		public TransactionalMailingParameters (string promotionName,MailAddress recipientAddress)
		{
			PromotionName = promotionName;
			RecipientAddress = recipientAddress;
		}
		
		public TransactionalMailingParameters (MailAddress fromAddress,string subject,MailAddress recipientAddress,string rawHtml,string rawPlainText)
		{
			FromAddress = fromAddress;
			Subject = subject;
			RecipientAddress = recipientAddress;
			RawHtml = rawHtml;
			RawPlainText = rawPlainText;
		}
		
		public string PromotionName {
			get {
				return this [PROMOTION_NAME];
			}
			set {
				this [PROMOTION_NAME] = value;
			}
		}
		
		public string Subject {
			get {
				return this [SUBJECT];
			}
			set {
				this [SUBJECT] = value;
			}
		}

		public string From {
			get {
				return this [PROMOTION_NAME];
			}
			set {
				this [PROMOTION_NAME] = value;
			}
		}
		
		public MailAddress FromAddress {
			set {
				this [FROM] = value.ToString (); 
			}
		}

		public string Recipient {
			get {
				return this [RECIPIENTS];
			}
			set {
				this [RECIPIENTS] = value;
			}
		}
		
		public MailAddress RecipientAddress {
			set {
				this [RECIPIENTS] = value.ToString ();
			}
		}
		
		public string RawHtml {
			get {
				return this [RAW_HTML];
			}
			set {
				this [RAW_HTML] = value;
			}
		}
		
		public string RawPlainText {
			get {
				return this [RAW_PLAIN_TEXT];
			}
			set {
				this [RAW_PLAIN_TEXT] = value;
			}
		}
		
		public override string ToParameterString()
		{
			// TODO: check for missing parameters
			// TODO: check for missing macros
			return base.ToParameterString ();
		}
	}
}

