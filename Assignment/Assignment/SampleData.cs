using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Assignment
{

    public class SampleData : ISampleData
    {
        //Column locations in the reccord
        private const int FirstName = 1;
        private const int LastName = 2;
        private const int Email = 3;
        private const int StreetAddress = 4;
        private const int City = 5;
        private const int State = 6;
        private const int ZipCode = 7;


        // 1.
        public IEnumerable<string> CsvRows => File.ReadLines("People.csv").Skip(1);
     
 
        // 2.
        public IEnumerable<string> GetUniqueSortedListOfStatesGivenCsvRows() 
            => CsvRows.Select(record => record = record.Split(",")[State]).OrderBy(state => state).Distinct().ToList();

        // 3.
        public string GetAggregateSortedListOfStatesUsingCsvRows()
            => string.Join(",", GetUniqueSortedListOfStatesGivenCsvRows().ToArray());

        // 4.
        public IEnumerable<IPerson> People => CsvRows.OrderBy(record => record = record.Split(",")[State]).
            ThenBy(record => record = record.Split(",")[City]).ThenBy(record => record = record.Split(",")[ZipCode]).
            Select(record => { string[] info = record.Split(","); return new Person(info[FirstName], info[LastName], new Address(info[StreetAddress], info[City], info[State], info[ZipCode]), info[Email]); });

        // 5.
        public IEnumerable<(string FirstName, string LastName)> FilterByEmailAddress(
            Predicate<string> filter) => People.Where( person => filter.Invoke(person.EmailAddress)).Select(person => (person.FirstName,person.LastName));

        // 6.
        public string GetAggregateListOfStatesGivenPeopleCollection(
            IEnumerable<IPerson> people) => people.Select(person => person.Address.State).Distinct()
            .Aggregate((result, next) => result + "," + next);
    }
}
