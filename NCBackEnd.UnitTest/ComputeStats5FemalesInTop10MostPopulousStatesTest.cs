using NCBackEnd.Services;
using NCBackEnd.Models;
using Xunit.Sdk;

namespace NCBackEnd.UnitTest;

public class ComputeStats5FemalesInTop10MostPopulousStatesTest
{
    [Fact]
    public void SingleStateWithAllFemalesTest()
    {
        var stats = new StatisticsService();
        var userData = new List<NCRPerson>
        {
            new NCRPerson { Gender = "female", Location = new NCRLocation { State = "Main" } },
            new NCRPerson { Gender = "female", Location = new NCRLocation { State = "Main" } },
            new NCRPerson { Gender = "female", Location = new NCRLocation { State = "Main" } }
        };

        var actual = stats.ComputeStats5FemalesInTop10MostPopulousStates(userData);
        var expected = new Statistic
        {
            Identifier = "stats_5",
            Description = "Percentage of females in each state, up to the top 10 most populous states",
            Data = new List<DataPoint>
            {
                new DataPoint {Tag = "Main", Percent = "100.0%"},
            }
        };

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void TwoStateWithEqualNumberOfFemalesTest()
    {
        var stats = new StatisticsService();
        var userData = new List<NCRPerson>
        {
            new NCRPerson { Gender = "female", Location = new NCRLocation { State = "Main" } },
            new NCRPerson { Gender = "female", Location = new NCRLocation { State = "Main" } },
            new NCRPerson { Gender = "female", Location = new NCRLocation { State = "Main" } },
            new NCRPerson { Gender = "male", Location = new NCRLocation { State = "Main" } },
            new NCRPerson { Gender = "female", Location = new NCRLocation { State = "Ohio" } },
            new NCRPerson { Gender = "female", Location = new NCRLocation { State = "Ohio" } },
            new NCRPerson { Gender = "female", Location = new NCRLocation { State = "Ohio" } },
            new NCRPerson { Gender = "male", Location = new NCRLocation { State = "Ohio" } },
        };

        var actual = stats.ComputeStats5FemalesInTop10MostPopulousStates(userData);
        var expected = new Statistic
        {
            Identifier = "stats_5",
            Description = "Percentage of females in each state, up to the top 10 most populous states",
            Data = new List<DataPoint>
            {
                new DataPoint {Tag = "Main", Percent = "75.0%"},
                new DataPoint {Tag = "Ohio", Percent = "75.0%"},
            }
        };

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ElevenStateWithDifferentNumberOfFemalesTest()
    {
        var stats = new StatisticsService();
        var userData = new List<NCRPerson>
        {
            // Main should be excluded since it has one female eventhough it has a higher percentage than Florida
            new NCRPerson { Gender = "female", Location = new NCRLocation { State = "Main" } },
            new NCRPerson { Gender = "male", Location = new NCRLocation { State = "Main" } },

            new NCRPerson { Gender = "female", Location = new NCRLocation { State = "Ohio" } },
            new NCRPerson { Gender = "female", Location = new NCRLocation { State = "Ohio" } },
            new NCRPerson { Gender = "male", Location = new NCRLocation { State = "Ohio" } },

            new NCRPerson { Gender = "female", Location = new NCRLocation { State = "Colorado" } },
            new NCRPerson { Gender = "female", Location = new NCRLocation { State = "Colorado" } },
            new NCRPerson { Gender = "female", Location = new NCRLocation { State = "Colorado" } },

            new NCRPerson { Gender = "female", Location = new NCRLocation { State = "New York" } },
            new NCRPerson { Gender = "female", Location = new NCRLocation { State = "New York" } },
            new NCRPerson { Gender = "female", Location = new NCRLocation { State = "New York" } },
            new NCRPerson { Gender = "male", Location = new NCRLocation { State = "New York" } },

            new NCRPerson { Gender = "female", Location = new NCRLocation { State = "Kentucky" } },
            new NCRPerson { Gender = "female", Location = new NCRLocation { State = "Kentucky" } },
            new NCRPerson { Gender = "female", Location = new NCRLocation { State = "Kentucky" } },
            new NCRPerson { Gender = "female", Location = new NCRLocation { State = "Kentucky" } },

            new NCRPerson { Gender = "female", Location = new NCRLocation { State = "Georgia" } },
            new NCRPerson { Gender = "female", Location = new NCRLocation { State = "Georgia" } },
            new NCRPerson { Gender = "female", Location = new NCRLocation { State = "Georgia" } },
            new NCRPerson { Gender = "male", Location = new NCRLocation { State = "Georgia" } },
            new NCRPerson { Gender = "male", Location = new NCRLocation { State = "Georgia" } },

            new NCRPerson { Gender = "female", Location = new NCRLocation { State = "New Jersey" } },
            new NCRPerson { Gender = "female", Location = new NCRLocation { State = "New Jersey" } },
            new NCRPerson { Gender = "male", Location = new NCRLocation { State = "New Jersey" } },
            new NCRPerson { Gender = "male", Location = new NCRLocation { State = "New Jersey" } },

            new NCRPerson { Gender = "female", Location = new NCRLocation { State = "California" } },
            new NCRPerson { Gender = "female", Location = new NCRLocation { State = "California" } },
            new NCRPerson { Gender = "female", Location = new NCRLocation { State = "California" } },
            new NCRPerson { Gender = "female", Location = new NCRLocation { State = "California" } },
            new NCRPerson { Gender = "male", Location = new NCRLocation { State = "California" } },

            new NCRPerson { Gender = "female", Location = new NCRLocation { State = "Nevada" } },
            new NCRPerson { Gender = "female", Location = new NCRLocation { State = "Nevada" } },
            new NCRPerson { Gender = "female", Location = new NCRLocation { State = "Nevada" } },
            new NCRPerson { Gender = "male", Location = new NCRLocation { State = "Nevada" } },
            new NCRPerson { Gender = "male", Location = new NCRLocation { State = "Nevada" } },

            new NCRPerson { Gender = "female", Location = new NCRLocation { State = "Wisconsin" } },
            new NCRPerson { Gender = "female", Location = new NCRLocation { State = "Wisconsin" } },
            new NCRPerson { Gender = "female", Location = new NCRLocation { State = "Wisconsin" } },
            new NCRPerson { Gender = "female", Location = new NCRLocation { State = "Wisconsin" } },

            new NCRPerson { Gender = "female", Location = new NCRLocation { State = "Florida" } },
            new NCRPerson { Gender = "female", Location = new NCRLocation { State = "Florida" } },
            new NCRPerson { Gender = "female", Location = new NCRLocation { State = "Florida" } },
            new NCRPerson { Gender = "female", Location = new NCRLocation { State = "Florida" } },
            new NCRPerson { Gender = "female", Location = new NCRLocation { State = "Florida" } },
            new NCRPerson { Gender = "female", Location = new NCRLocation { State = "Florida" } },
            new NCRPerson { Gender = "male", Location = new NCRLocation { State = "Florida" } },
            new NCRPerson { Gender = "male", Location = new NCRLocation { State = "Florida" } },
            new NCRPerson { Gender = "male", Location = new NCRLocation { State = "Florida" } },
            new NCRPerson { Gender = "male", Location = new NCRLocation { State = "Florida" } },
            new NCRPerson { Gender = "male", Location = new NCRLocation { State = "Florida" } },
            new NCRPerson { Gender = "male", Location = new NCRLocation { State = "Florida" } },
            new NCRPerson { Gender = "male", Location = new NCRLocation { State = "Florida" } },
            new NCRPerson { Gender = "male", Location = new NCRLocation { State = "Florida" } },
            new NCRPerson { Gender = "male", Location = new NCRLocation { State = "Florida" } },
            new NCRPerson { Gender = "male", Location = new NCRLocation { State = "Florida" } },
            new NCRPerson { Gender = "male", Location = new NCRLocation { State = "Florida" } },
            new NCRPerson { Gender = "male", Location = new NCRLocation { State = "Florida" } },
            new NCRPerson { Gender = "male", Location = new NCRLocation { State = "Florida" } },
            new NCRPerson { Gender = "male", Location = new NCRLocation { State = "Florida" } },
            new NCRPerson { Gender = "male", Location = new NCRLocation { State = "Florida" } },
        };

        var actual = stats.ComputeStats5FemalesInTop10MostPopulousStates(userData);
        var expected = new Statistic
        {
            Identifier = "stats_5",
            Description = "Percentage of females in each state, up to the top 10 most populous states",
            Data = new List<DataPoint>
            {
                new DataPoint {Tag = "Florida", Percent = "28.6%"},
                new DataPoint {Tag = "Kentucky", Percent = "100.0%"},
                new DataPoint {Tag = "California", Percent = "80.0%"},
                new DataPoint {Tag = "Wisconsin", Percent = "100.0%"},
                new DataPoint {Tag = "Colorado", Percent = "100.0%"},
                new DataPoint {Tag = "New York", Percent = "75.0%"},
                new DataPoint {Tag = "Georgia", Percent = "60.0%"},
                new DataPoint {Tag = "Nevada", Percent = "60.0%"},
                new DataPoint {Tag = "Ohio", Percent = "66.7%"},
                new DataPoint {Tag = "New Jersey", Percent = "50.0%"},
                
            }
        };

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void NoStateWithFemalesTest()
    {
        var stats = new StatisticsService();
        var userData = new List<NCRPerson>
        {
            new NCRPerson { Gender = "male", Location = new NCRLocation { State = "Main" } },
            new NCRPerson { Gender = "male", Location = new NCRLocation { State = "Florida" } },
            new NCRPerson { Gender = "male", Location = new NCRLocation { State = "California" } }
        };

        var actual = stats.ComputeStats5FemalesInTop10MostPopulousStates(userData);
        var expected = new Statistic
        {
            Identifier = "stats_5",
            Description = "Percentage of females in each state, up to the top 10 most populous states",
            Data = new List<DataPoint>()
        };

        Assert.Equal(expected, actual);
    }
}
