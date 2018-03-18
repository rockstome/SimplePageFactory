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

        public List<string> GetHeaders()
        {
            List<string> headers = new List<string>();
            foreach (IWebElement th in table.FindElements(By.TagName("th")))
            {
                headers.Add(th.Text);
            }
            return headers;
        }
    }

    public class PersonTable : AbstractTable
    {
        public PersonTable(IWebElement table) : base(table)
        {
        }

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
    }

    public class Person
    {
        private int id;
        private string name;
        private string surname;
        private List<IWebElement> sites;
        private string amount;

        public Person(int id, string name, string surname, List<IWebElement> sites, string amount)
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
            this.sites = sites;
            this.amount = amount;
        }

        public override string ToString()
        {
            var hrefs = sites.Select(s => s.GetAttribute("href")).ToList<string>();
            return $"new Person({id},\"{name}\",\"{surname}\",{hrefs.Count},{amount})";
        }

        public static Func<List<IWebElement>, Person> Mapper =
            (cells) =>
            {
                var links = cells[3].FindElements(By.XPath(".//a"));

                return new Person(
                int.Parse(cells[0].Text),
                cells[1].Text,
                cells[2].Text,
                new List<IWebElement>(links),
                cells[4].FindElement(By.XPath(".//div[1]")).Text + " " + cells[4].FindElement(By.XPath(".//div[2]")).Text);
            };
    }

    public class PersonTest
    {
        [Test]
        public void Test1()
        {
            IWebDriver d = new ChromeDriver();
            d.Navigate().GoToUrl("C:/Users/tomas/Documents/Visual%20Studio%202015/Projects/Tomasz/SimplePageFactory/Sites/table.html");
            var personTable = new PersonTable(d.FindElement(By.Id("table")));

            foreach (var header in personTable.GetHeaders())
                Console.Write(header + ", ");
            Console.WriteLine();

            foreach (var person in personTable.GetRows())
                Console.WriteLine(person);
        }
    }
}




