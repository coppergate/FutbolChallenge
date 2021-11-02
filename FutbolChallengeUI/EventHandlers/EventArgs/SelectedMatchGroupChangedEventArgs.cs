namespace FutbolChallengeUI.EventHandlers.EventArgs
{

	public enum MatchGroupChangeDirection
	{
		UP,
		DOWN,
	}

	public class SelectedMatchGroupChangedEventArgs
	{
		public readonly MatchGroupChangeDirection Direction;

		public SelectedMatchGroupChangedEventArgs(MatchGroupChangeDirection direction)
		{
			Direction = direction;
		}
	}
}