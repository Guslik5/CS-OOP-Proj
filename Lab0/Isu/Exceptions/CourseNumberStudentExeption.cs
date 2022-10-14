namespace Isu.Exceptions;

public class CourseNumberStudentExeption : Exception
{
    public CourseNumberStudentExeption()
    {
        Value = "Students studying on this course not found";
    }

    public string Value { get; }
}