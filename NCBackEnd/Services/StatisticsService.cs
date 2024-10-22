using NCBackEnd.Models;
using System.Reflection;
using System.Text;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NCBackEnd.Services;

/// <summary>
/// This class contains logic for computing user data statisitcs
/// </summary>
public class StatisticsService : IStatisticsService
{
    /// <summary>
    /// Given user data, compute statistics of that data
    /// </summary>
    /// <param name="data">User data where the stats are computed from</param>
    /// <returns></returns>
    public List<Statistic> ComputeStatistics(List<NCRPerson> data)
    {
        var statistics = new List<Statistic>();

        statistics.Add(ComputeStats1GenderByCategory(data));
        statistics.Add(ComputeStats2FirstNamesStartWithAthruM(data));
        statistics.Add(ComputeStats3LastNameStartWithAthruM(data));
        statistics.Add(ComputeStats4PeopleInTop10MostPopulousStates(data));
        statistics.Add(ComputeStats5FemalesInTop10MostPopulousStates(data));
        statistics.Add(ComputeStats6MalesInTop10MostPopulousStates(data));
        statistics.Add(ComputeStats7PeopleInRangeGroups(data));

        return statistics;
    }

    /// <summary>
    /// Computes percentage of people in the following age ranges: 0-20, 21-40, 41-60, 61-80, 81-100, 100+
    /// </summary>
    /// <param name="data">A list of the available randome user's data</param>
    /// <returns>A Statistic model object mapping age ranges to their corresponding percentages</returns>
    public Statistic ComputeStats7PeopleInRangeGroups(List<NCRPerson> data)
    {
        var rangeKey1 = "0-20";
        var rangeKey2 = "21-40";
        var rangeKey3 = "41-60";
        var rangeKey4 = "61-80";
        var rangeKey5 = "81-100";
        var rangeKey6 = "100+";
        var rangeGroups = new Dictionary<string, int>
        {
            { rangeKey1, 0 }, { rangeKey2, 0 }, { rangeKey3, 0 }, { rangeKey4, 0 }, { rangeKey5, 0 }, { rangeKey6, 0 }
        };

        int total = 0;
        foreach(var person in data)
        {
            int age;
            if (person.Dob == null) continue;
            if (person.Dob.Age == null) continue;
            if (!int.TryParse(person.Dob.Age.ToString(), out age)) continue;
            if (age < 0) continue;

            if (age >= 0 && age <= 20) rangeGroups[rangeKey1]++;
            else if (age >= 21 && age <= 40) rangeGroups[rangeKey2]++;
            else if (age >= 41 && age <= 60) rangeGroups[rangeKey3]++;
            else if (age >= 61 && age <= 80) rangeGroups[rangeKey4]++;
            else if (age >= 81 && age <= 100) rangeGroups[rangeKey5]++;
            else if (age > 100) rangeGroups[rangeKey6]++;
            total++;
        }

        var points = new List<DataPoint>();
        foreach (var groupCount in rangeGroups)
        {
            var percentage = CalculatePercentage(groupCount.Value, total);
            points.Add(new DataPoint { Tag = groupCount.Key, Percent = percentage });
        }

        var stats = new Statistic
        {
            Identifier = "stats_7",
            Description = "Percentage of people in the following age ranges: 0-20, 21-40, 41-60, 61-80, 81-100, 100+",
            Data = points
        };
        return stats;
    }

    /// <summary>
    /// Compute percentage of males in each state, up to the top 10 most populous states
    /// </summary>
    /// <param name="data">A list of the available randome user's data</param>
    /// <returns>A Statistic model object mapping populated states to their corresponding percentages</returns>
    public Statistic ComputeStats6MalesInTop10MostPopulousStates(List<NCRPerson> data)
    {
        var stats = new Statistic
        {
            Identifier = "stats_6",
            Description = "Percentage of males in each state, up to the top 10 most populous states",
            Data = Top10StatesWithGenderPerState(data, "male")
        };
        return stats;
    }

    /// <summary>
    /// Compute percentage of females in each state, up to the top 10 most populous states
    /// </summary>
    /// <param name="data">A list of the available randome user's data</param>
    /// <returns>A Statistic model object mapping femal populated states to their corresponding percentages</returns>
    public Statistic ComputeStats5FemalesInTop10MostPopulousStates(List<NCRPerson> data)
    {
        var stats = new Statistic
        {
            Identifier = "stats_5",
            Description = "Percentage of females in each state, up to the top 10 most populous states",
            Data = Top10StatesWithGenderPerState(data, "female")
        };
        return stats;
    }

