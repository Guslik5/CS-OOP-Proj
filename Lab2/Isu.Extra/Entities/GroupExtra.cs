using System.Net.Sockets;
using Isu.Entities;
using Isu.Extra.Moduls;
using Isu.Models;

namespace Isu.Extra.Entities;

public class GroupExtra : DecoratorGroup
{
    private readonly List<StudentExtra> _listStudentExtra = new List<StudentExtra>();
    private Group _group;
    public GroupExtra(Group group, TimeTable tTable)
        : base(group.NameOfGroup, tTable)
    {
        ArgumentNullException.ThrowIfNull(tTable, "TimeTable is null");
        TTable = tTable;
        _group = group;
    }

    public TimeTable TTable { get; internal set; }
    public List<StudentExtra> ListStudentExtra => _listStudentExtra;

    public void Add(StudentExtra studentExtra)
    {
        _listStudentExtra.Add(studentExtra);
    }

    public void Remove(StudentExtra studentExtra)
    {
        _listStudentExtra.Remove(studentExtra);
    }
}

public abstract class DecoratorGroup : Group
{
    private Group _group;
    public DecoratorGroup(GroupName group, TimeTable tTable)
        : base(group)
    {
        this._group = new Group(group);
    }
}