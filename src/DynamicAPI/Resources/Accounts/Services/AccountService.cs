using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DynamicAPI.Resources.Accounts.DataModels;

namespace DynamicAPI.Resources.Accounts.Services {


    public interface IAccountService
    {
        Task<AccountsQueryResponse> GetAccounts(int page, int perPage);
        Task<IEnumerable<AccountDataModel>> GetAccount(string id);
    }

    public class AccountService : IAccountService
    {
        public async Task<AccountsQueryResponse> GetAccounts(int page, int perPage)
        {
            var accountQueryResponse = new AccountsQueryResponse();
            accountQueryResponse.Accounts = await Accounts();
            accountQueryResponse.TotalCount = accountQueryResponse.Accounts.Count();
            return accountQueryResponse;
        }

        public async Task<IEnumerable<AccountDataModel>> GetAccount(string id)
        {
            return await Accounts();
        }

        private async Task<IEnumerable<AccountDataModel>> Accounts() {

            var accounts = new List<AccountDataModel> {
                new AccountDataModel {
                    Id = Guid.Parse("34b68a8a-e6f6-4db5-9d7f-762cbc4db494"),
                    PropertyOne = "Property One",
                    PropertyTwo = "Property Two",
                    PropertyThree = "Property Three",
                    PropertyFour = "Property Four",
                    PropertyFive = new List<string> {"one", "two", "three", "four", "five"},
                    PropertySix = new AccountOwnership { PropertyOne = "Property One", PropertyTwo = "Property Two", PropertyThree = "Property Three "}
                },
                 new AccountDataModel {
                    Id = Guid.Parse("faccc3f7-d95a-424b-950b-1114007230d8"),
                    PropertyOne = "Property One",
                    PropertyTwo = "Property Two",
                    PropertyThree = "Property Three",
                    PropertyFour = "Property Four",
                    PropertyFive = new List<string> {"one", "two", "three", "four", "five"},
                    PropertySix = new AccountOwnership { PropertyOne = "Property One", PropertyTwo = "Property Two", PropertyThree = "Property Three "}
                },
                 new AccountDataModel {
                    Id = Guid.Parse("6d65e41c-b13c-49f5-812c-abc57f704768"),
                    PropertyOne = "Property One",
                    PropertyTwo = "Property Two",
                    PropertyThree = "Property Three",
                    PropertyFour = "Property Four",
                    PropertyFive = new List<string> {"one", "two", "three", "four", "five"},
                    PropertySix = new AccountOwnership { PropertyOne = "Property One", PropertyTwo = "Property Two", PropertyThree = "Property Three "}
                }
            };

            return await Task.FromResult(accounts);
        }
        
    }

    public class AccountsQueryResponse {
        public int TotalCount { get; set;}
        public IEnumerable<AccountDataModel> Accounts { get; set;}
    }

}