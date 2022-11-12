using Isu.Extra.Exceptions;
using Isu.Extra.Moduls;

namespace Isu.Extra.Entities;

public class Ognp : IEquatable<Ognp>
{
    private readonly List<StudentExtra> _listStudent = new List<StudentExtra>();

    public Ognp(char flow, UniversitySubject ognpSubject)
    {
        Flow = flow;
        OgnpSubject = ognpSubject;
    }

    internal Ognp() { }

    public char Flow { get; }
    public UniversitySubject OgnpSubject { get; }
    internal List<StudentExtra> ListStudent => _listStudent;

    public bool Equals(Ognp other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Equals(OgnpSubject, other.OgnpSubject) && Flow == other.Flow;
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Ognp)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(OgnpSubject, Flow);
    }
}