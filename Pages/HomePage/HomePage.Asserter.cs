using NUnit.Framework;

namespace SimplePageFactory.Pages
{
    partial class HomePage
    {
        public HomePage AssertIsAt()
        {
            Assert.That(this.driver.Url, Is.EqualTo(Url));
            return this;
        }
    }
}
