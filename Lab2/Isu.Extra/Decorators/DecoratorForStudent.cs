using Isu.Entities;

namespace Isu.Extra.Decorators;

public class DecoratorForStudent : Student
{
    private Student _student;

    public DecoratorForStudent(Group group, string nameOfStudent, int id, Student student)
        : base(group, nameOfStudent, id)
    {
        _student = student;
    }
}