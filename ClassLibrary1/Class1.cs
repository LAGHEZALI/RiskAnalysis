using System;
using Projet.Instruments;
using Projet.Volatilite;
using Projet.Pricer;
using Projet.MarketData;
using NUnit.Framework;
using Moq;  


namespace ClassLibrary1
{   
    [TestFixture]
    public class Class1
    {
        [Test]
        public void PriceOption()
        {
            // le mock de la volatilé,
            Mock<Volatility> volMock = new Mock<Volatility>();
            volMock.Setup(x => x.Vol()).Returns(0.25); // utilsation des linq lambda expression.
            var maturity = DateTime.Parse("01/5/2019");
            var paramDate = DateTime.Now;
            var option = new OptionInstrument(100, maturity);
            var optionData = new OptionMarketData(120, paramDate, 0.05, volMock.Object.Vol());
            var pricer = new OptionPricer(option, optionData);
            var prix = 0.0; 
            Assert.AreEqual(prix, 0);
        }

    }
}
