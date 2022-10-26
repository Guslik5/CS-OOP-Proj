using Isu.Exceptions;
using Isu.Models;

namespace Isu.Entities;

public class Student
{
    public Student(Group group, string nameOfStudent, int id)
    {
        if (group == null || nameOfStudent == null)
        {
            throw new StudentException("Error Student name or Student Group");
        }

        if (id < 0)
        {
            throw new IdException("Invalid ID student");
        }

        (Group, NameOfStudent, ID) = (group, nameOfStudent, id);
    }

    public Group Group { get; }

    public string NameOfStudent { get; }
    public int ID { get; }
}