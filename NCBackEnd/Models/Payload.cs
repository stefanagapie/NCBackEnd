using System.ComponentModel.DataAnnotations;

namespace NCBackEnd.Models;

/// <summary>
/// This is the top level random user model
/// </summary>
public class NCRPayload
{
    /// <summary>
    /// Results models the encapsulation of multiple random users
    /// </summary>
    [Required]
    public List<NCRPerson> Results { get; set; } = new List<NCRPerson>();
}

/// <summary>
/// This is the model that encapsulates data about a random user
/// </summary>
public class NCRPerson
{
    /// <summary>
    /// The gender of a user
    /// </summary>
    /// <example>male | female | ...</example>
    public string? Gender { get; set; }

    /// <summary>
    /// This is name property model of a user
    /// </summary>
    public NCRName? Name { get; set; }

    /// <summary>
    /// This is the location property model  of a user
    /// </summary>
    public NCRLocation? Location { get; set; }

    /// <summary>
    /// This is the date-of-birtg property model of a user
    /// </summary>
    public NCRDateOfBirth? Dob {  get; set; }
}

/// <summary>
/// This is the model that encapsulates data about the name of a user
/// </summary>
public class NCRName
{
    /// <summary>
    /// The user's first name
    /// </summary>
    /// <example>Tommy</example>
    public string? First { get; set; }

    /// <summary>
    /// The user's last name
    /// </summary>
    /// <example>Anderson</example>
    public string? Last { get; set; }
}

/// <summary>
/// This is the model that encapsulates data about the location of a user
/// </summary>
public class NCRLocation
{
    /// <summary>
    /// The user's state that they reside in
    /// </summary>
    /// <example>New York</example>
    public string? State { get; set; }
}

/// <summary>
/// This is the model that encapsulates data about the date of birth of a user
/// </summary>
public class NCRDateOfBirth
{
    /// <summary>
    /// The user's age
    /// </summary>
    /// <example>32</example>
    public int? Age { get; set; }
}



