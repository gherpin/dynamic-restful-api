using Microsoft.AspNetCore.Mvc;


namespace DynamicAPI.Resources.Accounts.Controllers.InputModels
{
  public class AccountsQuery {

    /// <summary>
    /// Comma-separated list of fields to include or exclude (based on value for include_fields)
    /// in the result. Leave empty to retrieve all fields.
    /// </summary>
    /// <value></value>
    [FromQuery(Name = "fields")]
    public string Fields { get; set; }

    /// <summary>
    /// Whether specified fields are to be included (true) or excluded (false)
    /// </summary>
    /// <value></value>
    [FromQuery(Name = "include_fields")]
    public bool IncludeFields { get; set;}

    /// <summary>
    /// Page index of the results to return. First page is 0
    /// </summary>
    /// <value></value>
    [FromQuery(Name = "page")]
    public int Page { get; set;}

    /// <summary>
    /// Number of results per page. Paging is disabled if parameter is not sent.
    /// </summary>
    /// <value></value>
    [FromQuery(Name = "per_page")]
    public int PerPage { get; set;}

    /// <summary>
    /// Return results inside an object that contains the total result count (true) or as a
    /// direct array of results (false, default)
    /// </summary>
    /// <value></value>
    [FromQuery(Name = "include_totals")]
    public bool IncludeTotals { get; set; }
    }
}