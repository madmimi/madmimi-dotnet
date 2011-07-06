using System;

namespace MadMimi
{
	public class MissingMacroException : ArgumentException
	{
		const string DEFAULT_MESSAGE = "Missing required macro for API call";

		public MissingMacroException ()
			: base(DEFAULT_MESSAGE)
		{
		}

		public MissingMacroException (string macroName)
			: base(DEFAULT_MESSAGE, macroName)
		{
		}

		public MissingMacroException (Exception innerException)
			: base(DEFAULT_MESSAGE, innerException)
		{
		}

		public MissingMacroException (string message,string macroName)
			: base(message, macroName)
		{
		}

		public MissingMacroException (string message,string macroName, Exception innerExpection)
			: base(message, macroName, innerExpection)
		{
		}
	}
}

