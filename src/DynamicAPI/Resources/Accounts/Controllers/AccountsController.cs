using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using DynamicAPI.Resources.Accounts.Controllers.InputModels;
using DynamicAPI.Resources.Accounts.Services;
using Microsoft.AspNetCore.Http;


namespace DynamicAPI.Resources.Accounts.Controllers
{
    [ApiController]
  public class AccountsController : ControllerBase {
    private readonly IAccountService _accountService;
    private readonly IAccountPropertySelector _accountPropertySelector;

    public AccountsController(IAccountService accountService, IAccountPropertySelector accountPropertySeletor) {
      _accountService = accountService;
      _accountPropertySelector = accountPropertySeletor;
    }

    /// <summary>
    /// Obtain lists of accounts
    /// </summary>
    /// <remarks>
    /// Sample requests: (Shown without encoding for clarity)
    ///
    ///     GET /accounts
    ///     GET /accounts?fields=id,string&amp;include_fields=true&amp;page=0&amp;per_page=10
    ///
    ///     Results can be further refined using filter:
    ///
    ///     GET /accounts?$filter=id[eq]=34b68a8a-e6f6-4db5-9d7f-762cbc4db494
    ///     GET /accounts?$filter=id[eq]=34b68a8a-e6f6-4db5-9d7f-762cbc4db494&amp;float[gt]=2.0
    ///     GET /accounts?$filter=string[eq]=One
    ///     GET /accounts?$filter=string[neq]=John
    ///     
    ///
    /// </remarks>
    [Route("api/v1/accounts")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetAccounts([FromQuery] AccountsQuery accountsQuery) {
        
      var accountQueryResponse = await _accountService.GetAccounts(accountsQuery.Page, accountsQuery.PerPage, accountsQuery.Filter);
      var selectedAccountData = _accountPropertySelector.SelectPropertiesToReturn(accountQueryResponse.Accounts, accountsQuery.Fields, accountsQuery.IncludeFields);
     
      if (accountsQuery.IncludeTotals) { 
        var response = _accountPropertySelector.IncludeTotalsInResult(selectedAccountData, accountQueryResponse.TotalCount, accountsQuery.Page, accountsQuery.PerPage);
        return Ok(response);
      }
    
      return Ok(selectedAccountData);
    }

    /// <summary>
    /// Obtains data for a specific account
    /// </summary>
    /// <param name="id">This may be the AccountId (Guid)</param>
    /// <param name="accountQuery"></param>
    /// <returns>Account Data</returns>
    [Route("api/v1/accounts/{id}")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> GetAccount(string id, [FromQuery] AccountQuery accountQuery) {
      
      var accounts = await _accountService.GetAccount(id);
      var selectedAccountData = _accountPropertySelector.SelectPropertiesToReturn(accounts, accountQuery.Fields, accountQuery.IncludeFields);
      return Ok(selectedAccountData.FirstOrDefault());
    }

  }


 
 
}