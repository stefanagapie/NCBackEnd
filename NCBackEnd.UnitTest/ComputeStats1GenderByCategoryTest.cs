using NCBackEnd.Services;
using NCBackEnd.Models;

namespace NCBackEnd.UnitTest;

public class ComputeStats1GenderByCategoryTest
{

    [Fact]
    public void ThreeEqualGendersTest()
    {
        var stats = new StatisticsService();
        var userData = new List<NCRPerson>
        {
            new NCRPerson { Gender = "male" },
            new NCRPerson { Gender = "other"},
            new NCRPerson { Gender = "female" }
        };

        var actual = stats.ComputeStats1GenderByCategory(userData);
        var expected = new Statistic
        {
            Identifier = "stats_1",
            Description = "Percentage of gender in each category",
            Data = new List<DataPoint>
            {
                new DataPoint {Tag = "male", Percent = "33.3%"},
                new DataPoint {Tag = "other", Percent = "33.3%"},
                new DataPoint {Tag = "female", Percent = "33.3%"}
            }
        };

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void MoreMalesThanFemalsTest()
    {
        var stats = new StatisticsService();
        var userData = new List<NCRPerson>
        {
            new NCRPerson { Gender = "male"},
            new NCRPerson { Gender = "male"},
            new NCRPerson { Gender =  "male"},
            new NCRPerson { Gender = "female"}
        };

        var actual = stats.ComputeStats1GenderByCategory(userData);
        var expected = new Statistic
        {
            Identifier = "stats_1",
            Description = "Percentage of gender in each category",
            Data = new List<DataPoint>
            {
                new DataPoint {Tag = "male", Percent = "75.0%"},
                new DataPoint {Tag = "female", Percent = "25.0%"}
            }
        };

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void EqualMaleAndFemalsTest()
    {
        var stats = new StatisticsService();
        var userData = new List<NCRPerson>
        {
            new NCRPerson { Gender = "male"},
            new NCRPerson { Gender = "female" }
        };

        var actual = stats.ComputeStats1GenderByCategory(userData);
        var expected = new Statistic
        {
            Identifier = "stats_1",
            Description = "Percentage of gender in each category",
            Data = new List<DataPoint>
            {
                new DataPoint {Tag = "male", Percent = "50.0%"},
                new DataPoint {Tag = "female", Percent = "50.0%"}
            }
        };

        Assert.Equal(expected, actual);
    }
}