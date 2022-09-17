using Isu.Entities;
namespace Isu.exceptions;

public class StudentException : Exception
{
    public StudentException(string message)
        : base(message) { }
}