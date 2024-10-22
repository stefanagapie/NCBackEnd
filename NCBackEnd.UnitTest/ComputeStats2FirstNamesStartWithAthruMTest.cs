using NCBackEnd.Services;
using NCBackEnd.Models;

namespace NCBackEnd.UnitTest;

public class ComputeStats2FirstNamesStartWithAthruMTest
{
    [Fact]
    public void OneOutOfThreeTest()
    {
        var stats = new StatisticsService();
        var userData = new List<NCRPerson>
        {
            new NCRPerson { Name = new NCRName { First = "Tom" } },
            new NCRPerson { Name = new NCRName { First = "Bob" } },
            new NCRPerson { Name = new NCRName { First = "Penny" } }
        };

        var actual = stats.ComputeStats2FirstNamesStartWithAthruM(userData);
        var expected = new Statistic
        {
            Identifier = "stats_2",
            Description = "Percentage of first names that start with A-M versus N-Z",
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
            new NCRPerson { Name = new NCRName { First = "Pat" } },
            new NCRPerson { Name = new NCRName { First = "Tom" } },
            new NCRPerson { Name = new NCRName { First = "Penny" } }
        };

        var actual = stats.ComputeStats2FirstNamesStartWithAthruM(userData);
        var expected = new Statistic
        {
            Identifier = "stats_2",
            Description = "Percentage of first names that start with A-M versus N-Z",
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
            new NCRPerson { Name = new NCRName { First = "Cat" } },
            new NCRPerson { Name = new NCRName { First = "Bob" } },
            new NCRPerson { Name = new NCRName { First = "Donny" } }
        };

        var actual = stats.ComputeStats2FirstNamesStartWithAthruM(userData);
        var expected = new Statistic
        {
            Identifier = "stats_2",
            Description = "Percentage of first names that start with A-M versus N-Z",
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
            new NCRPerson { Name = new NCRName { Last = "Cat" } },
            new NCRPerson { Name = new NCRName { Last = "Bob" } },
            new NCRPerson { Name = new NCRName { Last = "Donny" } }
        };

        var actual = stats.ComputeStats2FirstNamesStartWithAthruM(userData);
        var expected = new Statistic
        {
            Identifier = "stats_2",
            Description = "Percentage of first names that start with A-M versus N-Z",
            Data = new List<DataPoint>()
        };

        Assert.Equal(expected, actual);
    }
}
