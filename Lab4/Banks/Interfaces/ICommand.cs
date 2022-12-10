using Banks.Commands;

namespace Banks.Interfaces;

public interface ICommand
{
    ConditionCommand ConditionCommand { get; }
    void Execute();
    void Revert();
}