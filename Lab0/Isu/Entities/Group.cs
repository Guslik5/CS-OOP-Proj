using Isu.exceptions;
using Isu.Models;

namespace Isu.Entities;

public class Group
{
    private const int MinGroupLenght = 5;
    private const int MaxGroupLenght = 5;
    private List<Student> _listStudents = new List<Student>();
    public Group(GroupName group)
    {
        if (group.NameGroup.Length < MinGroupLenght ^ group.NameGroup.Length > MaxGroupLenght)
        {
            throw new GroupNameExceptions("Error\n incorrect group name", group.NameGroup);
        }

        NameOfGroup = group;
    }

    public List<Student> ListStudents
    {
        get
        {
            return _listStudents;
        }
        set => _listStudents = value;
    }

    public GroupName NameOfGroup { get; }
}