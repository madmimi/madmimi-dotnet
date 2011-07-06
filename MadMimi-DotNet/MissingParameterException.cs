using System;

namespace MadMimi
{
	public class MissingParameterException : ArgumentException
	{
		const string DEFAULT_MESSAGE = "Missing required parameter for API call";
		
		public MissingParameterException ()
			: base(DEFAULT_MESSAGE)
		{
		}

		public MissingParameterException (string parameterName)
			: base(DEFAULT_MESSAGE, parameterName)
		{
		}

		public MissingParameterException (Exception innerException)
			: base(DEFAULT_MESSAGE, innerException)
		{
		}
		
		public MissingParameterException (string message, string parameterName)
			: base(message, parameterName)
		{
		}

		public MissingParameterException (string message, string parameterName, Exception innerExpection)
			: base(message, parameterName, innerExpection)
		{
		}
	}
}

