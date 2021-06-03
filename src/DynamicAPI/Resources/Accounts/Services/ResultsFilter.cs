using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DynamicAPI.Resources.Accounts.Services {

    public class ResultFilter {


        public List<T> FilterResults<T>(IEnumerable<T> entities, string filter) {

            var propertiesToFilter = filter.Split('&');
            var compiledExpressions = CreateCompiledExpressions<T>(propertiesToFilter);
            return entities.Where(entity=> compiledExpressions.All(expression => expression(entity))).ToList();
        }

        private FilterParameters ParseFilter(string filter) {
            var keyValue = filter.Split('=');
            var startPosition = keyValue[0].IndexOf("[");
            var endPosition = keyValue[0].IndexOf("]");
            var equalityOperator = filter.Substring(startPosition + 1, endPosition - startPosition - 1);
            var propertyName = keyValue[0].Substring(0, startPosition);
            var propertyValue = keyValue[1];

            return new FilterParameters(propertyName, propertyValue, equalityOperator);
        }

        private IEnumerable<Func<T, bool>> CreateCompiledExpressions<T>(string[] filters) {

            List<Func<T, bool>> compiledExpressions = new List<Func<T, bool>>();
            
            foreach(var propertyToFilter in filters) { 
                
                var filterParameters = ParseFilter(propertyToFilter);
                var myType = typeof(T).GetProperties().First(x => x.Name.ToLower() ==  filterParameters.PropertyName.ToLower()).PropertyType;
                var myValueInstance = CreateValue(myType, filterParameters.PropertyValue);
                
                ParameterExpression pe = Expression.Parameter(typeof(T), "model");
                MemberExpression me = Expression.Property(pe, filterParameters.PropertyName);
                ConstantExpression ce = Expression.Constant(myValueInstance, myType); 
                BinaryExpression body = CreateBinaryExpression(filterParameters.EqualityOperator, me, ce);

                var expressionTree = Expression.Lambda<Func<T, bool>>(body, new[] { pe });
                var compiledExpression = expressionTree.Compile();
                compiledExpressions.Add(compiledExpression);
            }

            return compiledExpressions;
        }


        //https://docs.oasis-open.org/odata/odata/v4.0/errata03/os/complete/part2-url-conventions/odata-v4.0-errata03-os-part2-url-conventions-complete.html#_Toc453752358
        private BinaryExpression CreateBinaryExpression(string equalityOperator, MemberExpression memberExpression, ConstantExpression constantExpression) {

            switch (equalityOperator) {
                case "eq" :
                    return BinaryExpression.Equal(memberExpression, constantExpression);
                case "ne" :
                    return BinaryExpression.NotEqual(memberExpression, constantExpression);
                case "gt" : 
                    return BinaryExpression.GreaterThan(memberExpression, constantExpression);
                case "lt" :
                    return BinaryExpression.LessThan(memberExpression, constantExpression);
                case "ge" :
                    return BinaryExpression.GreaterThanOrEqual(memberExpression, constantExpression);
                case "le" :
                    return BinaryExpression.LessThanOrEqual(memberExpression, constantExpression);
                default:
                    throw new System.Exception("Unnown equality operator");
            }
            
        }

        private object CreateValue(Type type, string propertyValue) {

            if (type == typeof(System.Guid)) {   
                return Activator.CreateInstance(type, propertyValue);
            }

            if (type == typeof(System.String)) {
                return Activator.CreateInstance(type, propertyValue.ToCharArray());
            }

            if (type == typeof(System.Int32)) {  //int
                var x = (int)Activator.CreateInstance(type, true);
                x = int.Parse(propertyValue);
                return x;
            }

            if (type == typeof(System.Single)) {  //float
                var x = (float)Activator.CreateInstance(type, true);
                x = float.Parse(propertyValue);
                return x;
            }

            if (type == typeof(System.Double)) {  //double
                var x = (double)Activator.CreateInstance(type, true);
                x = double.Parse(propertyValue);
                return x;
            }


            if (type == typeof(System.Decimal)) {  //decimal
                var x = (decimal)Activator.CreateInstance(type, true);
                x = decimal.Parse(propertyValue);
                return x;
            }

            //Add additional types as needed.

            throw new Exception("Unknown Type");
        }
    }

    internal class FilterParameters {

        public FilterParameters(string propertyName, string propertyValue, string equalityOperator) {
            PropertyName = propertyName;
            PropertyValue = propertyValue;
            EqualityOperator = equalityOperator;
        }

        public string PropertyName { get; }
        public string PropertyValue { get; }
        public string EqualityOperator { get; }
    }
}