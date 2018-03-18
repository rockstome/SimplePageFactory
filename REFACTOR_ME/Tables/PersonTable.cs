using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SimplePageFactory.REFACTOR_ME.Tables
{
    public abstract class AbstractTable
    {
        protected IWebElement table;

        public AbstractTable(IWebElement table)
        {
            this.table = table;
        }

        /// <summary>
        /// Method to get list of table headers.
        /// </summary>
        /// <returns>List of table headers.</returns>
        public List<string> GetHeaders()
        {
            List<string> headers = new List<string>();
            foreach (IWebElement th in table.FindElements(By.TagName("th")))
            {
                headers.Add(th.Text);
            }
            return headers;
        }

        /// <summary>
        /// Method to get number of rows.
        /// </summary>
        /// <returns>Number of rows.</returns>
        public int GetTotalRows()
        {
            return table.FindElement(By.TagName("tbody")).FindElements(By.TagName("tr")).Count;
        }
    }

    public class PersonTable : AbstractTable
    {
        public PersonTable(IWebElement table) : base(table)
        {
        }

        /// <summary>
        /// Method to get all table rows.
        /// </summary>
        /// <returns>List of Person class objects.</returns>
        public List<Person> GetRows()
        {
            List<Person> rows = new List<Person>();

            foreach (IWebElement tr in table.FindElement(By.TagName("tbody"))
                .FindElements(By.TagName("tr")))
            {
                List<IWebElement> cells = new List<IWebElement>();
                var row = tr.FindElements(By.TagName("td"));
                foreach (IWebElement cell in tr.FindElements(By.TagName("td")))
                {
                    cells.Add(cell);
                }
                rows.Add(Person.Mapper(cells));
            }

            return rows;
        }

        /// <summary>
        /// Method to get a table row element.
        /// </summary>
        /// <param name="index">The row index. Star from 1.</param>
        /// <returns>Person who meets the criteria.</returns>
        public Person GetRow(int index)
        {
            if (0 < index && index <= GetTotalRows())
            {
                var row = table
                    .FindElement(By.TagName("tbody"))
                    .FindElement(By.XPath($".//tr[{index}]"));
                return Person.Mapper(new List<IWebElement>(row.FindElements(By.XPath(".//td"))));
            }
            else
                throw new ArgumentOutOfRangeException();
        }

        /// <summary>
        /// Method to get a table row that contains specific query.
        /// </summary>
        /// <param name="query">The query text to search for.</param>
        /// <returns>Person who meets the criteria.</returns>
        public Person GetRow(string query)
        {
            var row = table
                .FindElement(By.TagName("tbody"))
                .FindElement(By.XPath($".//td[contains(.,'{query}')]//parent::tr"));
            return Person.Mapper(new List<IWebElement>(row.FindElements(By.XPath(".//td"))));
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
                if (person.GetBalance().Contains(balance))
                    return person;
            throw new NotFoundException();
        }
    }

    public class Person
    {
        private int id;
        private string name;
        private string surname;
        private List<IWebElement> links;
        private string balance;

        public int GetId() => id;
        public string GetName() => name;
        public string GetSurname() => surname;
        /// <param name="index">Start from 1.</param>
        public IWebElement GetLink(int index) => links[index - 1];
        public string GetBalance() => balance;

        public Person(int id, string name, string surname, List<IWebElement> links, string balance)
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
            this.links = links;
            this.balance = balance;
        }

        public override string ToString()
        {
            var hrefs = links.Select(s => s.GetAttribute("href")).ToList();
            return $"new Person({id},\"{name}\",\"{surname}\",{hrefs.Count},{balance})";
        }

        public static Func<List<IWebElement>, Person> Mapper =
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
                new List<IWebElement>(links),
                balance);
            };
    }

    public class PersonTest
    {
        [Test]
        public void BasicActions()
        {
            IWebDriver d = new ChromeDriver();
            d.Navigate().GoToUrl("C:/Users/tomas/Documents/Visual%20Studio%202015/" +
                "Projects/Tomasz/SimplePageFactory/Sites/table.html");
            var table = new PersonTable(d.FindElement(By.Id("table")));

            // Number of rows.
            Console.WriteLine(table.GetTotalRows());

            // Name of person in 1st row.
            Console.WriteLine(table.GetRow(1).GetName());

            // Balance of person in 2nd row.
            Console.WriteLine(table.GetRow(2).GetBalance());

            // Balance of Adam.
            Console.WriteLine(table.GetRow("Adam").GetBalance());

            // Click second link of person who have 99 PLN.
            table.GetRowByBalance("99 PLN").GetLink(2).Click();
        }        
    }
}