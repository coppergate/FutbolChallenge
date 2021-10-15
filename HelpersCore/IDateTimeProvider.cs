using System;

namespace Helpers.Core.DateTimeProvider
{
	public interface IDateTimeProvider
	{
		DateTime? CurrentUtcDateTime { get; }
	}

	public class DefaultDateTimeProvider : IDateTimeProvider
	{
		public DateTime? CurrentUtcDateTime { get => DateTime.UtcNow; }
	}
}