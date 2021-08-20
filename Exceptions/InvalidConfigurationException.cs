using System;

namespace Exceptions
{
	public class InvalidConfigurationException : Exception
	{
		public InvalidConfigurationException(string message) : base(message)
		{

		}

		public InvalidConfigurationException(string message, Exception e) : base(message, e)
		{

		}
	}
}
