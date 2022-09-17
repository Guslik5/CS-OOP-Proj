namespace Isu.exceptions;

public class MaxStudentExeption : Exception
{
    public MaxStudentExeption()
    {
        Value = "Error\n The group is complete";
    }

    public string Value { get; }
}