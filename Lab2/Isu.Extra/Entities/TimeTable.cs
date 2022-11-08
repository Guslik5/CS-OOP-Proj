using Isu.Extra.Moduls;

namespace Isu.Extra.Entities;

public class TimeTable
{
    private readonly List<UniversitySubject> _listSubjects = new List<UniversitySubject>();

    public TimeTable(List<UniversitySubject> listSubjects)
    {
        _listSubjects = listSubjects;
    }

    public List<UniversitySubject> ListSubject => _listSubjects;

    public void Add(UniversitySubject subject)
    {
        _listSubjects.Add(subject);
    }

    public void Remove(UniversitySubject subject)
    {
        _listSubjects.Remove(subject);
    }
}