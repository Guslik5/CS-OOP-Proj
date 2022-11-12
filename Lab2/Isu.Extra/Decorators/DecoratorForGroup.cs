using Isu.Entities;
using Isu.Extra.Entities;
using Isu.Models;

namespace Isu.Extra.Decorators;

public abstract class DecoratorForGroup : Group
{
    private Group _group;
    public DecoratorForGroup(GroupName group, TimeTable tTable)
        : base(group)
    {
        _group = new Group(group);
    }
}