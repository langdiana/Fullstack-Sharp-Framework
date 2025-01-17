namespace RealWorldSharp.Base;

public interface ICommandHandler<TCommand> where TCommand : ICommand
{
	Task Execute(TCommand cmd);
}
