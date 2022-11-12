using Isu.Entities;
using Isu.Extra.Decorators;

namespace Isu.Extra.Entities;

public class StudentExtra : DecoratorForStudent
{
    private Ognp _ognp1 = new Ognp();
    private Ognp _ognp2 = new Ognp();
    public StudentExtra(Student student)
        : base(student.Group, student.NameOfStudent, student.ID, student) { }

    public Ognp Ognp1 { get; internal set; }
    public Ognp Ognp2 { get; internal set; }
}