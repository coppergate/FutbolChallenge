using System;

namespace Exceptions
{
	public class InvalidArgumentException : Exception
	{
		public InvalidArgumentException(string message) : base(message)
		{

		}

		public InvalidArgumentException(string message, Exception e) : base(message, e)
		{

		}
	}
}
