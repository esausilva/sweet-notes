namespace Application.Commands;

public interface ICommandRequest<in TCommand>
{
    Task Handle(TCommand command);
}