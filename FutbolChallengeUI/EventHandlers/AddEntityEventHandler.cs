using FutbolChallengeUI.EventHandlers.EventArgs;

namespace FutbolChallengeUI.EventHandlers
{
	public delegate void AddEntityEventHandler<TAddTarget>(object? sender, AddEntityEventArgs<TAddTarget> e);

}