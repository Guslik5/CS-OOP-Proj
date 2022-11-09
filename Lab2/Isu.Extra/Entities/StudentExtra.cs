using Isu.Entities;

namespace Isu.Extra.Entities;

public class DecoratorStudent : Student
{
    private Student _student;
    public DecoratorStudent(Group group, string nameOfStudent, int id, Student student)
        : base(group, nameOfStudent, id)
    {
        this._student = student;
    }
}

public class StudentExtra : DecoratorStudent
{
    private Student _student;
    private Ognp _ognp1 = new Ognp();
    private Ognp _ognp2 = new Ognp();
    public StudentExtra(Student student)
        : base(student.Group, student.NameOfStudent, student.ID, student)
    {
        _student = student;
    }

    public Ognp Ognp1 { get; internal set; }
    public Ognp Ognp2 { get; internal set; }
}