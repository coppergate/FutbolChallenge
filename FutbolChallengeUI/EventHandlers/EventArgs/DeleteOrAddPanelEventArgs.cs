namespace FutbolChallengeUI.EventHandlers.EventArgs
{
	public class DeleteEntityEventArgs
	{
		public int Id;

		public DeleteEntityEventArgs(int id)
		{
			this.Id = id;
		}
	}
}