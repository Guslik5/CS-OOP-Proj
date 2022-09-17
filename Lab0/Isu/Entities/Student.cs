using Isu.exceptions;
using Isu.Models;

namespace Isu.Entities;

public class Student
{
    public Student(Group group, string nameOfStudent, int id)
    {
        (Group, NameOfStudent, ID) = (group.NameOfGroup, nameOfStudent, id);
    }

    public GroupName Group { get; }

    public string NameOfStudent { get; }
    public int ID { get; }
}