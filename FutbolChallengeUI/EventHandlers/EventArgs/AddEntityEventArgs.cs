namespace FutbolChallengeUI.EventHandlers.EventArgs
{
	public class AddEntityEventArgs<TAddTarget>
	{
		public TAddTarget AddTarget;

		public AddEntityEventArgs(TAddTarget target)
		{
			this.AddTarget = target;
		}
	}

}
