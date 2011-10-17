using System;
using System.Collections;
using System.Collections.Specialized;
using System.Web;
using System.Text;

namespace MadMimi
{
	public class AudienceMemberColumns : StringDictionary
	{
		const string EMAIL = "email"; // required
		const string FIRST_NAME = "first_name"; 
		const string LAST_NAME = "last_name";
		const string PHONE = "phone";
		const string COMPANY = "company";
		const string TITLE = "title";
		const string ADDRESS = "address";
		const string STATE = "state";
		const string ZIP = "zip";
		const string COUNTRY = "country";
		
		string Email { 
			get {
				return this[EMAIL];
			}
			set {
				this[EMAIL] = value;
			}
		}
		
		string FirstName { 
			get {
				return this[FIRST_NAME];
			}
			set {
				this[FIRST_NAME] = value;
			}
		}
		
		string LastName { 
			get {
				return this[LAST_NAME];
			}
			set {
				this[LAST_NAME] = value;
			}
		}
		
		string Phone { 
			get {
				return this[PHONE];
			}
			set {
				this[PHONE] = value;
			}
		}
		
		string Company { 
			get {
				return this[COMPANY];
			}
			set {
				this[COMPANY] = value;
			}
		}
		
		string Title { 
			get {
				return this[TITLE];
			}
			set {
				this[TITLE] = value;
			}
		}
		
		string Address { 
			get {
				return this[ADDRESS];
			}
			set {
				this[ADDRESS] = value;
			}
		}
		
		string State { 
			get {
				return this[STATE];
			}
			set {
				this[STATE] = value;
			}
		}
		
		string Zip { 
			get {
				return this[ZIP];
			}
			set {
				this[ZIP] = value;
			}
		}
		
		string Country { 
			get {
				return this[COUNTRY];
			}
			set {
				this[COUNTRY] = value;
			}
		}
		
	}
}

