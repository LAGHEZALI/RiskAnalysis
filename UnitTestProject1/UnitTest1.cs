//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Projet.Instruments;
//using Moq;
//using System;

//namespace UnitTestProject1
//{
//    [TestClass]
//    public class UnitTest1
//    {
//        private int spotMin = 50;
//        private int spotMax = 150;

//        [TestMethod]
//        public void TestPrice()
//        {
//            var spotMax = this.spotMax;
//            var spotMin = this.spotMin;
//            var mockVol = new Mock<Ivolatility>();
//            mockVol.Setup(x => x.Vol()).Returns(0.25);
//            var maturity = DateTime.Parse("01/5/2019");
//            var paramDate = DateTime.Parse("01/04/2019");
//            var option = new OptionInstrument(100, maturity);
//            //var optionData = new OptionMarketData(120, paramDate, 0.05, mockVol.Object.Vol());
//            //var pricer = new OptionPricer(option, optionData);
//            //var price = pricer.Price();
//            //a.Assert.AreEqual(20.415048825193423, price);
//        }
//    }
//}
