using System;
using System.Collections.Generic;

namespace DynamicAPI.Resources.Accounts.DataModels {


    public  class AccountDataModel {
        
        public Guid Id { get; set;}
        public string PropertyOne { get; set;}

        public string PropertyTwo { get; set;}

        public string PropertyThree { get; set;}

        public string PropertyFour { get; set;}

        public IEnumerable<string> PropertyFive { get; set;}

        public AccountOwnership PropertySix { get; set;}
        
    }


    public class AccountOwnership {

        public string PropertyOne { get; set;}

        public string PropertyTwo { get; set;}

        public string PropertyThree { get; set;}
    }

}