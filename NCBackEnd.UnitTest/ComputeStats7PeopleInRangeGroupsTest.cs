using NCBackEnd.Services;
using NCBackEnd.Models;
using Xunit.Sdk;

namespace NCBackEnd.UnitTest;

public class ComputeStats7PeopleInRangeGroupsTest
{
    [Fact]
    public void ThreePeopleInFirstAgeGroupTest()
    {
        var stats = new StatisticsService();
        var userData = new List<NCRPerson>
        {
            new NCRPerson { Dob = new NCRDateOfBirth { Age = 0} },
            new NCRPerson { Dob = new NCRDateOfBirth { Age = 12} },
            new NCRPerson { Dob = new NCRDateOfBirth { Age = 20} }
        };

        var actual = stats.ComputeStats7PeopleInRangeGroups(userData);
        var expected = new Statistic
        {
            Identifier = "stats_7",
            Description = "Percentage of people in the following age ranges: 0-20, 21-40, 41-60, 61-80, 81-100, 100+",
            Data = new List<DataPoint>
            {
                new DataPoint {Tag = "0-20", Percent = "100.0%"},
                new DataPoint {Tag = "21-40", Percent = "0.0%"},
                new DataPoint {Tag = "41-60", Percent = "0.0%"},
                new DataPoint {Tag = "61-80", Percent = "0.0%"},
                new DataPoint {Tag = "81-100", Percent = "0.0%"},
                new DataPoint {Tag = "100+", Percent = "0.0%"},
            }
        };

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void TwoThreAndFourPeopleInFirstAgeGroupsTest()
    {
        var stats = new StatisticsService();
        var userData = new List<NCRPerson>
        {
            new NCRPerson { Dob = new NCRDateOfBirth { Age = 10} },
            new NCRPerson { Dob = new NCRDateOfBirth { Age = 40} },
            new NCRPerson { Dob = new NCRDateOfBirth { Age = 60} }
        };

        var actual = stats.ComputeStats7PeopleInRangeGroups(userData);
        var expected = new Statistic
        {
            Identifier = "stats_7",
            Description = "Percentage of people in the following age ranges: 0-20, 21-40, 41-60, 61-80, 81-100, 100+",
            Data = new List<DataPoint>
            {
                new DataPoint {Tag = "0-20", Percent = "33.3%"},
                new DataPoint {Tag = "21-40", Percent = "33.3%"},
                new DataPoint {Tag = "41-60", Percent = "33.3%"},
                new DataPoint {Tag = "61-80", Percent = "0.0%"},
                new DataPoint {Tag = "81-100", Percent = "0.0%"},
                new DataPoint {Tag = "100+", Percent = "0.0%"},
            }
        };

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void OnePersonInEachAgeGroupsTest()
    {
        var stats = new StatisticsService();
        var userData = new List<NCRPerson>
        {
            new NCRPerson { Dob = new NCRDateOfBirth { Age = 10} },
            new NCRPerson { Dob = new NCRDateOfBirth { Age = 33} },
            new NCRPerson { Dob = new NCRDateOfBirth { Age = 47} },
            new NCRPerson { Dob = new NCRDateOfBirth { Age = 69} },
            new NCRPerson { Dob = new NCRDateOfBirth { Age = 81} },
            new NCRPerson { Dob = new NCRDateOfBirth { Age = 101} }
        };

        var actual = stats.ComputeStats7PeopleInRangeGroups(userData);
        var expected = new Statistic
        {
            Identifier = "stats_7",
            Description = "Percentage of people in the following age ranges: 0-20, 21-40, 41-60, 61-80, 81-100, 100+",
            Data = new List<DataPoint>
            {
                new DataPoint {Tag = "0-20", Percent = "16.7%"},
                new DataPoint {Tag = "21-40", Percent = "16.7%"},
                new DataPoint {Tag = "41-60", Percent = "16.7%"},
                new DataPoint {Tag = "61-80", Percent = "16.7%"},
                new DataPoint {Tag = "81-100", Percent = "16.7%"},
                new DataPoint {Tag = "100+", Percent = "16.7%"},
            }
        };

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void NoPersonInEachAgeGroupsTest()
    {
        var stats = new StatisticsService();
        var userData = new List<NCRPerson>
        {
            new NCRPerson(),
            new NCRPerson(),
            new NCRPerson(),
            new NCRPerson { Dob = new NCRDateOfBirth()},
            new NCRPerson { Dob = new NCRDateOfBirth()},
            new NCRPerson()
        };

        var actual = stats.ComputeStats7PeopleInRangeGroups(userData);
        var expected = new Statistic
        {
            Identifier = "stats_7",
            Description = "Percentage of people in the following age ranges: 0-20, 21-40, 41-60, 61-80, 81-100, 100+",
            Data = new List<DataPoint>
            {
                new DataPoint {Tag = "0-20", Percent = "0.0%"},
                new DataPoint {Tag = "21-40", Percent = "0.0%"},
                new DataPoint {Tag = "41-60", Percent = "0.0%"},
                new DataPoint {Tag = "61-80", Percent = "0.0%"},
                new DataPoint {Tag = "81-100", Percent = "0.0%"},
                new DataPoint {Tag = "100+", Percent = "0.0%"},
            }
        };

        Assert.Equal(expected, actual);
    }
}
