using FutbolChallengeUI.EventHandlers.EventArgs;

namespace FutbolChallengeUI.EventHandlers
{
	public delegate void EditEntityEventHandler (object? sender, EditPanelEventArgs targetArgs);

	public delegate void EditEntityEventHandler<TEditTarget>(object? sender, EditEntityEventArgs<TEditTarget> targetArgs);

}