using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        //Пустое название
        [TestMethod]
        public void SpectaclName()
        {
            string Name = "";
            string ActualResultName = "";

            Assert.AreEqual(Name, ActualResultName);
        }

        //Количество 0
        [TestMethod]
        public void NullCount()
        {
            int Price = 1000;
            int Count = 0;
            int Result = Count * Price;

            int ActualResultLastPrice = 0;

            Assert.AreEqual(Result, ActualResultLastPrice);
        }

        //Рассчет скидки
        [TestMethod]
        public void DiscountCalculate()
        {
            int Price = 1000;
            int Count = 30;
            int Total = 0;

            if(Count >= 30)
            {
                Total = ((Price * Count) / 100) * 25;
            }

            int ActualResult = 7500;
            Assert.AreEqual(Total, ActualResult);
        }
    }
}
