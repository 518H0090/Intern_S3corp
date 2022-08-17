using CheckUnitTest;

namespace TestProject1
{
    public class Tests
    {
        private BasicMaths _basicMaths;
        
        [SetUp]
        public void Setup()
        {
            _basicMaths = new BasicMaths();
        }

        [Test]
        public void Test_AddMethod()
        {
            double res = _basicMaths.Add(10, 10);
            Assert.AreEqual(res, 20);
        }

        [Test]
        public void Test_SubstractMethod()
        {
            double res = _basicMaths.Substract(10, 10);
            Assert.AreEqual(res, 0);
        }

        [Test]
        public void Test_DivideMethod()
        {
            double res = _basicMaths.divide(10, 5);
            Assert.AreEqual(res, 2);
        }

        [Test]
        public void Test_MultiplyMethod()
        {
            double res = _basicMaths.Multiply(10, 10);
            Assert.AreEqual(res, 100);
        }
    }
}