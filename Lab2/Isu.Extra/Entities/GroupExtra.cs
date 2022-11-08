using Isu.Entities;
using Isu.Extra.Moduls;
using Isu.Models;

namespace Isu.Extra.Entities;

public class GroupExtra : DecoratorGroup
{
    public GroupExtra(GroupName group, TimeTable tTable)
        : base(group, tTable)
    {
        ArgumentNullException.ThrowIfNull(tTable, "TimeTable is null");
        TTable = tTable;
    }

    public TimeTable TTable { get; }
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