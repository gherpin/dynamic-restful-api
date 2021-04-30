using Microsoft.AspNetCore.Mvc;


namespace DynamicAPI.Resources.Accounts.Controllers.InputModels
{
  public class AccountQuery {

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

  }
}