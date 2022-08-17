using TestXUnitInDotNet;

namespace XUnitTestInDotnet
{
    public class UnitTest1
    {
        [Fact]
        public void EvenNumberTest()
        {
            //Arrange
            var num = 6;
            //Act
            bool result = Mathematics.IsEvenNumber(num);
            //Assert
            Assert.True(result);
        }

        [Fact]
        public void OddNumberTest()
        {
            //Arrange
            var num = 5;
            //Act
            bool result = Mathematics.IsOddNumber(num);
            //Assert
            Assert.True(result);
        }
        [Fact]
        public void SquareOfNumberTest()
        {
            //Arrange
            var num = 5;
            var expectedResult = 25;
            //Act
            var result = Mathematics.SquareOfNumber(num);
            //Assert
            Assert.Equal(expectedResult, result);
        }
        [Fact]
        public void AdditionOfNumber()
        {
            //Arrange
            var num1 = 5;
            var num2 = 2;
            var expectedResult = 7;
            //Act
            var result = Mathematics.Addition(num1, num2);
            //Assert
            Assert.Equal(expectedResult, result);
        }
        [Fact]
        public void SubtractionOfNumber()
        {
            //Arrange
            var num1 = 5;
            var num2 = 2;
            var expectedResult = 3;
            //Act
            var result = Mathematics.Subtraction(num1, num2);
            //Assert
            Assert.Equal(expectedResult, result);
        }
        [Fact]
        public void MultiplicationOfNumber()
        {
            //Arrange
            var num1 = 5;
            var num2 = 2;
            var expectedResult = 10;
            //Act
            var result = Mathematics.Multiplication(num1, num2);
            //Assert
            Assert.Equal(expectedResult, result);
        }
        [Fact]
        public void DivisionOfNumber()
        {
            //Arrange
            var num1 = 10;
            var num2 = 2;
            var expectedResult = 5;
            //Act
            var result = Mathematics.Division(num1, num2);
            //Assert
            Assert.Equal(expectedResult, result);
        }
    }
}