namespace Isu.Exceptions;

public class CourseInvalidException : Exception
{
    public CourseInvalidException(string message)
        : base(message) { }
}