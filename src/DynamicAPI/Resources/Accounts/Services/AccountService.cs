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
            var accounts = await Accounts();
            var accountPages = new List<List<AccountDataModel>>();

            if (perPage > 0) {
                 accountPages = accounts.Select((x,i) => new { Index = i, Value = x})
                                .GroupBy(x => x.Index / perPage)
                                .Select(x => x.Select(v => v.Value).ToList())
                                .ToList();
            }
           
            var accountQueryResponse = new AccountsQueryResponse();
            accountQueryResponse.Accounts = perPage == 0 ? accounts : accountPages[page];
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
                },
                 new AccountDataModel {
                    Id = Guid.Parse("b0689a1c-6633-4f8c-bf4f-4d798e479371"),
                    PropertyOne = "Property One",
                    PropertyTwo = "Property Two",
                    PropertyThree = "Property Three",
                    PropertyFour = "Property Four",
                    PropertyFive = new List<string> {"one", "two", "three", "four", "five"},
                    PropertySix = new AccountOwnership { PropertyOne = "Property One", PropertyTwo = "Property Two", PropertyThree = "Property Three "}
                },
                 new AccountDataModel {
                    Id = Guid.Parse("93a5f780-3c16-45d6-be5a-347024b44698"),
                    PropertyOne = "Property One",
                    PropertyTwo = "Property Two",
                    PropertyThree = "Property Three",
                    PropertyFour = "Property Four",
                    PropertyFive = new List<string> {"one", "two", "three", "four", "five"},
                    PropertySix = new AccountOwnership { PropertyOne = "Property One", PropertyTwo = "Property Two", PropertyThree = "Property Three "}
                },
                 new AccountDataModel {
                    Id = Guid.Parse("e8dede7a-18de-40d1-a45d-51289cbb9ebc"),
                    PropertyOne = "Property One",
                    PropertyTwo = "Property Two",
                    PropertyThree = "Property Three",
                    PropertyFour = "Property Four",
                    PropertyFive = new List<string> {"one", "two", "three", "four", "five"},
                    PropertySix = new AccountOwnership { PropertyOne = "Property One", PropertyTwo = "Property Two", PropertyThree = "Property Three "}
                },
                 new AccountDataModel {
                    Id = Guid.Parse("9b6b2078-d4b6-40df-b49c-c4fa08329b53"),
                    PropertyOne = "Property One",
                    PropertyTwo = "Property Two",
                    PropertyThree = "Property Three",
                    PropertyFour = "Property Four",
                    PropertyFive = new List<string> {"one", "two", "three", "four", "five"},
                    PropertySix = new AccountOwnership { PropertyOne = "Property One", PropertyTwo = "Property Two", PropertyThree = "Property Three "}
                },
                 new AccountDataModel {
                    Id = Guid.Parse("2fd2a3ee-0b05-447c-88d1-a66376d2e8ba"),
                    PropertyOne = "Property One",
                    PropertyTwo = "Property Two",
                    PropertyThree = "Property Three",
                    PropertyFour = "Property Four",
                    PropertyFive = new List<string> {"one", "two", "three", "four", "five"},
                    PropertySix = new AccountOwnership { PropertyOne = "Property One", PropertyTwo = "Property Two", PropertyThree = "Property Three "}
                },
                 new AccountDataModel {
                    Id = Guid.Parse("9fa0246e-9ff8-46f0-ac6f-99fde2282b43"),
                    PropertyOne = "Property One",
                    PropertyTwo = "Property Two",
                    PropertyThree = "Property Three",
                    PropertyFour = "Property Four",
                    PropertyFive = new List<string> {"one", "two", "three", "four", "five"},
                    PropertySix = new AccountOwnership { PropertyOne = "Property One", PropertyTwo = "Property Two", PropertyThree = "Property Three "}
                },
                 new AccountDataModel {
                    Id = Guid.Parse("1f044955-6889-4bc8-b826-28a6648dc4b4"),
                    PropertyOne = "Property One",
                    PropertyTwo = "Property Two",
                    PropertyThree = "Property Three",
                    PropertyFour = "Property Four",
                    PropertyFive = new List<string> {"one", "two", "three", "four", "five"},
                    PropertySix = new AccountOwnership { PropertyOne = "Property One", PropertyTwo = "Property Two", PropertyThree = "Property Three "}
                },
            };

            return await Task.FromResult(accounts);
        }
        
    }

    public class AccountsQueryResponse {
        public int TotalCount { get; set;}
        public IEnumerable<AccountDataModel> Accounts { get; set;}
    }

}