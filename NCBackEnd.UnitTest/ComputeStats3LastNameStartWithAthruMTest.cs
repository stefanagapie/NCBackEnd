using NCBackEnd.Services;
using NCBackEnd.Models;

namespace NCBackEnd.UnitTest;

public class ComputeStats3LastNameStartWithAthruMTest
{
    [Fact]
    public void OneOutOfThreeTest()
    {
        var stats = new StatisticsService();
        var userData = new List<NCRPerson>
        {
            new NCRPerson { Name = new NCRName { Last = "Anderson" } },
            new NCRPerson { Name = new NCRName { Last = "Zell" } },
            new NCRPerson { Name = new NCRName { Last = "North" } }
        };

        var actual = stats.ComputeStats3LastNameStartWithAthruM(userData);
        var expected = new Statistic
        {
            Identifier = "stats_3",
            Description = "Percentage of last names that start with A-M versus N-Z",
            Data = new List<DataPoint>
            {
                new DataPoint {Tag = "A-M", Percent = "33.3%"},
            }
        };

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ZeroOfThreeTest()
    {
        var stats = new StatisticsService();
        var userData = new List<NCRPerson>
        {
            new NCRPerson { Name = new NCRName { Last = "Patterson" } },
            new NCRPerson { Name = new NCRName { Last = "Zell" } },
            new NCRPerson { Name = new NCRName { Last = "North" } }
        };

        var actual = stats.ComputeStats3LastNameStartWithAthruM(userData);
        var expected = new Statistic
        {
            Identifier = "stats_3",
            Description = "Percentage of last names that start with A-M versus N-Z",
            Data = new List<DataPoint>
            {
                new DataPoint {Tag = "A-M", Percent = "0.0%"},
            }
        };

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void AllOfThreeTest()
    {
        var stats = new StatisticsService();
        var userData = new List<NCRPerson>
        {
            new NCRPerson { Name = new NCRName { Last = "Anderson" } },
            new NCRPerson { Name = new NCRName { Last = "Cartwright" } },
            new NCRPerson { Name = new NCRName { Last = "Davidson" } }
        };

        var actual = stats.ComputeStats3LastNameStartWithAthruM(userData);
        var expected = new Statistic
        {
            Identifier = "stats_3",
            Description = "Percentage of last names that start with A-M versus N-Z",
            Data = new List<DataPoint>
            {
                new DataPoint {Tag = "A-M", Percent = "100.0%"},
            }
        };

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void NoDataTest()
    {
        var stats = new StatisticsService();
        var userData = new List<NCRPerson>
        {
            new NCRPerson { Name = new NCRName { First = "Cat" } },
            new NCRPerson { Name = new NCRName() },
            new NCRPerson { Name = new NCRName { First = "Donny" } }
        };

        var actual = stats.ComputeStats3LastNameStartWithAthruM(userData);
        var expected = new Statistic
        {
            Identifier = "stats_3",
            Description = "Percentage of last names that start with A-M versus N-Z",
            Data = new List<DataPoint>()
        };

        Assert.Equal(expected, actual);
    }
}
