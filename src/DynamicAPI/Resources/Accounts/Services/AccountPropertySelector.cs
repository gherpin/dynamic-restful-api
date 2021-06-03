using System;
using System.Collections.Generic;
using System.Linq;
using DynamicAPI.Resources.Accounts.DataModels;

namespace DynamicAPI.Resources.Accounts.Services {


    public interface IAccountPropertySelector
    {
        List<Dictionary<string, object>> SelectPropertiesToReturn(IEnumerable<AccountDataModel> accounts, string fields, bool includeFields);
        Dictionary<string, object> IncludeTotalsInResult(IEnumerable<Dictionary<string, object>> accountData, int totalCount, int currentPage, int resultsPerPage);
    }

    public class AccountPropertySelector : IAccountPropertySelector
    {
        public List<Dictionary<string, object>>  SelectPropertiesToReturn(IEnumerable<AccountDataModel> accounts, string fields, bool includeFields)
        {
            var selectedData = new List<Dictionary<string, object>>();

            if (!string.IsNullOrEmpty(fields)) {
                
                var listedFields = fields.Split(',');
            
                if (includeFields) {
                foreach (var account in accounts)
                {  //Return only fields listed.
                    selectedData.Add(IncludeProperties(account, listedFields));
                }
                } else {
                foreach (var account in accounts)
                {   //Return fields not listed.
                    selectedData.Add(ExcludeProperties(account, listedFields));
                }
                }
            } else {
                //Return all fields
                var props = typeof(AccountDataModel).GetProperties().Select(x => x.Name);
                foreach (var account in accounts) {
                    selectedData.Add(IncludeProperties(account, props));
                }
            }
            return selectedData;
        }

        public Dictionary<string, object> IncludeTotalsInResult(IEnumerable<Dictionary<string, object>> accountData, int totalCount, int currentPage, int resultsPerPage)
        {
             Dictionary<string, object> result = new Dictionary<string, object>();
            result.Add("total", totalCount);
            result.Add("start", currentPage);
            result.Add("limit", resultsPerPage);
            result.Add("accounts", accountData);
            return result;
        }

        private Dictionary<string, object> IncludeProperties(AccountDataModel accountDataModel, IEnumerable<string> propertyNames) {

            var response = new Dictionary<string, object>();
            var properties = propertyNames.Select(x => x.ToLower());

            if (properties.Contains(nameof(accountDataModel.Id).ToLower())){
                response.Add("id", accountDataModel.Id);
            }

            if (properties.Contains(nameof(accountDataModel.String).ToLower())){
                response.Add("string", accountDataModel.String);
            }

            if (properties.Contains(nameof(accountDataModel.Integer).ToLower())){
                response.Add("integer", accountDataModel.Integer);
            }

            if (properties.Contains(nameof(accountDataModel.Float).ToLower())){
                response.Add("float", accountDataModel.Float);
            }

            if (properties.Contains(nameof(accountDataModel.Double).ToLower())){
                response.Add("double", accountDataModel.Double);
            }

            if (properties.Contains(nameof(accountDataModel.Decimal).ToLower())) {
                response.Add("decimal", accountDataModel.Decimal);
            }

            if (properties.Contains(nameof(accountDataModel.PropertyFour).ToLower())){
                response.Add("propertyFour", accountDataModel.PropertyFour);
            }

            if (properties.Contains(nameof(accountDataModel.PropertyFive).ToLower())){
                response.Add("propertyFive", accountDataModel.PropertyFive);
            }

            if (properties.Contains(nameof(accountDataModel.PropertySix).ToLower())){
                response.Add("propertySix", accountDataModel.PropertySix);
            }

            return response;
        }

      private Dictionary<string, object> ExcludeProperties(AccountDataModel accountDataModel, IEnumerable<string> propertyNames) {
            var response = new Dictionary<string, object>();
            var properties = propertyNames.Select(x => x.ToLower());

            if (!properties.Contains(nameof(accountDataModel.Id).ToLower())){
                response.Add("id", accountDataModel.Id);
            }

            if (!properties.Contains(nameof(accountDataModel.String).ToLower())){
                response.Add("string", accountDataModel.String);
            }

            if (!properties.Contains(nameof(accountDataModel.Integer).ToLower())){
                response.Add("integer", accountDataModel.Integer);
            }

            if (!properties.Contains(nameof(accountDataModel.Float).ToLower())){
                response.Add("float", accountDataModel.Float);
            }

            if (!properties.Contains(nameof(accountDataModel.Double).ToLower())){
                response.Add("double", accountDataModel.Double);
            }


            if (!properties.Contains(nameof(accountDataModel.Decimal).ToLower())) {
                response.Add("decimal", accountDataModel.Decimal);
            }

            if (!properties.Contains(nameof(accountDataModel.PropertyFour).ToLower())){
                response.Add("propertyFour", accountDataModel.PropertyFour);
            }

            if (!properties.Contains(nameof(accountDataModel.PropertyFive).ToLower())){
                response.Add("propertyFive", accountDataModel.PropertyFive);
            }

            if (!properties.Contains(nameof(accountDataModel.PropertySix).ToLower())){
                response.Add("propertySix", accountDataModel.PropertySix);
            }

            return response;
        }

    }

}