using Isu.Entities;

namespace Isu.Extra.Entities;

public abstract class DecoratorStudent : Student
{
    private GroupExtra _group;
    public DecoratorStudent(Group group, string nameOfStudent, int id, GroupExtra groupExtra)
        : base(group, nameOfStudent, id)
    {
        this._group = groupExtra;
    }
}

public class StudentExtra : DecoratorStudent
{
    public StudentExtra(Student student, GroupExtra groupExtra, Ognp ognp)
        : base(student.Group, student.NameOfStudent, student.ID, groupExtra)
    {
        Ognp = ognp;
        GroupExtra = groupExtra;
        ognp.ListStudent.Add(this);
    }

    public GroupExtra GroupExtra { get; }

    public Ognp Ognp { get; internal set; }
}