namespace Isu.Extra.Exceptions;

public class StudentNotHaveOgnp : Exception
{
    public StudentNotHaveOgnp(string messege)
        : base(messege) { }
}