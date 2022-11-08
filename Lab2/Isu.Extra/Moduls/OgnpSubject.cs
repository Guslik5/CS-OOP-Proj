using Isu.Extra.Entities;

namespace Isu.Extra.Moduls;

public class OgnpSubject : UniversitySubject
{
    public OgnpSubject(string nameSubject, DateTime tDate, string nameTeacher, string classNumber)
        : base(nameSubject, tDate, nameTeacher, classNumber) { }
}