using NCBackEnd.Models;

namespace NCBackEnd.Services;

/// <summary>
/// Defins the statistics service interface
/// </summary>
public interface IStatisticsService
{
    /// <summary>
    /// API interface call to compute user data statistics from the given data
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    List<Statistic> ComputeStatistics(List<NCRPerson> data);
}
