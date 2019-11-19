using PaymentContext.Shared.Commands;

namespace PaymentContext.Shared.Hendlers
{
    public interface IHendler<T> where T : ICommand
    {
         ICommandResult Handler (T command);
    }
}