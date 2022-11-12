using Isu.Entities;
using Isu.Extra.Decorators;
using Isu.Extra.Exceptions;

namespace Isu.Extra.Entities;

public class GroupExtra : DecoratorForGroup
{
    private readonly List<StudentExtra> _listStudentExtra = new List<StudentExtra>();
    public GroupExtra(Group group, TimeTable tTable)
        : base(group.NameOfGroup, tTable)
    {
        ArgumentNullException.ThrowIfNull(tTable, "TimeTable is null");
        TTable = tTable;
    }

    public TimeTable TTable { get; internal set; }
    public List<StudentExtra> ListStudentExtra => _listStudentExtra;

    public void Add(StudentExtra studentExtra)
    {
        ArgumentNullException.ThrowIfNull(studentExtra, "StudentExtra is null");
        if (_listStudentExtra.Contains(studentExtra))
        {
            throw new StudentExtraException("Student has been added");
        }

        _listStudentExtra.Add(studentExtra);
    }

    public void Remove(StudentExtra studentExtra)
    {
        ArgumentNullException.ThrowIfNull(studentExtra, "StudentExtra is null");
        if (!_listStudentExtra.Contains(studentExtra))
        {
            throw new StudentExtraException("Not Found Student for removing");
        }

        _listStudentExtra.Remove(studentExtra);
    }
}
