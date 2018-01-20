using NUnit.Framework;

namespace SimplePageFactory.Pages
{
    partial class FlightPage
    {
        public FlightPage AssertIsAt()
        {
            Assert.That(this.driver.Url, Does.StartWith(Url));
            return this;
        }
    }
}
