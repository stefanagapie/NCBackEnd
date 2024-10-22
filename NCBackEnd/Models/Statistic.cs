using System.Data.Common;
using System.Diagnostics.CodeAnalysis;

namespace NCBackEnd.Models;

/// <summary>
/// This defines a particular statistic with all its data points.
/// </summary>
public class Statistic : IEquatable<Statistic>
{
    /// <summary>
    /// An identifier for a particular statistic
    /// </summary>
    /// <example>stat_1</example>
    public required string Identifier { get; set; }

    /// <summary>
    /// A description of a particular statistic
    /// </summary>
    /// <example>Percentage of gender in each category</example>
    public required string Description { get; set; }

    /// <summary>
    /// A list of statistic data points
    /// </summary>
    /// <example>{"tag":"Males", "Percent":"46%"}</example>
    public required List<DataPoint> Data {  get; set; }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public bool Equals(Statistic? other)
    {
        if (other == null) return false;
        return Identifier == other.Identifier && Description == other.Description && Data.SequenceEqual(other.Data);
    }

    public int GetHashCode([DisallowNull] Statistic obj)
    {
        return HashCode.Combine(obj.Identifier, obj.Description, obj.Data);
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
