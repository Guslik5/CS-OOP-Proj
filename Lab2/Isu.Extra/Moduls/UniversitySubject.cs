using System.Diagnostics.CodeAnalysis;
using Isu.Extra.Entities;

namespace Isu.Extra.Moduls;

public class UniversitySubject : IEquatable<UniversitySubject>
{
    private DateTime _endingTime;
    public UniversitySubject(string nameSubject, DateTime tDate, string nameTeacher, string classNumber)
    {
        ArgumentNullException.ThrowIfNull(nameSubject, "Subject name is null");
        ArgumentNullException.ThrowIfNull(tDate, "tDate is null");
        ArgumentNullException.ThrowIfNull(nameTeacher, "Teacher name is null");
        ArgumentNullException.ThrowIfNull(classNumber, "Class number is null");
        (Name, TDate, NameTeacher, ClassNumber) = (nameSubject, tDate, nameTeacher, classNumber);
        _endingTime = tDate.AddHours(1).AddMinutes(30);
    }

    public string Name { get; }
    public DateTime TDate { get; }
    public string NameTeacher { get; }
    public string ClassNumber { get; }

    public DateTime EndingTime => _endingTime;

    public bool Equals(UniversitySubject other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return _endingTime.Equals(other._endingTime) && Name == other.Name && TDate.Equals(other.TDate) && NameTeacher == other.NameTeacher && ClassNumber == other.ClassNumber;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((UniversitySubject)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_endingTime, Name, TDate, NameTeacher, ClassNumber);
    }
}