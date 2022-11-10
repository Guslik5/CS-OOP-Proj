using Isu.Extra.Exceptions;
using Isu.Extra.Moduls;

namespace Isu.Extra.Entities;

public class TimeTable
{
    private readonly List<UniversitySubject> _listSubjects = new List<UniversitySubject>();

    public TimeTable() { }

    public TimeTable(List<UniversitySubject> listSubjects)
    {
        _listSubjects = listSubjects;
        if (!listSubjects.Where(s => CheckingIntersectionSubject(s)).Any())
        {
            throw new IntersectionException("Error added ListSubject\nIntersection with the timetable");
        }
    }

    public List<UniversitySubject> ListSubject => _listSubjects;

    public void Add(UniversitySubject subject)
    {
        ArgumentNullException.ThrowIfNull(subject, "subject for add in timetable is null");
        if (_listSubjects.Where(s => s.TDate.Equals(subject.TDate)).Any())
        {
            throw new IntersectionException("Intersection in the timetable");
        }

        if (CheckingIntersectionSubject(subject))
        {
            throw new IntersectionException("Error added subject\nIntersection with the timetable");
        }

        _listSubjects.Add(subject);
    }

    public void Remove(UniversitySubject subject)
    {
        if (!_listSubjects.Where(s => s.Equals(subject)).Any())
        {
            throw new NotHaveSubjectException("Subject to remove was not found");
        }

        _listSubjects.Remove(subject);
    }

    private bool CheckingIntersectionSubject(UniversitySubject subject)
    {
        if (_listSubjects.Where(s => subject.TDate > s.TDate && subject.TDate < s.EndingTime).Any())
        {
            return true;
        }

        return false;
    }
}