using Microsoft.AspNetCore.Mvc;
using NCBackEnd.Models;
using NCBackEnd.Services;
using System.Xml.Serialization;
using System.Net.Mime;

namespace NCBackEnd.Controllers;

/// <summary>
/// The controller that is responsible for the Statistics API
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class StatisticsController : ControllerBase
{
    private readonly ILogger<StatisticsController> _logger;
    private readonly IStatisticsService _statisticsService;

    /// <summary>
    /// The Statistics Constructor
    /// </summary>
    /// <param name="logger">The built-in logger</param>
    /// <param name="statisticsService">The service responsible for computing user statistics</param>
    public StatisticsController(ILogger<StatisticsController> logger, IStatisticsService statisticsService)
    {
        _logger = logger;
        _statisticsService = statisticsService;
    }

    /// <summary>
    /// Computes user statistics from the provided user data
    /// </summary>
    /// <param name="acceptFormat" example="JSON | PlainText | XML">Specify in what format the statistic results will be returnd as an accept header parameter</param>
    /// <param name="payload" example="See provided sample request">The user data in JSON form as specified on https://randomuser.me</param>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST /api/statistics
    ///     {
    ///       "results": [
    ///         {
    ///           "gender": "female",
    ///           "name": {
    ///             "title": "Miss",
    ///             "first": "Jennie",
    ///             "last": "Nichols"
    ///           },
    ///           "location": {
    ///             "street": {
    ///               "number": 8929,
    ///               "name": "Valwood Pkwy"
    ///             },
    ///             "city": "Billings",
    ///             "state": "Michigan",
    ///             "country": "United States",
    ///             "postcode": "63104",
    ///             "coordinates": {
    ///               "latitude": "-69.8246",
    ///               "longitude": "134.8719"
    ///             },
    ///             "timezone": {
    ///               "offset": "+9:30",
    ///               "description": "Adelaide, Darwin"
    ///             }
    ///           },
    ///           "email": "jennie.nichols@example.com",
    ///           "login": {
    ///             "uuid": "7a0eed16-9430-4d68-901f-c0d4c1c3bf00",
    ///             "username": "yellowpeacock117",
    ///             "password": "addison",
    ///             "salt": "sld1yGtd",
    ///             "md5": "ab54ac4c0be9480ae8fa5e9e2a5196a3",
    ///             "sha1": "edcf2ce613cbdea349133c52dc2f3b83168dc51b",
    ///             "sha256": "48df5229235ada28389b91e60a935e4f9b73eb4bdb855ef9258a1751f10bdc5d"
    ///           },
    ///           "dob": {
    ///             "date": "1992-03-08T15:13:16.688Z",
    ///             "age": 30
    ///           },
    ///           "registered": {
    ///             "date": "2007-07-09T05:51:59.390Z",
    ///             "age": 14
    ///           },
    ///           "phone": "(272) 790-0888",
    ///           "cell": "(489) 330-2385",
    ///           "id": {
    ///             "name": "SSN",
    ///             "value": "405-88-3636"
    ///           },
    ///           "picture": {
    ///             "large": "https://randomuser.me/api/portraits/men/75.jpg",
    ///             "medium": "https://randomuser.me/api/portraits/med/men/75.jpg",
    ///             "thumbnail": "https://randomuser.me/api/portraits/thumb/men/75.jpg"
    ///           },
    ///           "nat": "US"
    ///         }
    ///       ],
    ///       "info": {
    ///         "seed": "56d27f4a53bd5441",
    ///         "results": 1,
    ///         "page": 1,
    ///         "version": "1.4"
    ///       }
    ///     }
    ///
    /// </remarks>
    /// <response code="200">User statistics</response>
    [HttpPost("")]
    [ProducesResponseType(typeof(List<Statistic>), 200)]
    [Produces("text/json", "text/plain", "text/xml")]
    public IActionResult UploadFile( 
        [FromBody] NCRPayload payload,
        [FromHeader(Name = "Accept")] string acceptFormat)
    {
        var results = payload.Results;
        var stats = _statisticsService.ComputeStatistics(results);

        if (acceptFormat == "text/json")
        {
            return Ok(stats);
        }
        else if (acceptFormat == "text/xml")
        {
            var serializer = new XmlSerializer(typeof(List<Statistic>));
            var xml = new StringWriter();
            serializer.Serialize(xml, stats);

            return Content(xml.ToString(), MediaTypeNames.Text.Xml);
        }
        else if (acceptFormat == "text/plain")
        {
            var plainText = StatisticsService.StatisticsAsMultilineString(stats);
            return Content(plainText, MediaTypeNames.Text.Plain);
        }
        return BadRequest("Invalid format. Supported formats: json, xml, txt");
    }
}
