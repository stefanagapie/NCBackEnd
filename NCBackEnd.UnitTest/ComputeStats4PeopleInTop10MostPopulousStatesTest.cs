using NCBackEnd.Services;
using NCBackEnd.Models;

namespace NCBackEnd.UnitTest;

public class ComputeStats4PeopleInTop10MostPopulousStatesTest
{
    [Fact]
    public void ThreeStatesWithEqualPercentagesTest()
    {
        var stats = new StatisticsService();
        var userData = new List<NCRPerson>
        {
            new NCRPerson { Location = new NCRLocation { State = "New York" } },
            new NCRPerson { Location = new NCRLocation { State = "New Jersey" } },
            new NCRPerson { Location = new NCRLocation { State = "Main" } }
        };

        var actual = stats.ComputeStats4PeopleInTop10MostPopulousStates(userData);
        var expected = new Statistic
        {
            Identifier = "stats_4",
            Description = "Percentage of people in each state, up to the top 10 most populous states",
            Data = new List<DataPoint>
            {
                new DataPoint {Tag = "New York", Percent = "33.3%"},
                new DataPoint {Tag = "New Jersey", Percent = "33.3%"},
                new DataPoint {Tag = "Main", Percent = "33.3%"}
            }
        };

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ThreeEqualStatesWithEqualTest()
    {
        var stats = new StatisticsService();
        var userData = new List<NCRPerson>
        {
            new NCRPerson { Location = new NCRLocation { State = "Main" } },
            new NCRPerson { Location = new NCRLocation { State = "Main" } },
            new NCRPerson { Location = new NCRLocation { State = "Main" } }
        };

        var actual = stats.ComputeStats4PeopleInTop10MostPopulousStates(userData);
        var expected = new Statistic
        {
            Identifier = "stats_4",
            Description = "Percentage of people in each state, up to the top 10 most populous states",
            Data = new List<DataPoint>
            {
                new DataPoint {Tag = "Main", Percent = "100.0%"}
            }
        };

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ElevenStateWithTwentyOneOccurrencesAllButOneAppearOnceTest()
    {
        var stats = new StatisticsService();
        var userData = new List<NCRPerson>
        {
            // Texas appears just once
            new NCRPerson { Location = new NCRLocation { State = "New York" } },
            new NCRPerson { Location = new NCRLocation { State = "New Jersey" } },
            new NCRPerson { Location = new NCRLocation { State = "California" } },
            new NCRPerson { Location = new NCRLocation { State = "Kentukey" } },
            new NCRPerson { Location = new NCRLocation { State = "Alaska" } },
            new NCRPerson { Location = new NCRLocation { State = "Texas" } },
            new NCRPerson { Location = new NCRLocation { State = "Ohio" } },
            new NCRPerson { Location = new NCRLocation { State = "Colorado" } },
            new NCRPerson { Location = new NCRLocation { State = "Minnesota" } },
            new NCRPerson { Location = new NCRLocation { State = "Oregon" } },
            new NCRPerson { Location = new NCRLocation { State = "Wisconsin" } },
            new NCRPerson { Location = new NCRLocation { State = "New York" } },
            new NCRPerson { Location = new NCRLocation { State = "New Jersey" } },
            new NCRPerson { Location = new NCRLocation { State = "California" } },
            new NCRPerson { Location = new NCRLocation { State = "Kentukey" } },
            new NCRPerson { Location = new NCRLocation { State = "Alaska" } },
            new NCRPerson { Location = new NCRLocation { State = "Ohio" } },
            new NCRPerson { Location = new NCRLocation { State = "Colorado" } },
            new NCRPerson { Location = new NCRLocation { State = "Minnesota" } },
            new NCRPerson { Location = new NCRLocation { State = "Oregon" } },
            new NCRPerson { Location = new NCRLocation { State = "Wisconsin" } },
        };

        var actual = stats.ComputeStats4PeopleInTop10MostPopulousStates(userData);
        var expected = new Statistic
        {
            Identifier = "stats_4",
            Description = "Percentage of people in each state, up to the top 10 most populous states",
            Data = new List<DataPoint>
            {
                new DataPoint {Tag = "New York", Percent = "9.5%"},
                new DataPoint {Tag = "New Jersey", Percent = "9.5%"},
                new DataPoint {Tag = "California", Percent = "9.5%"},
                new DataPoint {Tag = "Kentukey", Percent = "9.5%"},
                new DataPoint {Tag = "Alaska", Percent = "9.5%"},
                new DataPoint {Tag = "Ohio", Percent = "9.5%"},
                new DataPoint {Tag = "Colorado", Percent = "9.5%"},
                new DataPoint {Tag = "Minnesota", Percent = "9.5%"},
                new DataPoint {Tag = "Oregon", Percent = "9.5%"},
                new DataPoint {Tag = "Wisconsin", Percent = "9.5%"},
            }
        };

        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ZeroStatesTest()
    {
        var stats = new StatisticsService();
        var userData = new List<NCRPerson>
        {
            new NCRPerson { Location = new NCRLocation() },
            new NCRPerson { Location = new NCRLocation() },
            new NCRPerson { Location = new NCRLocation() }
        };

        var actual = stats.ComputeStats4PeopleInTop10MostPopulousStates(userData);
        var expected = new Statistic
        {
            Identifier = "stats_4",
            Description = "Percentage of people in each state, up to the top 10 most populous states",
            Data = new List<DataPoint>()
        };

        Assert.Equal(expected, actual);
    }
}
