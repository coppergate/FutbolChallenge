namespace FutbolChallengeUI.Controls
{
	public class EditPanelEventArgs<TDataType>
	{
		public TDataType EditTarget;

		public EditPanelEventArgs(TDataType editTarget)
		{
			this.EditTarget = editTarget;
		}
	}
}