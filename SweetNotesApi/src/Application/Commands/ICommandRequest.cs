namespace Application.Commands;

public interface ICommandRequest<in TCommand, TResult>
{
    Task<TResult> Handle(TCommand command, CancellationToken cancellationToken);
}