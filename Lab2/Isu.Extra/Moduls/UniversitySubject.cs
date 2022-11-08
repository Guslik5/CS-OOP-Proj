using System.Diagnostics.CodeAnalysis;
using Isu.Extra.Entities;

namespace Isu.Extra.Moduls;

public class UniversitySubject
{
    public UniversitySubject(string nameSubject, DateTime tDate, string nameTeacher, string classNumber)
    {
        (Name, TDate, NameTeacher, ClassNumber) = (nameSubject, tDate, nameTeacher, classNumber);
    }

    public string Name { get; }
    public DateTime TDate { get; }
    public string NameTeacher { get; }
    public string ClassNumber { get; }
}