    /// <summary>
    /// Compute percentage of people in each state, up to the top 10 most populous states
    /// </summary>
    /// <param name="data">A list of the available randome user's data</param>
    /// <returns>A Statistic model object mapping male populated states to their corresponding percentages</returns>
    public Statistic ComputeStats4PeopleInTop10MostPopulousStates(List<NCRPerson> data)
    {
        (int total, var stateCounts) = CountStateOccurrences(data);
        var top10 = Top10Counts(stateCounts);

        var points = new List<DataPoint>();
        foreach (var stateCount in top10)
        {
            var percentage = CalculatePercentage(stateCount.Value, total);
            points.Add(new DataPoint { Tag = stateCount.Key, Percent = percentage });
        }

        var stats = new Statistic
        {
            Identifier = "stats_4",
            Description = "Percentage of people in each state, up to the top 10 most populous states",
            Data = points
        };
        return stats;
    }

    /// <summary>
    /// Compute percentage of last names that start with A-M versus N-Z
    /// </summary>
    /// <param name="data">A list of the available randome user's data</param>
    /// <returns>A Statistic model object mapping people's first name starting with A thru M to their corresponding percentages</returns>
    public Statistic ComputeStats3LastNameStartWithAthruM(List<NCRPerson> data)
    {
        (int count, int total) = CountAndTotalNumberOfPersonsWithNameComponentStartingWithAthruM(data, "Last");

        var percentage = CalculatePercentage(count, total);
        var points = new List<DataPoint>();
        if (total > 0)
        {
            points.Add(new DataPoint { Tag = "A-M", Percent = percentage });
        }
        var stats = new Statistic
        {
            Identifier = "stats_3",
            Description = "Percentage of last names that start with A-M versus N-Z",
            Data = points
        };

        return stats;
    }

    /// <summary>
    /// Compute percentage of first names that start with A-M versus N-Z
    /// </summary>
    /// <param name="data">A list of the available randome user's data</param>
    /// <returns>A Statistic model object mapping people's last name starting with A thru M to their corresponding percentages</returns>
    public Statistic ComputeStats2FirstNamesStartWithAthruM(List<NCRPerson> data)
    {
        (int count, int total) = CountAndTotalNumberOfPersonsWithNameComponentStartingWithAthruM(data, "First");

        var percentage = CalculatePercentage(count, total);
        var points = new List<DataPoint>();
        if (total > 0)
        {
            points.Add(new DataPoint { Tag = "A-M", Percent = percentage });
        }
        var stats = new Statistic
        {
            Identifier = "stats_2",
            Description = "Percentage of first names that start with A-M versus N-Z",
            Data = points
        };

        return stats;
    }

    /// <summary>
    /// Compute percentages of gender in each category
    /// </summary>
    /// <param name="data">A list of the available randome user's data</param>
    /// <returns>A Statistic model object mapping people's gender to their corresponding percentages</returns>
    public Statistic ComputeStats1GenderByCategory(List<NCRPerson> data)
    {

        // Group the dictionaries by the "gender" key and count the occurrences
        var genderCounts = data.GroupBy(d => d.Gender).Select(g => new { Gender = g.Key, Count = g.Count() });
        var points = new List<DataPoint>();
        var total = data.Count();

        foreach (var genderCount in genderCounts)
        {
            if (genderCount.Gender == null) continue;

            var percentage = CalculatePercentage(genderCount.Count, total);
            points.Add(new DataPoint { Tag = genderCount.Gender, Percent = percentage});
        }

        var stats = new Statistic
        {
            Identifier = "stats_1",
            Description = "Percentage of gender in each category",
            Data = points
        };

        return stats;
    }

    /// <summary>
    /// A helper that generates a percentage given a numerator and denominator
    /// </summary>
    /// <param name="count">The numerator</param>
    /// <param name="total">The denominator</param>
    /// <returns>A string representing a percentage rounded to one decimal places</returns>
    public static string CalculatePercentage(int count, int total)
    {
        if (total == 0)
        {
            return "0.0%"; // Avoid division by zero
        }

        double percentage = (double)count / total * 100;
        return $"{percentage:F1}%";
    }

