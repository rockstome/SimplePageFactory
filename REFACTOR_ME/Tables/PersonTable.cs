using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SimplePageFactory.Tables
{
    public abstract class AbstractTable
    {
        protected IWebElement table;

        /// <summary>
        /// Method to get list of table headers.
        /// </summary>
        /// <returns>List of table headers.</returns>
        public List<string> GetHeaders()
        {
            return table
                .FindElements(By.TagName("th"))
                .Select(e => e.Text)
                .ToList();
        }

        /// <summary>
        /// Method to get number of rows.
        /// </summary>
        /// <returns>Number of rows.</returns>
        public virtual int GetRowsCount()
        {
            return table
                .FindElements(By.CssSelector("tbody tr"))
                .Count;
        }
    }

    public class PersonTable : AbstractTable
    {
        public PersonTable(IWebElement table)
        {
            this.table = table;
        }

        /// <summary>
        /// Method to get all table rows.
        /// </summary>
        /// <returns>List of Person class objects.</returns>
        public List<Person> GetRows()
        {
            var rows = table
                .FindElements(By.TagName("tbody tr"))
                .Select(tr => Person.PersonMapper(tr.FindElements(By.TagName("td")).ToList()))
                .ToList();
            return rows;
        }

        /// <summary>
        /// Method to get a table row element.
        /// </summary>
        /// <param name="index">The row index. Star from 1.</param>
        /// <returns>Person who meets the criteria.</returns>
        public Person GetRow(int index)
        {
            if (index <= 0 || index > GetRowsCount())
                throw new ArgumentOutOfRangeException();

            var row = table
                .FindElement(By.CssSelector("tbody > tr"));

            return Person.PersonMapper(row.FindElements(By.XPath(".//td")).ToList());
        }

        /// <summary>
        /// Method to get a table row that contains specific query.
        /// </summary>
        /// <param name="query">The query text to search for.</param>
        /// <returns>Person who meets the criteria.</returns>
        public Person GetRowByCellText(string query)
        {
            var row = table
                .FindElement(By.TagName("tbody"))
                .FindElement(By.XPath($".//td[contains(.,'{query}')]//parent::tr"));

            return Person.PersonMapper(new List<IWebElement>(row.FindElements(By.XPath(".//td"))));
        }

        /// <summary>
        /// Method to get the person who has specific balance.
        /// </summary>
        /// <param name="balance">
        /// Balance in format that was mapped, example: "100 PLN".
        /// </param>
        /// <returns>Person who meets the criteria.</returns>
        public Person GetRowByBalance(string balance)
        {
            // TODO: on large tables this sucks
            var persons = GetRows();
            foreach (var person in persons)
                if (person.Balance.Contains(balance))
                    return person;
            throw new NotFoundException();
        }
    }

    public class Person
    {
        public int Id { get; }
        public string Name { get; }
        public string Surname { get; }
        public List<string> Links { get; }
        public string Balance { get; }


        public Person(
            int id,
            string name,
            string surname,
            List<string> links,
            string balance)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Links = links;
            Balance = balance;
        }

        public override string ToString()
        {
            return $"new Person({Id},\"{Name}\",\"{Surname}\",{Links.Count},{Balance}).ToString()";
        }

        public static Func<List<IWebElement>, Person> PersonMapper =
            (cells) =>
            {
                int id = int.Parse(cells[0].Text);
                var links = cells[3].FindElements(By.XPath(".//a"));
                var divs = cells[4].FindElements(By.XPath(".//div"));
                string balance = string.Join(" ", divs.Select(e => e.Text));

                return new Person(
                id,
                cells[1].Text,
                cells[2].Text,
                links.Select(e => e.Text).ToList(),
                balance);
            };
    }

    public class PersonTableTest
    {
        [Fact]
        public void BasicActions()
        {
            IWebDriver d = new ChromeDriver();
            d.Navigate().GoToUrl("C:/Users/tomas/Documents/Visual%20Studio%202015/" +
                "Projects/Tomasz/SimplePageFactory/Sites/table.html");
            var table = new PersonTable(d.FindElement(By.Id("table")));

            // Number of rows.
            Console.WriteLine(table.GetRowsCount());

            // Name of person in 1st row.
            Console.WriteLine(table.GetRow(1).Name);

            // Balance of person in 2nd row.
            Console.WriteLine(table.GetRow(2).Balance);

            // Balance of Adam.
            Console.WriteLine(table.GetRowByCellText("Adam").Balance);

            // Text of second link of person who have 99 PLN.
            Console.WriteLine(table.GetRowByBalance("99 PLN").Links[1]);
        }
    }
}
