namespace FutbolChallengeUI.EventHandlers.EventArgs
{
	public class MatchListGroupButtonClickEventArgs
	{
		public int CurrentSequence;

		public MatchListGroupButtonClickEventArgs(int currentSequence)
		{
			this.CurrentSequence = currentSequence;
		}
	}
}