    /// <summary>
    /// A helper that counts the number of occurrences for each state
    /// </summary>
    /// <param name="data">A list of the available randome user's data</param>
    /// <returns>A count of the number of states seen and a dictionary mapping states to their count</returns>
    public static (int count, Dictionary<string, int> stateCounts) CountStateOccurrences(List<NCRPerson> data)
    {
        int total = 0;
        var stateCounts = new Dictionary<string, int>();
        foreach (var person in data)
        {
            if (person.Location != null && person.Location.State != null)
            {
                total++;
                var state = person.Location.State;
                if (stateCounts.ContainsKey(state))
                {
                    stateCounts[state]++;
                }
                else
                {
                    stateCounts[state] = 1;
                }
            }
        }
        return (total, stateCounts);
    }

    /// <summary>
    /// A helper that picks the top 10 dictionaries based on their values
    /// </summary>
    /// <param name="stateCounts">A dictionary with integers as values</param>
    /// <returns>A dictionary containing at most 10 dictionary with the largest values</returns>
    public static Dictionary<string, int> Top10Counts(Dictionary<string, int> stateCounts)
    {
        var sortedStateCounts = stateCounts.OrderByDescending(x => x.Value).ToList();
        var top10StateCounts = sortedStateCounts.Take(10).ToDictionary(x => x.Key, x => x.Value);
        return top10StateCounts;
    }

    /// <summary>
    /// A helper that picks the user states where the a particlar gender is the most populace
    /// </summary>
    /// <param name="data">A list of the available randome user's data</param>
    /// <param name="targetGender">A string with the gender of interest</param>
    /// <returns>A list of data points that maps a state to a percentage</returns>
    public static List<DataPoint> Top10StatesWithGenderPerState(List<NCRPerson> data, string targetGender)
    {
        var target = data.Where(p => p.Gender != null && p.Gender.Equals(targetGender, StringComparison.OrdinalIgnoreCase)).ToList();
        var other = data.Where(p => p.Gender != null && !p.Gender.Equals(targetGender, StringComparison.OrdinalIgnoreCase)).ToList();

        (var _, var targetStateCounts) = CountStateOccurrences(target);
        (var _, var otherStateCounts) = CountStateOccurrences(other);

        var top10Target = Top10Counts(targetStateCounts);

        var points = new List<DataPoint>();
        foreach (var targetState in top10Target)
        {
            var targetCount = targetState.Value;
            var otherCount = otherStateCounts.ContainsKey(targetState.Key) ? otherStateCounts[targetState.Key] : 0;
            var total = targetCount + otherCount;

            var percentage = CalculatePercentage(targetCount, total);
            points.Add(new DataPoint { Tag = targetState.Key, Percent = percentage });
        }
        return points;
    }

    /// <summary>
    /// A helper that is able to count the first and last name of a user.
    /// </summary>
    /// <param name="data">A list of the available randome user's data</param>
    /// <param name="component">A string that corresponds to the property of interest. Ex: First | Last</param>
    /// <returns>The count is the number of occurences and total is the number of users with that name component</returns>
    public static (int count, int total) CountAndTotalNumberOfPersonsWithNameComponentStartingWithAthruM(List<NCRPerson> data, string component)
    {
        Type type = typeof(NCRName);
        PropertyInfo property = type.GetProperty(component);

        if (property == null) return (0, 0);

        int total = 0;
        int count = 0;
        foreach (var person in data)
        {
            if (person.Name == null) continue;
            var componentValue = (string?)property.GetValue(person.Name);
            if (componentValue == null) continue;
            total++;
            char firstLetter = char.ToUpperInvariant(componentValue[0]);
            if (firstLetter >= 'A' && firstLetter <= 'M')
            {
                count++;
            }
        }
        return (count, total);
    }

    /// <summary>
    /// A Utility that converts a list of statistics to a multiline sting.
    /// </summary>
    /// <param name="statistics">The list of statistics tha the string is built from</param>
    /// <returns>A multiline string with all the available statistics</returns>
    public static string StatisticsAsMultilineString(List<Statistic> statistics)
    {
        StringBuilder multilineText = new StringBuilder();

        foreach(var stats in statistics)
        {
            foreach (var point in stats.Data)
            {
                var line = $"{stats.Description}. {point.Tag}: {point.Percent}";
                multilineText.AppendLine(line);
            }
        }

        return multilineText.ToString();
    }
}
