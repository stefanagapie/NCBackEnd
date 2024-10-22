using System.Diagnostics.CodeAnalysis;

namespace NCBackEnd.Models;

/// <summary>
/// Defins a particular data point of a statistic
/// </summary>
public class DataPoint : IEquatable<DataPoint>
{
    /// <summary>
    /// The name of the statistic that percent corresponds to.
    /// For example, if that stats is a percent of males in each state 
    /// then the tag would be a state
    /// </summary>
    /// <example>New York</example>
    public required string Tag { get; set; }

    /// <summary>
    /// The percent that corresponds to the tag.
    /// </summary>
    /// <example>35%</example>
    public required string Percent { get; set; }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public bool Equals(DataPoint? other)
    {   
        if (other == null) return false;
        return Tag == other.Tag && Percent == other.Percent;
    }

    public int GetHashCode([DisallowNull] DataPoint obj)
    {
        return HashCode.Combine(obj.Tag, obj.Percent);
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
