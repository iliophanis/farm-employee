namespace server.Data.Entities;

/// <summary>
/// Type used to hold lookup values
/// </summary>
public class Lookup<TKey>
{
    /// <summary>
    /// The ID value of the Instance
    /// </summary>
    public TKey Id { get; set; }

    /// <summary>
    /// The Description Tag of the Instance
    /// </summary>
    public string Description { get; set; }

    public bool Equals(Lookup<TKey> profile)
    {
        return this.GetHashCode() == profile.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj is Lookup<TKey>)
            return Equals(obj as Lookup<TKey>);
        else
            return false;
    }

    public override int GetHashCode()
    {
        unchecked
        {
            return Id!.GetHashCode();
        }
    }
}
