using System.Threading.Tasks;
using DevStore.Core.Messages;
using FluentValidation.Results;

namespace DevStore.Core.Mediator;

public interface IMediatorHandler
{
    Task PublishEvent<T>(T evento) where T : Event;
    Task<ValidationResult> SendCommand<T>(T comando) where T : Command;
}