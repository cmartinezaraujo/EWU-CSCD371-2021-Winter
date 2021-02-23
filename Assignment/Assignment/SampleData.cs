using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Assignment
{
    public class SampleData : ISampleData
    {
        // 1.
        public IEnumerable<string> CsvRows => File.ReadLines("People.csv").Skip(1);
     
 
        // 2.
        public IEnumerable<string> GetUniqueSortedListOfStatesGivenCsvRows() 
            => CsvRows.Select(record => record = record.Split(",")[6]).OrderBy(state => state).Distinct().ToList();

        // 3.
        public string GetAggregateSortedListOfStatesUsingCsvRows()
            => string.Join(",", GetUniqueSortedListOfStatesGivenCsvRows().ToArray());

        // 4.
        public IEnumerable<IPerson> People => CsvRows.OrderBy(record => record = record.Split(",")[6]).
            ThenBy(record => record = record.Split(",")[5]).ThenBy(record => record = record.Split(",")[7]).
            Select(record => { string[] info = record.Split(","); return new Person(info[1], info[2], new Address(info[4], info[5], info[6], info[7]), info[3]); });

        // 5.
        public IEnumerable<(string FirstName, string LastName)> FilterByEmailAddress(
            Predicate<string> filter) => People.Where( person => filter.Invoke(person.EmailAddress)).Select(person => (person.FirstName,person.LastName));

        // 6.
        public string GetAggregateListOfStatesGivenPeopleCollection(
            IEnumerable<IPerson> people) => people.Select(person => person.Address.State).Distinct()
            .Aggregate((result, next) => result + "," + next);
    }
}
