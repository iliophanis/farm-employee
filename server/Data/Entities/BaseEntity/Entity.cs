namespace server.Data.Entities.BaseEntity;

/// <summary>
/// Base class (abstract) of every DB Stored Type (Class)
/// </summary>
public abstract class Entity
{
    /// <summary>
    /// The Unique Identification (Primary Key) of the Entity
    /// </summary>
    public virtual int Id { get; set; }

    /// <summary>
    /// CreatedAt auto-generated after Creation of the Entity
    /// </summary>
    public virtual DateTime InsertDate { get; set; }


    /// <summary>
    /// UpdatedAt auto-generated after Update of the Entity
    /// </summary>
    public virtual DateTime UpdateDate { get; set; }
}
