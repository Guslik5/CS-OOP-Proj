namespace Isu.exceptions;

public class IdStudentException : Exception
{
    public IdStudentException()
    {
        Value = "Id student is incorrect";
    }

    public string Value { get; }
}