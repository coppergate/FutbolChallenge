namespace FutbolChallengeUI.EventHandlers.EventArgs
{
	public class EditPanelEventArgs
	{
		public int EditTargetId { get; set; }

		public EditPanelEventArgs(int editTarget)
		{
			this.EditTargetId = editTarget;
		}
	}

	public class EditEntityEventArgs<TEditTarget>
	{
		public TEditTarget EditTarget { get; set; }
		public EditEntityEventArgs(TEditTarget editTarget)
		{
			this.EditTarget = editTarget;
		}
	}
}