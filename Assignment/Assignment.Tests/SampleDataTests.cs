using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Tests
{
    [TestClass]
    public class SampleDataTests
    {
        [TestMethod]
        public void CsvRowsLoadsExoectedRecords()
        {
            SampleData data = new SampleData();

            IEnumerable<string> records = data.CsvRows;

            int recordsExpected = File.ReadLines("People.csv").Count() - 1;

            Assert.AreEqual<int>(recordsExpected, records.Count());
        }


        [TestMethod]   

        public void GetUniqueSortedListOfStatesGivenCsvRows_IsSorted_LinqTest()
        {
            SampleData data = new SampleData();

            IEnumerable<string> states = data.GetUniqueSortedListOfStatesGivenCsvRows();

            bool isSorted = states.Zip(states.Skip(1), (currentState, otherState) => new { currentState, otherState })
                .All(item => string.Compare(item.currentState, item.otherState) < 0);

            Assert.IsTrue(isSorted);
        }

        [TestMethod]

        public void GetUniqueSortedListOfStatesGivenCsvRows_IsUnique_LinqTest()
        {
            SampleData data = new SampleData();

            IEnumerable<string> states = data.GetUniqueSortedListOfStatesGivenCsvRows();

            List<String> temp = new List<string>();

            //List is constructed to add each state we encounter but if we have added it before then the results are not unique

            foreach(string state in states)
            {
                if(temp.Contains(state))
                {
                    Assert.Fail();
                }

                temp.Add(state);
            }

        }

        [TestMethod]
        public void GetAggregateSortedListOfStatesUsingCsvRows_ReturnsUniqueSortedStatesAsString()
        {
            SampleData data = new SampleData();

            string expected = string.Join(",", data.GetUniqueSortedListOfStatesGivenCsvRows());

            string result = data.GetAggregateSortedListOfStatesUsingCsvRows();

            Assert.AreEqual<string>(expected, result);
      
        }

        [TestMethod]

        public void PeopleProperty_ReturnsCorrectPeople()
        {
            SampleData data = new SampleData();

            IEnumerable<string> records = data.CsvRows.OrderBy(record => record = record.Split(",")[6]).
            ThenBy(record => record = record.Split(",")[5]).ThenBy(record => record = record.Split(",")[7]);

            IEnumerable<IPerson> people = data.People;

            //Two strings constructed with all information to check that all records from people are returned in the correct order

            string expected = "";
            foreach(string record in records)
            {
                string[] information = record.Split(",");

                expected += $"{information[1]},{information[2]},{information[4]},{information[5]},{information[6]},{information[7]},{information[3]};";
            }

            string result = "";
            foreach (IPerson person in people)
            {
                result += $"{person.FirstName},{person.LastName},{person.Address.StreetAddress},{person.Address.City},{person.Address.State}," +
                    $"{person.Address.Zip},{person.EmailAddress};";
            }

            Assert.AreEqual(expected, result);
        }

        [TestMethod]

        public void EmailFilter_ReturnsEmailsContaining_edu()
        {
            Predicate<string> filter = email => email.Contains("edu");

            SampleData data = new SampleData();

            string actualPeople = data.FilterByEmailAddress(filter).
                Aggregate("", (result, next) => result + next.FirstName + "," + next.LastName + ";");

            string expectedPeople = data.People.Where(person => person.EmailAddress.Contains("edu")).
                Aggregate("", (result, next) => result + next.FirstName + "," + next.LastName + ";");

            Assert.AreEqual<string>(expectedPeople, actualPeople);

        }

        [TestMethod]

        public void GetAggregateListOfStatesGivenPeopleCollection_ReturnsStatesFromPeople()
        {
            SampleData data = new SampleData();

            string expectedStates = data.GetUniqueSortedListOfStatesGivenCsvRows().Aggregate((result, next) => result + "," + next);

            string actualStates = data.GetAggregateListOfStatesGivenPeopleCollection(data.People).
                Split(",").OrderBy(state => state).Aggregate((result, next) => result + "," + next);

            Assert.AreEqual<string>(expectedStates, actualStates);
        }

    }
}
