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